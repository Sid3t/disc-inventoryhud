using disc_inventoryhud_client.Util;
using disc_inventoryhud_common.Inventory;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace disc_inventoryhud_client.Inventory
{
    class Actions
    {
        public static string APP_SHOW = new
        {
            type = "APP_SHOW"
        }.ToJson();

        public static string SET_INVENTORY(ExpandoObject data)
        {
            return new
            {
                type = "SET_INVENTORY",
                data = new
                {
                    data,
                }
            }.ToJson();
        }
    }
}
