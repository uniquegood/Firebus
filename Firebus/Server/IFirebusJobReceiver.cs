namespace Firebus.Server
{
    public interface IFirebusJobReceiver
    {
        void BeginReceive();
        void RegisterJobHandler(FirebusJobHandler handler);
    }
}
