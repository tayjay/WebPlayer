using System.Collections.Generic;
using WebPlayer.Server.Web.WebSocketServer;

namespace WebPlayer.Server.Web.Messages
{
    public class PartialStateUpdate
    {
        
        public const string Type = "PartialStateUpdate";
        public TargetTypeId TargetType { get; set; }
        public string TargetId { get; set; }
        public List<PartialUpdate> Updates { get; set; }
        
        protected PartialStateUpdate(TargetTypeId targetType, string targetId, List<PartialUpdate> updates)
        {
            TargetType = targetType;
            TargetId = targetId;
            Updates = updates;
        }
        
        public struct PartialUpdate
        {
            public string Key;
            public object Value;

            public PartialUpdate(string key, object value)
            {
                Key = key;
                Value = value;
            }
        }
        
        public enum TargetTypeId
        {
            Player,
            Pickup,
            Door,
            Room,
            GameState,
            Custom // For any custom types you might have
        }
        
        public static PartialStateUpdate Create(TargetTypeId targetType, string targetId, List<PartialUpdate> updates)
        {
            return new PartialStateUpdate(targetType, targetId, updates);
        }
    }
}