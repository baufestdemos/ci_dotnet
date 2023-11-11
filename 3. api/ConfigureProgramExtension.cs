using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Rewrite;

namespace Api;

public static class ConfigureProgramExtension
{
    public static WebApplicationBuilder ConfigServices(this WebApplicationBuilder builder)
    {
        IConfiguration config = builder.Configuration;
        var services = builder.Services;
        services.AddDataProtection().DisableAutomaticKeyGeneration();
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
        return builder;
    }

    public static WebApplicationBuilder ConfigApp(this WebApplicationBuilder builder)
    {
        var app = builder.Build();
        if (app.Environment.IsProduction())
        {
            app.UseHsts();
            app.UseHttpsRedirection();
        }

        app.UseSwagger();
        app.UseSwaggerUI();
        var option = new RewriteOptions();
        option.AddRedirect("^$", "swagger");
        app.UseRewriter(option);
        app.Run();
        return builder;
    }
}