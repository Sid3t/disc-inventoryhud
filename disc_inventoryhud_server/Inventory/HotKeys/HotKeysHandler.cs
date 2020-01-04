using CitizenFX.Core;
using disc_inventoryhud_common.Inventory;
using disc_inventoryhud_server.ESX;
using disc_inventoryhud_server.Inventory.Weapon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace disc_inventoryhud_server.Inventory.HotKeys
{
    class HotKeysHandler : BaseScript
    {
        public HotKeysHandler()
        {
            EventHandlers[Events.HotKeyUse] += new Action<Player, int>(HotKeyUse);
        }

        public void HotKeyUse([FromSource] Player player, int slot)
        {
            var xPlayer = ESXHandler.Instance.GetPlayerFromId(player.Handle);
            KeyValuePair<string, string> kp = new KeyValuePair<string, string>("hotbar", xPlayer.identifier);
            if (Inventory.Instance.LoadedInventories[kp].Inventory.ContainsKey(slot))
            {
                var item = Inventory.Instance.LoadedInventories[kp].Inventory[slot];
                if (item.Id.StartsWith("WEAPON_"))
                {
                    WeaponHandler.Instance.HandleWeapon(player, item, slot, "hotbar");
                }
                else
                {
                    dynamic obj = new
                    {
                        item.Id,
                        item.Count,
                        Inventory = "hotbar",
                        Slot = slot
                    };
                    if (ItemHandler.Instance.ItemUsages.ContainsKey(item.Id))
                    {
                        foreach (var action in ItemHandler.Instance.ItemUsages[item.Id])
                        {
                            action.Invoke(player.Handle, obj);
                        }
                    }
                }
            }
        }
    }
}
