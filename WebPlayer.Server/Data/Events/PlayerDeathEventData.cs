using LabApi.Events.Arguments.PlayerEvents;

namespace WebPlayer.Server.Data.Events
{
    public class PlayerDeathEventData : EventData
    {
        public override string EventName { get; } = "PlayerDeathEvent";
        public PlayerData PlayerData { get; set; }
        public PlayerData AttackerData { get; set; }

        public PlayerDeathEventData(PlayerDeathEventArgs ev)
        {
            PlayerData = new PlayerData(ev.Player);
            AttackerData = ev.Attacker != null ? new PlayerData(ev.Attacker) : null;
        }
    }
}