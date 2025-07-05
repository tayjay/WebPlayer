using System.Collections.Generic;
using WebPlayer.Server.Web.WebSocketServer;

namespace WebPlayer.Server.Web.Messages
{
    // A player has attempted to connect to the game through the web player.
    public class WebPlayerConnectRequest : WPMessage
    {
        public string DiscordId { get; set; }
        public string PlayerName { get; set; }

        public override void ProcessMessage(Client client, Dictionary<string, object> message)
        {
            if (message.TryGetValue("discordId", out var discordIdObj) && discordIdObj is string discordId)
            {
                DiscordId = discordId;
            }
            else
            {
                DiscordId = null;
            }

            if (message.TryGetValue("playerName", out var playerNameObj) && playerNameObj is string playerName)
            {
                PlayerName = playerName;
            }
            else
            {
                PlayerName = null;
            }

            // Process the connection request here, e.g., authenticate the player, etc.
        }
    }
}