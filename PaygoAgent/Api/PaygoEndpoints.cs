using PaygoAgent.Models;
using PaygoAgent.Services;

namespace PaygoAgent.Api;

/// <summary>
/// Minimal API surface exposed to ERP clients.
/// Keeps HTTP concerns isolated from device service logic.
/// </summary>
public static class PaygoEndpoints
{
    public static IEndpointRouteBuilder MapPaygoEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api").WithTags("PayGo");

        group.MapPost("/connect", async (PaygoService paygoService, CancellationToken ct) =>
        {
            var result = await paygoService.ConnectAsync(ct);
            return Results.Ok(result);
        });

        group.MapPost("/sale", async (SaleRequest request, PaygoService paygoService, CancellationToken ct) =>
        {
            var result = await paygoService.SaleAsync(request, ct);
            return Results.Ok(result);
        });

        group.MapPost("/xreport", async (PaygoService paygoService, CancellationToken ct) =>
        {
            var result = await paygoService.XReportAsync(ct);
            return Results.Ok(result);
        });

        group.MapPost("/zreport", async (PaygoService paygoService, CancellationToken ct) =>
        {
            var result = await paygoService.ZReportAsync(ct);
            return Results.Ok(result);
        });

        group.MapPost("/cashier/login", async (CashierLoginRequest request, PaygoService paygoService, CancellationToken ct) =>
        {
            var result = await paygoService.CashierLoginAsync(request, ct);
            return Results.Ok(result);
        });

        group.MapPost("/cashier/logout", async (PaygoService paygoService, CancellationToken ct) =>
        {
            var result = await paygoService.CashierLogoutAsync(ct);
            return Results.Ok(result);
        });

        group.MapGet("/status", (PaygoService paygoService) => Results.Ok(paygoService.GetStatus()));

        return app;
    }
}
