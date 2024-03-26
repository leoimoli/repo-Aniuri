using Añuri.Clases_Maestras;
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
    public class ContabilidadDao
    {
        private static MySql.Data.MySqlClient.MySqlConnection connection = new MySqlConnection(Properties.Settings.Default.db);
        public static List<string> CargarComboOrganismo()
        {
            try
            {
                connection.Close();
                connection.Open();
                List<string> _listaPerfiles = new List<string>();
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = connection;
                DataTable Tabla = new DataTable();
                MySqlParameter[] oParam = { };
                string proceso = "CargarComboOrganismo";
                MySqlDataAdapter dt = new MySqlDataAdapter(proceso, connection);
                dt.SelectCommand.CommandType = CommandType.StoredProcedure;
                dt.SelectCommand.Parameters.AddRange(oParam);
                dt.Fill(Tabla);
                if (Tabla.Rows.Count > 0)
                {
                    foreach (DataRow item in Tabla.Rows)
                    {
                        string Perfil = item["Nombre"].ToString();
                        _listaPerfiles.Add(Perfil);
                    }
                }
                connection.Close();
                return _listaPerfiles;
            }
            catch (Exception ex)
            {
                throw new Exception();
            }
        }
    }
}
