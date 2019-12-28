using CitizenFX.Core;
using CitizenFX.Core.Native;
using disc_inventoryhud_client.ESX;
using disc_inventoryhud_common.Inventory;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace disc_inventoryhud_client.Inventory.Search
{
    class SearchHandler : BaseScript
    {
        public SearchHandler()
        {
            EventHandlers[Events.OpenSearch] += new Action<ExpandoObject>(Open);
            API.RegisterCommand("steal", new Action<int, List<object>, string>((src, args, raw) =>
            {
                OpenSearch();
            }), false);

            API.RegisterCommand("search", new Action<int, List<object>, string>((src, args, raw) =>
            {
                OpenSearch();
            }), false);
        }

        private void OpenSearch()
        {
            var target = ESXHandler.Instance.Game.GetClosestPlayer();
            var targetPed = API.GetPlayerPed(target);
            var playerPed = API.PlayerPedId();
            var targetCoords = API.GetEntityCoords(targetPed, true);
            var playerCoords = API.GetEntityCoords(playerPed, true);
            var distance = API.GetDistanceBetweenCoords(playerCoords.X, playerCoords.Y, playerCoords.Z, targetCoords.X, targetCoords.Y, targetCoords.Z, true);
            if (distance < 3)
            {
                TriggerServerEvent(Events.OpenSearch, API.GetPlayerServerId(target).ToString());
            }
        }


        private void Open(ExpandoObject data)
        {
            API.SendNuiMessage(Actions.SET_INVENTORY(data));
            API.SendNuiMessage(Actions.SET_INVENTORY_TYPE("search"));
            API.SendNuiMessage(Actions.APP_SHOW);
            API.SetNuiFocus(true, true);
        }
    }
}
