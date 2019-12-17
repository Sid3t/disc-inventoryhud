using CitizenFX.Core;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace disc_inventoryhud_server.MySQL
{
    class MySQLHandler : BaseScript
    {
        public static MySQLHandler Instance { get; private set; }

        public MySQLHandler()
        {
            Instance = this;
        }

        public void FetchAll(string query, Dictionary<string, object> pars, Action<List<dynamic>> action)
        {
            Exports["mysql-async"].mysql_fetch_all(query, pars, action);
        }

        public void Execute(string query, Dictionary<string, object> pars, Action<int> action)
        {
            Exports["mysql-async"].mysql_execute(query, pars, action);
        }


    }
}
