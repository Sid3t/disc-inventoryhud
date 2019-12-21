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

namespace disc_inventoryhud_client.Inventory.Vehicle
{
    class VehicleHandler : BaseScript
    {
        public VehicleHandler()
        {
            EventHandlers[Events.OpenTrunk] += new Action<ExpandoObject>(OpenTrunk);
        }

        [Tick]
        public async Task HandleTrunkOpen()
        {
            if (API.IsControlJustReleased(0, 47))
            {
                var playerPed = API.PlayerPedId();
                var vehicle = ESXHandler.Instance.Game.GetVehicleInDirection();
                if (API.DoesEntityExist(vehicle) && API.GetVehiclePedIsIn(playerPed, false) == 0)
                {
                    if (!(API.GetVehicleDoorLockStatus(vehicle) == 2))
                    {
                        var boneIndex = API.GetEntityBoneIndexByName(vehicle, "platelight");
                        var vehicleCoords = API.GetWorldPositionOfEntityBone(vehicle, boneIndex);
                        var playerCoords = API.GetEntityCoords(playerPed, true);
                        var distance = API.GetDistanceBetweenCoords(vehicleCoords.X, vehicleCoords.Y, vehicleCoords.Z, playerCoords.X, playerCoords.Y, playerCoords.Z, true);
                        if (distance < 3)
                        {
                            var plate = API.GetVehicleNumberPlateText(vehicle);
                            TriggerServerEvent(Events.OpenTrunk, plate);
                        } 
                    }
                }
            }
        }

        private void OpenTrunk(ExpandoObject data)
        {
            API.SendNuiMessage(Actions.SET_INVENTORY(data));
            API.SendNuiMessage(Actions.SET_INVENTORY_TYPE("vehicle"));
            API.SendNuiMessage(Actions.APP_SHOW);
            API.SetNuiFocus(true, true);
        }
    }
}
