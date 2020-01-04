using CitizenFX.Core;
using CitizenFX.Core.Native;
using disc_inventoryhud_common.Inv;
using disc_inventoryhud_common.Inventory;
using disc_inventoryhud_server.ESX;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace disc_inventoryhud_server.Inventory.Weapon
{
    class WeaponHandler : BaseScript
    {
        public static WeaponHandler Instance { get; private set; }

        public WeaponHandler()
        {
            Instance = this;
            EventHandlers[Events.UpdateAmmo] += new Action<Player, int, string, int>(UpdateAmmo);
        }

        public void HandleWeapon(Player player, InventorySlot item, int Slot, string Inventory)
        {
            long ammo = 0;
            if (item.MetaData.ContainsKey("Ammo"))
            {
                ammo = item.MetaData["Ammo"];
            }
            player.TriggerEvent(Events.UseWeapon, API.GetHashKey(item.Id), ammo, Slot, Inventory);
        }

        public void UpdateAmmo([FromSource] Player player, int slot, string inventory, int ammo)
        {
            var xPlayer = ESXHandler.Instance.GetPlayerFromId(player.Handle);
            var kp = new KeyValuePair<string, string>(inventory, xPlayer.identifier);
            var invData = Inventory.Instance.LoadedInventories[kp];
            var invSlot = invData.Inventory[slot];
            invSlot.MetaData["Ammo"] = ammo;
            Inventory.UpdateSlot(slot, invData, invSlot);
        }
    }
}
