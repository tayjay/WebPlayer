using System.Collections.Generic;
using LabApi.Features.Wrappers;
using MEC;
using UnityEngine;
using WebPlayer.Server.Web.Messages;

namespace WebPlayer.Server.Handlers
{
    public class GameStateChangeTracker
    {
        
        public GameStateChangeTracker()
        {
            // Initialize any necessary state or resources here
            
        }
        
        CoroutineHandle _TrackChangeCoroutine;

        public void OnWaitingForPlayers()
        {
            _TrackChangeCoroutine = Timing.RunCoroutine(TrackChangeCoroutine());
        }

        public void OnRoundRestarted()
        {
            Timing.KillCoroutines(_TrackChangeCoroutine);
        }
        
        
        public IEnumerator<float> TrackChangeCoroutine()
        {
            while (true)
            {
                yield return Timing.WaitForOneFrame;
                // Here you can implement the logic to track game state changes
                Broadcaster.BroadcastPartialState(PartialStateUpdate.Create(PartialStateUpdate.TargetTypeId.Player, "", new List<PartialStateUpdate.PartialUpdate>()
                {
                    new PartialStateUpdate.PartialUpdate("Position", new Vector3(0, 0, 0)),
                    new  PartialStateUpdate.PartialUpdate("Rotation", new Vector3(0, 0, 0)),
                }));
            }
        }
    }
}