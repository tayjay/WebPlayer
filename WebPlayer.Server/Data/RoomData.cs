using System.Collections.Generic;
using LabApi.Features.Wrappers;
using UnityEngine;

namespace WebPlayer.Server.Data
{
    public class RoomData
    {
        public string Name { get; set; }
        public Vector3 Position { get; set; }
        public float Rotation { get; set; }
        public string Shape { get; set; }
        public RoomData(Room room)
        {
            Name = room.Name.ToString();
            Position = room.Position;
            Rotation = room.Rotation.eulerAngles.y;
            Shape = room.Shape.ToString();
        }
    }
}