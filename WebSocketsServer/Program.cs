using Services.Logic;
using Services.WebSocketService;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddSingleton<IWebSocketService, WebSocketService>();
builder.Services.AddSingleton<IBroadCastToAllService, BroadCastToAllService>();

var app = builder.Build();

app.Services.GetService<IBroadCastToAllService>();

var webSocketOptions = new WebSocketOptions
{
    KeepAliveInterval = TimeSpan.FromMinutes(2)
};

app.UseWebSockets(webSocketOptions);

app.UseDefaultFiles();
app.UseStaticFiles();

app.MapControllers();

app.Run();
