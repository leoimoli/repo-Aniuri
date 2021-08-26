using Añuri.Clases_Maestras;
using Añuri.Entidades;
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
    public partial class MasterInicioWF : Form
    {
        public MasterInicioWF()
        {
            InitializeComponent();
            List<MenuPorPerfilUsuario> MenuPorPerfilUsuario = new List<MenuPorPerfilUsuario>();
            MenuPorPerfilUsuario = UsuarioNeg.BuscarMenuPorPerfilUsuario(Sesion.UsuarioLogueado.idPerfil);

            if (MenuPorPerfilUsuario.Count > 0)
            {
                foreach (var item in MenuPorPerfilUsuario)
                {

                }
            }
            else
            {
                const string message2 = "Error: No se pudieron obtener los menú para el usuario logueado. Comuniquese con el administrador";
                const string caption2 = "Error";
                var result2 = MessageBox.Show(message2, caption2,
                                             MessageBoxButtons.OK,
                                             MessageBoxIcon.Exclamation);
            }
        }
        private void MasterInicioWF_Load(object sender, EventArgs e)
        {
            label6.Text = Sesion.UsuarioLogueado.Apellido + "  " + Sesion.UsuarioLogueado.Nombre;
        }
    }
}
