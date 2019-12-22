using CitizenFX.Core;
using disc_inventoryhud_common.Inv;
using disc_inventoryhud_common.Inventory;
using disc_inventoryhud_server.MySQL;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace disc_inventoryhud_server.Inventory.Vehicle
{
    class VehicleHandler : BaseScript
    {
        public VehicleHandler()
        {
            EventHandlers[Events.OpenTrunk] += new Action<Player, string>(OpenTrunk);
        }

        public void OpenTrunk([FromSource] Player player, string plate)
        {
            var pars = new Dictionary<string, object>
            {
                ["@owner"] = plate,
                ["@type"] = "vehicle"
            };

            KeyValuePair<string, string> kp = new KeyValuePair<string, string>("vehicle", plate);
            MySQLHandler.Instance.FetchAll("SELECT * FROM disc_inventory WHERE owner=@owner AND type=@type", pars, new Action<List<dynamic>>((objs) =>
            {
                if (objs.Count == 1)
                {
                    InventoryData data = new InventoryData
                    {
                        Owner = plate,
                        Type = "vehicle"
                    };
                    foreach (dynamic obj in objs)
                    {
                        data.Inventory.Add(obj.slot, JsonConvert.DeserializeObject<InventorySlot>(obj.data));
                    }
                    Inventory.Instance.LoadedInventories[kp] = data;
                    player.TriggerEvent(Events.OpenTrunk, data);
                }
                else
                {
                    InventoryData data = new InventoryData
                    {
                        Owner = plate,
                        Type = "vehicle"
                    };
                    Inventory.Instance.LoadedInventories[kp] = data;
                    player.TriggerEvent(Events.OpenTrunk, data);
                }
            }));
        }
    }
}
