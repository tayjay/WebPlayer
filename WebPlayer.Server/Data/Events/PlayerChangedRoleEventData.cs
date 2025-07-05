using System;
using LabApi.Events.Arguments.PlayerEvents;

namespace WebPlayer.Server.Data.Events
{
    public class PlayerChangedRoleEventData : EventData
    {
        public override string EventName { get; } = "PlayerChangedRole";
        public PlayerData PlayerData { get; set; }
        public string OldRole { get; set; }
        public string NewRole { get; set; }
        public PlayerChangedRoleEventData(PlayerChangedRoleEventArgs ev)
        {
            PlayerData = new PlayerData(ev.Player);
            OldRole = ev.OldRole.ToString();
            NewRole = ev.NewRole.ToString();
        }
    }
}