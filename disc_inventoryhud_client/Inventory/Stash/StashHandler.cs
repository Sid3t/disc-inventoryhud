using CitizenFX.Core;
using CitizenFX.Core.Native;
using disc_inventoryhud_client.Utils;
using disc_inventoryhud_common.Inventory;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace disc_inventoryhud_client.Inventory.Stash
{
    class StashHandler : BaseScript
    {
        public StashHandler()
        {
            string config = API.LoadResourceFile(API.GetCurrentResourceName(), "Stashes.json");
            var Stashes = JsonConvert.DeserializeObject<List<Stash>>(config);
            LoadStashes(Stashes);
            EventHandlers[Events.OpenStash] += new Action<ExpandoObject>(Open);
            EventHandlers[Events.AddStash] += new Action<string, Vector3>(AddStash);
            EventHandlers[Events.RemoveStash] += new Action<string>(RemoveStash);
        }

        private void LoadStashes(List<Stash> stashes)
        {
            foreach (Stash stash in stashes)
            {
                AddStash(stash.Name, stash.Coords);
            }
        }

        private void Open(ExpandoObject data)
        {
            API.SendNuiMessage(Actions.SET_INVENTORY(data));
            API.SendNuiMessage(Actions.SET_INVENTORY_TYPE("stash"));
            API.SendNuiMessage(Actions.APP_SHOW);
            API.SetNuiFocus(true, true);
        }

        private void AddStash(string Name, Vector3 coords)
        {
            var marker = new
            {
                name = Name + "_stash",
                type = 1,
                coords = new Vector3(coords.X, coords.Y, coords.Z),
                rotate = false,
                colour = new { r = 255, b = 255, g = 255 },
                size = new Vector3(1F, 1F, 1F),
                msg = "Press ~INPUT_CONTEXT~ to open " + Name,
                action = new Action<dynamic>(m =>
                {
                    TriggerServerEvent(Events.OpenStash, Name);
                })
            };
            TriggerEvent("disc-base:registerMarker", marker);
        }

        private void RemoveStash(string Name)
        {
            TriggerEvent("disc-base:removeMarker", Name + "_stash");
        }
    }
}
