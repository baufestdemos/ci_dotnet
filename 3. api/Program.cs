using Api;
using Serilog;

Host.CreateDefaultBuilder(args)
    .ConfigureWebHostDefaults(whost =>
        {
            whost.UseContentRoot(Directory.GetCurrentDirectory());
            whost.ConfigureAppConfiguration((context, builder) =>
            {
                builder.AddUserSecrets(typeof(Program).Assembly);
            });
        });

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((context, configuration) =>
    configuration.ReadFrom.Configuration(context.Configuration));

builder.DatabaseMigrations()
.ConfigServices()
.ConfigApp();
