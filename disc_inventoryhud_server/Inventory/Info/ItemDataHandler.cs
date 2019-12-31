using CitizenFX.Core;
using disc_inventoryhud_common.Inventory;
using disc_inventoryhud_server.MySQL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace disc_inventoryhud_server.Inventory.Info
{
    public class ItemDataHandler : BaseScript
    {
        public ItemDataHandler Instance { get; private set; }

        public Dictionary<string, ItemData> itemDatas = new Dictionary<string, ItemData>();
        

        public ItemDataHandler()
        {
            Instance = this;
            EventHandlers["onMySQLReady"]  += new Action(LoadInfo);
            EventHandlers["esx:playerLoaded"] += new Action<int>(LoadInfoForPlayer);
        }

        public void LoadInfo()
        {
            MySQLHandler.Instance?.FetchAll("SELECT * FROM disc_inventory_itemdata", new Dictionary<string, object>(), new Action<List<dynamic>>((objs) =>
            {
                foreach(dynamic obj in objs)
                {
                    itemDatas.Add(obj.item, new ItemData
                    {
                        Item = obj.item,
                        Description = obj.description,
                        Label = obj.label,
                        Meta = obj.meta,
                        ItemUrl = obj.itemurl
                    });
                }
                TriggerClientEvent(Events.UpdateInfo, itemDatas);
            }));
        }

        public void LoadInfoForPlayer(int source)
        {
            Players.First(player => player.Handle == source.ToString()).TriggerEvent(Events.UpdateInfo, itemDatas);
        }
    }
}
