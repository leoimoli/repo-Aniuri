using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;
using System.Runtime.InteropServices;
using Añuri.Negocio;

namespace Añuri
{
    public partial class ModContable_CargaMasivaProveedoresWF : Form
    {
        public ModContable_CargaMasivaProveedoresWF()
        {
            InitializeComponent();
        }
        #region Botones
        private void btnCargarArchivo_Click(object sender, EventArgs e)
        {
            bool Exito = false;
            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                string path = "";
                System.IO.StreamReader sr = new
                   System.IO.StreamReader(openFileDialog1.FileName);
                path = openFileDialog1.FileName;
                txtRuta.Text = path;
                sr.Close();
                Exito = true;
            }
            if (Exito == true)
            {
                ProgressBar();
                Datos();
                btnCargaMasiva.Enabled = true;
                progressBar1.Value = Convert.ToInt32(null);
                progressBar1.Visible = false;
                btnVolver.Enabled = true;
                btnCargaMasiva.Enabled = true;
            }
        }
        private void btnCerrar_Click(object sender, EventArgs e)
        {
            Close();
        }
        private void btnVolver_Click(object sender, EventArgs e)
        {
            Close();
        }
        private void btnCargaMasiva_Click(object sender, EventArgs e)
        {
            try
            {
                if (ListaStatic != null && ListaStatic.Count > 0)
                {
                    int ContadorExito = 0;
                    foreach (var item in ListaStatic)
                    {
                        ///// VALIDO PROVEEDORES EXISTENTES.
                        bool ValidarProveedorExistente = ProveedoresNeg_Conta.ValidarProveedorExistente(item);
                        if (ValidarProveedorExistente == false)
                        {
                            item.idSitIva = ProveedoresNeg_Conta.ObteneridSitIva(item.SitIva);
                            item.idIngresosBrutos = ProveedoresNeg_Conta.ObteneridIngresosBrutos(item.IngresosBrutos);
                            item.idPais = ProveedoresNeg_Conta.ObteneridPais(item.Pais);
                            item.idProvincia = ProveedoresNeg_Conta.ObteneridProvincia(item.Provincia, item.idPais);
                            item.idLocalalidad = ProveedoresNeg_Conta.ObteneridLocalidad(item.Localidad, item.idProvincia);
                            bool RegistroExitoso = ProveedoresNeg_Conta.RegistrarProveedor(item);
                            if (RegistroExitoso == true)
                            {
                                ContadorExito = ContadorExito + 1;
                            }
                        }
                        else
                        {

                        }
                    }
                    string message = "Se registro un total de '" + Convert.ToString(ContadorExito) + "' proveedores."; ;
                    const string caption = "Exito:";
                    var result = MessageBox.Show(message, caption,
                                                 MessageBoxButtons.OK,
                                               MessageBoxIcon.Exclamation);
                    LimpiarCampos();
                }
                else
                {
                    const string message = "Atención: Debe importar un archivo en formato Excel.";
                    const string caption = "Atención:";
                    var result = MessageBox.Show(message, caption,
                                                 MessageBoxButtons.OK,
                                               MessageBoxIcon.Exclamation);
                    throw new Exception();
                }
            }
            catch (Exception ex)
            {

            }
        }
        #endregion
        #region Metodos
        private void ProgressBar()
        {
            progressBar1.Visible = true;
            progressBar1.Maximum = 100000;
            progressBar1.Step = 1;

            for (int j = 0; j < 100000; j++)
            {
                Caluculate(j);
                progressBar1.PerformStep();
            }
        }
        private void Caluculate(int i)
        {
            double pow = Math.Pow(i, i);
        }
        public static List<Entidades.ProveedoresModCont> ListaStatic;
        private void Datos()
        {
            List<Entidades.ProveedoresModCont> listaFinal = new List<Entidades.ProveedoresModCont>();
            string RutaCargada = txtRuta.Text;
            //Hoja desde donde obtendremos los datos
            //string hoja = "Sheet1";
            string hoja = "LISTADO PROVEEDORES AÑURI 2024";
            //Cadena de conexión
            // Modifico la version para computadora de Arbi. Sino va 12.0
            string conexion = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" +
                           RutaCargada +
                            ";Extended Properties='Excel 12.0;HDR=NO;';";
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
                if (data.Rows.Count > 0)
                {
                    dataGridView1.Visible = true;
                    foreach (DataRow item in data.Rows)
                    {
                        Entidades.ProveedoresModCont list = new Entidades.ProveedoresModCont();
                        list.RazonSocial = item[1].ToString();
                        list.Pais = item[2].ToString();
                        list.Provincia = item[3].ToString();
                        list.Localidad = item[4].ToString();
                        list.Domicilio = item[5].ToString();
                        list.Altura = item[6].ToString();
                        list.Piso = item[7].ToString();
                        list.Departamento = item[8].ToString();
                        list.CodigoPostal = item[9].ToString();
                        list.SitIva = item[10].ToString();
                        list.Dni = item[11].ToString();
                        list.IngresosBrutos = item[12].ToString();
                        list.NumeroIB = item[13].ToString();
                        listaFinal.Add(list);
                        dataGridView1.Rows.Add(list.RazonSocial, list.Pais, list.Provincia, list.Localidad, list.Domicilio, list.Altura, list.Piso, list.Departamento, list.CodigoPostal, list.SitIva, list.Dni, list.IngresosBrutos, list.NumeroIB);
                    }
                    ListaStatic = listaFinal;
                    lblContador.Text = Convert.ToString(data.Rows.Count);
                }
            }
            catch (Exception ex)
            {
                string message2 = ex.Message;
                const string caption2 = "Atención";
                var result2 = MessageBox.Show(message2, caption2,
                                             MessageBoxButtons.OK,
                                             MessageBoxIcon.Exclamation);
                // MessageBox.Show(ex.Message);
            }
        }
        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hWnd, int wMsg, int wParam, int IParam);
        private void MenuCabecera_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }
        private void ModContable_CargaMasivaProveedoresWF_Load(object sender, EventArgs e)
        {

        }
        private void LimpiarCampos()
        {
            txtRuta.Clear();
            progressBar1.Value = Convert.ToInt32(null);
            progressBar1.Visible = false;
            btnCargaMasiva.Enabled = false;
            dataGridView1.Enabled = false;
        }
        #endregion
    }
}
