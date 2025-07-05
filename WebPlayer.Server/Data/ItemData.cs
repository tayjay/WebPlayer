using LabApi.Features.Wrappers;

namespace WebPlayer.Server.Data
{
    public class ItemData
    {
        public string Type { get; set; }
        public ushort Serial { get; set; }

        public ItemData(Item item)
        {
            Type = item.Type.ToString();
            Serial = item.Serial;
        }
    }
}