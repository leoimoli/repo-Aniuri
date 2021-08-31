using Añuri.Clases_Maestras;
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
        #endregion
        #region Funciones
        public static int Funcion = 0;
        public static int idUsuarioSeleccionado = 0;
        private void HabilitarCamposBotonNuevo()
        {
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
        #endregion     
    }
}
