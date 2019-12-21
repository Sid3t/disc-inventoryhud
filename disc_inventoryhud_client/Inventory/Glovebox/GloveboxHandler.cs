using CitizenFX.Core;
using CitizenFX.Core.Native;
using disc_inventoryhud_client.ESX;
using disc_inventoryhud_common.Inventory;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace disc_inventoryhud_client.Inventory.Glovebox
{
    class GloveboxHandler : BaseScript
    {
        public GloveboxHandler()
        {
            EventHandlers[Events.OpenGlovebox] += new Action<ExpandoObject>(Open);
        }

        [Tick]
        public async Task HandleTrunkOpen()
        {
            if (API.IsControlJustReleased(0, 47))
            {
                var playerPed = API.PlayerPedId();
                if (API.IsPedInAnyVehicle(playerPed, false))
                {
                    var vehicle = API.GetVehiclePedIsIn(playerPed, false);
                    var plate = API.GetVehicleNumberPlateText(vehicle);
                    TriggerServerEvent(Events.OpenGlovebox, plate);
                }
            }
        }


        private void Open(ExpandoObject data)
        {
            API.SendNuiMessage(Actions.SET_INVENTORY(data));
            API.SendNuiMessage(Actions.SET_INVENTORY_TYPE("glovebox"));
            API.SendNuiMessage(Actions.APP_SHOW);
            API.SetNuiFocus(true, true);
        }
    }
}
