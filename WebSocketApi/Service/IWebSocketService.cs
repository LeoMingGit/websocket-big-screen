namespace WebSocketApi.Service
{
    public interface IWebSocketService
    {

        void Start(string ip,int port);

        void PushData(string data);
    }
}
