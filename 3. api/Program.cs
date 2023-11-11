using Api;

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
builder
.DatabaseMigrations()
.ConfigServices()
.ConfigApp();
