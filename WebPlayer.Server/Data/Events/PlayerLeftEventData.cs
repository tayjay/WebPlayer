using LabApi.Events.Arguments.PlayerEvents;

namespace WebPlayer.Server.Data.Events
{
    public class PlayerLeftEventData : EventData
    {
        public override string EventName { get; } = "PlayerLeft";
        public PlayerData PlayerData { get; set; }
        
        public PlayerLeftEventData(PlayerLeftEventArgs args)
        {
            PlayerData = new PlayerData(args.Player);
        }
    }
}