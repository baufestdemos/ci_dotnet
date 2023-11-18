using Api;
using Serilog;

Host.CreateDefaultBuilder(args)
    .ConfigureWebHostDefaults(whost =>
        {
            whost.UseContentRoot(Directory.GetCurrentDirectory());
            whost.ConfigureAppConfiguration((context, builder) =>
            {
                if (context.HostingEnvironment.IsDevelopment())
                {
                    builder.AddUserSecrets(typeof(Program).Assembly);
                }
                else
                {
                    builder.AddEnvironmentVariables(prefix: "APIDEMO_");
                }
            });
        });

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((context, configuration) =>
    configuration.ReadFrom.Configuration(context.Configuration));

builder.DatabaseMigrations()
.ConfigServices()
.ConfigApp();
