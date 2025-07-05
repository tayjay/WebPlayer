namespace WebPlayer.Server.Data.Events
{
    public class RoundStartedEventData : EventData
    {
        public override string EventName { get; } = "RoundStarted";
    }
}