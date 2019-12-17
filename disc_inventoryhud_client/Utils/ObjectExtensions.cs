using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace disc_inventoryhud_client.Utils
{
    static class ObjectExtensions
    {
        public static string ToJson(this object obj) {
            return JsonConvert.SerializeObject(obj);
        }
    }
}
