using disc_inventoryhud_client.Utils;
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
        public static string HOTBAR_SHOW = new
        {
            type = "HOTBAR_SHOW"
        }.ToJson();

        public static string HOTBAR_HIDE = new
        {
            type = "HOTBAR_HIDE"
        }.ToJson();


        public static string SET_INVENTORY_TYPE(string type) {
            return new
            {
                type = "SET_INVENTORY_TYPE",
                data = new
                {
                    invType = type
                }
            }.ToJson();
        }

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

        public static string SET_INFO(ExpandoObject data)
        {
            return new
            {
                type = "SET_INFO",
                data = new
                {
                    data,
                }
            }.ToJson();
        }
    }
}
