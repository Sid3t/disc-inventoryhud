using CitizenFX.Core;
using disc_inventoryhud_common.Inventory;
using disc_inventoryhud_server.MySQL;
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

            MySQLHandler.Instance.FetchAll("SELECT * FROM disc_inventory WHERE owner=@owner AND type=@type", pars, new Action<List<dynamic>>((objs) =>
            {
                if (objs.Count == 1)
                {
                    player.TriggerEvent(Events.OpenGlovebox, objs.First());
                }
                else
                {
                    player.TriggerEvent(Events.OpenGlovebox, new InventoryData
                    {
                        Owner = plate,
                        Type = "glovebox"
                    });
                }
            }));
        }
    }
}
