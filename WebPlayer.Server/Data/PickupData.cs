using LabApi.Features.Wrappers;
using UnityEngine;

namespace WebPlayer.Server.Data
{
    public class PickupData
    {
        public string Type { get; set; }
        public Vector3 Position { get; set; }
        
        
        public PickupData(Pickup pickup)
        {
            Type = pickup.Type.ToString();
            Position = pickup.Position;
        }
    }
}