using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Añuri.Entidades;
using MySql.Data.MySqlClient;

namespace Añuri.Dao
{
    public class StockDao
    {
        private static MySql.Data.MySqlClient.MySqlConnection connection = new MySqlConnection(Properties.Settings.Default.db);
        public static bool InsertarStock(List<Stock> listaStock)
        {

        }
    }
}
