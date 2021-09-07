using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Añuri.Clases_Maestras
{
    public class AutoCompleteLocalidades
    {
        public static DataTable Datos(int idProvincia)
        {
            DataTable dt = new DataTable();
            MySqlConnection conexion = new MySqlConnection(Properties.Settings.Default.db);
            conexion.Open();
            string consulta = "Select Nombre from localidades where idProvincia = '" + idProvincia + "'";
            MySqlCommand cmd = new MySqlCommand(consulta, conexion);
            MySqlDataAdapter adap = new MySqlDataAdapter(cmd);
            adap.Fill(dt);
            conexion.Close();
            return dt;
        }

        public static AutoCompleteStringCollection Autocomplete(int idProvincia)
        {
            DataTable DT = Datos(idProvincia);
            AutoCompleteStringCollection coleccion = new AutoCompleteStringCollection();
            foreach (DataRow row in DT.Rows)
            {
                coleccion.Add(Convert.ToString(row["Nombre"]));
            }
            return coleccion;
        }
    }
}
