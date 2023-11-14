using System.Diagnostics;
using System.Net.NetworkInformation;
using System.Runtime.InteropServices;
using Microsoft.AspNetCore.SpaServices;

namespace SpaBff.Utils;

public static class NextJsDevelopServerExtension
{

    private static TimeSpan Timeout { get; } = TimeSpan.FromSeconds(180);
    private static int Port { get; set; }
    private static string DoneMessage { get; } = "compiled client and server successfully";

    public static void UseNextJsDevelopServer(this ISpaBuilder spa,
        bool dynamicPort = true, int port = 3000)
    {
        if (Port == default)
        {
            if (dynamicPort)
            {
                Port = TcpPortFinder.FindAvailablePort();
            }
            else
            {
                Port = port;
            }
        }

        spa.UseProxyToSpaDevelopmentServer(async () =>
        {
            var loggerFactory = spa.ApplicationBuilder.ApplicationServices.GetRequiredService<ILoggerFactory>();
            var logger = loggerFactory.CreateLogger("NextJs");

            if (IsRunning(Port))
            {
                return new Uri($"http://localhost:{Port}");
            }

            var isWindows = RuntimeInformation.IsOSPlatform(OSPlatform.Windows);
            var processInfo = new ProcessStartInfo
            {
                FileName = isWindows ? "cmd" : "npm",
                Arguments = $"{(isWindows ? "/c npm " : "")} run dev -- -p {Port}",
                WorkingDirectory = spa.Options.SourcePath,
                RedirectStandardError = true,
                RedirectStandardInput = true,
                RedirectStandardOutput = true,
                UseShellExecute = false,
            };

            var process = Process.Start(processInfo);

            var tcs = new TaskCompletionSource<int>();
            _ = Task.Run(() =>
            {
                try
                {
                    string line;
                    while ((line = process!.StandardOutput.ReadLine()!) != null)
                    {
                        logger.LogInformation(line);
                        if (!tcs.Task.IsCompleted && line.Contains(DoneMessage))
                        {
                            tcs.SetResult(1);
                        }
                    }
                }
                catch (EndOfStreamException ex)
                {
                    logger.LogError(ex.ToString());
                    tcs.SetException(new InvalidOperationException("'npm run dev' failed.", ex));
                }
            });

            _ = Task.Run(() =>
            {
                try
                {
                    string line;
                    while ((line = process!.StandardError.ReadLine()!) != null)
                    {
                        logger.LogError(line);
                    }
                }
                catch (EndOfStreamException ex)
                {
                    logger.LogError(ex.ToString());
                    tcs.SetException(new InvalidOperationException("'npm run dev' failed.", ex));
                }
            });

            var timeout = Task.Delay(Timeout);
            if (await Task.WhenAny(timeout, tcs.Task) == timeout)
            {
                throw new TimeoutException();
            }

            return new Uri($"http://localhost:{Port}"); ;
        });

    }

    private static bool IsRunning(int port) => IPGlobalProperties.GetIPGlobalProperties()
            .GetActiveTcpListeners()
            .Select(x => x.Port)
            .Contains(port);
}
