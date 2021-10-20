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
    public partial class InventarioMaterialesPesosWF : Form
    {
        public InventarioMaterialesPesosWF()
        {
            InitializeComponent();
        }

        private void btnInicio_Click(object sender, EventArgs e)
        {
            InventarioStockWF _inventario = new InventarioStockWF();
            _inventario.Show();
            Hide();
        }
        private void btnMaterialesKilos_Click(object sender, EventArgs e)
        {
            InventarioMaterialesKilosWF _inventario = new InventarioMaterialesKilosWF();
            _inventario.Show();
            Hide();
        }
        private void btnMaterialesEnPesos_Click(object sender, EventArgs e)
        {
            InventarioMaterialesPesosWF _inventario = new InventarioMaterialesPesosWF();
            _inventario.Show();
            Hide();
        }

        private void InventarioMaterialesKilosWF_Load(object sender, EventArgs e)
        {
            try
            {
                txtMateriales.AutoCompleteCustomSource = Clases_Maestras.AutoCompleteProductos.Autocomplete();
                txtMateriales.AutoCompleteMode = AutoCompleteMode.Suggest;
                txtMateriales.AutoCompleteSource = AutoCompleteSource.CustomSource;
                int anio = DateTime.Now.Year;
                txtAño.Text = Convert.ToString(anio);
            }
            catch (Exception ex)
            { }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                dgvInventario.Rows.Clear();
                string año = txtAño.Text;
                string Material = txtMateriales.Text;
                if (año == "" && Material == "")
                {
                    const string message = "Atención: Debe ingresar algun filtro de busqueda.";
                    const string caption = "Atención:";
                    var result = MessageBox.Show(message, caption,
                                                 MessageBoxButtons.OK,
                                               MessageBoxIcon.Exclamation);
                    throw new Exception();
                }
                if (año == "")
                {
                    const string message = "Atención: El campo año es obligatorio.";
                    const string caption = "Atención:";
                    var result = MessageBox.Show(message, caption,
                                                 MessageBoxButtons.OK,
                                               MessageBoxIcon.Exclamation);
                    throw new Exception();
                }
                else
                {
                    List<MesProducto> ListaProductoMes = new List<MesProducto>();
                    List<int> ListaId = new List<int>();
                    List<int> ListaMes = new List<int>();
                    decimal SumaEntrada = 0;
                    decimal SumaSalida = 0;
                    decimal Total = 0;
                    int Posicion = 0;
                    List<Stock> ListaStock = StockNeg.ListarInventarioMaterialesPesos(año, Material);
                    if (ListaStock.Count > 0)
                    {
                        btnExcel.Visible = true;
                        btnPdf.Visible = true;
                        //var lista = ListaStock.GroupBy(t => new {Mes = t.FechaFactura.Month, idProducto = t.idProducto,  });
                        List<Stock> lista = new List<Stock>();
                        var data = ListaStock.Select(k => new { k.FechaMovimiento.Month, k.idProducto, k.Descripcion, k.TipoMovimiento, k.PrecioNeto }).GroupBy(x => new { x.Month, x.idProducto, x.Descripcion, x.TipoMovimiento, x.PrecioNeto }, (key, group) => new
                        {

                            mes = key.Month,
                            id = key.idProducto,
                            nombre = key.Descripcion,
                            tipoMov = key.TipoMovimiento,
                            precio = key.PrecioNeto,
                            precioNeto = group.Sum(k => k.PrecioNeto)
                        }).ToList();

                        foreach (var item in data)
                        {
                            bool existe = ListaId.Any(x => x == item.id);
                            bool existeMes = ListaMes.Any(x => x == item.mes);
                            if (existe == true && existeMes == false)
                            {
                                MesProducto _Lista = new MesProducto();
                                _Lista.Monto = Total;
                                _Lista.idProducto = data[Posicion - 1].id;
                                _Lista.Producto = data[Posicion - 1].nombre;
                                string NombreMes = ObtenerMes(data[Posicion - 1].mes);
                                _Lista.Mes = data[Posicion - 1].mes;
                                _Lista.NombreMes = NombreMes;
                                ListaProductoMes.Add(_Lista);
                                SumaEntrada = 0;
                                SumaSalida = 0;
                                Total = 0;
                                //ListaMes.Clear();
                                ListaId.Add(item.id);
                                ListaMes.Add(item.mes);
                            }
                            if (existe == false)
                            {
                                if (Posicion > 1)
                                {
                                    bool YaExiste = ListaProductoMes.Any(x => x.idProducto == data[Posicion - 1].id && x.Mes == data[Posicion - 1].mes);
                                    if (YaExiste == false)
                                    {
                                        MesProducto _Lista = new MesProducto();
                                        _Lista.Monto = Total;
                                        _Lista.idProducto = data[Posicion - 1].id;
                                        _Lista.Producto = data[Posicion - 1].nombre;
                                        string NombreMes = ObtenerMes(data[Posicion - 1].mes);
                                        _Lista.Mes = data[Posicion - 1].mes;
                                        _Lista.NombreMes = NombreMes;
                                        ListaProductoMes.Add(_Lista);
                                    }

                                }
                                SumaEntrada = 0;
                                SumaSalida = 0;
                                Total = 0;
                                ListaMes.Clear();
                                ListaId.Add(item.id);
                                ListaMes.Add(item.mes);
                                if (item.tipoMov == "E")
                                {
                                    SumaEntrada = SumaEntrada + item.precioNeto;
                                }
                                if (item.tipoMov == "S")
                                {
                                    SumaSalida = SumaSalida + item.precioNeto;
                                }
                                Total = SumaEntrada - SumaSalida;
                            }
                            else
                            {
                                existeMes = ListaMes.Any(x => x == item.mes);
                                if (existeMes == false)
                                {
                                    SumaEntrada = 0;
                                    SumaSalida = 0;
                                    Total = 0;
                                    ListaMes.Add(item.mes);
                                    if (item.tipoMov == "E")
                                    {
                                        SumaEntrada = SumaEntrada + item.precioNeto;
                                    }
                                    if (item.tipoMov == "S")
                                    {
                                        SumaSalida = SumaSalida + item.precioNeto;
                                    }
                                    Total = SumaEntrada - SumaSalida;
                                }
                                else
                                {
                                    if (item.tipoMov == "E")
                                    {
                                        SumaEntrada = SumaEntrada + item.precioNeto;
                                    }
                                    if (item.tipoMov == "S")
                                    {
                                        SumaSalida = SumaSalida + item.precioNeto;
                                    }
                                    Total = SumaEntrada - SumaSalida;
                                }
                            }
                            Posicion = Posicion + 1;
                            if (Posicion == data.Count)
                            {
                                MesProducto _Lista = new MesProducto();
                                _Lista.Monto = Total;
                                _Lista.idProducto = data[Posicion - 1].id;
                                _Lista.Producto = data[Posicion - 1].nombre;
                                string NombreMes = ObtenerMes(data[Posicion - 1].mes);
                                _Lista.Mes = data[Posicion - 1].mes;
                                _Lista.NombreMes = NombreMes;
                                ListaProductoMes.Add(_Lista);
                            }
                        }
                        List<int> ListaProducto = new List<int>();
                        List<int> ListaidProducto = new List<int>();
                        int PosicionGrilla = 0;
                        int PosicionAsignadaEnGrilla = 0;
                        List<MesProducto> ListaProductoMesNueva = new List<MesProducto>();
                        
                        ////// Recorremos la lista existente y le cargamos los saldos de meses anteriores.
                        foreach (var item in ListaProductoMes)
                        {
                            bool ExisteProd = ListaProducto.Any(x => x == item.idProducto);
                            if (ExisteProd == false)
                            {
                                ListaProducto.Add(item.idProducto);
                            }
                            else
                            {
                                int Mes = item.Mes - 1;
                                var valor = ListaProductoMes.FirstOrDefault(x => x.Mes == Mes && x.idProducto == item.idProducto);

                                if (valor != null)
                                {
                                    item.Monto = item.Monto + valor.Monto;
                                }
                                else
                                {
                                    var ValorMasProximo = ListaProductoMes.FirstOrDefault(x => x.idProducto == item.idProducto);
                                    if (ValorMasProximo != null)
                                    {
                                        var diferencia = item.Mes - ValorMasProximo.Mes;
                                        for (int i = 1; i < diferencia; i++)
                                        {
                                            int MesDos = item.Mes - i;
                                            var valor2 = ListaProductoMes.FirstOrDefault(x => x.Mes == ValorMasProximo.Mes && x.idProducto == item.idProducto);
                                            if (valor2 != null)
                                            {
                                                MesProducto Lista = new MesProducto();
                                                string MesNuevo = ObtenerMes(MesDos);
                                                Lista.Mes = MesDos;
                                                Lista.NombreMes = MesNuevo;
                                                Lista.Monto = valor2.Monto;
                                                Lista.idProducto = valor2.idProducto;
                                                Lista.Producto = valor2.Producto;
                                                ListaProductoMesNueva.Add(Lista);
                                                item.Monto = item.Monto + valor2.Monto;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        ///// Le paso los nuevos valores a la lista ListaProductoMes
                        if (ListaProductoMesNueva.Count > 0)
                        {
                            foreach (var item in ListaProductoMesNueva)
                            {
                                MesProducto List = new MesProducto();
                                List.Mes = item.Mes;
                                List.NombreMes = item.NombreMes;
                                List.Monto = item.Monto;
                                List.idProducto = item.idProducto;
                                List.Producto = item.Producto;
                                ListaProductoMes.Add(List);
                            }
                        }
                        ////// Armamos el dataGridView con la informacion obtenida.                        
                        List<MesProducto> ListaProductoMes2 = ListaProductoMes.OrderBy(o => o.idProducto).ToList();
                        //List<MesProducto> ListaProductoMes3 = ListaProductoMes2.OrderBy(o => o.Mes).ToList();

                        ///// Le volvemos a pasar a la lista final la nueva lista ordenada por producto y mes.
                        ListaProductoMes = ListaProductoMes2;

                        foreach (var item in ListaProductoMes)
                        {
                            dgvInventario.Visible = true;
                            bool ExisteProd = ListaidProducto.Any(x => x == item.idProducto);
                            if (ExisteProd == false)
                            {
                                ListaidProducto.Add(item.idProducto);
                                dgvInventario.Rows.Add(item.idProducto, item.Producto);
                                if (item.NombreMes == "Enero")
                                {
                                    dgvInventario.Rows[PosicionGrilla].Cells["Enero"].Value = item.Monto;
                                }
                                if (item.NombreMes == "Febrero")
                                {
                                    dgvInventario.Rows[PosicionGrilla].Cells["Febrero"].Value = item.Monto;
                                }
                                if (item.NombreMes == "Marzo")
                                {
                                    dgvInventario.Rows[PosicionGrilla].Cells["Marzo"].Value = item.Monto;
                                }
                                if (item.NombreMes == "Abril")
                                {
                                    dgvInventario.Rows[PosicionGrilla].Cells["Abril"].Value = item.Monto;
                                }
                                if (item.NombreMes == "Mayo")
                                {
                                    dgvInventario.Rows[PosicionGrilla].Cells["Mayo"].Value = item.Monto;
                                }
                                if (item.NombreMes == "Junio")
                                {
                                    dgvInventario.Rows[PosicionGrilla].Cells["Junio"].Value = item.Monto;
                                }
                                if (item.NombreMes == "Julio")
                                {
                                    dgvInventario.Rows[PosicionGrilla].Cells["Julio"].Value = item.Monto;
                                }
                                if (item.NombreMes == "Agosto")
                                {
                                    dgvInventario.Rows[PosicionGrilla].Cells["Agosto"].Value = item.Monto;
                                }
                                if (item.NombreMes == "Septiembre")
                                {
                                    dgvInventario.Rows[PosicionGrilla].Cells["Septiembre"].Value = item.Monto;
                                }
                                if (item.NombreMes == "Octubre")
                                {
                                    dgvInventario.Rows[PosicionGrilla].Cells["Octubre"].Value = item.Monto;
                                }
                                if (item.NombreMes == "Noviembre")
                                {
                                    dgvInventario.Rows[PosicionGrilla].Cells["Noviembre"].Value = item.Monto;
                                }
                                if (item.NombreMes == "Diciembre")
                                {
                                    dgvInventario.Rows[PosicionGrilla].Cells["Diciembre"].Value = item.Monto;
                                }
                                PosicionGrilla = PosicionGrilla + 1;
                            }

                            else
                            {
                                foreach (DataGridViewRow Row in dgvInventario.Rows)
                                {
                                    for (int i = 0; i < dgvInventario.Rows.Count; i++)
                                    {
                                        string valorGrilla = Row.Cells["materiales"].Value.ToString();
                                        if (valorGrilla == item.Producto)
                                        {
                                            PosicionAsignadaEnGrilla = i;
                                        }
                                    }
                                }
                                if (item.NombreMes == "Enero")
                                {
                                    dgvInventario.Rows[PosicionAsignadaEnGrilla].Cells["Enero"].Value = item.Monto;
                                }
                                if (item.NombreMes == "Febrero")
                                {
                                    dgvInventario.Rows[PosicionAsignadaEnGrilla].Cells["Febrero"].Value = item.Monto;
                                }
                                if (item.NombreMes == "Marzo")
                                {
                                    dgvInventario.Rows[PosicionAsignadaEnGrilla].Cells["Marzo"].Value = item.Monto;
                                }
                                if (item.NombreMes == "Abril")
                                {
                                    dgvInventario.Rows[PosicionAsignadaEnGrilla].Cells["Abril"].Value = item.Monto;
                                }
                                if (item.NombreMes == "Mayo")
                                {
                                    dgvInventario.Rows[PosicionAsignadaEnGrilla].Cells["Mayo"].Value = item.Monto;
                                }
                                if (item.NombreMes == "Junio")
                                {
                                    dgvInventario.Rows[PosicionAsignadaEnGrilla].Cells["Junio"].Value = item.Monto;
                                }
                                if (item.NombreMes == "Julio")
                                {
                                    dgvInventario.Rows[PosicionAsignadaEnGrilla].Cells["Julio"].Value = item.Monto;
                                }
                                if (item.NombreMes == "Agosto")
                                {
                                    dgvInventario.Rows[PosicionAsignadaEnGrilla].Cells["Agosto"].Value = item.Monto;
                                }
                                if (item.NombreMes == "Septiembre")
                                {
                                    dgvInventario.Rows[PosicionAsignadaEnGrilla].Cells["Septiembre"].Value = item.Monto;
                                }
                                if (item.NombreMes == "Octubre")
                                {
                                    dgvInventario.Rows[PosicionAsignadaEnGrilla].Cells["Octubre"].Value = item.Monto;
                                }
                                if (item.NombreMes == "Noviembre")
                                {
                                    dgvInventario.Rows[PosicionAsignadaEnGrilla].Cells["Noviembre"].Value = item.Monto;
                                }
                                if (item.NombreMes == "Diciembre")
                                {
                                    dgvInventario.Rows[PosicionAsignadaEnGrilla].Cells["Diciembre"].Value = item.Monto;
                                }
                            }
                        }
                        ListaMontoMes = ListaProductoMes;
                    }
                    else
                    {
                        dgvInventario.Rows.Clear();
                        dgvInventario.Visible = false;
                        btnExcel.Visible = false;
                        btnPdf.Visible = false;
                    }
                }
            }
            catch (Exception ex)
            { }
        }
        public string ObtenerMes(int mes)
        {
            string NombreMes = "";
            if (mes == 1)
            {
                NombreMes = "Enero";
            }
            if (mes == 2)
            {
                NombreMes = "Febrero";
            }
            if (mes == 3)
            {
                NombreMes = "Marzo";
            }
            if (mes == 4)
            {
                NombreMes = "Abril";
            }
            if (mes == 5)
            {
                NombreMes = "Mayo";
            }
            if (mes == 6)
            {
                NombreMes = "Junio";
            }
            if (mes == 7)
            {
                NombreMes = "Julio";
            }
            if (mes == 8)
            {
                NombreMes = "Agosto";
            }
            if (mes == 9)
            {
                NombreMes = "Septiembre";
            }
            if (mes == 10)
            {
                NombreMes = "Octubre";
            }
            if (mes == 11)
            {
                NombreMes = "Noviembre";
            }
            if (mes == 12)
            {
                NombreMes = "Diciembre";
            }
            return NombreMes;
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            Close();
        }

        private static List<MesProducto> ListaMontoMes;
        private void dgvInventario_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewColumn currentColumn = dgvInventario.Columns[e.ColumnIndex];
            if (currentColumn.Name == "Septiembre")
            {
                DataGridViewRow currentRow = dgvInventario.Rows[e.RowIndex];
                DataRowView data = currentRow.DataBoundItem as DataRowView;

                if (data == null)
                    return;

                if (Convert.ToBoolean(data["fracc"]))
                    currentRow.Cells["tipoventa"].Value = Convert.ToString(data["FORM_VENT"]);
                else
                    currentRow.Cells["tipoventa"].Value = Convert.ToString(data["PRESENTACI"]);

            }
        }
        private void btnExcel_Click(object sender, EventArgs e)
        {
            ProgressBar();
            dgvInventario.ClipboardCopyMode = DataGridViewClipboardCopyMode.EnableAlwaysIncludeHeaderText;
            dgvInventario.MultiSelect = true;
            dgvInventario.SelectAll();
            DataObject dataObj = dgvInventario.GetClipboardContent();
            if (dataObj != null)
                Clipboard.SetDataObject(dataObj);
            //Open an excel instance and paste the copied data
            Microsoft.Office.Interop.Excel.Application xlexcel;
            Microsoft.Office.Interop.Excel.Workbook xlWorkBook;
            Microsoft.Office.Interop.Excel.Worksheet xlWorkSheet;
            object misValue = System.Reflection.Missing.Value;
            xlexcel = new Microsoft.Office.Interop.Excel.Application();
            xlexcel.Visible = true;
            xlWorkBook = xlexcel.Workbooks.Add(misValue);
            xlWorkSheet = (Microsoft.Office.Interop.Excel.Worksheet)xlWorkBook.Worksheets.get_Item(1);
            Microsoft.Office.Interop.Excel.Range CR = (Microsoft.Office.Interop.Excel.Range)xlWorkSheet.Cells[1, 1];
            CR.Select();
            xlWorkSheet.PasteSpecial(CR, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, true);
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
    }
}
