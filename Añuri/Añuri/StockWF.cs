using Añuri.Clases_Maestras;
using Añuri.Dao;
using Añuri.Entidades;
using Añuri.Negocio;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Añuri
{
    public partial class StockWF : Form
    {
        public StockWF()
        {
            InitializeComponent();
        }
        private void StockWF_Load(object sender, EventArgs e)
        {
            try
            {
                FuncionListarProductos();
                FuncionBuscartexto();
            }
            catch (Exception ex)
            { }
        }
        #region Botones
        private void btnNuevo_Click(object sender, EventArgs e)
        {
            panelGrilla.Visible = false;
            btnRegistroStock.Visible = false;
            PanelRegistroStock.Visible = true;
            btnConsultaStock.Visible = true;
            label5.Visible = false;
            txtDescipcionBus.Visible = false;
            label1.Text = "Registrar Stock";
            btnEditarProducto.Visible = false;
            txtMaterial.Focus();
            BuscarProveedores();
            PanelDetalleStock.Visible = false;
            PanelNuevoMaterial.Visible = true;
            lblNuevoProducto.Text = "Nuevo Material";
        }
        private void btnConsultaStock_Click(object sender, EventArgs e)
        {
            panelGrilla.Visible = true;
            btnRegistroStock.Visible = true;
            PanelRegistroStock.Visible = false;
            PanelNuevoMaterial.Enabled = false;
            btnConsultaStock.Visible = false;
            label5.Visible = true;
            txtDescipcionBus.Visible = true;
            label1.Text = "Stock de Productos";
            btnEditarProducto.Visible = true;
            FuncionListarProductos();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            Producto _producto = CargarEntidad();
            if (idProductoSeleccionado > 0)
            {
                if (Funcion == 2)
                {
                    ProgressBar();
                    bool Exito = ProductoNeg.EditarProducto(_producto, idProductoSeleccionado);
                    if (Exito == true)
                    {
                        const string message2 = "La edición del producto se registro exitosamente.";
                        const string caption2 = "Éxito";
                        var result2 = MessageBox.Show(message2, caption2,
                                                     MessageBoxButtons.OK,
                                                     MessageBoxIcon.Asterisk);
                        LimpiarCamposNuevoProducto();
                    }
                }
            }
            else
            {
                bool Exito = ProductoNeg.CargarProducto(_producto);
                if (Exito == true)
                {
                    ProgressBar();
                    const string message2 = "Se registro el producto exitosamente.";
                    const string caption2 = "Éxito";
                    var result2 = MessageBox.Show(message2, caption2,
                                                 MessageBoxButtons.OK,
                                                 MessageBoxIcon.Asterisk);
                    LimpiarCamposNuevoProducto();
                }
            }
            FuncionListarProductos();
        }
        private void btnCrear_Click(object sender, EventArgs e)
        {
            CargarCombo();
            idProductoSeleccionado = 0;
            Funcion = 1;
            txtDescripcionProducto.Clear();
            txtDescripcionProducto.Focus();
            PanelNuevoMaterial.Visible = true;
            PanelNuevoMaterial.Enabled = true;
        }

        private void CargarCombo()
        {
            cmbTipoMedicion.Items.Clear();
            cmbTipoMedicion.Text = "Seleccione";
            cmbTipoMedicion.Items.Add("Seleccione");
            cmbTipoMedicion.Items.Add("KILOS");
            cmbTipoMedicion.Items.Add("UNIDAD");

        }

        private void btnCargar_Click(object sender, EventArgs e)
        {
            try
            {
                bool existeProducto = StockDao.ValidarProductoExistente(txtMaterial.Text);
                if (existeProducto == false)
                {
                    const string message2 = "Atención: El material ingresado no existe.";
                    const string caption2 = "Atención";
                    var result2 = MessageBox.Show(message2, caption2,
                                                 MessageBoxButtons.OK,
                                                 MessageBoxIcon.Exclamation);
                }
                bool existeProveedor = ProveedoresDao.ValidarProveedorExistente(txtProveedor.Text);
                if (existeProveedor == false)
                {
                    const string message2 = "Atención: El proveedor ingresado no existe.";
                    const string caption2 = "Atención";
                    var result2 = MessageBox.Show(message2, caption2,
                                                 MessageBoxButtons.OK,
                                                 MessageBoxIcon.Exclamation);
                }
                else
                {
                    Stock Entidad = CargarEntidadRegistroStock();
                    dgvListaCargaStock.Rows.Add(Entidad.idProducto, Entidad.Descripcion, Entidad.Cantidad, Entidad.ValorUnitario, Entidad.PrecioNeto, Entidad.Observaciones);
                    decimal PrecioTotalFinal = 0;
                    foreach (DataGridViewRow row in dgvListaCargaStock.Rows)
                    {
                        if (row.Cells[4].Value != null)
                            PrecioTotalFinal += Convert.ToDecimal(row.Cells[4].Value.ToString());
                    }
                    string PrecioMostrar = PrecioTotalFinal.ToString("N", new CultureInfo("es-CL"));
                    //lblTotalPagarReal.Text = Convert.ToString(PrecioTotalFinal);
                    lblTotalPagarReal.Text = Convert.ToString(PrecioMostrar);
                    txtProveedor.Enabled = false;
                    dtFechaCompra.Enabled = false;
                    txtRemito.Enabled = false;
                    txtCantidad.Clear();
                    txtValorUni.Clear();
                    txtObservaciones.Clear();
                    txtDescripcionProducto.Clear();
                    txtDescipcionBus.Focus();
                }
            }
            catch (Exception ex)
            { }
        }
        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                List<Stock> ListaStock = new List<Stock>();
                ListaStock = CargarEntidadFinal();
                bool Exito = StockNeg.CargarlistaStock(ListaStock);
                if (Exito == true)
                {
                    ProgressBar2();
                    const string message2 = "Se registro el stock exitosamente.";
                    const string caption2 = "Éxito";
                    var result2 = MessageBox.Show(message2, caption2,
                                                 MessageBoxButtons.OK,
                                                 MessageBoxIcon.Asterisk);
                    LimpiarCampos();
                }
            }
            catch (Exception ex)
            {
                throw new Exception();
            }
        }
        private void btnEditarProducto_Click(object sender, EventArgs e)
        {
            Funcion = 2;
            if (this.dgvStock.RowCount > 0)
            {
                PanelNuevoMaterial.Enabled = true;
                btnCrear.Enabled = true;
                idProductoSeleccionado = Convert.ToInt32(this.dgvStock.CurrentRow.Cells[0].Value);
                string TipoMedicion = ProductoDao.BuscarTipoMedicionPorIdProducto(idProductoSeleccionado);
                txtDescripcionProducto.Text = dgvStock.CurrentRow.Cells[1].Value.ToString();
                cmbTipoMedicion.Items.Clear();
                cmbTipoMedicion.Text = TipoMedicion;
                cmbTipoMedicion.Items.Add("KILOS");
                cmbTipoMedicion.Items.Add("UNIDAD");
                string TotalCaracteres = Convert.ToString(txtDescripcionProducto.Text.Length);
                lblContador.Visible = true;
                lblTotal.Visible = true;
                lblContador.Text = TotalCaracteres;
            }
            else
            {
                const string message2 = "Debe seleccionar un Material de la grilla.";
                const string caption2 = "Atención";
                var result2 = MessageBox.Show(message2, caption2,
                                             MessageBoxButtons.OK,
                                             MessageBoxIcon.Asterisk);
            }
        }
        #endregion
        #region Funciones
        public static int idProdcutoStatic;
        public static int Funcion = 0;
        public static int idProductoSeleccionado = 0;
        private void BuscarProveedores()
        {
            txtProveedor.AutoCompleteCustomSource = Clases_Maestras.ListarProveedores.Autocomplete();
            txtProveedor.AutoCompleteMode = AutoCompleteMode.Suggest;
            txtProveedor.AutoCompleteSource = AutoCompleteSource.CustomSource;

        }
        private void LimpiarCamposNuevoProducto()
        {
            txtDescripcionProducto.Clear();
            progressBar1.Value = Convert.ToInt32(null);
            progressBar1.Visible = false;
            txtDescripcionProducto.Focus();
            CargarCombo();
        }
        private void LimpiarCampos()
        {
            txtProveedor.Enabled = true;
            dtFechaCompra.Enabled = true;
            txtRemito.Enabled = true;
            txtDescipcionBus.Clear();
            txtMaterial.Clear();
            txtProveedor.Clear();
            dtFechaCompra.Value = DateTime.Now;
            txtRemito.Clear();
            txtCantidad.Clear();
            txtValorUni.Clear();
            dgvListaCargaStock.Rows.Clear();
            progressBar2.Value = Convert.ToInt32(null);
            progressBar2.Visible = false;
            txtMaterial.Focus();
            txtObservaciones.Clear();
            lblTotalPagarReal.Text = "0";
            dtFechaCompra.Enabled = true;
        }
        private Producto CargarEntidad()
        {
            Producto _producto = new Producto();
            int idusuarioLogueado = Sesion.UsuarioLogueado.idUsuario;
            _producto.DescripcionProducto = txtDescripcionProducto.Text;
            DateTime fechaActual = DateTime.Now;
            _producto.FechaDeAlta = fechaActual;
            _producto.idUsuario = idusuarioLogueado;
            _producto.TipoMedicion = cmbTipoMedicion.Text;
            return _producto;
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
        private void ProgressBar2()
        {
            progressBar2.Visible = true;
            progressBar2.Maximum = 100000;
            progressBar2.Step = 1;

            for (int j = 0; j < 100000; j++)
            {
                Caluculate(j);
                progressBar2.PerformStep();
            }
        }
        private void Caluculate2(int i)
        {
            double pow = Math.Pow(i, i);
        }
        private void txtDescripcionProducto_TextChanged(object sender, EventArgs e)
        {
            lblContador.Text = Convert.ToString(txtDescripcionProducto.Text.Length);
        }
        private void FuncionListarProductos()
        {
            FuncionBuscartexto();
            dgvStock.Rows.Clear();
            List<Producto> ListaProductos = ProductoNeg.ListaDeProductos();
            if (ListaProductos.Count > 0)
            {
                foreach (var item in ListaProductos)
                {
                    string cantidad = Convert.ToString(item.Stock);
                    dgvStock.Rows.Add(item.idProducto, item.DescripcionProducto, cantidad);
                }
                btnEditarProducto.Visible = true;
            }
            dgvStock.ReadOnly = true;
        }
        private void FuncionBuscartexto()
        {
            txtMaterial.AutoCompleteCustomSource = Clases_Maestras.AutoCompleteProductos.Autocomplete();
            txtMaterial.AutoCompleteMode = AutoCompleteMode.Suggest;
            txtMaterial.AutoCompleteSource = AutoCompleteSource.CustomSource;

            txtDescipcionBus.AutoCompleteCustomSource = Clases_Maestras.AutoCompleteProductos.Autocomplete();
            txtDescipcionBus.AutoCompleteMode = AutoCompleteMode.Suggest;
            txtDescipcionBus.AutoCompleteSource = AutoCompleteSource.CustomSource;
        }
        private Stock CargarEntidadRegistroStock()
        {
            Stock _producto = new Stock();
            _producto.idProducto = idProdcutoStatic;
            int idusuarioLogueado = Sesion.UsuarioLogueado.idUsuario;
            _producto.idUsuario = idusuarioLogueado;
            if (String.IsNullOrEmpty(txtProveedor.Text))
            {
                const string message = "El campo proveedor es obligatorio.";
                const string caption = "Error";
                var result = MessageBox.Show(message, caption,
                                             MessageBoxButtons.OK,
                                           MessageBoxIcon.Exclamation);
                throw new Exception();
            }
            else { _producto.Proveedor = txtProveedor.Text; }
            if (String.IsNullOrEmpty(txtMaterial.Text))
            {
                const string message = "El campo Material es obligatorio.";
                const string caption = "Error";
                var result = MessageBox.Show(message, caption,
                                             MessageBoxButtons.OK,
                                           MessageBoxIcon.Exclamation);
                throw new Exception();
            }
            else { _producto.Descripcion = txtMaterial.Text; }

            _producto.Remito = txtRemito.Text;
            _producto.FechaFactura = Convert.ToDateTime(dtFechaCompra.Text);


            if (String.IsNullOrEmpty(txtValorUni.Text))
            {
                const string message = "El campo Valor Unitario es obligatorio.";
                const string caption = "Error";
                var result = MessageBox.Show(message, caption,
                                             MessageBoxButtons.OK,
                                           MessageBoxIcon.Exclamation);
                throw new Exception();
            }
            else
            {
                string Valor = txtValorUni.Text;
                var temp = Valor.Replace(".", "<TEMP>");
                var temp2 = temp.Replace(",", ",");
                var replaced = temp2.Replace("<TEMP>", ",");
                _producto.ValorUnitario = Convert.ToDecimal(replaced);
            }
            if (String.IsNullOrEmpty(txtCantidad.Text))
            {
                const string message = "El campo Cantidad es obligatorio.";
                const string caption = "Error";
                var result = MessageBox.Show(message, caption,
                                             MessageBoxButtons.OK,
                                           MessageBoxIcon.Exclamation);
                throw new Exception();
            }
            else { _producto.Cantidad = Convert.ToInt32(txtCantidad.Text); }
            _producto.PrecioNeto = _producto.ValorUnitario * _producto.Cantidad;
            _producto.Observaciones = txtObservaciones.Text;
            return _producto;
        }
        private void dgvListaCargaStock_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                idProductoSeleccionado = Convert.ToInt32(dgvListaCargaStock.CurrentRow.Cells[0].Value.ToString());
                dgvListaCargaStock.Rows.Remove(dgvListaCargaStock.CurrentRow);
            }
        }
        private void txtMaterial_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                FuncionBuscartexto();
                dgvStock.Rows.Clear();
                List<Producto> ListaProductos = ProductoNeg.ListaProductoPorDescripcion(txtMaterial.Text);
                if (ListaProductos.Count > 0)
                {
                    foreach (var item in ListaProductos)
                    {
                        //string cantidad = Convert.ToString(item.Stock);
                        //dgvStock.Rows.Add(item.idProducto, item.DescripcionProducto, cantidad);
                        idProdcutoStatic = item.idProducto;
                    }
                    btnEditarProducto.Visible = true;
                }
                dgvStock.ReadOnly = true;
            }
        }
        private List<Stock> CargarEntidadFinal()
        {
            List<Stock> ListaStock = new List<Stock>();
            foreach (DataGridViewRow row in dgvListaCargaStock.Rows)
            {
                Stock Lista = new Stock();
                Lista.idProducto = Convert.ToInt32(row.Cells[0].Value.ToString());                
                int idusuarioLogueado = Sesion.UsuarioLogueado.idUsuario;
                Lista.idUsuario = idusuarioLogueado;
                Lista.CodigoProducto = row.Cells[1].Value.ToString();
                Lista.Proveedor = txtProveedor.Text;
                List<Proveedores> provId = new List<Proveedores>();
                provId = ProveedoresDao.BuscarProvedorPorNombre(Lista.Proveedor);
                Lista.idProveedor = provId[0].idProveedor;
                Lista.Remito = txtRemito.Text;
                Lista.Descripcion = row.Cells[2].Value.ToString();
                DateTime fecha = Convert.ToDateTime(dtFechaCompra.Text);
                Lista.FechaFactura = Convert.ToDateTime(fecha.ToShortDateString());
                Lista.PrecioNeto = Convert.ToDecimal(row.Cells[4].Value.ToString());
                Lista.ValorUnitario = Convert.ToDecimal(row.Cells[3].Value.ToString());
                Lista.Cantidad = Convert.ToInt32(row.Cells[2].Value.ToString());
                Lista.Observaciones = Convert.ToString(row.Cells[5].Value.ToString());
                ListaStock.Add(Lista);
            }
            return ListaStock;
        }
        private void txtDescipcionBus_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                FuncionBuscartexto();
                dgvStock.Rows.Clear();
                List<Producto> ListaProductos = ProductoNeg.BuscarProductoPorDescripcion(txtDescipcionBus.Text);
                if (ListaProductos.Count > 0)
                {
                    foreach (var item in ListaProductos)
                    {
                        string cantidad = Convert.ToString(item.Stock);
                        dgvStock.Rows.Add(item.idProducto, item.DescripcionProducto, cantidad);
                    }
                    btnEditarProducto.Visible = true;
                }
                dgvStock.ReadOnly = true;
            }
        }
        #endregion

        private void dgvStock_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //if (dgvStock.CurrentCell.ColumnIndex == 3)
            //{
            //    PanelDetalleStock.Visible = true;
            //    PanelNuevoMaterial.Visible = false;
            //    btnEditarProducto.Visible = false;
            //    lblNuevoProducto.Text = "Historial de stock seleccionado";
            //}
            if (dgvStock.CurrentCell.ColumnIndex == 4)
            {
                idProductoSeleccionado = Convert.ToInt32(this.dgvStock.CurrentRow.Cells[0].Value.ToString());
                string Material = dgvStock.CurrentRow.Cells[1].Value.ToString();
                InformeStockWF frm2 = new InformeStockWF(idProductoSeleccionado, Material);
                frm2.Show();
            }
        }
        private void dgvStock_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.ColumnIndex >= 0 && this.dgvStock.Columns[e.ColumnIndex].Name == "Ver" && e.RowIndex >= 0)
            {
                e.Paint(e.CellBounds, DataGridViewPaintParts.All);
                DataGridViewButtonCell BotonVer = this.dgvStock.Rows[e.RowIndex].Cells["Ver"] as DataGridViewButtonCell;
                Icon icoAtomico = new Icon(Environment.CurrentDirectory + "\\" + @"ver (3).ico");
                e.Graphics.DrawIcon(icoAtomico, e.CellBounds.Left + 20, e.CellBounds.Top + 4);
                this.dgvStock.Rows[e.RowIndex].Height = icoAtomico.Height + 8;
                this.dgvStock.Columns[e.ColumnIndex].Width = icoAtomico.Width + 40;
                e.Handled = true;
            }
            if (e.ColumnIndex >= 0 && this.dgvStock.Columns[e.ColumnIndex].Name == "Informe" && e.RowIndex >= 0)
            {
                e.Paint(e.CellBounds, DataGridViewPaintParts.All);
                DataGridViewButtonCell BotonVer = this.dgvStock.Rows[e.RowIndex].Cells["Informe"] as DataGridViewButtonCell;
                Icon icoAtomico = new Icon(Environment.CurrentDirectory + "\\" + @"informe-empresarial-con-crecimiento.ico");
                e.Graphics.DrawIcon(icoAtomico, e.CellBounds.Left + 20, e.CellBounds.Top + 4);
                this.dgvStock.Rows[e.RowIndex].Height = icoAtomico.Height + 8;
                this.dgvStock.Columns[e.ColumnIndex].Width = icoAtomico.Width + 40;
                e.Handled = true;
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            LimpiarCamposStock();
        }

        private void LimpiarCamposStock()
        {
            txtDescipcionBus.Clear();
            txtMaterial.Clear();
            txtProveedor.Clear();
            dtFechaCompra.Value = DateTime.Now;
            txtRemito.Clear();
            txtCantidad.Clear();
            txtValorUni.Clear();
            dgvListaCargaStock.Rows.Clear();
            progressBar1.Value = Convert.ToInt32(null);
            progressBar1.Visible = false;
            lblTotalPagarReal.Text = Convert.ToString(0);
            txtObservaciones.Clear();
            txtProveedor.Enabled = true;
            txtRemito.Enabled = true;
            dtFechaCompra.Enabled = true;
        }
        private void SoloNumerosyDecimales(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
            // solo 1 punto decimal
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
            //e.Handled = !char.IsNumber(e.KeyChar) && e.KeyChar != Convert.ToChar(Keys.Back);
        }
        private void SoloNumeros(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsNumber(e.KeyChar) && e.KeyChar != Convert.ToChar(Keys.Back);
        }
    }
}
