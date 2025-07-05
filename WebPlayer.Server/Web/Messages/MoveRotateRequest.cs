using System.Collections.Generic;
using UnityEngine;
using WebPlayer.Server.Web.WebSocketServer;

namespace WebPlayer.Server.Web.Messages
{
    public class MoveRotateRequest : WPMessage
    {
        public override void ProcessMessage(Client client, Dictionary<string, object> message)
        {
            string playerId = GetStringValue(message, "playerId");
            Vector3 moveVector = GetVector3Value(message, "moveVector");
            Vector3 lookVector = GetVector3Value(message, "lookVector");
            
            WebPlayerPlugin.Instance.FakePlayerHandler.MoveAndLook(playerId, moveVector, lookVector);
        }
    }
}