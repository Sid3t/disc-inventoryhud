using CitizenFX.Core;
using CitizenFX.Core.Native;
using disc_inventoryhud_common.Inv;
using disc_inventoryhud_common.Inventory;
using disc_inventoryhud_server.ESX;
using disc_inventoryhud_server.MySQL;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace disc_inventoryhud_server.Inventory.Search
{
    class SearchHandler : BaseScript
    {
        public SearchHandler()
        {
            EventHandlers[Events.OpenSearch] += new Action<Player, string>(Open);
        }

        public void Open([FromSource] Player player, string target)
        {
            var owner = ESXHandler.Instance.GetPlayerFromId(target).identifier;
            KeyValuePair<string, string> kp = new KeyValuePair<string, string>("player", owner);
            if (Inventory.Instance.OpenInventories.ContainsKey(kp)) return;
            Inventory.Instance.OpenInventories[kp] = player.Handle;
            ;
            var newInv = new InventoryData
            {
                Owner = owner,
                Type = "search",
                Inventory = Inventory.Instance.LoadedInventories[kp].Inventory
            };
            player.TriggerEvent(Events.OpenSearch, newInv);
        }
    }
}
