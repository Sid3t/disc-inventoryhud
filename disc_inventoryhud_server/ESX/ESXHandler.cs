using CitizenFX.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace disc_inventoryhud_server.ESX
{
    public class ESXHandler :BaseScript
    {
        public static dynamic Instance { get; private set; }

        public ESXHandler()
        {
            TriggerEvent("esx:getSharedObject", new Action<dynamic>((response) =>
            {
                Instance = response;
            }));
        }
    }
}
