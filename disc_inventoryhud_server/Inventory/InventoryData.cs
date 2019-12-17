using CitizenFX.Core;
using disc_inventoryhud_common.Inv;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace disc_inventoryhud_common.Inventory
{
    public class InventoryData
    {
        public string Owner { get; set; }
        public string Type { get; set; }

        public Vector3 Coords { get; set; }

        public bool Active { get; set; }

        public Dictionary<int, InventorySlot> Inventory { get; set; } = new Dictionary<int, InventorySlot>();

    }
}
