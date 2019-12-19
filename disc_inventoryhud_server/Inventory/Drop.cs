using CitizenFX.Core;
using disc_inventoryhud_common.Inv;
using disc_inventoryhud_common.Inventory;
using disc_inventoryhud_server.MySQL;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace disc_inventoryhud_server.Inventory
{
    class Drop : BaseScript
    {
        public Dictionary<string, InventoryData> Drops
        {
            get
            {
                return
                    Inventory.Instance.LoadedInventories.Where(kp => kp.Key.Key == "drop").ToDictionary(v => v.Key.Value, v => v.Value);

            }
        }

        public static Drop Instance { get; private set; }

        public Drop()
        {
            Instance = this;
            LoadDrops();
        }

        public void LoadDrops()
        {
            var pars = new Dictionary<string, object>()
            {
                ["@type"] = "drop"
            };

            MySQLHandler.Instance?.FetchAll("SELECT * FROM disc_inventory WHERE type=@type", pars, new Action<List<dynamic>>((objs) =>
            {
                foreach (var item in objs)
                {
                    InventoryData inventoryData = new InventoryData
                    {
                        Owner = item.owner,
                        Type = item.type,
                        Coords = fromOwner(item.owner)
                    };
                    inventoryData.Inventory.Add(item.slot, JsonConvert.DeserializeObject<InventorySlot>(item.data.ToString()));
                    Inventory.Instance.LoadedInventories[new KeyValuePair<string, string>(item.type, item.owner)] = inventoryData;
                }
                SyncDrops();
            }));
        }

        public void SyncDrops()
        {
            TriggerClientEvent(Events.UpdateDrops, Drops);
        }


        public static Vector3 fromOwner(string owner)
        {
            var x = float.Parse(Regex.Match(owner, @"x[-+]?[0-9]*\.?[0-9]*", RegexOptions.ECMAScript).Value.Substring(1));
            var y = float.Parse(Regex.Match(owner, @"y[-+]?[0-9]*\.?[0-9]*", RegexOptions.ECMAScript).Value.Substring(1));
            var z = float.Parse(Regex.Match(owner, @"z[-+]?[0-9]*\.?[0-9]*", RegexOptions.ECMAScript).Value.Substring(1));
            return new Vector3(x, y, z);
        }

        public static string toOwner(Vector3 vector)
        {
            return 'x' + vector.X.ToString() + 'y' + vector.Y.ToString() + 'z' + vector.Z.ToString();
        }
    }
}
