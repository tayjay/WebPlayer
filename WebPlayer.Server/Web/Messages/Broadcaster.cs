using System.Diagnostics;
using GameCore;
using LabApi.Features.Console;
using MEC;
using Utf8Json;
using WebPlayer.Server.Data;
using WebPlayer.Server.Data.Events;

namespace WebPlayer.Server.Web.Messages
{
    // Handles broadcasting messages to all connected WebSocket clients.
    public class Broadcaster
    {
        
        private static void BroadcastMessage<T>(T data) where T : class
        {
            Timing.CallDelayed(0, () =>
            {
                Logger.Debug($"Broadcasting message: {typeof(T).Name}");
                Stopwatch sw = new Stopwatch();
                sw.Start();
                string response = JsonSerializer.ToJsonString(data);
                WsServer.Server.BroadcastMessage(response);
                sw.Stop();
                Logger.Debug($"Broadcasted message: {typeof(T).Name} in {sw.ElapsedMilliseconds}ms");
            });
        }
        
        public static void BroadcastEvent<T>(T data) where T : EventData
        {
            BroadcastMessage(data);
            /*Timing.CallDelayed(0, () =>
            {
                Logger.Debug($"Broadcasting event: {data.EventName}");
                Stopwatch sw = new Stopwatch();
                sw.Start();
                string response = JsonSerializer.ToJsonString(data);
                WsServer.Server.BroadcastMessage(response);
                sw.Stop();
                Logger.Debug($"Broadcasted event: {data.EventName} in {sw.ElapsedMilliseconds}ms");
            });*/
        }
        
        // Sends a full update of the player's current state. This is useful for when a player joins the server or when their state changes significantly.
        public static void BroadcastPlayerState(PlayerData data)
        {
            BroadcastMessage(data);
            /*Timing.CallDelayed(0, () =>
            {
                Logger.Debug($"Broadcasting player: {data.UserId}");
                Stopwatch sw = new Stopwatch();
                sw.Start();
                string response = JsonSerializer.ToJsonString(data);
                WsServer.Server.BroadcastMessage(response);
                sw.Stop();
                Logger.Debug($"Broadcasted player: {data.UserId} in {sw.ElapsedMilliseconds}ms");
            });*/
        }
        
        // This is a bulky message, should be sent once per round
        public static void BroadcastFacilityData(FacilityData data)
        {
            BroadcastMessage(data);
            /*Timing.CallDelayed(0, () =>
            {
                Logger.Debug($"Broadcasting facility data");
                Stopwatch sw = new Stopwatch();
                sw.Start();
                string response = JsonSerializer.ToJsonString(data);
                WsServer.Server.BroadcastMessage(response);
                sw.Stop();
                Logger.Debug($"Broadcasted facility data in {sw.ElapsedMilliseconds}ms");
            });*/
        }

        public static void BroadcastPartialState(PartialStateUpdate data)
        {
            BroadcastMessage(data);
        }
    }
}