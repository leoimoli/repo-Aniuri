using Añuri.Entidades;
using Añuri.Negocio;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Sico;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
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
                    List<SaldoInicialEnPesos> SaldoInicial = new List<SaldoInicialEnPesos>();
                    List<int> ListaId = new List<int>();
                    List<int> ListaMes = new List<int>();
                    decimal SumaEntrada = 0;
                    decimal SumaSalida = 0;
                    decimal Total = 0;
                    int Posicion = 0;
                    List<Stock> ListaStock = StockNeg.ListarInventarioMaterialesPesos(año, Material);
                    List<Stock> ListaStockSaldoInicial = StockNeg.ListarSaldoInicialInventarioMaterialesPesos(año, Material);
                    if (ListaStockSaldoInicial.Count > 0)
                    {
                        SaldoInicial = CalcularSaldoInicial(ListaStockSaldoInicial);
                    }

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
                        List<int> listaMeses = new List<int>();
                        if (SaldoInicial.Count > 0)
                        {
                            foreach (var item in SaldoInicial)
                            {
                                foreach (var itemLista in ListaProductoMes)
                                {
                                    if (item.idProducto == itemLista.idProducto)
                                    {
                                        itemLista.SaldoInicial = item.Saldo;
                                    }
                                }
                            }
                        }

                        ////// Recorremos la lista existente y le cargamos los saldos de meses anteriores.
                        foreach (var item in ListaProductoMes)
                        {
                            bool ExisteProd = ListaProducto.Any(x => x == item.idProducto);
                            if (ExisteProd == false)
                            {
                                listaMeses.Clear();
                                ListaProducto.Add(item.idProducto);
                                listaMeses.Add(item.Mes);
                                item.Monto = item.Monto + item.SaldoInicial;
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
                                    listaMeses = listaMeses.OrderBy(o => o.ToString()).ToList();
                                    var UltimoMes = listaMeses.LastOrDefault();
                                    var ValorMasProximo = ListaProductoMes.FirstOrDefault(x => x.idProducto == item.idProducto && x.Mes == UltimoMes);
                                    if (ValorMasProximo != null)
                                    {
                                        var diferencia = item.Mes - ValorMasProximo.Mes;

                                        bool existeMesEnLista = listaMeses.Any(x => x == item.Mes);
                                        if (existeMesEnLista == false)
                                        {
                                            listaMeses.Add(item.Mes);
                                        }
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
                                                listaMeses.Add(MesDos);
                                                if (item.Mes - MesDos == 1)
                                                { item.Monto = item.Monto + valor2.Monto; }
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

                        ///// Le volvemos a pasar a la lista final la nueva lista ordenada por producto y mes.
                        ListaProductoMes = ListaProductoMes2;
                        int contador = 0;
                        List<int> listaMes = new List<int>();
                        foreach (var item in ListaProductoMes)
                        {
                            dgvInventario.Visible = true;
                            bool ExisteProd = ListaidProducto.Any(x => x == item.idProducto);
                            if (ExisteProd == false)
                            {                                
                                contador = contador + 1;
                                if (listaMes.Count > 0)
                                {                                  
                                    if (listaMes.Count == 1)
                                    {
                                        int MesMayor = listaMes.Max();
                                        int idProd = ListaidProducto.Last();
                                        var valorMes = ListaProductoMes.FirstOrDefault(x => x.Mes == MesMayor && x.idProducto == idProd);
                                        if (MesMayor > 0)
                                        { ReformularGrilla(MesMayor, PosicionAsignadaEnGrilla + 1, valorMes.Monto); }
                                    }
                                    //else
                                    //{
                                    //    int MesMayor = listaMes.Max();
                                    //    int idProd = ListaidProducto.Last();
                                    //    var valorMes = ListaProductoMes.FirstOrDefault(x => x.Mes == MesMayor && x.idProducto == idProd);
                                    //    if (MesMayor > 0)
                                    //    { ReformularGrilla(MesMayor, PosicionAsignadaEnGrilla, valorMes.Monto); }
                                    //}
                                }
                                listaMes.Clear();
                                listaMes.Add(item.Mes);
                                ListaidProducto.Add(item.idProducto);
                                dgvInventario.Rows.Add(item.idProducto, item.Producto);
                                dgvInventario.Rows[PosicionGrilla].Cells["SaldoInicial"].Value = item.SaldoInicial;
                                if (item.NombreMes == "Enero")
                                {
                                    dgvInventario.Rows[PosicionGrilla].Cells["Enero"].Value = item.Monto;
                                }
                                else
                                {
                                    dgvInventario.Rows[PosicionGrilla].Cells["Enero"].Value = dgvInventario.Rows[PosicionGrilla].Cells["SaldoInicial"].Value;
                                }
                                if (item.NombreMes == "Febrero")
                                {
                                    dgvInventario.Rows[PosicionGrilla].Cells["Febrero"].Value = item.Monto;
                                }
                                else
                                {
                                    dgvInventario.Rows[PosicionGrilla].Cells["Febrero"].Value = dgvInventario.Rows[PosicionGrilla].Cells["SaldoInicial"].Value;
                                }
                                if (item.NombreMes == "Marzo")
                                {
                                    dgvInventario.Rows[PosicionGrilla].Cells["Marzo"].Value = item.Monto;
                                }
                                else
                                {
                                    dgvInventario.Rows[PosicionGrilla].Cells["Marzo"].Value = dgvInventario.Rows[PosicionGrilla].Cells["SaldoInicial"].Value;
                                }
                                if (item.NombreMes == "Abril")
                                {
                                    dgvInventario.Rows[PosicionGrilla].Cells["Abril"].Value = item.Monto;
                                }
                                else
                                {
                                    dgvInventario.Rows[PosicionGrilla].Cells["Abril"].Value = dgvInventario.Rows[PosicionGrilla].Cells["SaldoInicial"].Value;
                                }
                                if (item.NombreMes == "Mayo")
                                {
                                    dgvInventario.Rows[PosicionGrilla].Cells["Mayo"].Value = item.Monto;
                                }
                                else
                                {
                                    dgvInventario.Rows[PosicionGrilla].Cells["Mayo"].Value = dgvInventario.Rows[PosicionGrilla].Cells["SaldoInicial"].Value;
                                }
                                if (item.NombreMes == "Junio")
                                {
                                    dgvInventario.Rows[PosicionGrilla].Cells["Junio"].Value = item.Monto;
                                }
                                else
                                {
                                    dgvInventario.Rows[PosicionGrilla].Cells["Junio"].Value = dgvInventario.Rows[PosicionGrilla].Cells["SaldoInicial"].Value;
                                }
                                if (item.NombreMes == "Julio")
                                {
                                    dgvInventario.Rows[PosicionGrilla].Cells["Julio"].Value = item.Monto;
                                }
                                else
                                {
                                    dgvInventario.Rows[PosicionGrilla].Cells["Julio"].Value = dgvInventario.Rows[PosicionGrilla].Cells["SaldoInicial"].Value;
                                }
                                if (item.NombreMes == "Agosto")
                                {
                                    dgvInventario.Rows[PosicionGrilla].Cells["Agosto"].Value = item.Monto;
                                }
                                else
                                {
                                    dgvInventario.Rows[PosicionGrilla].Cells["Agosto"].Value = dgvInventario.Rows[PosicionGrilla].Cells["SaldoInicial"].Value;
                                }
                                if (item.NombreMes == "Septiembre")
                                {
                                    dgvInventario.Rows[PosicionGrilla].Cells["Septiembre"].Value = item.Monto;
                                }
                                else
                                {
                                    dgvInventario.Rows[PosicionGrilla].Cells["Septiembre"].Value = dgvInventario.Rows[PosicionGrilla].Cells["SaldoInicial"].Value;
                                }
                                if (item.NombreMes == "Octubre")
                                {
                                    dgvInventario.Rows[PosicionGrilla].Cells["Octubre"].Value = item.Monto;
                                }
                                else
                                {
                                    dgvInventario.Rows[PosicionGrilla].Cells["Octubre"].Value = dgvInventario.Rows[PosicionGrilla].Cells["SaldoInicial"].Value;
                                }
                                if (item.NombreMes == "Noviembre")
                                {
                                    dgvInventario.Rows[PosicionGrilla].Cells["Noviembre"].Value = item.Monto;
                                }
                                else
                                {
                                    dgvInventario.Rows[PosicionGrilla].Cells["Noviembre"].Value = dgvInventario.Rows[PosicionGrilla].Cells["SaldoInicial"].Value;
                                }
                                if (item.NombreMes == "Diciembre")
                                {
                                    dgvInventario.Rows[PosicionGrilla].Cells["Diciembre"].Value = item.Monto;
                                }
                                else
                                {
                                    dgvInventario.Rows[PosicionGrilla].Cells["Diciembre"].Value = dgvInventario.Rows[PosicionGrilla].Cells["SaldoInicial"].Value;
                                }
                                PosicionGrilla = PosicionGrilla + 1;
                            }
                            else
                            {
                                listaMes.Add(item.Mes);
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
                                contador = contador + 1;
                            }
                        }
                        if (contador == ListaProductoMes.Count)
                        {
                            if (listaMes.Count > 0)
                            {
                                PosicionAsignadaEnGrilla = 0;
                                foreach (var item in ListaidProducto)
                                {                                  
                                    //var info = ListaProductoMes.Where(n => n.idProducto == item);
                                    var info1 = ListaProductoMes.Where(n => n.idProducto == item).ToList();
                                    var info2 = info1.OrderByDescending(x => x.Mes).ToList().First();
                                    int MesMayor = info2.Mes;
                                    int idProd = info2.idProducto;
                                    var valorMes = ListaProductoMes.FirstOrDefault(x => x.Mes == MesMayor && x.idProducto == idProd);
                                    if (MesMayor > 0)
                                    {
                                        ReformularGrilla(MesMayor, PosicionAsignadaEnGrilla, valorMes.Monto);
                                        PosicionAsignadaEnGrilla = PosicionAsignadaEnGrilla + 1;
                                    }
                                }
                                //int MesMayor = listaMes.Max();
                                //int idProd = ListaidProducto.Last();
                                //var valorMes = ListaProductoMes.FirstOrDefault(x => x.Mes == MesMayor && x.idProducto == idProd);
                                //if (MesMayor > 0)
                                //{ ReformularGrilla(MesMayor, PosicionAsignadaEnGrilla, valorMes.Monto); }
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
                        const string message = "No se encontro información para mostrar con los parametros de busqueda ingresados..";
                        const string caption = "Atención:";
                        var result = MessageBox.Show(message, caption,
                                                     MessageBoxButtons.OK,
                                                   MessageBoxIcon.Exclamation);
                        throw new Exception();
                    }
                }
            }
            catch (Exception ex)
            { }
        }

        private void ReformularGrilla(int MesMayor, int posicionEnGrilla, decimal monto)
        {
            if (1 > MesMayor)
            {
                dgvInventario.Rows[posicionEnGrilla].Cells["Enero"].Value = monto;
            }
            if (2 > MesMayor)
            {
                dgvInventario.Rows[posicionEnGrilla].Cells["Febrero"].Value = monto;
            }
            if (3 > MesMayor)
            {
                dgvInventario.Rows[posicionEnGrilla].Cells["Marzo"].Value = monto;
            }
            if (4 > MesMayor)
            {
                dgvInventario.Rows[posicionEnGrilla].Cells["Abril"].Value = monto;
            }
            if (5 > MesMayor)
            {
                dgvInventario.Rows[posicionEnGrilla].Cells["Mayo"].Value = monto;
            }
            if (6 > MesMayor)
            {
                dgvInventario.Rows[posicionEnGrilla].Cells["Junio"].Value = monto;
            }
            if (7 > MesMayor)
            {
                dgvInventario.Rows[posicionEnGrilla].Cells["Julio"].Value = monto;
            }
            if (8 > MesMayor)
            {
                dgvInventario.Rows[posicionEnGrilla].Cells["Agosto"].Value = monto;
            }
            if (9 > MesMayor)
            {
                dgvInventario.Rows[posicionEnGrilla].Cells["Septiembre"].Value = monto;
            }
            if (10 > MesMayor)
            {
                dgvInventario.Rows[posicionEnGrilla].Cells["Octubre"].Value = monto;
            }
            if (11 > MesMayor)
            {
                dgvInventario.Rows[posicionEnGrilla].Cells["Noviembre"].Value = monto;
            }
            if (12 > MesMayor)
            {
                dgvInventario.Rows[posicionEnGrilla].Cells["Diciembre"].Value = monto;
            }
        }

        private List<SaldoInicialEnPesos> CalcularSaldoInicial(List<Stock> listaStockSaldoInicial)
        {
            decimal SumaEntrada = 0;
            decimal SumaSalida = 0;
            decimal Total = 0;
            List<int> ListaIdProducto = new List<int>();
            List<SaldoInicialEnPesos> ListaSaldo = new List<SaldoInicialEnPesos>();
            int Contador = 0;
            foreach (var item in listaStockSaldoInicial)
            {
                bool existeEnLista = ListaIdProducto.Any(x => x == item.idProducto);
                if (existeEnLista == false)
                {
                    Contador = Contador + 1;
                    if (Total > 0)
                    {
                        SaldoInicialEnPesos lista = new SaldoInicialEnPesos();
                        lista.idProducto = ListaIdProducto.Last();
                        lista.Saldo = Total;
                        ListaSaldo.Add(lista);
                    }
                    ListaIdProducto.Add(item.idProducto);
                    SumaEntrada = 0;
                    SumaSalida = 0;
                    //Total = 0;
                    if (item.TipoMovimiento == "E")
                    {
                        SumaEntrada = SumaEntrada + item.PrecioNeto;
                    }
                    if (item.TipoMovimiento == "S")
                    {
                        SumaSalida = SumaSalida + item.PrecioNeto;
                    }
                    Total = SumaEntrada - SumaSalida;
                }
                else
                {
                    Contador = Contador + 1;
                    if (item.TipoMovimiento == "E")
                    {
                        SumaEntrada = SumaEntrada + item.PrecioNeto;
                    }
                    if (item.TipoMovimiento == "S")
                    {
                        SumaSalida = SumaSalida + item.PrecioNeto;
                    }
                    Total = SumaEntrada - SumaSalida;
                    if (Contador == listaStockSaldoInicial.Count)
                    {
                        SaldoInicialEnPesos lista = new SaldoInicialEnPesos();
                        lista.idProducto = item.idProducto;
                        lista.Saldo = Total;
                        ListaSaldo.Add(lista);
                    }
                }
                if (Contador == listaStockSaldoInicial.Count)
                {
                    SaldoInicialEnPesos lista = new SaldoInicialEnPesos();
                    lista.idProducto = ListaIdProducto.Last();
                    lista.Saldo = Total;
                    ListaSaldo.Add(lista);
                }
            }
            if (listaStockSaldoInicial.Count == 1 && Total > 0)
            {
                SaldoInicialEnPesos lista = new SaldoInicialEnPesos();
                lista.idProducto = listaStockSaldoInicial[0].idProducto;
                lista.Saldo = Total;
                ListaSaldo.Add(lista);
            }
            return ListaSaldo;
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
        private void btnPdf_Click(object sender, EventArgs e)
        {
            MemoryStream m = new MemoryStream();
            Document doc = new Document(PageSize.LETTER);

            string folderPath = "C:\\Añuri-Archivos\\PDFs\\Reporte-Inventario\\";
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }
            string replaceWith = "";
            //material = material.Replace("\r\n", replaceWith).Replace("\n", replaceWith).Replace("\r", replaceWith);
            string ruta = folderPath;
            //string Periodo = "Reporte de Obra";
            string fecha = txtAño.Text;
            fecha = fecha.Replace("/", "-");
            PdfWriter writer = PdfWriter.GetInstance(doc,
                                        new FileStream(ruta + "Año " + fecha + " Reporte Inventario en pesos" + ".pdf", FileMode.Create));
            writer.PageEvent = new PDF();

            doc.AddTitle("PDF");
            doc.AddCreator("jliCode");

            // Abrimos el archivo
            doc.Open();
            // Creamos el tipo de Font que vamos utilizar
            iTextSharp.text.Font _standardFont = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 7, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);
            iTextSharp.text.Font letraContenido = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 5, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);
            iTextSharp.text.Font UltimoRegistro = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 5, iTextSharp.text.Font.NORMAL, BaseColor.BLUE);
            iTextSharp.text.Font DomicilioFontMenos30 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 7, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);
            iTextSharp.text.Font DomicilioFont30a40 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 7, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);
            iTextSharp.text.Font DomicilioFontHasta40a50 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 6, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);
            iTextSharp.text.Font DomicilioFontHasta50a60 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 5, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);
            // Escribimos el encabezamiento en el documento
            string TextoInicial = "Año " + fecha + " Reporte inventario en Pesos";


            doc.Add(new Paragraph(" "));

            Paragraph p1 = new Paragraph(new Chunk(TextoInicial));
            p1.Alignment = Element.ALIGN_LEFT;
            doc.Add(new Paragraph(p1));
            doc.Add(new Paragraph(" "));



            // Creamos una tabla que contendrá las cabeceras
            // de nuestros visitante.
            PdfPTable tblPrueba = new PdfPTable(14);
            tblPrueba.WidthPercentage = 110;

            // Configuramos el título de las columnas de la tabla
            PdfPCell clMaterial = new PdfPCell(new Phrase("Material", _standardFont));
            clMaterial.BorderWidth = 0;
            clMaterial.BorderWidthBottom = 0.50f;
            clMaterial.BorderWidthLeft = 0.50f;
            clMaterial.BorderWidthRight = 0.50f;
            clMaterial.BorderWidthTop = 0.50f;

            PdfPCell clSaldoInicial = new PdfPCell(new Phrase("Saldo Inicial", _standardFont));
            clSaldoInicial.BorderWidth = 0;
            clSaldoInicial.BorderWidthBottom = 0.50f;
            clSaldoInicial.BorderWidthLeft = 0.50f;
            clSaldoInicial.BorderWidthRight = 0.50f;
            clSaldoInicial.BorderWidthTop = 0.50f;

            PdfPCell clEnero = new PdfPCell(new Phrase("Enero", _standardFont));
            clEnero.BorderWidth = 0;
            clEnero.BorderWidthBottom = 0.50f;
            clEnero.BorderWidthLeft = 0.50f;
            clEnero.BorderWidthRight = 0.50f;
            clEnero.BorderWidthTop = 0.50f;

            PdfPCell clFebrero = new PdfPCell(new Phrase("Febrero", _standardFont));
            clFebrero.BorderWidth = 0;
            clFebrero.BorderWidthBottom = 0.50f;
            clFebrero.BorderWidthLeft = 0.50f;
            clFebrero.BorderWidthRight = 0.50f;
            clFebrero.BorderWidthTop = 0.50f;

            PdfPCell clMarzo = new PdfPCell(new Phrase("Marzo", _standardFont));
            clMarzo.BorderWidth = 0;
            clMarzo.BorderWidthBottom = 0.50f;
            clMarzo.BorderWidthLeft = 0.50f;
            clMarzo.BorderWidthRight = 0.50f;
            clMarzo.BorderWidthTop = 0.50f;


            PdfPCell clAbril = new PdfPCell(new Phrase("Abril", _standardFont));
            clAbril.BorderWidth = 0;
            clAbril.BorderWidthBottom = 0.50f;
            clAbril.BorderWidthLeft = 0.50f;
            clAbril.BorderWidthRight = 0.50f;
            clAbril.BorderWidthTop = 0.50f;

            PdfPCell clMayo = new PdfPCell(new Phrase("Mayo", _standardFont));
            clMayo.BorderWidth = 0;
            clMayo.BorderWidthBottom = 0.50f;
            clMayo.BorderWidthLeft = 0.50f;
            clMayo.BorderWidthRight = 0.50f;
            clMayo.BorderWidthTop = 0.50f;

            PdfPCell clJunio = new PdfPCell(new Phrase("Junio", _standardFont));
            clJunio.BorderWidth = 0;
            clJunio.BorderWidthBottom = 0.50f;
            clJunio.BorderWidthLeft = 0.50f;
            clJunio.BorderWidthRight = 0.50f;
            clJunio.BorderWidthTop = 0.50f;

            PdfPCell clJulio = new PdfPCell(new Phrase("Julio", _standardFont));
            clJulio.BorderWidth = 0;
            clJulio.BorderWidthBottom = 0.50f;
            clJulio.BorderWidthLeft = 0.50f;
            clJulio.BorderWidthRight = 0.50f;
            clJulio.BorderWidthTop = 0.50f;

            PdfPCell clAgosto = new PdfPCell(new Phrase("Agosto", _standardFont));
            clAgosto.BorderWidth = 0;
            clAgosto.BorderWidthBottom = 0.50f;
            clAgosto.BorderWidthLeft = 0.50f;
            clAgosto.BorderWidthRight = 0.50f;
            clAgosto.BorderWidthTop = 0.50f;

            PdfPCell clSeptiembre = new PdfPCell(new Phrase("Septiembre", _standardFont));
            clSeptiembre.BorderWidth = 0;
            clSeptiembre.BorderWidthBottom = 0.50f;
            clSeptiembre.BorderWidthLeft = 0.50f;
            clSeptiembre.BorderWidthRight = 0.50f;
            clSeptiembre.BorderWidthTop = 0.50f;

            PdfPCell clOctubre = new PdfPCell(new Phrase("Octubre", _standardFont));
            clOctubre.BorderWidth = 0;
            clOctubre.BorderWidthBottom = 0.50f;
            clOctubre.BorderWidthLeft = 0.50f;
            clOctubre.BorderWidthRight = 0.50f;
            clOctubre.BorderWidthTop = 0.50f;

            PdfPCell clNoviembre = new PdfPCell(new Phrase("Noviembre", _standardFont));
            clNoviembre.BorderWidth = 0;
            clNoviembre.BorderWidthBottom = 0.50f;
            clNoviembre.BorderWidthLeft = 0.50f;
            clNoviembre.BorderWidthRight = 0.50f;
            clNoviembre.BorderWidthTop = 0.50f;

            PdfPCell clDiciembre = new PdfPCell(new Phrase("Diciembre", _standardFont));
            clDiciembre.BorderWidth = 0;
            clDiciembre.BorderWidthBottom = 0.50f;
            clDiciembre.BorderWidthLeft = 0.50f;
            clDiciembre.BorderWidthRight = 0.50f;
            clDiciembre.BorderWidthTop = 0.50f;


            // Añadimos las celdas a la tabla
            tblPrueba.AddCell(clMaterial);
            tblPrueba.AddCell(clSaldoInicial);
            tblPrueba.AddCell(clEnero);
            tblPrueba.AddCell(clFebrero);
            tblPrueba.AddCell(clMarzo);
            tblPrueba.AddCell(clAbril);
            tblPrueba.AddCell(clMayo);
            tblPrueba.AddCell(clJunio);
            tblPrueba.AddCell(clJulio);
            tblPrueba.AddCell(clAgosto);
            tblPrueba.AddCell(clSeptiembre);
            tblPrueba.AddCell(clOctubre);
            tblPrueba.AddCell(clNoviembre);
            tblPrueba.AddCell(clDiciembre);


            // Llenamos la tabla con información
            string Descripcion = "";
            decimal ImporteTotal = 0;
            string StockTotal = "";
            List<string> ListaDescripcion = new List<string>();
            int TotalDeElementos = ListaMontoMes.Count;
            int Contador = 0;
            int Contador2 = 0;
            List<MaterialMesMonto> ListaPdfFinal = new List<MaterialMesMonto>();
            List<int> ListaIdProducto = new List<int>();
            List<MaterialMesMonto> ListaIdProductoAUX = new List<MaterialMesMonto>();
            MaterialMesMonto lista = new MaterialMesMonto();
            foreach (var item in ListaMontoMes)
            {
                bool ExisteProd = ListaIdProducto.Any(x => x == item.idProducto);
                if (ExisteProd == false)
                {
                    MaterialMesMonto lista2 = new MaterialMesMonto();
                    ListaIdProducto.Add(item.idProducto);
                    lista2.Producto = item.Producto;
                    lista2.idProducto = item.idProducto;

                    lista2.SaldoInicial = item.SaldoInicial;
                    if (item.Mes == 1)
                    {
                        lista2.Enero = item.Monto;
                    }
                    if (item.Mes == 2)
                    {
                        lista2.Febrero = item.Monto;
                    }
                    if (item.Mes == 3)
                    {
                        lista2.Marzo = item.Monto;
                    }
                    if (item.Mes == 3)
                    {
                        lista2.Abril = item.Monto;
                    }
                    if (item.Mes == 5)
                    {
                        lista2.Mayo = item.Monto;
                    }
                    if (item.Mes == 6)
                    {
                        lista2.Junio = item.Monto;
                    }
                    if (item.Mes == 7)
                    {
                        lista2.Julio = item.Monto;
                    }
                    if (item.Mes == 8)
                    {
                        lista2.Agosto = item.Monto;
                    }
                    if (item.Mes == 9)
                    {
                        lista2.Septiembre = item.Monto;
                    }
                    if (item.Mes == 10)
                    {
                        lista2.Octubre = item.Monto;
                    }
                    if (item.Mes == 11)
                    {
                        lista2.Noviembre = item.Monto;
                    }
                    if (item.Mes == 12)
                    {
                        lista2.Diciembre = item.Monto;
                    }

                    lista = lista2;
                }
                else
                {
                    //lista.SaldoInicial = item.SaldoInicial;
                    lista.Producto = item.Producto;
                    lista.idProducto = item.idProducto;
                    if (item.Mes == 1)
                    {
                        lista.Enero = item.Monto;
                    }
                    if (item.Mes == 2)
                    {
                        lista.Febrero = item.Monto;
                    }
                    if (item.Mes == 3)
                    {
                        lista.Marzo = item.Monto;
                    }
                    if (item.Mes == 3)
                    {
                        lista.Abril = item.Monto;
                    }
                    if (item.Mes == 5)
                    {
                        lista.Mayo = item.Monto;
                    }
                    if (item.Mes == 6)
                    {
                        lista.Junio = item.Monto;
                    }
                    if (item.Mes == 7)
                    {
                        lista.Julio = item.Monto;
                    }
                    if (item.Mes == 8)
                    {
                        lista.Agosto = item.Monto;
                    }
                    if (item.Mes == 9)
                    {
                        lista.Septiembre = item.Monto;
                    }
                    if (item.Mes == 10)
                    {
                        lista.Octubre = item.Monto;
                    }
                    if (item.Mes == 11)
                    {
                        lista.Noviembre = item.Monto;
                    }
                    if (item.Mes == 12)
                    {
                        lista.Diciembre = item.Monto;
                    }
                }
                ListaPdfFinal.Add(lista);
                ListaIdProductoAUX = ListaPdfFinal;
            }
            ListaIdProducto.Clear();
            foreach (var item in ListaPdfFinal)
            {
                bool ExisteProd = ListaIdProducto.Any(x => x == item.idProducto);
                if (ExisteProd == false)
                {
                    ListaIdProducto.Add(item.idProducto);
                    clMaterial = new PdfPCell(new Phrase(item.Producto, letraContenido));
                    clMaterial.BorderWidth = 0;

                    string SaldoInicial = Convert.ToString(item.SaldoInicial);
                    clSaldoInicial = new PdfPCell(new Phrase(SaldoInicial, letraContenido));
                    clSaldoInicial.BorderWidth = 0;
                    string SiguienteValor = "";

                    string Enero = Convert.ToString(item.Enero);
                    if (Enero == "0")
                    {
                        clEnero = new PdfPCell(new Phrase(SaldoInicial, letraContenido));
                        clEnero.BorderWidth = 0;
                        SiguienteValor = SaldoInicial;
                    }
                    else
                    {
                        clEnero = new PdfPCell(new Phrase(Enero, letraContenido));
                        clEnero.BorderWidth = 0;
                        SiguienteValor = Enero;
                    }
                    string Febrero = Convert.ToString(item.Febrero);
                    if (Febrero == "0")
                    {
                        clFebrero = new PdfPCell(new Phrase(SiguienteValor, letraContenido));
                        clFebrero.BorderWidth = 0;

                    }
                    else
                    {
                        clFebrero = new PdfPCell(new Phrase(Febrero, letraContenido));
                        clFebrero.BorderWidth = 0;
                        SiguienteValor = Febrero;
                    }


                    string Marzo = Convert.ToString(item.Marzo);
                    if (Marzo == "0")
                    {
                        clMarzo = new PdfPCell(new Phrase(SiguienteValor, letraContenido));
                        clMarzo.BorderWidth = 0;
                    }
                    else
                    {
                        clMarzo = new PdfPCell(new Phrase(Marzo, letraContenido));
                        clMarzo.BorderWidth = 0;
                        SiguienteValor = Marzo;
                    }


                    string Abril = Convert.ToString(item.Abril);
                    if (Abril == "0")
                    {
                        clAbril = new PdfPCell(new Phrase(SiguienteValor, letraContenido));
                        clAbril.BorderWidth = 0;
                    }
                    else
                    {
                        clAbril = new PdfPCell(new Phrase(Abril, letraContenido));
                        clAbril.BorderWidth = 0;
                        SiguienteValor = Abril;
                    }


                    string Mayo = Convert.ToString(item.Mayo);
                    if (Mayo == "0")
                    {
                        clMayo = new PdfPCell(new Phrase(SiguienteValor, letraContenido));
                        clMayo.BorderWidth = 0;
                    }
                    else
                    {
                        clMayo = new PdfPCell(new Phrase(Mayo, letraContenido));
                        clMayo.BorderWidth = 0;
                        SiguienteValor = Mayo;
                    }


                    string Junio = Convert.ToString(item.Junio);
                    if (Junio == "0")
                    {
                        clJunio = new PdfPCell(new Phrase(SiguienteValor, letraContenido));
                        clJunio.BorderWidth = 0;
                    }
                    else
                    {
                        clJunio = new PdfPCell(new Phrase(Junio, letraContenido));
                        clJunio.BorderWidth = 0;
                        SiguienteValor = Junio;
                    }


                    string Julio = Convert.ToString(item.Julio);
                    if (Julio == "0")
                    {
                        clJulio = new PdfPCell(new Phrase(SiguienteValor, letraContenido));
                        clJulio.BorderWidth = 0;
                    }
                    else
                    {
                        clJulio = new PdfPCell(new Phrase(Julio, letraContenido));
                        clJulio.BorderWidth = 0;
                        SiguienteValor = Julio;
                    }


                    string Agosto = Convert.ToString(item.Agosto);
                    if (Agosto == "0")
                    {
                        clAgosto = new PdfPCell(new Phrase(SiguienteValor, letraContenido));
                        clAgosto.BorderWidth = 0;
                    }
                    else
                    {
                        clAgosto = new PdfPCell(new Phrase(Agosto, letraContenido));
                        clAgosto.BorderWidth = 0;
                        SiguienteValor = Agosto;
                    }


                    string Septiembre = Convert.ToString(item.Septiembre);
                    if (Septiembre == "0")
                    {
                        clSeptiembre = new PdfPCell(new Phrase(SiguienteValor, letraContenido));
                        clSeptiembre.BorderWidth = 0;
                    }
                    else
                    {
                        clSeptiembre = new PdfPCell(new Phrase(Septiembre, letraContenido));
                        clSeptiembre.BorderWidth = 0;
                        SiguienteValor = Septiembre;
                    }


                    string Octubre = Convert.ToString(item.Octubre);
                    if (Octubre == "0")
                    {
                        clOctubre = new PdfPCell(new Phrase(SiguienteValor, letraContenido));
                        clOctubre.BorderWidth = 0;
                    }
                    else
                    {
                        clOctubre = new PdfPCell(new Phrase(Octubre, letraContenido));
                        clOctubre.BorderWidth = 0;
                        SiguienteValor = Octubre;
                    }


                    string Noviembre = Convert.ToString(item.Noviembre);
                    if (Noviembre == "0")
                    {
                        clNoviembre = new PdfPCell(new Phrase(SiguienteValor, letraContenido));
                        clNoviembre.BorderWidth = 0;
                    }
                    else
                    {
                        clNoviembre = new PdfPCell(new Phrase(Noviembre, letraContenido));
                        clNoviembre.BorderWidth = 0;
                        SiguienteValor = Noviembre;
                    }


                    string Diciembre = Convert.ToString(item.Diciembre);
                    if (Diciembre == "0")
                    {
                        clDiciembre = new PdfPCell(new Phrase(SiguienteValor, letraContenido));
                        clDiciembre.BorderWidth = 0;
                    }
                    else
                    {
                        clDiciembre = new PdfPCell(new Phrase(Diciembre, letraContenido));
                        clDiciembre.BorderWidth = 0;
                        SiguienteValor = Diciembre;
                    }


                    tblPrueba.AddCell(clMaterial);
                    tblPrueba.AddCell(clSaldoInicial);
                    tblPrueba.AddCell(clEnero);
                    tblPrueba.AddCell(clFebrero);
                    tblPrueba.AddCell(clMarzo);
                    tblPrueba.AddCell(clAbril);
                    tblPrueba.AddCell(clMayo);
                    tblPrueba.AddCell(clJunio);
                    tblPrueba.AddCell(clJulio);
                    tblPrueba.AddCell(clAgosto);
                    tblPrueba.AddCell(clSeptiembre);
                    tblPrueba.AddCell(clOctubre);
                    tblPrueba.AddCell(clNoviembre);
                    tblPrueba.AddCell(clDiciembre);
                }
            }
            doc.Add(tblPrueba);
            doc.Close();
            writer.Close();
            string mensaje = "Se generó el PDF exitosamente en la carpeta" + " " + folderPath;
            string message2 = mensaje;
            const string caption2 = "Éxito";
            var result2 = MessageBox.Show(message2, caption2,
                                         MessageBoxButtons.OK,
                                         MessageBoxIcon.Asterisk);
        }
    }
}
