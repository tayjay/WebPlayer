using System.Collections.Generic;
using System.Net;
using System.Threading;
using LabApi.Features.Console;
using Utf8Json;
using WebPlayer.Server.Web.Messages;

namespace WebPlayer.Server.Web
{
    public class WsServer
{
    public static WebSocketServer.Server Server { get; private set; }
    
    private static MessageHandler _messageHandler;

    public static void Start()
    {
        var wsThread = new Thread(StartWebSocketServer);
        wsThread.Start();
        
        _messageHandler = new MessageHandler();
        
        Logger.Info("Websocket server running...");
    }

    private static void StartWebSocketServer()
    {
        //IPAddress.Parse(TTAdmin.Instance.Config.Host)
        Server = new WebSocketServer.Server(new IPEndPoint(IPAddress.Any, WebPlayerPlugin.Instance.Config.Port));
        Server.OnClientConnected += (sender, client) =>
        {
            Logger.Info($"Client connected: {client.GetClient().GetGuid()}");
            if(client.GetClient().GetPath()!="/events" && client.GetClient().GetPath()!="/console")
            {
                Server.SendMessage(client.GetClient(), JsonSerializer.ToJsonString(new Dictionary<string, object>
                {
                    {"message", "Invalid path. Valid paths are /events and /console"}
                }));
                return; 
            }
                
            Server.SendMessage(client.GetClient(), JsonSerializer.ToJsonString(new Dictionary<string, object>
            {
                {"message", "Welcome to the TTAdmin websocket server"}
            }));
            if (client.GetClient().GetPath() == "/console")
            {
                //TTAdmin.Instance.SubscriptionHandler.SubscribeLog(client.GetClient());
            }
        };
        
        Server.OnClientDisconnected += (sender, client) =>
        {
            Logger.Info($"Client disconnected: {client.GetClient().GetGuid()}");
            //if(client.GetClient().GetPath()=="/console")
                //TTAdmin.Instance.SubscriptionHandler.UnsubscribeLog(client.GetClient());
            //if(client.GetClient().GetPath()=="/events")
                //TTAdmin.Instance.SubscriptionHandler.ClearSubscriptions(client.GetClient());
        };

        Server.OnMessageReceived += _messageHandler.HandleMessage;
        
        /*Server.OnMessageReceived += (sender, message) =>
        {
            Logger.Info($"Message received: {message.GetMessage()}");
            //Parse out the Json message
            if(message.GetMessage()=="")
                return;
            var messageObject = JsonSerializer.Deserialize<Dictionary<string, object>>(message.GetMessage());
            foreach(var item in messageObject)
            {
                Logger.Info($"{item.Key} : {item.Value}");
            }
            
            // Handle message

            if (message.GetClient().GetPath() == "/events")
            {
                if (messageObject.TryGetValue("subscribe", out var subscribe))
                {
                    Logger.Info(subscribe.ToString());
                    //if(subscribe is List<object> list)
                        //TTAdmin.Instance.SubscriptionHandler.Subscribe(message.GetClient(),list);
                }
                if(messageObject.TryGetValue("unsubscribe", out var unsubscribe))
                {
                    Logger.Info(unsubscribe.ToString());
                    //if(unsubscribe is List<object> list)
                        //TTAdmin.Instance.SubscriptionHandler.Unsubscribe(message.GetClient(),list);
                }
            }
        };*/
        
        Server.OnSendMessage += (sender, message) =>
        {
            //Logger.Info($"Message sent: {message.GetMessage()}");
        };
        
    }
    
    public static void Stop()
    {
        Server.Stop();
    }
}
}