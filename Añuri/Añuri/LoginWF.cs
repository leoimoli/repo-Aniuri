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
using System.Security.Cryptography;
using System.IO;

namespace Añuri
{
    public partial class LoginWF : Form
    {
        public LoginWF()
        {
            InitializeComponent();
        }
        #region botones
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }
        private void btnAcceder_Click(object sender, EventArgs e)
        {
            Login();
        }
        #endregion
        #region Funciones
        private void Login()
        {
            List<Usuario> usuarios = new List<Usuario>();
            try
            {
                string usuario = txtUsuario.Text;
                string contraseña = txtClave.Text;
                string cifrar = Cifrar(contraseña);

                //string descifrar = Descifrar(cifrar);
                contraseña = cifrar;
                usuarios = UsuarioNeg.LoginUsuario(usuario, contraseña);
                if (usuarios.Count == 0)
                {
                    const string message2 = "Usuario/contraseña ingresado incorrecta.";
                    const string caption2 = "Error";
                    var result2 = MessageBox.Show(message2, caption2,
                                                 MessageBoxButtons.OK,
                                                 MessageBoxIcon.Exclamation);
                }
                else
                {
                    Sesion.UsuarioLogueado = usuarios.First();
                    MasterInicioWF _inicio = new MasterInicioWF();
                    _inicio.Show();
                    Hide();
                }
                //}              
            }
            catch (Exception ex)
            {

            }
        }
        public string Cifrar(string clave)
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
        //public string Descifrar(string cadena)
        //{
        //    byte[] llave;
        //    byte[] arreglo = Convert.FromBase64String(cadena); // Arreglo donde guardaremos la cadena descovertida.
        //                                                       // Ciframos utilizando el Algoritmo MD5.
        //    MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
        //    llave = md5.ComputeHash(UTF8Encoding.UTF8.GetBytes(clave));
        //    md5.Clear();

        //    //Ciframos utilizando el Algoritmo 3DES.
        //    TripleDESCryptoServiceProvider tripledes = new TripleDESCryptoServiceProvider();
        //    tripledes.Key = llave;
        //    tripledes.Mode = CipherMode.ECB;
        //    tripledes.Padding = PaddingMode.PKCS7;
        //    ICryptoTransform convertir = tripledes.CreateDecryptor();
        //    byte[] resultado = convertir.TransformFinalBlock(arreglo, 0, arreglo.Length);
        //    tripledes.Clear();

        //    string cadena_descifrada = UTF8Encoding.UTF8.GetString(resultado); // Obtenemos la cadena
        //    return cadena_descifrada; // Devolvemos la cadena
        //}
        // Función para descifrar una cadena.
        //private string clave = "cadenadecifrado"; // Clave de cifrado. NOTA: Puede ser cualquier combinación de carácteres.
        //        
        #endregion

        private void txtClave_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Login();
            }
        }
        private void txtUsuario_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Login();
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {
            ConexionWF _cone = new ConexionWF();
            _cone.Show();
        }
        private void LoginWF_Load(object sender, EventArgs e)
        {
            ValidarFechasFestivas();
        }
        private void ValidarFechasFestivas()
        {
            int AñoActual = DateTime.Now.Year;
            DateTime FechaActual = DateTime.Now;
            string PruebaIncio = Convert.ToString(30 + "/" + 10 + "/" + AñoActual + " " + "23" + ":59" + ":59");
            string PruebaFin = Convert.ToString(30 + "/" + 11 + "/" + AñoActual + " " + "23" + ":59" + ":59");
            string FiestasNavideñas = Convert.ToString(07 + "/" + 12 + "/" + AñoActual + " " + "23" + ":59" + ":59");
            string FechaFinFiestas = Convert.ToString(06 + "/" + 01 + "/" + AñoActual + " " + "23" + ":59" + ":59");
            //// Imagenes Navideñas
            if (FechaActual > Convert.ToDateTime(FiestasNavideñas) && Convert.ToDateTime(FechaActual) < Convert.ToDateTime(FechaFinFiestas))
            //if (FechaActual > Convert.ToDateTime(PruebaIncio) && Convert.ToDateTime(FechaActual) < Convert.ToDateTime(PruebaFin))
            {
                Image imgFiestas = Image.FromFile(Environment.CurrentDirectory + "\\" + @"Feliz-Fiesta-Login.gif");
                picFiestas.Image = imgFiestas;
            }
            else
            {
                picFiestas.Visible = false;
            }
        }
    }
}
