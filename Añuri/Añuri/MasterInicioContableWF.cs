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
    public partial class MasterInicioContableWF : Form
    {
        public MasterInicioContableWF()
        {
            InitializeComponent();
            string[] VersionTexto = Clases_Maestras.ValoresConstantes.Version;
            lblVersion.Text = VersionTexto[0].ToString();
            AbrirFormEnPanel(new ModContable_InicioWFcs());
            ValidarFechasFestivas();
            var imagen = new Bitmap(Añuri.Properties.Resources.hogar__3_);
            ImagenPagina.Image = imagen;
            lblPantalla.Text = "Inicio";
            List<MenuPorPerfilUsuario> MenuPorPerfilUsuario = new List<MenuPorPerfilUsuario>();
            MenuPorPerfilUsuario = UsuarioNeg.BuscarMenuPorPerfilUsuario(Sesion.UsuarioLogueado.idPerfil);
            if (MenuPorPerfilUsuario.Count > 0)
            {
                foreach (var item in MenuPorPerfilUsuario)
                {
                    if (item.NombreMenu == "btnStock")
                    {
                        btnContable.Visible = true;
                        btnPanelStock.Visible = true;
                    }
                    if (item.NombreMenu == "btnProveedores")
                    {
                        btnProveedores.Visible = true;
                        btnPanelProveedores.Visible = true;
                    }
                    if (item.NombreMenu == "btnReportes")
                    {
                        btnReportes.Visible = true;
                        btnPanelReportes.Visible = true;
                    }
                    if (item.NombreMenu == "btnUsuarios")
                    {
                        btnUsuarios.Visible = true;
                        btnPanelUsuarios.Visible = true;
                    }
                    if (item.NombreMenu == "btnConfiguracion")
                    {
                        btnConfiguaracion.Visible = true;
                        btnPanelConfiguracion.Visible = true;
                    }
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
        private void ValidarFechasFestivas()
        {
            int AñoActual = DateTime.Now.Year;
            int AñoSiguiente = DateTime.Now.Year + 1;
            DateTime FechaActual = DateTime.Now;
            string PruebaIncio = Convert.ToString(30 + "/" + 10 + "/" + AñoActual + " " + "23" + ":59" + ":59");
            string PruebaFin = Convert.ToString(30 + "/" + 11 + "/" + AñoActual + " " + "23" + ":59" + ":59");
            string FiestasNavideñas = Convert.ToString(07 + "/" + 12 + "/" + AñoActual + " " + "23" + ":59" + ":59");
            string FiestasReyes = Convert.ToString(01 + "/" + 01 + "/" + AñoSiguiente + " " + "23" + ":59" + ":59");
            string FechaFinFiestas = Convert.ToString(06 + "/" + 01 + "/" + AñoSiguiente + " " + "23" + ":59" + ":59");
            //// Imagenes Navideñas
            if (FechaActual > Convert.ToDateTime(FiestasNavideñas) && Convert.ToDateTime(FechaActual) < Convert.ToDateTime(FiestasReyes))
            //if (FechaActual > Convert.ToDateTime(PruebaIncio) && Convert.ToDateTime(FechaActual) < Convert.ToDateTime(PruebaFin))
            {
                Image imgFiestas = Image.FromFile(Environment.CurrentDirectory + "\\" + @"Navidad-5.gif");
                picNavidad.Image = imgFiestas;
            }
            else if (FechaActual > Convert.ToDateTime(FiestasReyes) && Convert.ToDateTime(FechaActual) < Convert.ToDateTime(FechaFinFiestas))
            {
                Image imgFiestas = Image.FromFile(Environment.CurrentDirectory + "\\" + @"Reyes4.gif");
                picNavidad.Image = imgFiestas;
            }
            else
            {
                picNavidad.Visible = false;
            }
        }
        private void MasterInicioWF_Load(object sender, EventArgs e)
        {
            label6.Text = Sesion.UsuarioLogueado.Apellido + "  " + Sesion.UsuarioLogueado.Nombre;
        }
        private void PanelContenedor_Paint(object sender, PaintEventArgs e)
        {

        }
        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hWnd, int wMsg, int wParam, int IParam);
        private void AbrirFormEnPanel(object formhija)
        {
            if (this.PanelContenedor.Controls.Count > 0)
                this.PanelContenedor.Controls.RemoveAt(0);
            Form fh = formhija as Form;
            fh.TopLevel = false;
            fh.Dock = DockStyle.Fill;
            this.PanelContenedor.Controls.Add(fh);
            this.PanelContenedor.Tag = fh;
            fh.Show();
        }
        private void btnCerrar_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        private void btnMinimizar_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }
        private void btnRestaurar_Click(object sender, EventArgs e)
        {
            this.Location = new Point(50, 50); //sobra si tienes la posición en el diseño
            this.Size = new Size(1300, 650);
            btnMaximizar.Visible = true;
            btnRestaurar.Visible = false;
            //this.WindowState = FormWindowState.Normal;
            //btnMaximizar.Visible = true;
            //btnRestaurar.Visible = false;
        }
        private void btnMaximizar_Click(object sender, EventArgs e)
        {
            this.Location = new Point(0, 0); //sobra si tienes la posición en el diseño
            this.Size = new Size(this.Width + 60, Screen.PrimaryScreen.WorkingArea.Size.Height);
            //this.WindowState = FormWindowState.Maximized;
            btnMaximizar.Visible = false;
            btnRestaurar.Visible = true;         
        }
        private void btnUsuarios_Click(object sender, EventArgs e)
        {
            AbrirFormEnPanel(new UsuariosWF());
            var imagen = new Bitmap(Añuri.Properties.Resources.usuario__4_);
            ImagenPagina.Image = imagen;
            lblPantalla.Text = "Usuarios";
        }
        private void btnInicio_Click(object sender, EventArgs e)
        {
            AbrirFormEnPanel(new InicioWF());
            var imagen = new Bitmap(Añuri.Properties.Resources.hogar__3_);
            ImagenPagina.Image = imagen;
            lblPantalla.Text = "Inicio";
        }
        private void btnStock_Click(object sender, EventArgs e)
        {
            AbrirFormEnPanel(new StockWF());
            var imagen = new Bitmap(Añuri.Properties.Resources.cajas_de_carga_de_trabajador__1_);
            ImagenPagina.Image = imagen;
            lblPantalla.Text = "Stock";
        }
        private void btnObras_Click(object sender, EventArgs e)
        {
            AbrirFormEnPanel(new ObrasWF());
            var imagen = new Bitmap(Añuri.Properties.Resources.ingeniero__1_);
            ImagenPagina.Image = imagen;
            lblPantalla.Text = "Obras";
        }
        private void btnProveedores_Click(object sender, EventArgs e)
        {
            AbrirFormEnPanel(new ProveedoresWF());
            var imagen = new Bitmap(Añuri.Properties.Resources.repartidor__1_);
            ImagenPagina.Image = imagen;
            lblPantalla.Text = "Proveedores";
        }
        private void btnReportes_Click(object sender, EventArgs e)
        {
            AbrirFormEnPanel(new ReportesWF());
            var imagen = new Bitmap(Añuri.Properties.Resources.estadisticas__1_);
            ImagenPagina.Image = imagen;
            lblPantalla.Text = "Reportes";
        }
        private void btnConfiguracion_Click(object sender, EventArgs e)
        {
            AbrirFormEnPanel(new ConfiguracionWF());
            var imagen = new Bitmap(Añuri.Properties.Resources.configuraciones);
            ImagenPagina.Image = imagen;
            lblPantalla.Text = "Configuarición";
        }
        private void MenuCabecera_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }
        private void btnModulos_Click(object sender, EventArgs e)
        {
            ModulosWF _modulo = new ModulosWF();
            _modulo.Show();
            Hide();
        }
    }
}
