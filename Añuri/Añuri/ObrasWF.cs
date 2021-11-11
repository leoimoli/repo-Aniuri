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
    public partial class ObrasWF : Form
    {
        public ObrasWF()
        {
            InitializeComponent();
        }
        private void ObrasWF_Load(object sender, EventArgs e)
        {
            try
            {
                FuncionBuscartexto();
                FuncionListarObras();
            }
            catch (Exception ex)
            {

            }
        }
        #region Botones
        private void btnFinalizaObra_Click(object sender, EventArgs e)
        {
            try
            {
                const string message = "¿Usted desea finalizar la obra selccionada?";
                const string caption = "Consulta";
                var result = MessageBox.Show(message, caption,
                                             MessageBoxButtons.YesNo,
                                             MessageBoxIcon.Question);
                {
                    if (result == DialogResult.Yes)
                    {
                        bool Exito = ObrasNeg.FinalizarObra(idObraSeleccionada);
                        if (Exito == true)
                        {
                            const string message2 = "Se finalizo la obra seleccionada exitosamente.";
                            const string caption2 = "Éxito";
                            var result2 = MessageBox.Show(message2, caption2,
                                                         MessageBoxButtons.OK,
                                                         MessageBoxIcon.Asterisk);
                        }
                        else
                        {
                            const string message2 = "Atención: No se pudo finalizar la obra seleccionada.";
                            const string caption2 = "Atención";
                            var result2 = MessageBox.Show(message2, caption2,
                                                         MessageBoxButtons.OK,
                                                         MessageBoxIcon.Exclamation);
                        }
                    }
                    else
                    {

                    }

                }
            }
            catch (Exception ex)
            { }
        }
        private void btnNuevo_Click(object sender, EventArgs e)
        {
            try
            {
                HabilitarCampos();
            }
            catch (Exception ex)
            { }
        }
        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (idObraSeleccionada > 0)
            {

                Obra _obra = CargarEntidadEdicion();
                bool Exito = ObrasNeg.EditarObra(_obra, idObraSeleccionada);
                if (Exito == true)
                {
                    ProgressBar();
                    const string message2 = "La edición de la obra se registro exitosamente.";
                    const string caption2 = "Éxito";
                    var result2 = MessageBox.Show(message2, caption2,
                                                 MessageBoxButtons.OK,
                                                 MessageBoxIcon.Asterisk);
                    LimpiarCampos();
                    FuncionListarObras();
                }
            }
            else
            {
                Entidades.Obra _obra = CargarEntidad();
                bool Exito = ObrasNeg.InsertObra(_obra);
                if (Exito == true)
                {
                    ProgressBar();
                    const string message2 = "Se registro  la obra exitosamente.";
                    const string caption2 = "Éxito";
                    var result2 = MessageBox.Show(message2, caption2,
                                                 MessageBoxButtons.OK,
                                                 MessageBoxIcon.Asterisk);
                    LimpiarCampos();
                    FuncionListarObras();
                }
            }
        }
        private void btnEditar_Click(object sender, EventArgs e)
        {
            Funcion = 2;
            if (this.dgvObras.RowCount > 0)
            {
                List<Obra> _obra = new List<Obra>();
                idObraSeleccionada = Convert.ToInt32(this.dgvObras.CurrentRow.Cells[0].Value);
                _obra = ObrasNeg.BuscarObraPorID(idObraSeleccionada);
                HabilitarCamposUsuarioSeleccionado(_obra);
            }
            else
            {
                const string message2 = "Debe seleccionar una obra de la grilla.";
                const string caption2 = "Atención";
                var result2 = MessageBox.Show(message2, caption2,
                                             MessageBoxButtons.OK,
                                             MessageBoxIcon.Asterisk);
            }
        }
        private void btnCargar_Click(object sender, EventArgs e)
        {
            try
            {
                List<Stock> listaMaterial = new List<Stock>();
                List<Stock> listaMaterialObtenido = new List<Stock>();
                string Material = txtMaterial.Text;
                int CantidadIngresada = Convert.ToInt32(txtCantidad.Text);
                listaMaterial = ObrasNeg.VerificarDisponibilidadDeMaterial(Material);
                if (listaMaterial.Count == 0)
                {
                    txtMaterial.Clear();
                    txtCantidad.Clear();
                    const string message2 = "Atención: En estos momentos no cuenta con el stock ingresado.";
                    const string caption2 = "Atención";
                    var result2 = MessageBox.Show(message2, caption2,
                                                 MessageBoxButtons.OK,
                                                 MessageBoxIcon.Exclamation);
                }
                else
                {
                    var lista = listaMaterial.First();
                    if (lista.Cantidad >= CantidadIngresada)
                    {
                        foreach (var item in listaMaterial)
                        {
                            if (item.Cantidad >= CantidadIngresada)
                            {
                                int idProducto = item.idProducto;
                                listaMaterialObtenido = ObrasNeg.ObtenerStockDisponible(idProducto, CantidadIngresada);
                            }
                        }
                        if (listaMaterialObtenido.Count > 0)
                        {
                            foreach (var item in listaMaterialObtenido)
                            {
                                string MaterialLista = item.Descripcion;
                                decimal CalculoNeto = item.Cantidad * item.ValorUnitario;
                                dgvListaCargaStock.Rows.Add(item.idProducto, item.idMovimientoEntrada, item.Descripcion, item.Cantidad, item.ValorUnitario, CalculoNeto, item.EstadoEntrada, 0);
                            }
                        }
                        txtMaterial.Clear();
                        txtCantidad.Clear();
                    }
                    else
                    {
                        txtMaterial.Clear();
                        txtCantidad.Clear();
                        const string message2 = "Atención: En estos momentos no cuenta con el stock ingresado.";
                        const string caption2 = "Atención";
                        var result2 = MessageBox.Show(message2, caption2,
                                                     MessageBoxButtons.OK,
                                                     MessageBoxIcon.Exclamation);
                    }
                }
            }
            catch (Exception ex)
            { }
        }
        private void btnCancelar_Click(object sender, EventArgs e)
        {
            bool Exito = false;
            try
            {
                LimpiarCamposDeExito();
                List<int> ListaIdProd = new List<int>();
                int idProducto = 0;
                foreach (DataGridViewRow row in dgvListaCargaStock.Rows)
                {
                    idProducto = Convert.ToInt32(row.Cells["idprod"].Value);
                    ListaIdProd.Add(idProducto);
                }
                Exito = ObrasNeg.LiberarSotckReservado(ListaIdProd);
                dgvListaCargaStock.Rows.Clear();
            }
            catch (Exception ex)
            { }
        }
        private void btnGuardarObra_Click(object sender, EventArgs e)
        {
            try
            {

                List<Stock> StockObra = CargarEntidadStockParaObra();
                bool Exito = ObrasNeg.GuardarDetalleObra(StockObra, idObraSeleccionada);
                if (Exito == true)
                {
                    ProgressBar();
                    const string message2 = "Se registraron los materiales para la obra seleccionada exitosamente.";
                    const string caption2 = "Éxito";
                    var result2 = MessageBox.Show(message2, caption2,
                                                 MessageBoxButtons.OK,
                                                 MessageBoxIcon.Asterisk);
                    LimpiarCamposDeExito();
                    FuncionListarObras();
                }
            }
            catch (Exception ex)
            { }
        }
        #endregion
        #region Funciones
        public static int Funcion = 0;
        public static int idObraSeleccionada = 0;
        private void HabilitarCampos()
        {
            LimpiarCampos();
            idObraSeleccionada = 0;
            Funcion = 1;
            HabilitarCamposNuevaObra();
            CargarProvincias();
        }
        private void CargarProvincias()
        {
            txtProvincia.AutoCompleteCustomSource = Clases_Maestras.AutoCompleteProvincias.Autocomplete();
            txtProvincia.AutoCompleteMode = AutoCompleteMode.Suggest;
            txtProvincia.AutoCompleteSource = AutoCompleteSource.CustomSource;
        }
        private void LimpiarCampos()
        {
            txtNombreObra.Clear();
            txtPersonaContacto.Clear();
            txtEmail.Clear();
            txtProvincia.Clear();
            txtLocalidad.Clear();
            txtCalle.Clear();
            txtAltura.Clear();
            txtCodArea.Clear();
            txtTelefono.Clear();
            txtCodigoPostal.Clear();
            progressBar1.Value = Convert.ToInt32(null);
            progressBar1.Visible = false;
        }
        private void HabilitarCamposNuevaObra()
        {
            panelDetalleObra.Visible = false;
            panelNuevaObra.Enabled = true;
            panelObra.Visible = true;
            txtNombreObra.Focus();
            txtNombreObra.Enabled = true;
            CargarProvincias();
        }
        private void SoloNumeros(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsNumber(e.KeyChar) && e.KeyChar != Convert.ToChar(Keys.Back);
        }
        public static int idProvinciaSeleccionada;
        private void txtProvincia_TextChanged(object sender, EventArgs e)
        {
            try
            {
                string provincia = txtProvincia.Text;
                if (provincia != "")
                {
                    int idProvincia = ObrasNeg.BuscarIdProvincia(provincia);
                    CargarLocalidades(idProvincia);
                    idProvinciaSeleccionada = idProvincia;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void CargarLocalidades(int idProvincia)
        {
            txtLocalidad.AutoCompleteCustomSource = Clases_Maestras.AutoCompleteLocalidades.Autocomplete(idProvincia);
            txtLocalidad.AutoCompleteMode = AutoCompleteMode.Suggest;
            txtLocalidad.AutoCompleteSource = AutoCompleteSource.CustomSource;
        }
        public static int idLocalidadSeleccionada;
        private void txtLocalidad_TextChanged(object sender, EventArgs e)
        {
            try
            {
                string localidad = txtLocalidad.Text;
                if (localidad != "")
                {
                    List<Obra> Localidad = new List<Obra>();
                    Localidad = ObrasNeg.BuscarInformacionLocalidad(localidad, idProvinciaSeleccionada);
                    var loc = Localidad.First();
                    txtCodigoPostal.Text = loc.CodigoPostalLocalidad;
                    idLocalidadSeleccionada = loc.idLocalidad;
                }
            }
            catch (Exception ex)
            {
            }
        }
        private void FuncionListarObras()
        {
            FuncionBuscartexto();
            dgvObras.Rows.Clear();
            List<Obra> ListaObras = ObrasNeg.ListaDeObras();
            if (ListaObras.Count > 0)
            {
                foreach (var item in ListaObras)
                {
                    string Calle = item.Calle;
                    string Altura = item.Altura;
                    string Domicilio = Calle + " " + "N° " + item.Altura + ", " + item.NombreProvincia + ", " + item.NombreLocalidad;
                    string estado = "";
                    if (item.Estado == 1)
                    {
                        estado = "En Ejecución";
                    }
                    if (item.Estado == 0)
                    {
                        estado = "Finalizada";
                    }
                    dgvObras.Rows.Add(item.idObra, item.NombreObra, Domicilio, estado);
                }
            }
            dgvObras.ReadOnly = true;
        }
        private void FuncionBuscartexto()
        {
            txtObraBus.AutoCompleteCustomSource = Clases_Maestras.AutoCompleteObras.Autocomplete();
            txtObraBus.AutoCompleteMode = AutoCompleteMode.Suggest;
            txtObraBus.AutoCompleteSource = AutoCompleteSource.CustomSource;
        }
        private Obra CargarEntidad()
        {
            Obra _proveedor = new Obra();
            int idusuarioLogueado = Sesion.UsuarioLogueado.idUsuario;
            _proveedor.NombreObra = txtNombreObra.Text;
            _proveedor.Contacto = txtPersonaContacto.Text;
            _proveedor.Email = txtEmail.Text;
            _proveedor.idProvincia = idProvinciaSeleccionada;
            _proveedor.idLocalidad = idLocalidadSeleccionada;
            _proveedor.CodigoPostalLocalidad = txtCodigoPostal.Text;
            _proveedor.Calle = txtCalle.Text;
            _proveedor.Altura = txtAltura.Text;
            string telefono = txtCodArea.Text + "-" + txtTelefono.Text;
            _proveedor.Telefono = telefono;
            _proveedor.FechaDeAlta = dateTimePicker1.Value;
            _proveedor.idUsuario = idusuarioLogueado;
            return _proveedor;
        }
        private Obra CargarEntidadEdicion()
        {
            Obra _proveedor = new Obra();
            int idusuarioLogueado = Sesion.UsuarioLogueado.idUsuario;
            _proveedor.NombreObra = txtNombreObra.Text;
            _proveedor.Contacto = txtPersonaContacto.Text;
            _proveedor.Email = txtEmail.Text;
            _proveedor.idProvincia = idProvinciaSeleccionada;
            _proveedor.idLocalidad = idLocalidadSeleccionada;
            _proveedor.CodigoPostalLocalidad = txtCodigoPostal.Text;
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
        private void HabilitarCamposUsuarioSeleccionado(List<Obra> _obra)
        {
            var obra = _obra.First();
            HabilitarCamposNuevaObra();
            txtNombreObra.Text = obra.NombreObra;
            txtNombreObra.Enabled = false;
            txtPersonaContacto.Text = obra.Contacto;
            txtEmail.Text = obra.Email;
            txtProvincia.Text = obra.NombreProvincia;
            txtLocalidad.Text = obra.NombreLocalidad;
            txtCalle.Text = obra.Calle;
            txtAltura.Text = obra.Altura;
            var codigo = obra.Telefono.Split('-');
            txtCodArea.Text = codigo[0];
            txtTelefono.Text = codigo[1];
        }
        private void dgvObras_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.ColumnIndex >= 0 && this.dgvObras.Columns[e.ColumnIndex].Name == "Ver" && e.RowIndex >= 0)
            {
                e.Paint(e.CellBounds, DataGridViewPaintParts.All);
                DataGridViewButtonCell BotonVer = this.dgvObras.Rows[e.RowIndex].Cells["Ver"] as DataGridViewButtonCell;
                Icon icoAtomico = new Icon(Environment.CurrentDirectory + "\\" + @"casco.ico");
                e.Graphics.DrawIcon(icoAtomico, e.CellBounds.Left + 20, e.CellBounds.Top + 4);
                this.dgvObras.Rows[e.RowIndex].Height = icoAtomico.Height + 8;
                this.dgvObras.Columns[e.ColumnIndex].Width = icoAtomico.Width + 40;
                e.Handled = true;
            }
            if (e.ColumnIndex >= 0 && this.dgvObras.Columns[e.ColumnIndex].Name == "Informe" && e.RowIndex >= 0)
            {
                e.Paint(e.CellBounds, DataGridViewPaintParts.All);
                DataGridViewButtonCell BotonVer = this.dgvObras.Rows[e.RowIndex].Cells["Informe"] as DataGridViewButtonCell;
                Icon icoAtomico = new Icon(Environment.CurrentDirectory + "\\" + @"informe-empresarial-con-crecimiento.ico");
                e.Graphics.DrawIcon(icoAtomico, e.CellBounds.Left + 20, e.CellBounds.Top + 4);
                this.dgvObras.Rows[e.RowIndex].Height = icoAtomico.Height + 8;
                this.dgvObras.Columns[e.ColumnIndex].Width = icoAtomico.Width + 40;
                e.Handled = true;
            }
        }
        private void dgvObras_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvObras.CurrentCell.ColumnIndex == 4)
            {
                dgvListaCargaStock.Rows.Clear();
                idObraSeleccionada = Convert.ToInt32(this.dgvObras.CurrentRow.Cells[0].Value.ToString());
                panelObra.Visible = true;
                panelNuevaObra.Visible = false;
                panelDetalleObra.Visible = true;
                btnEditar.Visible = false;
                lblNombreObra.Text = "Detalle de la Obra" + " " + dgvObras.CurrentRow.Cells[1].Value.ToString();
                FuncionBuscartextoMateriales();
                ListarMaterialesPreCargados();
            }
            if (dgvObras.CurrentCell.ColumnIndex == 5)
            {
                idObraSeleccionada = Convert.ToInt32(this.dgvObras.CurrentRow.Cells[0].Value.ToString());
                string Obra = dgvObras.CurrentRow.Cells[1].Value.ToString();
                string Domicilio = dgvObras.CurrentRow.Cells[2].Value.ToString();
                //this.Visible = false;              
                InformeObraWF frm2 = new InformeObraWF(idObraSeleccionada, Obra, Domicilio);
                frm2.Show();
            }
        }
        private void ListarMaterialesPreCargados()
        {
            List<Stock> ListaMateriales = ObrasNeg.ListaMaterialesExistentes(idObraSeleccionada);
            if (ListaMateriales.Count > 0)
            {
                foreach (var item in ListaMateriales)
                {
                    //string cantidad = Convert.ToString(item.Stock);
                    dgvListaCargaStock.Rows.Add(item.idProducto, item.idMovimientoEntrada, item.Descripcion, item.Cantidad, item.ValorUnitario, item.PrecioNeto, 0, 1);
                }
                if (ListaMateriales[0].EstadoEntrada == 1)
                {
                    panelDetalleObra.Enabled = true;
                }
                if (ListaMateriales[0].EstadoEntrada == 0)
                {
                    panelDetalleObra.Enabled = false;
                }
            }
            else
            {
                int Estado = ObrasNeg.ValidaEstadoObra(idObraSeleccionada);
                if (Estado == 1)
                {
                    panelDetalleObra.Enabled = true;
                }
                if (Estado == 0)
                {
                    panelDetalleObra.Enabled = false;
                }
            }
            dgvListaCargaStock.ReadOnly = true;
        }
        private void FuncionBuscartextoMateriales()
        {
            txtMaterial.AutoCompleteCustomSource = Clases_Maestras.AutoCompleteProductos.Autocomplete();
            txtMaterial.AutoCompleteMode = AutoCompleteMode.Suggest;
            txtMaterial.AutoCompleteSource = AutoCompleteSource.CustomSource;
        }
        private void txtObraBus_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                FuncionBuscartexto();
                dgvObras.Rows.Clear();
                string Obra = txtObraBus.Text;
                List<Obra> ListaObras = ObrasNeg.ListaDeObrasPorNombre(Obra);
                if (ListaObras.Count > 0)
                {
                    foreach (var item in ListaObras)
                    {
                        string Calle = item.Calle;
                        string Altura = item.Altura;
                        string Domicilio = Calle + " " + "N° " + item.Altura + ", " + item.NombreProvincia + ", " + item.NombreLocalidad;
                        string estado = "";
                        if (item.Estado == 1)
                        {
                            estado = "En Ejecución";
                        }
                        if (item.Estado == 0)
                        {
                            estado = "Finalizada";
                        }
                        dgvObras.Rows.Add(item.idObra, item.NombreObra, Domicilio, estado);
                    }
                }
                dgvObras.ReadOnly = true;
            }
        }
        private void txtMaterial_TextChanged(object sender, EventArgs e)
        {

        }
        private List<Stock> CargarEntidadStockParaObra()
        {
            int Valor = 0;
            List<Stock> ListaStock = new List<Stock>();

            foreach (DataGridViewRow row in dgvListaCargaStock.Rows)
            {
                Valor = Convert.ToInt32(row.Cells["Existente"].Value);
                if (Valor == 0)
                {
                    Stock Stock = new Stock();
                    Stock.idMovimientoEntrada = Convert.ToInt32(row.Cells["idEntrada"].Value);
                    Stock.idProducto = Convert.ToInt32(row.Cells["idprod"].Value);
                    Stock.Cantidad = Convert.ToInt32(row.Cells["kilos"].Value);
                    Stock.ValorUnitario = Convert.ToInt32(row.Cells["PrecioUnitario"].Value);
                    Stock.PrecioNeto = Convert.ToInt32(row.Cells["PrecioNeto"].Value);
                    Stock.FechaFactura = Convert.ToDateTime(dtFechaCompra.Value.ToShortDateString());
                    Stock.EstadoEntrada = Convert.ToInt32(row.Cells["EstadoEntrada"].Value);
                    ListaStock.Add(Stock);
                }
            }
            return ListaStock;
        }
        private void LimpiarCamposDeExito()
        {
            dtFechaCompra.Value = DateTime.Now;
            LimpiarGrilla();
            txtMaterial.Clear();
            txtCantidad.Clear();
            txtMaterial.Focus();
        }
        private void LimpiarGrilla()
        {
            bool Exito = false;
            List<int> ListaIdProd = new List<int>();
            int idProducto = 0;
            //int idEntrada = 0;
            foreach (DataGridViewRow row in dgvListaCargaStock.Rows)
            {
                //idEntrada = Convert.ToInt32(row.Cells["idEntrada"].Value);
                idProducto = Convert.ToInt32(row.Cells["idprod"].Value);
                ListaIdProd.Add(idProducto);
            }
            Exito = ObrasNeg.LiberarSotckReservado(ListaIdProd);
            dgvListaCargaStock.Rows.Clear();
        }
        private void dgvListaCargaStock_KeyDown(object sender, KeyEventArgs e)
        {
            int Valor = 0;
            if (e.KeyCode == Keys.Delete)
            {
                Valor = Convert.ToInt32(this.dgvListaCargaStock.CurrentRow.Cells[7].Value.ToString());
                if (Valor == 0)
                {
                    int MaterialEliminar = Convert.ToInt32(this.dgvListaCargaStock.CurrentRow.Cells[0].Value.ToString());
                    List<int> listaIdMaterial = new List<int>();
                    listaIdMaterial.Add(MaterialEliminar);
                    bool Exito = ObrasNeg.LiberarSotckReservado(listaIdMaterial);

                    if (Exito == true)
                    {
                        foreach (DataGridViewRow row in dgvListaCargaStock.Rows)
                        {
                            Valor = Convert.ToInt32(this.dgvListaCargaStock.CurrentRow.Cells[7].Value.ToString());
                            int idMaterial = Convert.ToInt32(this.dgvListaCargaStock.CurrentRow.Cells[0].Value.ToString());
                            if (idMaterial == MaterialEliminar && Valor == 0)
                            {
                                dgvListaCargaStock.Rows.Remove(dgvListaCargaStock.CurrentRow);
                            }
                        }
                    }
                }
                else
                {
                    txtMaterial.Clear();
                    txtCantidad.Clear();
                    const string message2 = "Atención: No se puede eliminar el material seleccionado.";
                    const string caption2 = "Atención";
                    var result2 = MessageBox.Show(message2, caption2,
                                                 MessageBoxButtons.OK,
                                                 MessageBoxIcon.Exclamation);
                }

            }
        }
        #endregion

        private void panelDetalleObra_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
