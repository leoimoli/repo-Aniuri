using Añuri.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Añuri
{
    public partial class ConexionWF : Form
    {
        public Assembly assembly;
        public string path;

        public Configuration config;
        public string cadenaConexionCentral, cadenaConexionNotebook;
        public ConexionWF()
        {
            InitializeComponent();
        }

        private void ConexionWF_Load(object sender, EventArgs e)
        {
            txtDni.Focus();
            assembly = Assembly.GetExecutingAssembly();
            path = assembly.Location;

            config = ConfigurationManager.OpenExeConfiguration(path);
            cargaConf();
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

        private void txtDni_KeyDown(object sender, KeyEventArgs e)
        {
            if (txtDni.Text == "33244793")
            {
                panel1.Visible = true;
                txtDni.Visible = false;
            }
            else
            {
                panel1.Visible = false;
                txtDni.Visible = true;
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
