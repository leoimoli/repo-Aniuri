using Añuri.Negocio;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Añuri
{
    public partial class ModContable_Contabilidad : Form
    {
        public ModContable_Contabilidad()
        {
            InitializeComponent();
        }
        private void ModContable_Contabilidad_Load(object sender, EventArgs e)
        {
            CargarCombos();
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

            ////// CARGAR COMBO TIPO COMPROBANTE......
            //List<string> TipoComprobante = new List<string>();
            string[] TipoComprobante = Clases_Maestras.ValoresConstantes.TipoComprobante;

            this.dgvComprobantes.AutoGenerateColumns = false;
            DataTable dtValores = new DataTable();
            dtValores.Columns.Add("descripcion");
         
            foreach (var item in TipoComprobante)
            {
                dtValores.Rows.Add(item);
                ((DataGridViewComboBoxColumn)this.dgvComprobantes.Columns[0]).DataPropertyName = "TcType";
                ((DataGridViewComboBoxColumn)this.dgvComprobantes.Columns[0]).DisplayMember = "descripcion";
                ((DataGridViewComboBoxColumn)this.dgvComprobantes.Columns[0]).DataSource = dtValores;
            }          
        }
        private void importarPadrónToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ModContable_ImportarPadronWF _padron = new ModContable_ImportarPadronWF();
            _padron.Show();
        }
    }
}
