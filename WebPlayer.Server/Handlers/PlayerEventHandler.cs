using LabApi.Events.Arguments.PlayerEvents;
using WebPlayer.Server.Data;
using WebPlayer.Server.Data.Events;
using WebPlayer.Server.Web;
using WebPlayer.Server.Web.Messages;
using WebPlayer.Server.Web.Responses;

namespace WebPlayer.Server.Handlers
{
    public class PlayerEventHandler
    {
        public void OnJoined(PlayerJoinedEventArgs ev)
        {
            // Handle player joined event
            // Notify web of new player in game
            Broadcaster.BroadcastEvent(new PlayerJoinedEventData(ev));
        }
        
        public void OnLeft(PlayerLeftEventArgs ev)
        {
            // Handle player left event
            // Notify web of player leaving the game
            Broadcaster.BroadcastEvent(new PlayerLeftEventData(ev));
        }
        
        public void OnDied(PlayerDeathEventArgs ev)
        {
            // Handle player died event
            // Notify web of player death
            Broadcaster.BroadcastEvent(new PlayerDeathEventData(ev));
        }
        
        public void OnPlayerChangedRole(PlayerChangedRoleEventArgs ev)
        {
            // Handle player changed role event
            // Notify web of player role change
            Broadcaster.BroadcastEvent(new PlayerChangedRoleEventData(ev));
        }
        
        public void OnPlayerChangedItem(PlayerChangedItemEventArgs ev)
        {
            // Handle player changed item event
            // Notify web of player item change
            Broadcaster.BroadcastEvent(new PlayerChangedItemEventData(ev));
        }
    }
}