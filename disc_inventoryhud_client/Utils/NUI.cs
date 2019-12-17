using CitizenFX.Core;
using CitizenFX.Core.Native;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace disc_inventoryhud_client.Utils
{
    class NUI : BaseScript
    {
        public static NUI Instance { get; private set; }

        public NUI()
        {
            Instance = this;
        }

        public void RegisterNUICallback(string pipe, Action<IDictionary<string, object>> callback)
        {
            API.RegisterNuiCallbackType(pipe);
            EventHandlers[$"__cfx_nui:{pipe}"] += new Action<ExpandoObject, CallbackDelegate>((body, result) =>
            {
                callback.Invoke(body);
                result.Invoke("OK");
            });
        }

    }
}
