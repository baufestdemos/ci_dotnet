using SpaBff.Utils;

namespace SpaBff;

public static class ConfigureProgramExtension
{
    public static WebApplicationBuilder ConfigServices(this WebApplicationBuilder builder)
    {
        IConfiguration config = builder.Configuration;
        builder.Services.AddReverseProxy().LoadFromConfig(config.GetSection("YarpProxy"));
        if (builder.Environment.IsDevelopment())
        {
            builder.Services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp/out";
            });
        }

        return builder;
    }

    public static void ConfigApp(this WebApplicationBuilder builder)
    {
        var app = builder.Build();
        if (app.Environment.IsProduction())
        {
            app.UseHsts();
            app.UseHttpsRedirection();
        }

        app.UseStaticFiles();
        if (app.Environment.IsDevelopment())
        {
            app.UseSpaStaticFiles();
        }

        app.UseRouting();
        app.UseMiddleware<StrictSameSiteExternalAuthenticationMiddleware>();
        app.MapReverseProxy();


#pragma warning disable ASP0014
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapReverseProxy();
        });
#pragma warning restore ASP0014

        if (app.Environment.IsDevelopment())
        {
            app.UseSpa(spa =>
            {
                spa.Options.SourcePath = "ClientApp";
                spa.UseNextJsDevelopServer(dynamicPort: false);
            });
        }

        if (!app.Environment.IsDevelopment())
        {
            app.MapFallbackToFile("index.html");
        }

        app.Run();
    }
}