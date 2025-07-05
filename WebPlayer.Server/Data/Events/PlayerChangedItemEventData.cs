using System;
using LabApi.Events.Arguments.PlayerEvents;

namespace WebPlayer.Server.Data.Events
{
    public class PlayerChangedItemEventData : EventData
    {
        public override string EventName { get; } = "PlayerChangedItem";
        public int PlayerId { get; set; }
        public string NewItem { get; set; }
        public string OldItem { get; set; }

        public PlayerChangedItemEventData(PlayerChangedItemEventArgs ev)
        {
            PlayerId = ev.Player.PlayerId;
            NewItem = ev.NewItem?.Type.ToString();
            OldItem = ev.OldItem?.Type.ToString();
        }
    }
}