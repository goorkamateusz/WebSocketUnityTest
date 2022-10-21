using System.Net.WebSockets;
using System.Text;

namespace Services.WebSocketService;

public struct MessageToClient
{
    public string Message;

    public MessageToClient(string message)
    {
        this.Message = message;
    }
}

public class WebSocketHandler : IDisposable
{
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
        Send("Are you new?");
    }

    internal void OnMessage(byte[] buffer, int count)
    {
        string message = Encoding.UTF8.GetString(buffer, 0, count);
        Send($"Hi! Did you say '{message}'?");
    }

    internal void OnClosed()
    {
    }

    public void Dispose()
    {
    }
}