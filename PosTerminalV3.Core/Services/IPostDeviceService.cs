namespace PosTerminalV3.Core.Services
{
    public interface IPostDeviceService
    {
        string TestConnection();
        string Sale(decimal amount);
        string XReport();
        string ZReport();
    }
}
