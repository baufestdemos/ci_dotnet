using Core.Cross.Cqrs;
using Core.Cross.Transactions;
using Core.Todo.Domain.Contract;
using Core.Todo.Infra;
using Core.Todo.Infra.Repos;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.EntityFrameworkCore;

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

        services.AddDbContextPool<EFDemoDbContext>(options =>
        {
            options.UseSqlServer(config.GetConnectionString("DemoDbConnection"));
        });

        services.AddDbContextPool<EFDemoDbReadContext>(options =>
        {
            options.UseSqlServer(config.GetConnectionString("DemoDbConnection"));
        });

        services.AddScoped<ITransactor, EFImplicitTransactor<EFDemoDbContext>>();

        services.Scan(selector =>
        {
            selector.FromApplicationDependencies()
                    .AddClasses(filter =>
                    {
                        filter.AssignableTo(typeof(IQueryHandler<,>));
                    })
                    .AsImplementedInterfaces()
                    .WithLifetime(ServiceLifetime.Transient);
        });

        services.Scan(selector =>
        {
            selector.FromApplicationDependencies()
                    .AddClasses(filter =>
                    {
                        filter.AssignableTo(typeof(ICommandHandler<,>));
                    })
                    .AsImplementedInterfaces()
                    .WithLifetime(ServiceLifetime.Transient);
        });

        services.AddTransient<IReadTaskRepo, EFReadTaskRepo>();
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

        app.UseTodoTaskEndpoints();

        app.UseSwagger();
        app.UseSwaggerUI();

        var option = new RewriteOptions();
        option.AddRedirect("^$", "swagger");
        app.UseRewriter(option);

        app.Run();
        return builder;
    }
}