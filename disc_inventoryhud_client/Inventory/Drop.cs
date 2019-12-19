using CitizenFX.Core;
using CitizenFX.Core.Native;
using disc_inventoryhud_client.Utils;
using disc_inventoryhud_common.Inventory;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace disc_inventoryhud_client.Inventory
{
    public class Drop : BaseScript
    {
        public IDictionary<string, dynamic> Drops { get; private set; } = new Dictionary<string, dynamic>();
        public IDictionary<string, dynamic> CloseDrops { get; private set; } = new Dictionary<string, dynamic>();

        public Drop()
        {
            EventHandlers[Events.UpdateDrops] += new Action<IDictionary<string, dynamic>>(UpdateServerDrops);
            NUI.Instance.RegisterNUICallback(Callbacks.DROP_ITEM, DropItem);
        }

        private string toOwner(Vector3 vector)
        {
            return 'x' + vector.X.ToString() + 'y' + vector.Y.ToString() + 'z' + vector.Z.ToString();
        }

        public void UpdateServerDrops(IDictionary<string, dynamic> data)
        {
            Drops = data;
        }

        [Tick]
        public async Task GetCloseDrops()
        {
            await Delay(1000);
            var player = API.PlayerPedId();
            var playerCoords = API.GetEntityCoords(player, true);
            foreach (KeyValuePair<string, dynamic> kp in Drops)
            {
                var distance = Math.Abs((playerCoords - kp.Value.Coords).Length());
                if (distance < 5)
                {
                    kp.Value.Active = true;
                    CloseDrops[kp.Key] = kp.Value;
                }
            }
            foreach (KeyValuePair<string, dynamic> kp in CloseDrops)
            {
                if (kp.Value.Active)
                {
                    var marker = new
                    {
                        name = kp.Key + "_drop",
                        type = 2,
                        coords = new Vector3(kp.Value.Coords.X, kp.Value.Coords.Y, kp.Value.Coords.Z),
                        rotate = false,
                        colour = new { r = 255, b = 255, g = 255 },
                        size = new Vector3(0.3F, 0.3F, 0.3F),
                        msg = "Press ~INPUT_CONTEXT~ for Drop",
                        action = new Action<dynamic>(m =>
                        {

                            API.SendNuiMessage(Actions.SET_INVENTORY(kp.Value));
                            API.SendNuiMessage(Actions.SET_INVENTORY_TYPE("drop"));
                            API.SendNuiMessage(Actions.APP_SHOW);
                            API.SetNuiFocus(true, true);
                        })
                    };
                    kp.Value.Active = false;
                    TriggerEvent("disc-base:registerMarker", marker);
                }
                else
                {
                    TriggerEvent("disc-base:removeMarker", kp.Key + "_drop");
                    CloseDrops.Remove(kp.Key);
                }
            }


        }

        public void DropItem(IDictionary<string, object> data)
        {
            var player = API.PlayerPedId();
            var nonRounded = API.GetEntityCoords(player, true);
            var playerCoords = new Vector3((float)Math.Round(nonRounded.X, 1), (float)Math.Round(nonRounded.Y, 1), (float)Math.Round(nonRounded.Z, 1));
            var ownerTo = toOwner(playerCoords);
            IDictionary<string, dynamic> dataData = (IDictionary<string, dynamic>)data["data"];
            dataData.Add("ownerTo", ownerTo);
            dataData.Add("typeTo", "drop");
            dataData.Add("coords", ownerTo);
            if (Drops.ContainsKey(ownerTo))
            {
                dataData.Add("slotTo", -1);
            }
            else
            {
                dataData.Add("slotTo", 1);
            }
            TriggerServerEvent(Events.MoveItem, data);
        }
    }
}
