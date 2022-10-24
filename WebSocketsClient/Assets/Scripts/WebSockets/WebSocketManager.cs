using System;
using UnityEngine;
using Goo.Tools.Patterns;
using WebSocketSharp;

public class WebSocketManager : SceneSingleton<WebSocketManager>
{
    [SerializeField] string _serverUrl = "ws://localhost:5000/ws";

    private WebSocket socket;

    protected override void OnAwake()
    {
        base.OnAwake();

        socket = new WebSocket(_serverUrl);

        socket.OnClose += OnClose;
        socket.OnError += OnError;
        socket.OnOpen += OnOpen;
        socket.OnMessage += OnMessage;

        socket.ConnectAsync();
    }

    private void OnMessage(object sender, MessageEventArgs e)
    {
        Debug.Log($"[Websocket] Message: {e.Data}");
    }

    private void OnOpen(object sender, EventArgs e)
    {
        Debug.Log("[Websocket] Opened");
    }

    private void OnError(object sender, ErrorEventArgs e)
    {
        Debug.LogError($"[Websocket] Error: {e.Message}");
    }

    private void OnClose(object sender, CloseEventArgs e)
    {
        Debug.Log($"[Websocket] Closed {e.Code}. {e.Reason}\nWas clean: {e.WasClean} ");
    }

    public void Send(string msg)
    {
        socket.Send(msg);
    }
}
