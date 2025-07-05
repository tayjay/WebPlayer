using System.Collections.Generic;
using System.Linq;
using CommandSystem.Commands.RemoteAdmin.Dummies;
using LabApi.Features.Wrappers;
using Mirror;
using NetworkManagerUtils.Dummies;
using UnityEngine;
using WebPlayer.Server.Web.WebSocketServer;
using Logger = LabApi.Features.Console.Logger;

namespace WebPlayer.Server.Handlers
{
    public class FakePlayerHandler
    {
        public List<Player> WebPlayers { get; private set; }
        
        public FakePlayerHandler()
        {
            WebPlayers = new List<Player>();
        }

        public void RemovePlayer(string playerId)
        {
            
            Player player = Player.Get(playerId);
            WebPlayers.Remove(player);
            player.Kick("Disconnected by WebPlayer server.");
        }

        public void AddPlayer(Client client, string name, string id)
        {
            WebPlayers.Add(Player.Get(CreatePlayerHub(client, name, id)));
        }
        
        public ReferenceHub CreatePlayerHub(Client client, string name, string id)
        {
            GameObject player = Object.Instantiate<GameObject>(NetworkManager.singleton.playerPrefab);
            ReferenceHub component;
            if (!player.TryGetComponent<ReferenceHub>(out component))
                return (ReferenceHub) null;
            component.nicknameSync.MyNick = name;
            component.authManager.UserId = id;
            NetworkServer.AddPlayerForConnection((NetworkConnectionToClient) new WebPlayerNetworkConnection(client), player);
            return component;
        }



        public void ChangeHeldItem(string playerId, ushort itemSerial)
        {
            Player player = Player.Get(playerId);
            if (player == null)
            {
                Logger.Error($"Player with Discord ID {playerId} not found.");
                return;
            }
            if (player.Inventory == null)
            {
                Logger.Error($"Player {playerId} does not have an inventory.");
                return;
            }
            if(player.CurrentItem.Serial == itemSerial)
            {
                Logger.Info($"Player {playerId} already holding item with Serial {itemSerial}.");
                return;
            }
            
            Item item = player.Items.FirstOrDefault(i=> i.Serial == itemSerial);
            if (item == null)
            {
                Logger.Error($"Item with Serial {itemSerial} not found in player {playerId}'s inventory.");
                return;
            }
            player.CurrentItem = item;
            
        }

        public void MoveAndLook(string playerId, Vector3 moveVector, Vector2 lookVector)
        {
            Player player = Player.Get(playerId);
            if (player == null)
            {
                Logger.Error($"Player with Discord ID {playerId} not found.");
                return;
            }

            if (moveVector != Vector3.zero)
            {
                player.Move(moveVector);
            }
            if (lookVector != Vector2.zero)
            {
                player.Rotate(lookVector);
            }
        }
    }
}