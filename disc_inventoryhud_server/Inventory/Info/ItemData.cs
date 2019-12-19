using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace disc_inventoryhud_server.Inventory.Info
{
    public class ItemData
    {
        public string Item { get; set; }
        public string Description { get; set; }
        public string Label { get; set; }
        public bool Meta { get; set; }
        public string ItemUrl { get; set; }
    }
}
