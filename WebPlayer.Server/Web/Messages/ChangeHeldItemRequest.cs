using System.Collections.Generic;
using WebPlayer.Server.Web.WebSocketServer;

namespace WebPlayer.Server.Web.Messages
{
    public class ChangeHeldItemRequest : WPMessage
    {
        public override void ProcessMessage(Client client, Dictionary<string, object> message)
        {
            string discordId = GetStringValue(message, "discordId");
            int serial = GetIntValue(message, "serial");
            
            WebPlayerPlugin.Instance.FakePlayerHandler.ChangeHeldItem(discordId, (ushort)serial);
        }
    }
}