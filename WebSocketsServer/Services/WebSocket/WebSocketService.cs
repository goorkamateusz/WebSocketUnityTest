using System.Net.WebSockets;

namespace Services.WebSocketService;

public interface IWebSocketService
{
    public event Action<WebSocketHandler> OnNewSocketOpened;

    Task ProcessWebSocket(WebSocket socket);
}

public class WebSocketService : IWebSocketService
{
    public event Action<WebSocketHandler> OnNewSocketOpened;

    public async Task ProcessWebSocket(WebSocket socket)
    {
        var handler = new WebSocketHandler(socket);
        OnNewSocketOpened?.Invoke(handler);
        byte[] buffer = new byte[1024 * 4];

        while (true)
        {
            WebSocketReceiveResult result = await socket.ReceiveAsync(
                new ArraySegment<byte>(buffer),
                CancellationToken.None);

            if (socket.CloseStatus.HasValue)
            {
                handler.ProcessOnClosed();
                break;
            }

            handler.ProcessOnMessage(buffer, result.Count);
        }

        handler.Dispose();
    }
}