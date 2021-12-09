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
using System.Data.OleDb;
using Añuri.Entidades;
using Añuri.Clases_Maestras;

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

        private void btnImportar_Click(object sender, EventArgs e)
        {
            //if (chcProveedores.Checked == true)
            //{
            //    DatosProveedores();
            //}
            if (chcMateriales.Checked == true)
            {
                Datos();
            }           
        }
        public static List<Producto> ListaStatic;  
        private void Datos()
        {
            string RutaCargada = txtRuta.Text;
            //Hoja desde donde obtendremos los datos
            string hoja = "Hoja1";
            //Cadena de conexión
            // Modifico la version para computadora de Arbi. Sino va 12.0
            string conexion = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" +
                           RutaCargada +
                            ";Extended Properties='Excel 12.0;HDR=YES;';";
            OleDbConnection con = new OleDbConnection(conexion);
            //Consulta contra la hoja de Excel
            OleDbCommand cmd = new OleDbCommand("Select * From [" + hoja + "$]", con);
            try
            {
                //Conectarse al archivo de Excel
                con.Open();
                OleDbDataAdapter sda = new OleDbDataAdapter(cmd);
                DataTable data = new DataTable();
                //Cargar los datos
                sda.Fill(data);
                List<Producto> Material = new List<Producto>();
                if (data.Rows.Count > 0)
                {
                    foreach (DataRow item in data.Rows)
                    {
                        Producto list = new Producto();
                        list.DescripcionProducto = item[0].ToString();
                        string fecha = DateTime.Now.ToShortDateString();
                        list.FechaDeAlta = Convert.ToDateTime(fecha);
                        list.idUsuario = Sesion.UsuarioLogueado.idUsuario;
                        dataGridView1.Rows.Add(list.DescripcionProducto, list.FechaDeAlta, list.idUsuario);
                        Material.Add(list);
                        label5.Visible = true;
                        label4.Visible = true;
                        ListaStatic = Material;
                        label4.Text = Convert.ToString(Material.Count);
                        dataGridView1.Visible = true;
                        btnGuardar.Visible = true;
                        DiseñoGrilla();
                    }
                }
            }
            catch (Exception ex)
            {
                string message2 = ex.Message;
                const string caption2 = "Atención";
                var result2 = MessageBox.Show(message2, caption2,
                                             MessageBoxButtons.OK,
                                             MessageBoxIcon.Exclamation);
            }
        }
        private void DiseñoGrilla()
        {
            this.dataGridView1.DefaultCellStyle.Font = new Font("Tahoma", 9);
            this.dataGridView1.DefaultCellStyle.ForeColor = Color.Black;
            this.dataGridView1.DefaultCellStyle.BackColor = Color.White;
            this.dataGridView1.DefaultCellStyle.SelectionForeColor = Color.Red;
            this.dataGridView1.DefaultCellStyle.SelectionBackColor = Color.White;
        }
        private void btnCargarArchivo_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                string path = "";
                System.IO.StreamReader sr = new
                   System.IO.StreamReader(openFileDialog1.FileName);
                path = openFileDialog1.FileName;
                txtRuta.Text = path;
                sr.Close();
            }
        }

        private void btnGuardarImportacion_Click(object sender, EventArgs e)
        {          
            int Exito = Dao.ProductoDao.GuardarCargaMasivaProductos(ListaStatic);
            if (Exito == 1)
            {
                string Numero = Convert.ToString(Exito);
                string message2 = "Se registraron los materiales exitosamente.";
                const string caption2 = "Éxito";
                var result2 = MessageBox.Show(message2, caption2,
                                             MessageBoxButtons.OK,
                                             MessageBoxIcon.Asterisk);
                //LimpiarCamposImportacion();
            }
        }

        private void LimpiarCamposImportacion()
        {
            throw new NotImplementedException();
        }

        private void chcMateriales_CheckedChanged(object sender, EventArgs e)
        {
            if (chcMateriales.Checked == true)
            { chcProveedores.Checked = false; }
        }

        private void chcProveedores_CheckedChanged(object sender, EventArgs e)
        {
            if (chcProveedores.Checked == true)
            { chcMateriales.Checked = false; }
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
