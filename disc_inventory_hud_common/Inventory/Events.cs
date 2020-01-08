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
        public const string OpenInventory = prefix + "openInventory";
        public const string OpenTrunk = prefix + "openTrunk";
        public const string OpenGlovebox = prefix + "openGlovebox";
        public const string OpenStash = prefix + "openStash";
        public const string OpenDrop = prefix + "openDrop";
        public const string OpenSearch = prefix + "openSearch";
        public const string AddStash = prefix + "addStash";
        public const string RemoveStash = prefix + "removeStash";
        public const string HotKeyUse = prefix + "hotkeyUse";
        public const string CloseInventory = prefix + "closeInventory";
        public const string UseWeapon = prefix + "useWeapon";
        public const string UpdateAmmo = prefix + "updateAmmo";
        public const string AddAmmo = prefix + "addAmmo";
        public const string HasItem = prefix + "hasItem";
    }
}
