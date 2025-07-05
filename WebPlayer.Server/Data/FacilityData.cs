using System.Collections.Generic;
using LabApi.Features.Wrappers;

namespace WebPlayer.Server.Data
{
    public class FacilityData
    {
        public List<PlayerData> Players { get; set; }
        public List<RoomData> Rooms { get; set; }
        public List<DoorData> Doors { get; set; }
        public List<PickupData> Pickups { get; set; }

        public FacilityData()
        {
            Players = new List<PlayerData>();
            Rooms = new List<RoomData>();
            Doors = new List<DoorData>();
            Pickups = new List<PickupData>();
            LoadData();
            
        }
        
        private void LoadData()
        {
            // Load players, rooms, doors, and pickups from the game world
            foreach (Player player in Player.List)
            {
                Players.Add(new PlayerData(player));
            }
            foreach (var room in Room.List)
            {
                Rooms.Add(new RoomData(room));
            }

            foreach (var door in Door.List)
            {
                Doors.Add(new DoorData(door));
            }

            foreach (var pickup in Pickup.List)
            {
                Pickups.Add(new PickupData(pickup));
            }
        }
    }
}