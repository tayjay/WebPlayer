using System.Collections.Generic;
using LabApi.Features.Console;
using Utf8Json;
using WebPlayer.Server.Web.WebSocketServer;

namespace WebPlayer.Server.Web.Messages
{
    // Handles all incoming messages through the Websocket
    public class MessageHandler
    {
        // Dedicated message handlers per type
        public Dictionary<string, WPMessage> Messages { get; set; }
        
        public MessageHandler()
        {
            Messages = new Dictionary<string, WPMessage>();
            // Register all message types
            RegisterMessage<NewPlayerRequest>();
            RegisterMessage<ChangeHeldItemRequest>();
        }

        private void RegisterMessage<T>() where T : WPMessage, new()
        {
            Messages.Add(typeof(T).Name, new T());
        }

        public void HandleMessage(object client, OnMessageReceivedHandler message)
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
            // All messages should be coming in on the / path
            // todo: List of Message templates for types of requests that could come in
            // Like: WebPlayerConnected, MoveLook, KeyPress, OpenInventory, UseItem, Shoot, Talk

            if (messageObject.ContainsKey("type"))
            {
                if (Messages.ContainsKey(messageObject["type"].ToString()))
                {
                    Messages[messageObject["type"].ToString()].ProcessMessage(message.GetClient(), messageObject);
                }
            }
            

            /*if (message.GetClient().GetPath() == "/events")
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
            }*/
        }
    }
}