using Services.WebSocketService;

namespace Services.Logic;

public interface IBroadCastToAllService { }

public class BroadCastToAllService : IBroadCastToAllService
{
    private readonly List<WebSocketHandler> handlers = new();

    public BroadCastToAllService(IWebSocketService webSocket)
    {
        webSocket.OnNewSocketOpened += OnNewSocketOpened;
    }

    private void OnNewSocketOpened(WebSocketHandler handler)
    {
        handler.OnClosed += OnClosed;
        handler.OnMessage += OnMessage;
        handlers.Add(handler);
    }

    #pragma warning disable CS4014
    private void OnMessage(WebSocketHandler source, string message)
    {
        foreach (var handler in handlers)
        {
            if (source != handler)
                handler.Send(message);
        }
    }

    private void OnClosed(WebSocketHandler handler)
    {
        handlers.Remove(handler);
    }
}