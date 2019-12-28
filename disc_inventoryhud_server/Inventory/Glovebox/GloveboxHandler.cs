using CitizenFX.Core;
using disc_inventoryhud_common.Inv;
using disc_inventoryhud_common.Inventory;
using disc_inventoryhud_server.MySQL;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace disc_inventoryhud_server.Inventory.Glovebox
{
    class GloveboxHandler : BaseScript
    {
        public GloveboxHandler()
        {
            EventHandlers[Events.OpenGlovebox] += new Action<Player, string>(Open);
        }

        private void Open([FromSource] Player player, string plate)
        {
            var pars = new Dictionary<string, object>
            {
                ["@owner"] = plate,
                ["@type"] = "glovebox"
            };
            KeyValuePair<string, string> kp = new KeyValuePair<string, string>("glovebox", plate);

            if (Inventory.Instance.OpenInventories.ContainsKey(kp)) return;
            Inventory.Instance.OpenInventories[kp] = player.Handle;

            MySQLHandler.Instance.FetchAll("SELECT * FROM disc_inventory WHERE owner=@owner AND type=@type", pars, new Action<List<dynamic>>((objs) =>
            {
                if (objs.Count == 1)
                {
                    InventoryData data = new InventoryData
                    {
                        Owner = plate,
                        Type = "glovebox"
                    };
                    foreach (dynamic obj in objs)
                    {
                        data.Inventory.Add(obj.slot, JsonConvert.DeserializeObject<InventorySlot>(obj.data));
                    }
                    Inventory.Instance.LoadedInventories[kp] = data;
                    player.TriggerEvent(Events.OpenGlovebox, data);
                }
                else
                {
                    InventoryData data = new InventoryData
                    {
                        Owner = plate,
                        Type = "glovebox"
                    };
                    Inventory.Instance.LoadedInventories[kp] = data;
                    player.TriggerEvent(Events.OpenGlovebox, data);
                }
            }));
        }
    }
}
