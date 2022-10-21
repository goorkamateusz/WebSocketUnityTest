using System.Net.WebSockets;

namespace Services.WebSocketService;

public interface IWebSocketService
{
    Task ProcessWebSocket(WebSocket socket);
}

public class WebSocketService : IWebSocketService
{
    // todo maximum buffer size?
    public async Task ProcessWebSocket(WebSocket socket)
    {
        var handler = new WebSocketHandler(socket);
        byte[] buffer = new byte[1024 * 4];

        while (true)
        {
            WebSocketReceiveResult result = await socket.ReceiveAsync(
                new ArraySegment<byte>(buffer),
                CancellationToken.None);

            if (socket.CloseStatus.HasValue)
            {
                handler.OnClosed();
                break;
            }

            handler.OnMessage(buffer, result.Count);
        }

        handler.Dispose();
    }
}