using Añuri.Clases_Maestras;
using Añuri.Dao;
using Añuri.Entidades;
using Añuri.Negocio;
using Añuri.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Security.Cryptography;
using System.IO;

namespace Añuri
{
    public partial class ModContable_ImportarPadronWF : Form
    {
        public ModContable_ImportarPadronWF()
        {
            InitializeComponent();
        }
        private void ModContable_ImportarPadronWF_Load(object sender, EventArgs e)
        {
            try
            {
                CargarCombos();
                txtAño.Text = Convert.ToString(DateTime.Now.Year);
            }
            catch (Exception ex)
            {
                throw new Exception();
            }
        }

        private void CargarCombos()
        {
            ////// CARGAR COMBO ORGANISMO......
            List<string> Organismos = new List<string>();
            Organismos = ContabilidadNeg.CargarComboOrganismo();
            cmbOrganismo.Items.Clear();
            cmbOrganismo.Text = "Seleccione";
            cmbOrganismo.Items.Add("Seleccione");
            foreach (string item in Organismos)
            {
                cmbOrganismo.Text = "Seleccione";
                cmbOrganismo.Items.Add(item);
            }

            ////// CARGAR COMBO MES......
            string[] Meses = Clases_Maestras.ValoresConstantes.Meses;
            cmbMes.Items.Clear();
            cmbMes.Text = "Seleccione";
            cmbMes.Items.Add("Seleccione");
            foreach (string item in Meses)
            {
                cmbMes.Text = "Seleccione";
                cmbMes.Items.Add(item);
            }
        }
        #region Botones
        private void btnCerrar_Click(object sender, EventArgs e)
        {
            Close();
        }
        #endregion
        #region Metodos
        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hWnd, int wMsg, int wParam, int IParam);
        private void MenuCabecera_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }
        #endregion
    }
}
