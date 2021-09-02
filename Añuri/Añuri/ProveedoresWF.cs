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
    public partial class ProveedoresWF : Form
    {
        public ProveedoresWF()
        {
            InitializeComponent();
        }

        private void ProveedoresWF_Load(object sender, EventArgs e)
        {
            try
            {
                FuncionListarproveedores();
                FuncionBuscartexto();
            }
            catch (Exception ex)
            { }
        }
        #region Funciones
        private void FuncionListarproveedores()
        {
            FuncionBuscartexto();
            dgvProveedores.Rows.Clear();
            List<Proveedores> ListaProveedores = ProveedoresNeg.ListaDeProveedores();
            if (ListaProveedores.Count > 0)
            {
                foreach (var item in ListaProveedores)
                {
                    string Calle = item.Calle;
                    string Altura = item.Altura;
                    string Domicilio = Calle + " " + "N° " + item.Altura;
                    dgvProveedores.Rows.Add(item.idProveedor, item.NombreEmpresa, Domicilio, item.Telefono);
                }
            }
            dgvProveedores.ReadOnly = true;
        }
        private void FuncionBuscartexto()
        {
            txtDescipcionBus.AutoCompleteCustomSource = Clases_Maestras.AutoCompleteProveedores.Autocomplete();
            txtDescipcionBus.AutoCompleteMode = AutoCompleteMode.Suggest;
            txtDescipcionBus.AutoCompleteSource = AutoCompleteSource.CustomSource;
        }
        public static int Funcion = 0;
        public static int idProveedorSeleccionado = 0;
        private void HabilitarCampos()
        {
            LimpiarCampos();
            txtNombreEmpresa.Enabled = true;
            idProveedorSeleccionado = 0;
            Funcion = 1;
            HabilitarCamposNuevoProveedor();
            txtNombreEmpresa.Focus();
        }
        private void HabilitarCamposNuevoProveedor()
        {
            txtNombreEmpresa.Enabled = true;
            txtPersonaContacto.Enabled = true;
            txtCodArea.Enabled = true;
            txtTelefono.Enabled = true;
            txtEmail.Enabled = true;
            txtSitioWeb.Enabled = true;
            txtCalle.Enabled = true;
            txtAltura.Enabled = true;
        }
        private void LimpiarCampos()
        {
            txtNombreEmpresa.Clear();
            txtPersonaContacto.Clear();
            txtEmail.Clear();
            txtSitioWeb.Clear();
            txtCalle.Clear();
            txtAltura.Clear();
            txtCodArea.Clear();
            txtTelefono.Clear();
            progressBar1.Value = Convert.ToInt32(null);
            progressBar1.Visible = false;

            txtNombreEmpresa.Enabled = false;
            txtPersonaContacto.Enabled = false;
            txtCodArea.Enabled = false;
            txtTelefono.Enabled = false;
            txtEmail.Enabled = false;
            txtSitioWeb.Enabled = false;
            txtCalle.Enabled = false;
            txtAltura.Enabled = false;
        }
        private void HabilitarCamposUsuarioSeleccionado(List<Proveedores> _proveedor)
        {
            var proveedor = _proveedor.First();
            txtNombreEmpresa.Text = proveedor.NombreEmpresa;
            txtNombreEmpresa.Enabled = false;
            txtPersonaContacto.Text = proveedor.Contacto;
            txtEmail.Text = proveedor.Email;
            txtSitioWeb.Text = proveedor.SitioWeb;
            txtCalle.Text = proveedor.Calle;
            txtAltura.Text = proveedor.Altura;
            var codigo = proveedor.Telefono.Split('-');
            txtCodArea.Text = codigo[0];
            txtTelefono.Text = codigo[1];
            HabilitarCamposNuevoProveedor();
        }
        private Proveedores CargarEntidad()
        {
            Proveedores _proveedor = new Proveedores();
            int idusuarioLogueado = Sesion.UsuarioLogueado.idUsuario;
            _proveedor.NombreEmpresa = txtNombreEmpresa.Text;
            _proveedor.Contacto = txtPersonaContacto.Text;
            _proveedor.Email = txtEmail.Text;
            _proveedor.SitioWeb = txtSitioWeb.Text;
            _proveedor.Calle = txtCalle.Text;
            _proveedor.Altura = txtAltura.Text;
            string telefono = txtCodArea.Text + "-" + txtTelefono.Text;
            _proveedor.Telefono = telefono;
            DateTime fechaActual = DateTime.Now;
            _proveedor.FechaDeAlta = fechaActual;
            _proveedor.idUsuario = idusuarioLogueado;
            return _proveedor;
        }
        private Proveedores CargarEntidadEdicion()
        {
            Proveedores _proveedor = new Proveedores();
            int idusuarioLogueado = Sesion.UsuarioLogueado.idUsuario;
            _proveedor.NombreEmpresa = txtNombreEmpresa.Text;
            _proveedor.Contacto = txtPersonaContacto.Text;
            _proveedor.Email = txtEmail.Text;
            _proveedor.SitioWeb = txtSitioWeb.Text;
            _proveedor.Calle = txtCalle.Text;
            _proveedor.Altura = txtAltura.Text;
            string telefono = txtCodArea.Text + "-" + txtTelefono.Text;
            _proveedor.Telefono = telefono;
            _proveedor.idUsuario = idusuarioLogueado;
            return _proveedor;
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
        private void txtDescipcionBus_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                dgvProveedores.Rows.Clear();
                string RazonSocial = txtDescipcionBus.Text;
                List<Entidades.Proveedores> ListaProveedores = ProveedoresNeg.BuscarProvedorPorNombre(RazonSocial);
                if (ListaProveedores.Count > 0)
                {
                    foreach (var item in ListaProveedores)
                    {
                        string Calle = item.Calle;
                        string Altura = item.Altura;
                        string Domicilio = Calle + "N° " + item.Altura;
                        dgvProveedores.Rows.Add(item.idProveedor, item.NombreEmpresa, Domicilio, item.Telefono);
                    }
                }
                dgvProveedores.ReadOnly = true;
            }
        }
        private void SoloNumeros(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsNumber(e.KeyChar) && e.KeyChar != Convert.ToChar(Keys.Back);
        }
        #endregion

        #region Botones
        private void btnNuevo_Click(object sender, EventArgs e)
        {
            try
            {
                HabilitarCampos();
            }
            catch (Exception ex)
            { }
        }
        private void btnEditar_Click(object sender, EventArgs e)
        {
            Funcion = 2;
            if (this.dgvProveedores.RowCount > 0)
            {
                List<Entidades.Proveedores> _proveedor = new List<Entidades.Proveedores>();
                idProveedorSeleccionado = Convert.ToInt32(this.dgvProveedores.CurrentRow.Cells[0].Value);
                _proveedor = ProveedoresNeg.BuscarProveedorPorID(idProveedorSeleccionado);
                HabilitarCamposUsuarioSeleccionado(_proveedor);
            }
            else
            {
                const string message2 = "Debe seleccionar un proveedor de la grilla.";
                const string caption2 = "Atención";
                var result2 = MessageBox.Show(message2, caption2,
                                             MessageBoxButtons.OK,
                                             MessageBoxIcon.Asterisk);
            }
        }
        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (idProveedorSeleccionado > 0)
            {

                Proveedores _proveedor = CargarEntidadEdicion();
                bool Exito = ProveedoresNeg.EditarProveedor(_proveedor, idProveedorSeleccionado);
                if (Exito == true)
                {
                    ProgressBar();
                    const string message2 = "La edición del proveedor se registro exitosamente.";
                    const string caption2 = "Éxito";
                    var result2 = MessageBox.Show(message2, caption2,
                                                 MessageBoxButtons.OK,
                                                 MessageBoxIcon.Asterisk);
                    LimpiarCampos();
                    FuncionListarproveedores();
                }
            }
            else
            {
                Entidades.Proveedores _proveedor = CargarEntidad();
                ProgressBar();
                bool Exito = ProveedoresNeg.CargarProveedor(_proveedor);
                if (Exito == true)
                {
                    const string message2 = "Se registro el proveedor exitosamente.";
                    const string caption2 = "Éxito";
                    var result2 = MessageBox.Show(message2, caption2,
                                                 MessageBoxButtons.OK,
                                                 MessageBoxIcon.Asterisk);
                    LimpiarCampos();
                    FuncionListarproveedores();
                }
            }
        }
        #endregion


    }
}
