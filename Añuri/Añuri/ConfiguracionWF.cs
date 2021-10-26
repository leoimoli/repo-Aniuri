using Añuri.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;
using Añuri.Dao;

namespace Añuri
{
    public partial class ConfiguracionWF : Form
    {
        public Assembly assembly;
        public string path;

        public Configuration config;
        public string cadenaConexionCentral, cadenaConexionNotebook;
        public ConfiguracionWF()
        {
            InitializeComponent();
        }

        private void ConfiguracionWF_Load(object sender, EventArgs e)
        {
            assembly = Assembly.GetExecutingAssembly();
            path = assembly.Location;

            config = ConfigurationManager.OpenExeConfiguration(path);
            cargaConf();

            txtVariableStockFaltante.Text = DaoConsultasGenerales.ConsultaVariableStockFaltante();
        }

        private void cargaConf()
        {
            char[] div = { ';' };
            // Carga los valores de la cadena de conexion de la netbook
            string[] cadena = Settings.Default.db.Split(div);
            txtServidor.Text = cadena[0].Replace("server=", "");
            txtPuerto.Text = cadena[1].Replace("Port=", "");
            txtUsuario.Text = cadena[2].Replace("User Id=", "");
            txtClave.Text = cadena[3].Replace("password=", "");
            txtBase.Text = cadena[4].Replace("database=", "");
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            grabaConf();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int variableStockFalntante = Convert.ToInt32(txtVariableStockFaltante.Text);
            bool exito = DaoConsultasGenerales.UpdateVariableStockFaltante(variableStockFalntante);
            if (exito == true)
            {
                const string message2 = "Se modifico la variable con exito..";
                const string caption2 = "Exito:";
                var result2 = MessageBox.Show(message2, caption2,
                                             MessageBoxButtons.OK,
                                             MessageBoxIcon.Asterisk);
            }
        }

        private void grabaConf()
        {
            string cadenaConexionCentral = "server=" + txtServidor.Text + ";" + "Port=" + txtPuerto.Text + ";";
            cadenaConexionCentral += "User Id=" + txtUsuario.Text + ";" + "password=" + txtClave.Text + ";" + "database=" + txtBase.Text + ";" + "Persist Security Info = True";
            Properties.Settings.Default["db"] = cadenaConexionCentral;
            Properties.Settings.Default.Save(); // Saves settings in application configuration file
            Application.Restart();
        }

    }
}
