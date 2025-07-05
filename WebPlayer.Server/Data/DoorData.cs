using LabApi.Features.Wrappers;
using UnityEngine;

namespace WebPlayer.Server.Data
{
    public class DoorData
    {
        public Vector3 Position { get; set; }
        public float RotationY { get; set; }
        public bool IsOpen { get; set; }
        public bool IsLocked { get; set; }
        public float ExactState { get; set; }
        
        
        public DoorData(Door door)
        {
            Position = door.Position;
            RotationY = door.Rotation.eulerAngles.y;
            IsOpen = door.IsOpened;
            ExactState = door.ExactState;
            IsLocked = door.IsLocked;
        }
    }
}