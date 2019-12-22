using CitizenFX.Core;
using CitizenFX.Core.Native;
using disc_inventoryhud_common;
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

namespace disc_inventoryhud_server.Inventory.Stash
{
    class StashHandler : BaseScript
    {
        public StashHandler()
        {
            EventHandlers[Events.OpenStash] += new Action<Player, string>(Open);
        }

        public void Open([FromSource] Player player, string owner)
        {
            var pars = new Dictionary<string, object>
            {
                ["@owner"] = owner,
                ["@type"] = "stash"
            };

            KeyValuePair<string, string> kp = new KeyValuePair<string, string>("stash", owner);
            MySQLHandler.Instance.FetchAll("SELECT * FROM disc_inventory WHERE owner=@owner AND type=@type", pars, new Action<List<dynamic>>((objs) =>
            {
                if (objs.Count >= 1)
                {
                    InventoryData data = new InventoryData
                    {
                        Owner = owner,
                        Type = "stash"
                    };
                    foreach(dynamic obj in objs)
                    {
                        data.Inventory.Add(obj.slot, JsonConvert.DeserializeObject<InventorySlot>(obj.data));
                    }
                    Inventory.Instance.LoadedInventories[kp] = data;
                    player.TriggerEvent(Events.OpenStash, data);
                }
                else
                {
                    InventoryData data = new InventoryData
                    {
                        Owner = owner,
                        Type = "stash"
                    };
                    Inventory.Instance.LoadedInventories[kp] = data;
                    player.TriggerEvent(Events.OpenStash, data);
                }
            }));
        }
    }
}
