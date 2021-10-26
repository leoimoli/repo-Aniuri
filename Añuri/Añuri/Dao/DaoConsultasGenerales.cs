using Añuri.Entidades;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Añuri.Dao
{
    public class DaoConsultasGenerales
    {
        private static MySql.Data.MySqlClient.MySqlConnection connection = new MySqlConnection(Properties.Settings.Default.db);
        public static int ContadorProveedores()
        {
            connection.Close();
            connection.Open();
            int total = 0;
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = connection;
            DataTable Tabla = new DataTable();
            MySqlParameter[] oParam = { };
            string proceso = "ContadorProveedores";
            MySqlDataAdapter dt = new MySqlDataAdapter(proceso, connection);
            dt.SelectCommand.CommandType = CommandType.StoredProcedure;
            dt.SelectCommand.Parameters.AddRange(oParam);
            dt.Fill(Tabla);
            if (Tabla.Rows.Count > 0)
            {
                foreach (DataRow item in Tabla.Rows)
                {
                    total = Convert.ToInt32(item["Total"].ToString());
                }
            }
            connection.Close();
            return total;
        }

        public static string ConsultaVariableStockFaltante()
        {
            connection.Close();
            connection.Open();
            string variable = "";
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = connection;
            DataTable Tabla = new DataTable();
            MySqlParameter[] oParam = { };
            string proceso = "ConsultaVariableStockFaltante";
            MySqlDataAdapter dt = new MySqlDataAdapter(proceso, connection);
            dt.SelectCommand.CommandType = CommandType.StoredProcedure;
            dt.SelectCommand.Parameters.AddRange(oParam);
            dt.Fill(Tabla);
            if (Tabla.Rows.Count > 0)
            {
                foreach (DataRow item in Tabla.Rows)
                {
                    variable = item["StockFaltante"].ToString();
                }
            }
            connection.Close();
            return variable;
        }

        public static int ContadorProductos()
        {
            connection.Close();
            connection.Open();
            int total = 0;
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = connection;
            DataTable Tabla = new DataTable();
            MySqlParameter[] oParam = { };
            string proceso = "ContadorProductos";
            MySqlDataAdapter dt = new MySqlDataAdapter(proceso, connection);
            dt.SelectCommand.CommandType = CommandType.StoredProcedure;
            dt.SelectCommand.Parameters.AddRange(oParam);
            dt.Fill(Tabla);
            if (Tabla.Rows.Count > 0)
            {
                foreach (DataRow item in Tabla.Rows)
                {
                    total = Convert.ToInt32(item["Total"].ToString());
                }
            }
            connection.Close();
            return total;
        }
        public static bool UpdateVariableStockFaltante(int variableStockFalntante)
        {
            bool Exito = false;
            connection.Close();
            connection.Open();
            ///PROCEDIMIENTO
            string proceso = "UpdateVariableStockFaltante";
            MySqlCommand cmd = new MySqlCommand(proceso, connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("variableStockFalntante_in", variableStockFalntante);
            cmd.ExecuteNonQuery();
            Exito = true;
            connection.Close();
            return Exito;
        }
        public static int ContadorObras()
        {
            connection.Close();
            connection.Open();
            int total = 0;
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = connection;
            DataTable Tabla = new DataTable();
            MySqlParameter[] oParam = { };
            string proceso = "ContadorTotalDeObras";
            MySqlDataAdapter dt = new MySqlDataAdapter(proceso, connection);
            dt.SelectCommand.CommandType = CommandType.StoredProcedure;
            dt.SelectCommand.Parameters.AddRange(oParam);
            dt.Fill(Tabla);
            if (Tabla.Rows.Count > 0)
            {
                foreach (DataRow item in Tabla.Rows)
                {
                    total = Convert.ToInt32(item["Total"].ToString());
                }
            }
            connection.Close();
            return total;
        }
        public static int ContadorUsuarios()
        {
            connection.Close();
            connection.Open();
            int total = 0;
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = connection;
            DataTable Tabla = new DataTable();
            MySqlParameter[] oParam = { };
            string proceso = "ContadorUsuarios";
            MySqlDataAdapter dt = new MySqlDataAdapter(proceso, connection);
            dt.SelectCommand.CommandType = CommandType.StoredProcedure;
            dt.SelectCommand.Parameters.AddRange(oParam);
            dt.Fill(Tabla);
            if (Tabla.Rows.Count > 0)
            {
                foreach (DataRow item in Tabla.Rows)
                {
                    total = Convert.ToInt32(item["Total"].ToString());
                }
            }
            connection.Close();
            return total;
        }
    }
}
