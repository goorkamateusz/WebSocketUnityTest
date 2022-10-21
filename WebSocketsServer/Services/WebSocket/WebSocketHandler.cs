using System.Net.WebSockets;
using System.Text;

namespace Services.WebSocketService;

public class WebSocketHandler : IDisposable
{
    public event Action<WebSocketHandler> OnClosed;
    public event Action<WebSocketHandler, string> OnMessage;

    private WebSocket socket;

    public WebSocketHandler(WebSocket socket)
    {
        this.socket = socket;
        OnConnected();
    }

    public async Task Send(string message)
    {
        byte[] buffer = Encoding.UTF8.GetBytes(message);

        await socket.SendAsync(
            new ArraySegment<byte>(buffer, 0, buffer.Length),
            WebSocketMessageType.Text,
            true,
            CancellationToken.None);
    }

    private void OnConnected()
    {
    }

    internal void ProcessOnMessage(byte[] buffer, int count)
    {
        string message = Encoding.UTF8.GetString(buffer, 0, count);
        OnMessage?.Invoke(this, message);
    }

    internal void ProcessOnClosed()
    {
        OnClosed?.Invoke(this);
    }

    public void Dispose()
    {
    }
}