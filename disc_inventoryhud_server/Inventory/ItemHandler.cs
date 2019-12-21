using CitizenFX.Core;
using disc_inventoryhud_common.Inv;
using disc_inventoryhud_common.Inventory;
using disc_inventoryhud_server.ESX;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace disc_inventoryhud_server.Inventory
{

    public class ItemHandler : BaseScript
    {
        private Dictionary<string, List<CallbackDelegate>> itemUsages = new Dictionary<string, List<CallbackDelegate>>();

        public ItemHandler()
        {
            EventHandlers[Events.AddItem] += new Action<int, string, int, dynamic>(AddItem);
            EventHandlers[Events.RemoveItem] += new Action<int, string, int, int>(RemoveItem);
            EventHandlers[Events.RegisterItemUse] += new Action<string, CallbackDelegate>(RegisterItemUse);
            EventHandlers[Events.UseItem] += new Action<Player, string>(UseItem);
        }

        public void AddItem(int source, string Id, int Count, dynamic MetaData)
        {
            Player player = Players[source];
            InventoryData data = GetInventoryData(player);
            if (data.Inventory.Values.Any(value => value.Id == Id))
            {
                KeyValuePair<int, InventorySlot> slot = data.Inventory.First(value => value.Value.Id == Id);
                slot.Value.Count += Count;
                Inventory.UpdateSlot(slot.Key, data, slot.Value);
            }
            else
            {
                var slot = new InventorySlot
                {
                    Id = Id,
                    Count = Count,
                    MetaData = MetaData
                };
                int slotPos = FindFreeSlot(data.Inventory);
                data.Inventory.Add(slotPos, slot);
                Inventory.CreateSlot(slotPos, data, slot);
            }
            player.TriggerEvent(Events.UpdateInventory, data);
        }

        public void RemoveItem(int source, string Id, int Count, int Slot)
        {
            Player player = Players[source];
            InventoryData data = GetInventoryData(player);
            if (Slot == 0)
            {
                KeyValuePair<int, InventorySlot> slot = data.Inventory.First(value => value.Value.Id == Id);
                if (slot.Value.Count - Count == 0)
                {
                    data.Inventory.Remove(slot.Key);
                    Inventory.DeleteSlot(slot.Key, data);
                }
                else if (slot.Value.Count - Count > 0)
                {
                    slot.Value.Count -= Count;
                    Inventory.UpdateSlot(slot.Key, data, slot.Value);
                }
                else
                {
                    Debug.WriteLine("Attempted to remove to much Items");
                }
            }
            else
            {
                KeyValuePair<int, InventorySlot> slot = data.Inventory.First(value => value.Key == Slot);
                if (slot.Value.Count - Count == 0)
                {
                    data.Inventory.Remove(slot.Key);
                    Inventory.DeleteSlot(slot.Key, data);
                }
                else if (slot.Value.Count - Count > 0)
                {
                    slot.Value.Count -= Count;
                    Inventory.UpdateSlot(slot.Key, data, slot.Value);
                }
                else
                {
                    Debug.WriteLine("Attempted to remove to much Items");
                }
            }
            player.TriggerEvent(Events.UpdateInventory, data);
        }

        public void RegisterItemUse(string Id, CallbackDelegate callbackDelegate)
        {
            if (itemUsages.ContainsKey(Id))
            {
                itemUsages[Id].Add(callbackDelegate);
            }
            else
            {
                itemUsages.Add(Id, new List<CallbackDelegate>()
                {
                    callbackDelegate
                });
            }
        }

        public void UseItem([FromSource] Player player, dynamic data)
        {
            if (itemUsages.ContainsKey(data.data.item.Id))
            {
                foreach (var action in itemUsages[data.data.item.Id])
                {
                    action.Invoke(player.Handle, data.data.item);
                }
            }
        }

        private InventoryData GetInventoryData(Player player)
        {
            var xPlayer = ESXHandler.Instance.GetPlayerFromId(player.Handle);
            var InvKey = new KeyValuePair<string, string>("player", xPlayer.identifier);
            return Inventory.Instance.LoadedInventories[InvKey];
        }

        private int FindFreeSlot(Dictionary<int, InventorySlot> inv)
        {
            for (int i = 0; i < inv.Count(); i++)
            {
                if (!inv.ContainsKey(i)) return i;
            }
            return inv.Count() + 1;
        }
    }
}
