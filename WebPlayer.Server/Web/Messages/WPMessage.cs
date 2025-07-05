using System;
using System.Collections.Generic;
using UnityEngine;
using WebPlayer.Server.Web.WebSocketServer;

namespace WebPlayer.Server.Web.Messages
{
    public abstract class WPMessage
    {
        
        public abstract void ProcessMessage(Client client, Dictionary<string, object> message);
        
        public string GetStringValue(Dictionary<string, object> message, string key)
        {
            if (message.TryGetValue(key, out var value) && value is string strValue)
            {
                return strValue;
            }
            return null;
        }
        
        public int GetIntValue(Dictionary<string, object> message, string key)
        {
            if (message.TryGetValue(key, out var value) && value is int intValue)
            {
                return intValue;
            }
            return 0;
        }
        
        public bool GetBoolValue(Dictionary<string, object> message, string key)
        {
            if (message.TryGetValue(key, out var value) && value is bool boolValue)
            {
                return boolValue;
            }
            return false;
        }
        
        // Get Vector3 value from message
        public Vector3 GetVector3Value(Dictionary<string, object> message, string key)
        {
            if (message.TryGetValue(key, out var value) && value is Dictionary<string, object> vectorDict)
            {
                if (vectorDict.TryGetValue("x", out var x) && vectorDict.TryGetValue("y", out var y) && vectorDict.TryGetValue("z", out var z))
                {
                    return new Vector3(Convert.ToSingle(x), Convert.ToSingle(y), Convert.ToSingle(z));
                }
            }
            return Vector3.zero;
        }
        
        // Get Vector2 value from message
        public Vector2 GetVector2Value(Dictionary<string, object> message, string key)
        {
            if (message.TryGetValue(key, out var value) && value is Dictionary<string, object> vectorDict)
            {
                if (vectorDict.TryGetValue("x", out var x) && vectorDict.TryGetValue("y", out var y))
                {
                    return new Vector2(Convert.ToSingle(x), Convert.ToSingle(y));
                }
            }
            return Vector2.zero;
        }
    }
}