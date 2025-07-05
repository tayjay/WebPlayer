using System;

namespace WebPlayer.Server.Data.Events
{
    public abstract class EventData
    {
        public abstract string EventName { get; }
        public DateTime TimeStamp => DateTime.Now;

    }
}