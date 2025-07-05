using Interactables.Interobjects.DoorUtils;
using LabApi.Events.Arguments.PlayerEvents;
using LabApi.Events.Arguments.ServerEvents;
using WebPlayer.Server.Data.Events;
using WebPlayer.Server.Web.Messages;

namespace WebPlayer.Server.Handlers
{
    public class ServerEventHandler
    {
        public void OnRoundStarted()
        {
            Broadcaster.BroadcastEvent(new RoundStartedEventData());
        }
        
        public void OnWaitingForPlayers()
        {
            //Broadcaster.BroadcastEvent(new WaitingForPlayersEventData());
        }
        
        public void OnRoundEnded(RoundEndedEventArgs args)
        {
            //Broadcaster.BroadcastEvent(new RoundEndedEventData(args));
        }
        
        
        
        // Warhead Events
    }
}