using LabApi.Events.Arguments.PlayerEvents;

namespace WebPlayer.Server.Data.Events
{
    public class PlayerJoinedEventData : EventData
    {
        public override string EventName { get; } = "PlayerJoined";
        public PlayerData PlayerData { get; set; }

        public PlayerJoinedEventData(PlayerJoinedEventArgs args)
        {
            PlayerData = new PlayerData(args.Player);
        }
    }
}