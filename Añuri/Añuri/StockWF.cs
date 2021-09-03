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

namespace Añuri
{
    public partial class StockWF : Form
    {
        public StockWF()
        {
            InitializeComponent();
        }
        private void btnNuevo_Click(object sender, EventArgs e)
        {
            panelGrilla.Visible = false;
            btnRegistroStock.Visible = false;
            PanelRegistroStock.Visible = true;
            btnConsultaStock.Visible = true;
            label5.Visible = false;
            txtDescipcionBus.Visible = false;
            label1.Text = "Registrar Stock";
        }
        private void btnConsultaStock_Click(object sender, EventArgs e)
        {
            panelGrilla.Visible = true;
            btnRegistroStock.Visible = true;
            PanelRegistroStock.Visible = false;
            btnConsultaStock.Visible = false;
            label5.Visible = true;
            txtDescipcionBus.Visible = true;
            label1.Text = "Stock de Productos";
        }
    }
}
