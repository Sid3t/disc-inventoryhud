using CitizenFX.Core;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace disc_inventoryhud_client.ESX
{
    class ESXHandler : BaseScript
    {
        public dynamic Instance { get; private set; }
        public bool Loaded { get; set; } = false;

        public ESXHandler()
        {
            Tick += ObtainESX;
            EventHandlers["esx:setJob"] += new Action<DynamicObject>(SetJob);
        }

        private async Task ObtainESX()
        {
            await Delay(10);
            TriggerEvent("esx:getSharedObject", new Action<dynamic>((response) =>
            {
                Instance = response;
                if (Instance != null)
                {
                    Tick -= ObtainESX;
                    Tick += ObtainPlayerData;
                }
            }));
        }

        private async Task ObtainPlayerData()
        {
            await Delay(10);
            try
            {
                if (Instance.GetPlayerData().job != null)
                {
                    Instance.PlayerData = Instance.GetPlayerData();
                    Tick -= ObtainPlayerData;
                    Loaded = true;
                }
            }
            catch { }
        }

        private void SetJob(DynamicObject job)
        {
            Instance.PlayerData.job = job;
        }

        
    }
}
