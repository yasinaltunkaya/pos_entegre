using PosTerminal.Models;
using PosTerminal.Services;

namespace PosTerminal.Api;

/// <summary>
/// ERP-facing HTTP endpoints.
/// Operations are delegated to the selected device implementation via FiscalDeviceManager.
/// </summary>
public static class PosEndpoints
{
    public static IEndpointRouteBuilder MapPosEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api").WithTags("POS Terminal");

        group.MapPost("/connect", async (FiscalDeviceManager manager, CancellationToken ct) =>
        {
            var result = await manager.Current.ConnectAsync(ct);
            return Results.Ok(result);
        }).WithName("ConnectDevice");

        group.MapPost("/sale", async (SaleRequest request, FiscalDeviceManager manager, CancellationToken ct) =>
        {
            var result = await manager.Current.SaleAsync(request, ct);
            return Results.Ok(result);
        }).WithName("DoSale");

        group.MapPost("/xreport", async (FiscalDeviceManager manager, CancellationToken ct) =>
        {
            var result = await manager.Current.XReportAsync(ct);
            return Results.Ok(result);
        }).WithName("XReport");

        group.MapPost("/zreport", async (FiscalDeviceManager manager, CancellationToken ct) =>
        {
            var result = await manager.Current.ZReportAsync(ct);
            return Results.Ok(result);
        }).WithName("ZReport");

        group.MapPost("/cashier/login", async (CashierLoginRequest request, FiscalDeviceManager manager, CancellationToken ct) =>
        {
            var result = await manager.Current.CashierLoginAsync(request, ct);
            return Results.Ok(result);
        }).WithName("CashierLogin");

        group.MapPost("/cashier/logout", async (FiscalDeviceManager manager, CancellationToken ct) =>
        {
            var result = await manager.Current.CashierLogoutAsync(ct);
            return Results.Ok(result);
        }).WithName("CashierLogout");

        group.MapGet("/status", (FiscalDeviceManager manager) => Results.Ok(manager.Current.GetStatus()))
            .WithName("GetStatus");

        return app;
    }
}
