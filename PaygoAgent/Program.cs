using PaygoAgent.Api;
using PaygoAgent.Config;
using PaygoAgent.Models;
using PaygoAgent.Services;

// Architecture overview:
// - ConfigReader loads startup settings from config.ini into AppConfig.
// - LogService writes operational events to daily log files.
// - PaygoService contains device-facing business operations (currently placeholders).
// - PaygoEndpoints exposes all ERP-facing REST endpoints via Minimal API.

var builder = WebApplication.CreateBuilder(args);

// Force Kestrel to listen on the required endpoint for cloud ERP calls.
builder.WebHost.UseUrls("http://0.0.0.0:5050");

var configPath = Path.Combine(builder.Environment.ContentRootPath, "config.ini");
var appConfig = ConfigReader.Load(configPath);

builder.Services.AddSingleton(appConfig);
builder.Services.AddSingleton<LogService>();
builder.Services.AddSingleton<PaygoService>();

// CORS is intentionally permissive to prevent browser-originated ERP requests from failing.
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod());
});

var app = builder.Build();

app.UseCors("AllowAll");

app.UseExceptionHandler(errorApp =>
{
    errorApp.Run(async context =>
    {
        var logger = context.RequestServices.GetRequiredService<LogService>();
        var feature = context.Features.Get<Microsoft.AspNetCore.Diagnostics.IExceptionHandlerFeature>();

        if (feature?.Error is not null)
        {
            await logger.ErrorAsync($"Unhandled exception: {feature.Error}");
        }

        context.Response.StatusCode = StatusCodes.Status500InternalServerError;
        await context.Response.WriteAsJsonAsync(new { success = false, message = "Internal server error." });
    });
});

app.MapPaygoEndpoints();

var startupLogger = app.Services.GetRequiredService<LogService>();
await startupLogger.InfoAsync("PaygoAgent started.");

app.Run();
