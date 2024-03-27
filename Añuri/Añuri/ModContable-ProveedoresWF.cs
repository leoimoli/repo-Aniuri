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
    public partial class ModContable_ProveedoresWF : Form
    {
        public ModContable_ProveedoresWF()
        {
            InitializeComponent();
        }

        private void btnCargaMasiva_Click(object sender, EventArgs e)
        {
            ModContable_CargaMasivaProveedoresWF _cargaMasiva = new ModContable_CargaMasivaProveedoresWF();
            _cargaMasiva.Show();
        }
    }
}
