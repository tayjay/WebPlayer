using System.Diagnostics;
using GameCore;
using LabApi.Features.Console;
using MEC;
using Utf8Json;
using WebPlayer.Server.Data;
using WebPlayer.Server.Data.Events;

namespace WebPlayer.Server.Web.Messages
{
    public class EventBroadcaster
    {
        public static void BroadcastEvent<T>(T data) where T : EventData
        {
            Timing.CallDelayed(0, () =>
            {
                Logger.Debug($"Broadcasting event: {data.EventName}");
                Stopwatch sw = new Stopwatch();
                sw.Start();
                string response = JsonSerializer.ToJsonString(data);
                WsServer.Server.BroadcastMessage(response);
                sw.Stop();
                Logger.Debug($"Broadcasted event: {data.EventName} in {sw.ElapsedMilliseconds}ms");
            });
        }
        
        public static void BroadcastPlayerState(PlayerData data)
        {
            Timing.CallDelayed(0, () =>
            {
                Logger.Debug($"Broadcasting player: {data.UserId}");
                Stopwatch sw = new Stopwatch();
                sw.Start();
                string response = JsonSerializer.ToJsonString(data);
                WsServer.Server.BroadcastMessage(response);
                sw.Stop();
                Logger.Debug($"Broadcasted player: {data.UserId} in {sw.ElapsedMilliseconds}ms");
            });
        }
    }
}