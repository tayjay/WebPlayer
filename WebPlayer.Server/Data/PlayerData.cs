using System.Collections.Generic;
using LabApi.Features.Wrappers;

namespace WebPlayer.Server.Data
{
    public class PlayerData
    {
        public string UserId { get; set; }
        public string DisplayName { get; set; }
        public int PlayerId { get; set; }
        public string Role { get; set; }
        public float Health { get; set; }
        public float MaxHealth { get; set; }
        public string CurrentItem { get; set; }
        
        public List<ItemData> Inventory { get; set; } = new List<ItemData>();
        
        

        public PlayerData(Player player)
        {
            this.UserId = player.UserId;
            this.DisplayName = player.DisplayName;
            this.Role = player.Role.ToString();
            this.PlayerId = player.PlayerId;
            this.Health = player.Health;
            this.MaxHealth = player.MaxHealth;
            this.CurrentItem = player.CurrentItem?.Type.ToString() ?? "None";
            this.Inventory = new List<ItemData>();
            foreach (var item in player.Items)
            {
                if (item != null)
                {
                    this.Inventory.Add(new ItemData(item));
                }
            }
        }
    }
}