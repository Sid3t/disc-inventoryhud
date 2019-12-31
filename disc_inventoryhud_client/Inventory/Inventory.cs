using CitizenFX.Core;
using CitizenFX.Core.Native;
using CitizenFX.Core.UI;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using disc_inventoryhud_client.Utils;
using Newtonsoft.Json;
using disc_inventoryhud_common.Inventory;

namespace disc_inventoryhud_client.Inventory
{
    class Inventory : BaseScript
    {
        public Inventory()
        {
            Init();
            InitEventHandlers();
        }

        private void Init()
        {
            API.RegisterCommand("closeUI", new Action<int, List<object>, string>((src, args, raw) =>
            {
                Close(API.GetPlayerServerId(API.PlayerId()));
            }), false);
            NUI.Instance.RegisterNUICallback("CloseUI", (data) =>
            {
                Close(API.GetPlayerServerId(API.PlayerId()));
            });
            NUI.Instance.RegisterNUICallback(Callbacks.MOVE_ITEM, (data) =>
            {
                TriggerServerEvent(Events.MoveItem, data);
            });
            NUI.Instance.RegisterNUICallback(Callbacks.USE_ITEM, (data) =>
            {
                TriggerServerEvent(Events.UseItem, data);
            });
            EventHandlers[Events.OpenInventory] += new Action<ExpandoObject, ExpandoObject>(Open);
        }

        public void InitEventHandlers()
        {
            EventHandlers["onResourceStop"] += new Action<string>(onResourceStop);
            EventHandlers[Events.UpdateInventory] += new Action<dynamic>((inv) =>
            {
                Debug.WriteLine("Loading Inventory");
                API.SendNuiMessage(Actions.SET_INVENTORY(inv));
            });
        }

        private void onResourceStop(string ResourceName)
        {
            if (ResourceName == API.GetCurrentResourceName())
            {
                Close(API.GetPlayerServerId(API.PlayerId()));
            }
        }


        [Tick]
        private async Task HandleOpenInventory()
        {
            if (API.IsControlJustReleased(0, 289))
            {
                TriggerServerEvent(Events.OpenInventory);
            }
        }

        private void Open(ExpandoObject inv, ExpandoObject hotbar)
        {
            API.SendNuiMessage(Actions.SET_INVENTORY(inv));
            API.SendNuiMessage(Actions.SET_INVENTORY(hotbar));
            API.SendNuiMessage(Actions.SET_INVENTORY_TYPE("single"));
            API.SendNuiMessage(Actions.APP_SHOW);
            API.SetNuiFocus(true, true);
        }

        private void Close(int Handle)
        {
            API.SetNuiFocus(false, false);
            TriggerServerEvent(Events.CloseInventory, Handle);
        }
    }
}
