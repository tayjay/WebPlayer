using System.Collections.Generic;
using System.Net;
using WebPlayer.Server.Web.Responses;
using WebPlayer.Server.Web.WebSocketServer;

namespace WebPlayer.Server.Web.Messages
{
    // A player has attempted to connect to the game through the web player.
    public class NewPlayerRequest : WPMessage
    {
        public string DiscordId { get; set; }
        public string PlayerName { get; set; }

        public override void ProcessMessage(Client client, Dictionary<string, object> message)
        {
            DiscordId = GetStringValue(message, "discordId");
            PlayerName = GetStringValue(message, "playerName");

            // Process the connection request here, e.g., authenticate the player, etc.
            
            WebPlayerPlugin.Instance.FakePlayerHandler.AddPlayer(client, PlayerName, DiscordId);
            WsServer.Server.SendMessage(client, new ErrorResponse(HttpStatusCode.OK, "Player connected successfully.").Message);
            
        }
    }
}