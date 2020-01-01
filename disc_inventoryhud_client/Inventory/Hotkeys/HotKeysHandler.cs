using CitizenFX.Core;
using CitizenFX.Core.Native;
using disc_inventoryhud_common.Inventory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace disc_inventoryhud_client.Inventory.Hotkeys
{
    class HotKeysHandler : BaseScript
    {
        private List<int> Keys = new List<int> { 157, 158, 160, 164, 165, 159 };

        [Tick]
        public async Task HandleHotKeys()
        {
            Keys.ForEach(key =>
            {
                if (API.IsDisabledControlJustReleased(0, key))
                {
                    TriggerServerEvent(Events.HotKeyUse, Keys.IndexOf(key)+ 1);
                }
            });
            if (API.IsDisabledControlJustReleased(0, 37))
            {
                API.SendNuiMessage(Actions.HOTBAR_HIDE);
            }
            if (API.IsDisabledControlJustPressed(0, 37))
            {
                API.SendNuiMessage(Actions.HOTBAR_SHOW);
            }
        }

        [Tick]
        public async Task DisableKeys()
        {
            API.BlockWeaponWheelThisFrame();
            API.HideHudComponentThisFrame(19);
            API.HideHudComponentThisFrame(20);
            API.HideHudComponentThisFrame(17);
            API.DisableControlAction(0, 37, true);
        }

    }
}
