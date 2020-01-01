using CitizenFX.Core;
using CitizenFX.Core.Native;
using disc_inventoryhud_client.Inventory;
using disc_inventoryhud_common.Inventory;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace disc_inventoryhud_server.Inventory.Info
{
    public class ItemDataHandler : BaseScript
    {
        public ItemDataHandler()
        {
            EventHandlers[Events.UpdateInfo] += new Action<ExpandoObject>(UpdateInfo);
        }

        public void UpdateInfo(ExpandoObject data)
        {
            API.SendNuiMessage(Actions.SET_INFO(data));
        }

    }
}
