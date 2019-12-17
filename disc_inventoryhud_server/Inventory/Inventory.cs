using CitizenFX.Core;
using CitizenFX.Core.Native;
using disc_inventoryhud_common.Inv;
using disc_inventoryhud_common.Inventory;
using disc_inventoryhud_server.ESX;
using disc_inventoryhud_server.MySQL;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace disc_inventoryhud_server.Inventory
{
    class Inventory : BaseScript
    {

        protected Dictionary<KeyValuePair<string, string>, InventoryData> LoadedInventories = new Dictionary<KeyValuePair<string, string>, InventoryData>();

        public Inventory()
        {
            EventHandlers["onResourceStart"] += new Action<string>(onResourceStart);
            EventHandlers["esx:playerLoaded"] += new Action<int>(PlayerLoaded);
            EventHandlers[Events.MoveItem] += new Action<Player, IDictionary<string, dynamic>>(MoveItem);
            EventHandlers[Events.DropItem] += new Action<Player, IDictionary<string, dynamic>>(DropItem);
        }


        public void onResourceStart(string resource)
        {
            SyncPlayers();
        }

        public void PlayerLoaded(int source)
        {
            SyncPlayer(Players.First(player => player.Handle == source.ToString()));
        }

        public void SyncPlayers()
        {
            foreach (Player player in Players)
            {
                SyncPlayer(player);
            }
        }

        public void SyncPlayer(Player player)
        {
            var xPlayer = ESXHandler.Instance.GetPlayerFromId(player.Handle);
            Debug.WriteLine($"[Disc-InventoryHUD] Syncing {player.Handle} with {xPlayer.identifier}");
            LoadInventory("player", xPlayer.identifier, player);
            LoadInventory("hotbar", xPlayer.identifier, player);
            LoadInventory("equipment", xPlayer.identifier, player);
        }

        public void LoadInventory(string type, string owner, Player destination)
        {
            var pars = new Dictionary<string, object>()
            {
                ["@owner"] = owner,
                ["@type"] = type
            };

            MySQLHandler.Instance?.FetchAll("SELECT * FROM disc_inventory WHERE owner=@owner AND type=@type", pars, new Action<List<dynamic>>((objs) =>
            {
                InventoryData inventoryData = new InventoryData
                {
                    Owner = owner,
                    Type = type,
                };
                foreach (var obj in objs)
                {
                    inventoryData.Inventory.Add(obj.slot, JsonConvert.DeserializeObject<InventorySlot>(obj?.data.ToString()));
                }
                LoadedInventories[new KeyValuePair<string, string>(type, owner)] = inventoryData;
                destination.TriggerEvent(Events.UpdateInventory, inventoryData);
            }));
        }

        public void MoveItem([FromSource] Player player, IDictionary<string, dynamic> data)
        {
            var movingData = data.FirstOrDefault().Value;
            if (movingData.typeFrom == movingData.typeTo && movingData.ownerFrom == movingData.ownerTo)
            {
                var key = new KeyValuePair<string, string>(movingData.typeFrom, movingData.ownerFrom);
                var inv = LoadedInventories[key];
                var slotFrom = inv.Inventory[movingData.slotFrom];
                if (slotFrom.Count - movingData.item.Count <= 0)
                {
                    inv.Inventory.Remove(movingData.slotFrom);
                    DeleteSlot(movingData.slotFrom, inv);
                }
                else
                {
                    slotFrom.Count -= movingData.item.Count;
                    UpdateSlot(movingData.slotFrom, inv, slotFrom);
                }

                if (inv.Inventory.ContainsKey(movingData.slotTo))
                {
                    InventorySlot slot = inv.Inventory[movingData.slotTo];
                    slot.Count += movingData.item.Count;
                    UpdateSlot(movingData.slotTo, inv, slot);
                }
                else
                {
                    InventorySlot slot = new InventorySlot
                    {
                        Name = movingData.item.Name,
                        Count = movingData.item.Count
                    };
                    inv.Inventory.Add(movingData.slotTo, slot);
                    CreateSlot(movingData.slotTo, inv, slot);
                }
            }
            else
            {
                var fromKey = new KeyValuePair<string, string>(movingData.typeFrom, movingData.ownerFrom);
                var invFrom = LoadedInventories[fromKey];
                var slotFrom = invFrom.Inventory[movingData.slotFrom];
                if (slotFrom.Count - movingData.item.Count <= 0)
                {
                    invFrom.Inventory.Remove(movingData.slotFrom);
                    DeleteSlot(movingData.slotFrom, invFrom);
                }
                else
                {
                    slotFrom.Count -= movingData.item.Count;
                    UpdateSlot(movingData.slotFrom, invFrom, slotFrom);
                }

                var toKey = new KeyValuePair<string, string>(movingData.typeTo, movingData.ownerTo);
                if (LoadedInventories.ContainsKey(toKey))
                {
                    var invTo = LoadedInventories[toKey];
                    if (invTo.Inventory.ContainsKey(movingData.slotTo))
                    {
                        InventorySlot slot = invTo.Inventory[movingData.slotTo];
                        slot.Count += movingData.item.Count;
                        UpdateSlot(movingData.slotTo, invTo, slot);
                    }
                    else
                    {
                        InventorySlot slot = new InventorySlot
                        {
                            Name = movingData.item.Name,
                            Count = movingData.item.Count
                        };
                        invTo.Inventory.Add(movingData.slotTo, slot);
                        CreateSlot(movingData.slotTo, invTo, slot);
                    }
                }
                else
                {
                    InventorySlot slot = new InventorySlot
                    {
                        Name = movingData.item.Name,
                        Count = movingData.item.Count
                    };
                    InventoryData newData = new InventoryData
                    {
                        Owner = movingData.ownerTo,
                        Type = movingData.typeTo,
                        Inventory = new Dictionary<int, InventorySlot>
                        {
                            [movingData.slotTo] = slot
                        }
                    };
                    LoadedInventories[toKey] = newData;
                    CreateSlot(movingData.slotTo, newData, slot);
                }
            }
        }



        public static void CreateSlot(int slot, InventoryData data, InventorySlot iSlot)
        {
            MySQLHandler.Instance.Execute("INSERT INTO disc_inventory (owner, type, slot, data) VALUES (@owner, @type, @slot, @data)", new Dictionary<string, object>()
            {
                ["@owner"] = data.Owner,
                ["@type"] = data.Type,
                ["@slot"] = slot,
                ["@data"] = JsonConvert.SerializeObject(iSlot)
            }, new Action<int>(_ => { }));
        }

        public static void UpdateSlot(int slot, InventoryData data, InventorySlot iSlot)
        {
            MySQLHandler.Instance.Execute("UPDATE disc_inventory SET data=@data WHERE owner=@owner AND type=@type AND slot=@slot", new Dictionary<string, object>()
            {
                ["@owner"] = data.Owner,
                ["@type"] = data.Type,
                ["@slot"] = slot,
                ["@data"] = JsonConvert.SerializeObject(iSlot)
            }, new Action<int>(_ => { }));
        }

        public static void DeleteSlot(int slot, InventoryData data)
        {
            MySQLHandler.Instance.Execute("DELETE FROM disc_inventory WHERE owner=@owner AND type=@type AND slot=@slot", new Dictionary<string, object>()
            {
                ["@owner"] = data.Owner,
                ["@type"] = data.Type,
                ["@slot"] = slot,
            }, new Action<int>(_ => { }));
        }

        public void DropItem([FromSource] Player player, IDictionary<string, dynamic> data)
        {
            var droppingData = data["data"];
            Debug.WriteLine(JsonConvert.SerializeObject(droppingData));
            var playerCoords = data["coords"];
            Debug.WriteLine(playerCoords.ToString());
            var key = new KeyValuePair<string, string>(droppingData.type, droppingData.owner);
            var fromInv = LoadedInventories[key];
            if (fromInv.Inventory[droppingData.slot].Count - droppingData.Count <= 0)
            {
                fromInv.Inventory.Remove(droppingData.slot);
                Inventory.DeleteSlot(droppingData.slot, fromInv);
                Drop.Instance.UpdateDrops(playerCoords, droppingData);
            }
            else
            {
                InventorySlot slot = fromInv.Inventory[droppingData.slot];
                slot.Count -= droppingData.Count;
                Inventory.UpdateSlot(droppingData.slot, fromInv, slot);
                Drop.Instance.UpdateDrops(playerCoords, droppingData);
            }
        }
    }
}
