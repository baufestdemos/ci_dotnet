
using Microsoft.Data.SqlClient;
using EvolveDb;
using Serilog;

namespace Api;
public static class EvolveMigrationExtension
{
    public static WebApplicationBuilder DatabaseMigrations(this WebApplicationBuilder builder)
    {
        try
        {
            if (builder.Environment.IsDevelopment())
            {
                IConfiguration config = builder.Configuration;
                string location = config["DatabaseLocation"]!;
                using var demoDbconnection = new SqlConnection(config.GetConnectionString("DemoDbConnection"));
                var evolve = new Evolve(demoDbconnection, Log.Information)
                {
                    Locations = new[] { location },
                    IsEraseDisabled = true,
                    Placeholders = new Dictionary<string, string>
                    {
                        ["${schema1}"] = "demo"
                    }
                };
                evolve.Migrate();
            }
        }
        catch (Exception ex)
        {
            Log.Error("Database migration failed.", ex);
            throw;
        }
        return builder;
    }
}