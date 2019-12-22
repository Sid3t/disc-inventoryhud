using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace disc_inventoryhud_common.Inventory
{
    public class Events
    {
        private const string prefix = "disc-inventoryhud:";
        public const string UpdateInventory = prefix + "updateInventory";
        public const string UpdateDrops = prefix + "updateDrops";
        public const string MoveItem = prefix + "moveItem";
        public const string DropItem = prefix + "dropItem";
        public const string AddItem = prefix + "addItem";
        public const string RegisterItemUse = prefix + "registerItemUse";
        public const string UseItem = prefix + "useItem";
        public const string RemoveItem = prefix + "removeItem";
        public const string UpdateInfo = prefix + "updateInfo";
        public const string OpenTrunk = prefix + "openTrunk";
        public const string OpenGlovebox = prefix + "openGlovebox";
        public const string OpenStash = prefix + "openStash";
        public const string AddStash = prefix + "addStash";
        public const string RemoveStash = prefix + "removeStash";
    }
}
