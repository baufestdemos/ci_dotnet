using Serilog;
using SpaBff;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((context, configuration) =>
    configuration.ReadFrom.Configuration(context.Configuration));

builder
.ConfigServices()
.ConfigApp();