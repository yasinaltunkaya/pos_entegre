using PosTerminal.Api;
using PosTerminal.Config;
using PosTerminal.Services;

// Architecture:
// 1) ConfigReader loads config.ini into AppConfig.
// 2) FiscalDeviceManager selects PayGo or Beko service using DEVICENAME.
// 3) Minimal API endpoints call manager.Current for device operations.
// 4) Swagger is enabled for API documentation/testing.

var builder = WebApplication.CreateBuilder(args);

builder.WebHost.UseUrls("http://0.0.0.0:5050");

var configPath = Path.Combine(builder.Environment.ContentRootPath, "config.ini");
var appConfig = ConfigReader.Load(configPath);

builder.Services.AddSingleton(appConfig);
builder.Services.AddSingleton<LogService>();
builder.Services.AddSingleton<PaygoService>();
builder.Services.AddSingleton<BekoService>();
builder.Services.AddSingleton<FiscalDeviceManager>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
        policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseCors("AllowAll");
app.UseSwagger();
app.UseSwaggerUI();

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

app.MapPosEndpoints();

var startupLogger = app.Services.GetRequiredService<LogService>();
await startupLogger.InfoAsync($"PosTerminal started with device '{appConfig.DeviceName}'.");

app.Run();
