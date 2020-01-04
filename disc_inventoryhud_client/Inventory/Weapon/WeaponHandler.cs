using CitizenFX.Core;
using CitizenFX.Core.Native;
using disc_inventoryhud_common.Inventory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace disc_inventoryhud_client.Inventory.Weapon
{
    class WeaponHandler : BaseScript
    {
        public WeaponHandler()
        {
            EventHandlers[Events.UseWeapon] += new Action<int, int, int, string>(UseWeapon);
        }

        public void UseWeapon(int weaponHash, int ammoCount, int slot, string inventory)
        {
            var playerPedId = API.PlayerPedId();
            if (API.IsPedArmed(playerPedId, 7))
            {
                if (weaponHash == API.GetSelectedPedWeapon(playerPedId))
                {
                    var ammo = API.GetAmmoInPedWeapon(playerPedId, (uint)weaponHash);
                    TriggerServerEvent(Events.UpdateAmmo, slot, inventory, ammo);
                    API.RemoveWeaponFromPed(playerPedId, (uint)weaponHash);
                    API.SetPedAmmo(playerPedId, (uint)weaponHash, 0);
                }
            }
            else
            {
                API.GiveWeaponToPed(playerPedId, (uint)weaponHash, 0, false, true);
                API.SetPedAmmo(playerPedId, (uint)weaponHash, ammoCount);
            }
        }
    }
}
