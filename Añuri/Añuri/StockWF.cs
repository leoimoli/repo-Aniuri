using Añuri.Clases_Maestras;
using Añuri.Entidades;
using Añuri.Negocio;
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
                        LimpiarCampos();
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
                    LimpiarCampos();
                }
            }
        }
        private void btnCrear_Click(object sender, EventArgs e)
        {
            PanelNuevoMaterial.Visible = true;
            PanelNuevoMaterial.Enabled = true;
        }
        private void btnCargar_Click(object sender, EventArgs e)
        {
            Stock Entidad = CargarEntidadRegistroStock();
            dgvListaCargaStock.Rows.Add(Entidad.idProducto, Entidad.Descripcion, Entidad.Cantidad, Entidad.ValorUnitario, Entidad.PrecioNeto);
            txtProveedor.Enabled = false;
            dtFechaCompra.Enabled = false;
            txtRemito.Enabled = false;
            txtCantidad.Clear();
            txtValorUni.Clear();
            txtDescripcionProducto.Clear();
            txtDescipcionBus.Focus();
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
        private void LimpiarCampos()
        {
            txtDescripcionProducto.Clear();
            progressBar1.Value = Convert.ToInt32(null);
            progressBar1.Visible = false;
        }
        private Producto CargarEntidad()
        {
            Producto _producto = new Producto();
            int idusuarioLogueado = Sesion.UsuarioLogueado.idUsuario;
            _producto.DescripcionProducto = txtDescripcionProducto.Text;
            DateTime fechaActual = DateTime.Now;
            _producto.FechaDeAlta = fechaActual;
            _producto.idUsuario = idusuarioLogueado;
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
            }
            dgvStock.ReadOnly = true;
        }
        private void FuncionBuscartexto()
        {
            txtMaterial.AutoCompleteCustomSource = Clases_Maestras.AutoCompleteProductos.Autocomplete();
            txtMaterial.AutoCompleteMode = AutoCompleteMode.Suggest;
            txtMaterial.AutoCompleteSource = AutoCompleteSource.CustomSource;
        }
        private Stock CargarEntidadRegistroStock()
        {
            Stock _producto = new Stock();
            _producto.idProducto = idProdcutoStatic;
            int idusuarioLogueado = Sesion.UsuarioLogueado.idUsuario;
            _producto.idUsuario = idusuarioLogueado;
            _producto.Proveedor = txtProveedor.Text;
            _producto.Remito = txtRemito.Text;
            _producto.Descripcion = txtMaterial.Text;
            _producto.FechaFactura = Convert.ToDateTime(dtFechaCompra.Text);
            string Valor = txtValorUni.Text;
            var temp = Valor.Replace(".", "<TEMP>");
            var temp2 = temp.Replace(",", ",");
            var replaced = temp2.Replace("<TEMP>", ",");
            _producto.ValorUnitario = Convert.ToDecimal(replaced);
            _producto.Cantidad = Convert.ToInt32(txtCantidad.Text);
            _producto.PrecioNeto = _producto.ValorUnitario * _producto.Cantidad;
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
                string Descripcion = txtMaterial.Text;
                List<Producto> ListaProductos = ProductoNeg.ListaProductoPorDescripcion(Descripcion);
                if (ListaProductos.Count > 0)
                {
                    foreach (var item in ListaProductos)
                    {
                        idProdcutoStatic = item.idProducto;
                    }

                    txtProveedor.Focus();
                    txtDescipcionBus.Clear();
                    txtProveedor.Enabled = true;
                    txtRemito.Enabled = true;
                    dtFechaCompra.Enabled = true;
                }
                else
                {
                    const string message = "El producto no existe. Desea agregar un nuevo producto ?";
                    const string caption = "Producto Inexistente";
                    var result = MessageBox.Show(message, caption,
                                                 MessageBoxButtons.YesNo,
                                                 MessageBoxIcon.Question);
                    {
                        if (result == DialogResult.Yes)
                        {
                        }
                    }
                    dgvListaCargaStock.ReadOnly = true;
                }
            }
        }
        #endregion

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                List<Stock> ListaStock = new List<Stock>();
                ListaStock = CargarEntidadFinal();
                bool Exito = StockNeg.CargarlistaStock(ListaStock);
                if (Exito == true)
                {
                    ProgressBar();
                    const string message2 = "Se registro el stock exitosamente.";
                    const string caption2 = "Éxito";
                    var result2 = MessageBox.Show(message2, caption2,
                                                 MessageBoxButtons.OK,
                                                 MessageBoxIcon.Asterisk);
                    LimpiarCampos();
                    const string message = "Desea adjuntar la factura de la compra?";
                    const string caption = "Consulta";
                    var result = MessageBox.Show(message, caption,
                                                 MessageBoxButtons.YesNo,
                                                 MessageBoxIcon.Question);
                    {
                        if (result == DialogResult.Yes)
                        {
                            ArchivosWF _archivo = new ArchivosWF();
                            _archivo.Show();
                        }
                        else
                        {

                        }

                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception();
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
                Lista.Remito = txtRemito.Text;
                Lista.Descripcion = row.Cells[2].Value.ToString();
                Lista.FechaFactura = Convert.ToDateTime(dtFechaCompra.Text);
                Lista.PrecioNeto = Convert.ToDecimal(row.Cells[4].Value.ToString());
                Lista.ValorUnitario = Convert.ToDecimal(row.Cells[3].Value.ToString());
                Lista.Cantidad = Convert.ToInt32(row.Cells[2].Value.ToString());
                ListaStock.Add(Lista);
            }
            return ListaStock;
        }
    }
}
