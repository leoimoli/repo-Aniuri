using Añuri.Clases_Maestras;
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
    public partial class ModulosWF : Form
    {
        public ModulosWF()
        {
            InitializeComponent();
        }
        private void ModulosWF_Load(object sender, EventArgs e)
        {
            try
            {
                lblUsuario.Text = Sesion.UsuarioLogueado.Apellido + ", " + Sesion.UsuarioLogueado.Nombre;
            }
            catch (Exception ex)
            {

            }
        }
        #region botones
        private void btnCerrar_Click(object sender, EventArgs e)
        {
            Close();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            MasterInicioWF _inicio = new MasterInicioWF();
            _inicio.Show();
            Hide();
        }
        private void btnContable_Click(object sender, EventArgs e)
        {
            MasterInicioContableWF _inicio = new MasterInicioContableWF();
            _inicio.Show();
            Hide();
        }
        #endregion
        #region Funciones
        #endregion
    }
}

