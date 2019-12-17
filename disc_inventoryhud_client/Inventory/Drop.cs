using CitizenFX.Core;
using CitizenFX.Core.Native;
using disc_inventoryhud_common.Inventory;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace disc_inventoryhud_client.Inventory
{
    class Drop : BaseScript
    {
        public Dictionary<Vector3, dynamic> Drops { get; private set; } = new Dictionary<Vector3, dynamic>();
        public Dictionary<Vector3, dynamic> CloseDrops { get; private set; } = new Dictionary<Vector3, dynamic>();

        public Drop()
        {
            EventHandlers[Events.UpdateDrops] += new Action<IDictionary<string, dynamic>>(UpdateServerDrops);
        }

        private Vector3 fromOwner(string owner)
        {
            var x = float.Parse(Regex.Match(owner, @"x\d*\.\d").Value.Substring(1));
            var y = float.Parse(Regex.Match(owner, @"y\d*\.\d").Value.Substring(1));
            var z = float.Parse(Regex.Match(owner, @"z\d*\.\d").Value.Substring(1));
            return new Vector3(x, y, z);
        }

        private string getOwnerFromCoords(Vector3 vector)
        {
            return 'x' + vector.X.ToString() + 'y' + vector.Y.ToString() + 'z' + vector.Z.ToString();
        }

        public void UpdateServerDrops(IDictionary<string, dynamic> data)
        {
            foreach (KeyValuePair<string, dynamic> item in data)
            {
                Drops.Add(fromOwner(item.Key), item.Value);
            }
            Debug.WriteLine(JsonConvert.SerializeObject(Drops));
        }

        [Tick]
        public async Task GetCloseDrops()
        {
            await Delay(1000);
            var player = API.PlayerPedId();
            var playerCoords = API.GetEntityCoords(player, true);
            foreach (KeyValuePair<Vector3, dynamic> kp in Drops)
            {
                var distance = Math.Abs((playerCoords - kp.Key).Length());
                if (distance > 5)
                {
                    kp.Value.Active = true;
                    CloseDrops[kp.Key] = kp.Value;
                }
            }

            foreach (KeyValuePair<Vector3, dynamic> kp in CloseDrops)
            {
                if (kp.Value.Active)
                {
                    var marker = new
                    {
                        name = kp.Key + "_drop",
                        type = 2,
                        coords = new Vector3(kp.Key.X, kp.Key.Y, kp.Key.Z + 1.0F),
                        rotate = false,
                        colour = new { r = 255, b = 255, g = 255 },
                        size = new Vector3(0.5F, 0.5F, 0.5F),
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




    }
}
