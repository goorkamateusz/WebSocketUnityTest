using UnityEngine;
using WebSocketSharp;

public class WebsocketTest : MonoBehaviour
{
    private WebSocket ws;

    private void Start()
    {
        ws = new WebSocket("ws://localhost:5000/ws");
        ws.Connect();
        ws.OnMessage += (sender, e) =>
        {
            Debug.Log("Message Received from " + ((WebSocket)sender).Url + ", Data : " + e.Data);
        };
    }

    private void Update()
    {
        if (ws == null)
        {
            return;
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ws.Send("Hello");
        }
    }
}