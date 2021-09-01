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
    public partial class UsuariosWF : Form
    {
        public UsuariosWF()
        {
            InitializeComponent();
        }
        private void UsuariosWF_Load(object sender, EventArgs e)
        {
            try
            {
                int idPerfil = Sesion.UsuarioLogueado.idPerfil;
                if (idPerfil == 1 || idPerfil == 2)
                {
                    btnNuevo.Visible = true;
                    btnEditar.Visible = true;
                }
                else
                {
                    btnNuevo.Visible = false;
                    btnEditar.Visible = false;
                }
                FuncionListarUsuarios();
                FuncionBuscartexto();
            }
            catch (Exception ex)
            { }
        }
        #region Botones
        private void btnNuevo_Click(object sender, EventArgs e)
        {
            try
            {
                HabilitarCamposBotonNuevo();
            }
            catch (Exception ex)
            { }
        }
        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (this.dgvUsuarios.RowCount > 0)
            {
                Funcion = 2;
                panel1.Enabled = true;
                idUsuarioSeleccionado = Convert.ToInt32(this.dgvUsuarios.CurrentRow.Cells[0].Value);
                string Persona = dgvUsuarios.CurrentRow.Cells[1].Value.ToString();
                string Ape = Persona.Split(',')[0];
                string Nom = Persona.Split(',')[1];
                txtApellido.Text = Ape;
                txtNombre.Text = Nom;
                txtDni.Text = dgvUsuarios.CurrentRow.Cells[2].Value.ToString();
                txtContraseña.Enabled = false;
                txtRepitaContraseña.Enabled = false;
                chcActivo.Visible = true;
                chcInactivo.Visible = true;
                lblEstado.Visible = true;
                CargarCombo();
                string perfil = dgvUsuarios.CurrentRow.Cells[3].Value.ToString();
                if (perfil == "Administrador")
                { cmbPerfil.Text = "Administrador"; }
                if (perfil == "Operador")
                { cmbPerfil.Text = "Operador"; }
            }
            else
            {
                const string message2 = "Debe seleccionar un usuario de la grilla.";
                const string caption2 = "Atención";
                var result2 = MessageBox.Show(message2, caption2,
                                             MessageBoxButtons.OK,
                                             MessageBoxIcon.Asterisk);
            }
        }
        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                if (idUsuarioSeleccionado > 0)
                {
                    if (Funcion == 2)
                    {
                        Usuario _usuario = CargarEntidadEdicion();
                        bool Exito = UsuarioNeg.EditarUsuario(_usuario, idUsuarioSeleccionado);
                        if (Exito == true)
                        {
                            ProgressBar();
                            const string message2 = "La edición del usuario se registro exitosamente.";
                            const string caption2 = "Éxito";
                            var result2 = MessageBox.Show(message2, caption2,
                                                         MessageBoxButtons.OK,
                                                         MessageBoxIcon.Asterisk);
                            LimpiarCampos();
                            FuncionListarUsuarios();
                        }
                    }
                }
                else
                {
                    Usuario _usuario = CargarEntidad();
                    bool Exito = UsuarioNeg.CargarUsuario(_usuario);
                    if (Exito == true)
                    {
                        ProgressBar();
                        const string message2 = "Se registro el usuario exitosamente.";
                        const string caption2 = "Éxito";
                        var result2 = MessageBox.Show(message2, caption2,
                                                     MessageBoxButtons.OK,
                                                     MessageBoxIcon.Asterisk);
                        LimpiarCampos();
                        FuncionListarUsuarios();
                    }
                }
            }
            catch (Exception ex)
            { }
        }
        #endregion

        #region Funciones
        public static int Funcion = 0;
        public static int idUsuarioSeleccionado = 0;
        private void txtDescipcionBus_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                dgvUsuarios.Rows.Clear();
                string Descripcion = txtDescipcionBus.Text;
                string Ape = Descripcion.Split(',')[0];
                string Nom = Descripcion.Split(',')[1];
                List<Usuario> Lista = UsuarioNeg.BuscarUsuarioPorApellidoNombre(Ape, Nom);
                if (Lista.Count > 0)
                {
                    foreach (var item in Lista)
                    {
                        string Perfil = "";
                        string Apellido = item.Apellido;
                        string Nombre = item.Nombre;
                        string Persona = Apellido + "," + Nombre;
                        string Dni = item.Dni;
                        if (item.idPerfil == 2)
                        { Perfil = "Administrador"; }
                        if (item.idPerfil == 3)
                        { Perfil = "Operador"; }
                        dgvUsuarios.Rows.Add(item.idUsuario, Persona, Dni, Perfil);
                    }
                }
                dgvUsuarios.ReadOnly = true;
            }
        }
        private void HabilitarCamposBotonNuevo()
        {
            panel1.Enabled = true;
            txtDni.Enabled = true;
            txtContraseña.Enabled = true;
            txtRepitaContraseña.Enabled = true;
            LimpiarCampos();
            txtDni.Focus();
            idUsuarioSeleccionado = 0;
            Funcion = 1;
            lblEstado.Visible = false;
            chcActivo.Visible = false;
            chcInactivo.Visible = false;
        }
        private void CargarCombo()
        {
            List<string> Perfiles = new List<string>();
            Perfiles = UsuarioNeg.CargarComboPerfiles();
            cmbPerfil.Items.Clear();
            cmbPerfil.Text = "Seleccione";
            cmbPerfil.Items.Add("Seleccione");
            foreach (string item in Perfiles)
            {
                if (item != "Super Admin")
                {
                    cmbPerfil.Text = "Seleccione";
                    cmbPerfil.Items.Add(item);
                }

            }
        }
        private void FuncionBuscartexto()
        {
            txtDescipcionBus.AutoCompleteCustomSource = Clases_Maestras.AutoCompleteUsuarios.Autocomplete();
            txtDescipcionBus.AutoCompleteMode = AutoCompleteMode.Suggest;
            txtDescipcionBus.AutoCompleteSource = AutoCompleteSource.CustomSource;
        }
        private void FuncionListarUsuarios()
        {
            try
            {
                FuncionBuscartexto();
                dgvUsuarios.Rows.Clear();
                List<Entidades.Usuario> Lista = Negocio.UsuarioNeg.ListarUsuarios();
                if (Lista.Count > 0)
                {
                    foreach (var item in Lista)
                    {
                        string Perfil = "";
                        string Apellido = item.Apellido;
                        string Nombre = item.Nombre;
                        string Persona = Apellido + "," + Nombre;
                        string Dni = item.Dni;
                        if (item.idPerfil == 2)
                        { Perfil = "Administrador"; }
                        if (item.idPerfil == 3)
                        { Perfil = "Operador"; }
                        dgvUsuarios.Rows.Add(item.idUsuario, Persona, Dni, Perfil);
                    }
                }
                dgvUsuarios.ReadOnly = true;
            }
            catch (Exception ex)
            {
            }
        }
        private void DiseñoGrilla()
        {
            this.dgvUsuarios.DefaultCellStyle.Font = new Font("Tahoma", 9);
            this.dgvUsuarios.DefaultCellStyle.ForeColor = Color.Black;
            this.dgvUsuarios.DefaultCellStyle.BackColor = Color.White;
            this.dgvUsuarios.DefaultCellStyle.SelectionForeColor = Color.Red;
            this.dgvUsuarios.DefaultCellStyle.SelectionBackColor = Color.White;
        }
        private void LimpiarCampos()
        {
            txtDni.Clear();
            txtNombre.Clear();
            txtApellido.Clear();
            txtContraseña.Clear();
            txtRepitaContraseña.Clear();
            CargarCombo();
            progressBar1.Value = Convert.ToInt32(null);
            progressBar1.Visible = false;
        }
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
        private Usuario CargarEntidadEdicion()
        {
            int estado = 1;
            Usuario _usuario = new Usuario();
            if (chcInactivo.Checked == true)
            {
                estado = 0;
            }
            _usuario.Dni = txtDni.Text;
            _usuario.Apellido = txtApellido.Text;
            _usuario.Nombre = txtNombre.Text;
            if (cmbPerfil.Text == "Administrador")
            { _usuario.Perfil = "2"; }
            if (cmbPerfil.Text == "Operador")
            { _usuario.Perfil = "3"; }
            _usuario.Estado = estado;
            return _usuario;
        }
        private Usuario CargarEntidad()
        {
            Usuario _usuario = new Usuario();
            _usuario.Dni = txtDni.Text;
            _usuario.Apellido = txtApellido.Text;
            _usuario.Nombre = txtNombre.Text;
            DateTime fechaActual = DateTime.Now;
            _usuario.FechaDeAlta = fechaActual;
            _usuario.FechaUltimaConexion = fechaActual;
            if (cmbPerfil.Text == "Administrador")
            { _usuario.Perfil = "2"; }
            if (cmbPerfil.Text == "Operador")
            { _usuario.Perfil = "3"; }
            _usuario.Estado = 1;
            ValidarClavesIngresadas();
            string clave = txtContraseña.Text;
            string claveCifrada = cifrar(clave);
            _usuario.Contraseña = claveCifrada;
            _usuario.Contraseña2 = txtRepitaContraseña.Text;
            return _usuario;
        }
        private void ValidarClavesIngresadas()
        {
            if (txtContraseña.Text != txtRepitaContraseña.Text)
            {
                const string message = "Las claves ingresadas no coinciden.";
                const string caption = "Error";
                var result = MessageBox.Show(message, caption,
                                             MessageBoxButtons.OK,
                                           MessageBoxIcon.Exclamation);
                throw new Exception();
            }
        }
        // Función para cifrar una cadena.
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
        private void chcActivo_CheckedChanged(object sender, EventArgs e)
        {
            if (chcActivo.Checked == true)
            {
                chcInactivo.Checked = false;
            }

        }
        private void chcInactivo_CheckedChanged(object sender, EventArgs e)
        {
            if (chcInactivo.Checked == true)
            {
                chcActivo.Checked = false;
            }
        }
        #endregion              
    }
}
