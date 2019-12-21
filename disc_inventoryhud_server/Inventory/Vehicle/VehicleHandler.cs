using CitizenFX.Core;
using disc_inventoryhud_common.Inventory;
using disc_inventoryhud_server.MySQL;
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
            EventHandlers[Events.OpenTrunk] += new Action<Player, string>(Open);
        }

        public void Open([FromSource] Player player, string plate)
        {
            var pars = new Dictionary<string, object>
            {
                ["@owner"] = plate,
                ["@type"] = "vehicle"
            };

            MySQLHandler.Instance.FetchAll("SELECT * FROM disc_inventory WHERE owner=@owner AND type=@type", pars, new Action<List<dynamic>>((objs) =>
            {
                if (objs.Count == 1)
                {
                    player.TriggerEvent(Events.OpenTrunk, objs.First());
                }
                else
                {
                    player.TriggerEvent(Events.OpenTrunk, new InventoryData
                    {
                        Owner = plate,
                        Type = "vehicle"
                    });
                }
            }));
        }
    }
}
