using Microsoft.AspNetCore.Mvc;
using Services.WebSocketService;

namespace WebSocketsSample.Controllers;

public class WebSocketController : ControllerBase
{
    private IWebSocketService webSocketService;

    public WebSocketController(IWebSocketService webSocketService)
    {
        this.webSocketService = webSocketService;
    }

    [HttpGet("/ws")]
    public async Task Get()
    {
        if (HttpContext.WebSockets.IsWebSocketRequest)
        {
            using var webSocket = await HttpContext.WebSockets.AcceptWebSocketAsync();
            await this.webSocketService.ProcessWebSocket(webSocket);
        }
        else
        {
            HttpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
        }
    }
}