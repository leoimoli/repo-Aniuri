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
    public partial class MasterInicioWF : Form
    {
        public MasterInicioWF()
        {
            InitializeComponent();
            AbrirFormEnPanel(new InicioWF());
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
                        btnStock.Visible = true;
                        btnPanelStock.Visible = true;
                    }
                    if (item.NombreMenu == "btnObra")
                    {
                        btnObras.Visible = true;
                        btnPanelObras.Visible = true;
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
            DateTime FechaActual = DateTime.Now;
            string PruebaIncio = Convert.ToString(30 + "/" + 10 + "/" + AñoActual + " " + "23" + ":59" + ":59");
            string PruebaFin = Convert.ToString(02 + "/" + 11 + "/" + AñoActual + " " + "23" + ":59" + ":59");
            string FiestasNavideñas = Convert.ToString(07 + "/" + 12 + "/" + AñoActual + " " + "23" + ":59" + ":59");
            string FechaFinFiestas = Convert.ToString(06 + "/" + 01 + "/" + AñoActual + " " + "23" + ":59" + ":59");
            //// Imagenes Navideñas
            if (FechaActual > Convert.ToDateTime(FiestasNavideñas) && Convert.ToDateTime(FechaActual) < Convert.ToDateTime(FechaFinFiestas))
            {
                Image imgFiestas = Image.FromFile(Environment.CurrentDirectory + "\\" + @"Feliz-Fiesta-Login.gif");
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
            this.WindowState = FormWindowState.Normal;
            btnMaximizar.Visible = true;
            btnRestaurar.Visible = false;
        }
        private void btnMaximizar_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
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

        public static int contadorClic = 0;
        private void pictureBox6_Click(object sender, EventArgs e)
        {
            if (contadorClic == 0)
            {
                txtNuevaClave.Visible = true;
                txtNuevaClave.Focus();
                btnModificarClave.Visible = true;
                label6.Text = "Ingrese Nueva Contraseña";
                label6.Font = new Font(label6.Font.Name, 9);
                contadorClic = contadorClic + 1;
            }
            else
            {
                txtNuevaClave.Visible = false;
                btnModificarClave.Visible = false;
                label6.Text = Sesion.UsuarioLogueado.Apellido + " " + Sesion.UsuarioLogueado.Nombre;
                contadorClic = 0;
            }
        }

        private void btnModificarClave_Click(object sender, EventArgs e)
        {
            string clave = txtNuevaClave.Text;
            string claveCifrada = cifrar(clave);
            bool Exito = UsuarioDao.ResetearClave(claveCifrada);
            if (Exito == true)
            {
                const string message2 = "Se reseteo la clave exitosamente.";
                const string caption2 = "Éxito";
                var result2 = MessageBox.Show(message2, caption2,
                                             MessageBoxButtons.OK,
                                             MessageBoxIcon.Asterisk);
                txtNuevaClave.Clear();
                txtNuevaClave.Visible = false;
                btnModificarClave.Visible = false;
                label6.Text = Sesion.UsuarioLogueado.Apellido + " " + Sesion.UsuarioLogueado.Nombre;
                contadorClic = 0;
            }
            else
            {
                const string message2 = "Atención: Fallo el reseteo de la clave. Intente nuevamente.";
                const string caption2 = "Atención";
                var result2 = MessageBox.Show(message2, caption2,
                                             MessageBoxButtons.OK,
                                             MessageBoxIcon.Exclamation);
            }
        }
        public string cifrar(string clave)
        {
            byte[] llave; //Arreglo donde guardaremos la llave para el cifrado 3DES.
            byte[] arreglo = UTF8Encoding.UTF8.GetBytes(clave); //Arreglo donde guardaremos la cadena descifrada.
            // Ciframos utilizando el Algoritmo MD5.
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            llave = md5.ComputeHash(UTF8Encoding.UTF8.GetBytes(clave));
            md5.Clear();
            //Ciframos utilizando el Algoritmo 3DES.
            TripleDESCryptoServiceProvider tripledes = new TripleDESCryptoServiceProvider();
            tripledes.Key = llave;
            tripledes.Mode = CipherMode.ECB;
            tripledes.Padding = PaddingMode.PKCS7;
            ICryptoTransform convertir = tripledes.CreateEncryptor(); // Iniciamos la conversión de la cadena
            byte[] resultado = convertir.TransformFinalBlock(arreglo, 0, arreglo.Length); //Arreglo de bytes donde guardaremos la cadena cifrada.
            tripledes.Clear();
            return Convert.ToBase64String(resultado, 0, resultado.Length); // Convertimos la cadena y la regresamos.
        }

        private void btnConfiguracion_Click(object sender, EventArgs e)
        {
            AbrirFormEnPanel(new ConfiguracionWF());
            var imagen = new Bitmap(Añuri.Properties.Resources.configuraciones);
            ImagenPagina.Image = imagen;
            lblPantalla.Text = "Configuarición";
        }
    }
}
