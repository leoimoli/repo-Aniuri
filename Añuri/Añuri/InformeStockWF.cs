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
    public partial class InformeStockWF : Form
    {
        private int idProductoSeleccionado;
        private string material;

        public InformeStockWF(int idProductoSeleccionado, string material)
        {
            InitializeComponent();
            this.idProductoSeleccionado = idProductoSeleccionado;
            this.material = material;
        }

        private void InformeStockWF_Load(object sender, EventArgs e)
        {
            try
            {
                idProducto.Text = Convert.ToString(idProductoSeleccionado);
                lblNombreProducto.Text = material;
                ListaStockDisponible();
                ListarMovimientosStock();
                ArmarGraficos();
            }
            catch (Exception ex)
            { }
        }

        private void ListaStockDisponible()
        {
            List<Stock> ListaStock = StockNeg.ListarStockDisponible(idProductoSeleccionado);
            if (ListaStock.Count > 0)
            {
                ArmarGrillaStockDisponible(ListaStock);
            }
        }
        private void ArmarGrillaStockDisponible(List<Stock> ListaStock)
        {
            int idEntrada = 0;
            int TotalEntrada = 0;
            int TotalSalida = 0;
            int Total = 0;
            List<Stock> ListaStockFinal = new List<Stock>();
            List<Stock> ListaStockFinal2 = new List<Stock>();
            dgvStockDisponible.Rows.Clear();
            foreach (var item in ListaStock)
            {
                if (idEntrada != 0)
                {
                    if (idEntrada == item.idMovimientoEntrada)
                    {
                        if (item.TipoMovimiento == "E")
                        {
                            TotalEntrada = item.Cantidad;

                        }
                        if (item.TipoMovimiento == "S")
                        {
                            TotalSalida = TotalSalida + item.Cantidad;
                        }
                        Total = TotalEntrada - TotalSalida;
                        item.Cantidad = Total;
                        //ListaStockFinal = ListaStock;
                        Stock _lista = new Stock();

                        _lista.idProducto = item.idProducto;
                        _lista.Descripcion = item.Descripcion;
                        _lista.FechaFactura = item.FechaFactura;
                        _lista.Cantidad = Total;
                        _lista.ValorUnitario = item.ValorUnitario;
                        _lista.PrecioNeto = item.PrecioNeto;
                        _lista.TipoMovimiento = item.TipoMovimiento;
                        _lista.idMovimientoEntrada = item.idMovimientoEntrada;
                        ListaStockFinal.Add(_lista);
                        idEntrada = item.idMovimientoEntrada;
                    }
                    else
                    {
                        if (!ListaStockFinal2.Any(x => x.idMovimientoEntrada == idEntrada))
                        {
                            ListaStockFinal2.Add(ListaStockFinal[ListaStockFinal.Count - 1]);
                        }
                        TotalEntrada = 0;
                        TotalSalida = 0;
                        Total = 0;
                        if (item.TipoMovimiento == "E")
                        {
                            TotalEntrada = item.Cantidad;

                        }
                        if (item.TipoMovimiento == "S")
                        {
                            TotalSalida = TotalSalida + item.Cantidad;
                        }
                        Total = TotalEntrada - TotalSalida;
                        item.Cantidad = Total;
                        //ListaStockFinal = ListaStock;
                        Stock _lista = new Stock();

                        _lista.idProducto = item.idProducto;
                        _lista.Descripcion = item.Descripcion;
                        _lista.FechaFactura = item.FechaFactura;
                        _lista.Cantidad = Total;
                        _lista.ValorUnitario = item.ValorUnitario;
                        _lista.PrecioNeto = item.PrecioNeto;
                        _lista.TipoMovimiento = item.TipoMovimiento;
                        _lista.idMovimientoEntrada = item.idMovimientoEntrada;
                        ListaStockFinal.Add(_lista);
                        idEntrada = item.idMovimientoEntrada;
                        ///// Valido si ya existe el material en la lista Final.
                        if (!ListaStockFinal2.Any(x => x.idMovimientoEntrada == idEntrada))
                        {
                            ListaStockFinal2.Add(ListaStockFinal[ListaStockFinal.Count - 1]);
                        }
                    }
                }
                else
                {
                    TotalEntrada = 0;
                    TotalSalida = 0;
                    Total = 0;
                    if (item.TipoMovimiento == "E")
                    {
                        TotalEntrada = item.Cantidad;
                    }
                    Total = TotalEntrada - TotalSalida;
                    item.Cantidad = Total;
                    //ListaStockFinal = ListaStock;
                    Stock _lista = new Stock();

                    _lista.idProducto = item.idProducto;
                    _lista.Descripcion = item.Descripcion;
                    _lista.FechaFactura = item.FechaFactura;
                    _lista.Cantidad = Total;
                    _lista.ValorUnitario = item.ValorUnitario;
                    _lista.PrecioNeto = item.PrecioNeto;
                    _lista.TipoMovimiento = item.TipoMovimiento;
                    _lista.idMovimientoEntrada = item.idMovimientoEntrada;
                    ListaStockFinal.Add(_lista);
                    idEntrada = item.idMovimientoEntrada;
                }
            }
            foreach (var item in ListaStockFinal2)
            {
                dgvStockDisponible.Rows.Add(item.idProducto, item.Descripcion, item.FechaFactura, item.Cantidad, item.ValorUnitario, item.PrecioNeto, "", "");
            }
            dgvLista.ReadOnly = true;
        }
        private void ArmarGraficos()
        {
            //////// Grafico en Pesos
            List<StockProducto> GraficoProductoEnPesos = new List<StockProducto>();
            GraficoProductoEnPesos = DatosParaGraficoEnPesos();
            if (GraficoProductoEnPesos.Count > 0)
            {
                DiseñoGraficoProductoEnPesos(GraficoProductoEnPesos);
            }
            else
            { }
            //////// Grafico en Kilos
            List<StockProducto> GraficoProductoEnKilos = new List<StockProducto>();
            GraficoProductoEnKilos = DatosParaGraficoKilos();
            if (GraficoProductoEnPesos.Count > 0)
            {
                DiseñoGraficoProductoEnKilos(GraficoProductoEnKilos);
            }
            else
            { }
        }
        private void DiseñoGraficoProductoEnKilos(List<StockProducto> graficoProductoEnKilos)
        {
            List<string> Nombre = new List<string>();
            List<string> NombreValor = new List<string>();
            List<string> Total = new List<string>();
            foreach (var item in graficoProductoEnKilos)
            {
                Nombre.Add(item.TipoMovimiento);
                string total = Convert.ToString(item.kilos);
                string totalFinal = "$" + " " + total;
                Total.Add(totalFinal);
            }
            chartEnKilos.Series[0].Points.DataBindXY(Nombre, Total);
        }
        private List<StockProducto> DatosParaGraficoKilos()
        {
            List<StockProducto> lista = new List<StockProducto>();
            List<StockProducto> listaFinal = new List<StockProducto>();
            decimal SumaEntradas = 0;
            decimal SumaSalidas = 0;
            foreach (DataGridViewRow row in dgvLista.Rows)
            {
                StockProducto _stock = new StockProducto();
                string Tipo = Convert.ToString(row.Cells["TipoMovimiento"].Value);
                if (Tipo == "Entrada")
                {
                    int Valor = Convert.ToInt32(row.Cells["Kilos"].Value);
                    //SumaEntradas = SumaEntradas + Valor;
                    _stock.SumaEntradaKilos = Valor;
                    _stock.TipoMovimiento = "Entrada";
                }
                if (Tipo == "Salida")
                {
                    int Valor = Convert.ToInt32(row.Cells["Kilos"].Value);
                    //SumaSalidas = SumaSalidas + Valor;
                    _stock.SumaSalidaKilos = Valor;
                    _stock.TipoMovimiento = "Salida";
                }
                lista.Add(_stock);
            }

            int Entradas = 0;
            int Salidas = 0;
            foreach (var item in lista)
            {
                if (item.TipoMovimiento == "Entrada")
                {
                    Entradas = Entradas + item.SumaEntradaKilos;
                }
            }
            StockProducto _listafinal = new StockProducto();
            _listafinal.kilos = Entradas;
            _listafinal.TipoMovimiento = "Entrada";
            listaFinal.Add(_listafinal);

            foreach (var item in lista)
            {
                if (item.TipoMovimiento == "Salida")
                {
                    Salidas = Salidas + item.SumaSalidaKilos;
                }
            }
            StockProducto _listafinalSalidas = new StockProducto();
            _listafinalSalidas.kilos = Salidas;
            _listafinalSalidas.TipoMovimiento = "Salida";
            listaFinal.Add(_listafinalSalidas);
            return listaFinal;
        }
        private void DiseñoGraficoProductoEnPesos(List<StockProducto> graficoProductoEnPesos)
        {
            List<string> Nombre = new List<string>();
            List<string> NombreValor = new List<string>();
            List<string> Total = new List<string>();
            foreach (var item in graficoProductoEnPesos)
            {
                Nombre.Add(item.TipoMovimiento);
                string total = Convert.ToString(item.Valor);
                string totalFinal = "$" + " " + total;
                Total.Add(totalFinal);
            }
            chartEnPesos.Series[0].Points.DataBindXY(Nombre, Total);
        }
        private List<StockProducto> DatosParaGraficoEnPesos()
        {
            List<StockProducto> lista = new List<StockProducto>();
            List<StockProducto> listaFinal = new List<StockProducto>();
            decimal SumaEntradas = 0;
            decimal SumaSalidas = 0;
            foreach (DataGridViewRow row in dgvLista.Rows)
            {
                StockProducto _stock = new StockProducto();
                string Tipo = Convert.ToString(row.Cells["TipoMovimiento"].Value);
                if (Tipo == "Entrada")
                {
                    decimal Valor = Convert.ToDecimal(row.Cells["PrecioNeto"].Value);
                    SumaEntradas = SumaEntradas + Valor;
                    _stock.SumaEntrada = Valor;
                    _stock.TipoMovimiento = "Entrada";
                }
                if (Tipo == "Salida")
                {
                    decimal Valor = Convert.ToDecimal(row.Cells["PrecioNeto"].Value);
                    SumaSalidas = SumaSalidas + Valor;
                    _stock.SumaSalida = Valor;
                    _stock.TipoMovimiento = "Salida";
                }
                lista.Add(_stock);
            }

            decimal Entradas = 0;
            decimal Salidas = 0;
            foreach (var item in lista)
            {
                if (item.TipoMovimiento == "Entrada")
                {
                    Entradas = Entradas + item.SumaEntrada;
                }
            }
            StockProducto _listafinal = new StockProducto();
            _listafinal.Valor = Entradas;
            _listafinal.TipoMovimiento = "Entrada";
            listaFinal.Add(_listafinal);

            foreach (var item in lista)
            {
                if (item.TipoMovimiento == "Salida")
                {
                    Salidas = Salidas + item.SumaSalida;
                }
            }
            StockProducto _listafinalSalidas = new StockProducto();
            _listafinalSalidas.Valor = Salidas;
            _listafinalSalidas.TipoMovimiento = "Salida";
            listaFinal.Add(_listafinalSalidas);
            return listaFinal;
        }
        private void ListarMovimientosStock()
        {
            List<Stock> ListaStock = StockNeg.ListarMovimientosStock(idProductoSeleccionado);
            if (ListaStock.Count > 0)
            {
                //Stock ultimo = new Stock();
                //ultimo.Descripcion = "TOTALES";
                //int Kilos = CalcularTotalKilos(ListaMateriales);
                //decimal TotalPrecioNeto = CalculaPrecioNeto(ListaMateriales);
                //ultimo.Cantidad = Convert.ToInt32(Kilos);
                //ultimo.PrecioNeto = Convert.ToDecimal(TotalPrecioNeto);
                //ultimo.ValorUnitario = 0;
                //ListaMateriales.Add(ultimo);
                //ListaMaterialesStatic = ListaMateriales;
                //string fecha = "";
                foreach (var item in ListaStock)
                {
                    string Movimiento = "";
                    if (item.TipoMovimiento == "E")
                    {
                        Movimiento = "Entrada";
                    }
                    if (item.TipoMovimiento == "S")
                    {
                        Movimiento = "Salida";
                    }
                    dgvLista.Rows.Add(item.idProducto, item.Descripcion, item.FechaFactura, item.Cantidad, item.ValorUnitario, item.PrecioNeto, Movimiento);
                }
            }
            dgvLista.ReadOnly = true;
        }
        private void btnCerrar_Click(object sender, EventArgs e)
        {
            Close();
        }
        private void btnBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                dgvLista.Rows.Clear();
                bool filtroFecha = false;
                bool filtroTipo = false;
                DateTime FechaDesde = Convert.ToDateTime("1/1/1900 00:00:00");
                DateTime FechaHasta = Convert.ToDateTime("1/1/1900 00:00:00");
                string TipoMovimiento = "";

                if (dtFechaDesde.Value.ToString() != "1/1/1900 00:00:00")
                {
                    if (dtFechaHasta.Value.ToString() != "1/1/1900 00:00:00")
                    {
                        FechaDesde = Convert.ToDateTime(dtFechaDesde.Value.ToShortDateString());
                        FechaHasta = Convert.ToDateTime(dtFechaHasta.Value.ToShortDateString());
                        filtroFecha = true;
                    }
                    else
                    {
                        filtroFecha = false;
                        const string message2 = "Atención: Debe seleccionar una fecha Hasta.";
                        const string caption2 = "Atención";
                        var result2 = MessageBox.Show(message2, caption2,
                                                     MessageBoxButtons.OK,
                                                     MessageBoxIcon.Exclamation);
                    }
                }
                else if (dtFechaHasta.Value.ToString() != "1/1/1900 00:00:00")
                {
                    if (dtFechaDesde.Value.ToString() != "1/1/1900 00:00:00")
                    {
                        FechaDesde = Convert.ToDateTime(dtFechaDesde.Value.ToShortDateString());
                        FechaHasta = Convert.ToDateTime(dtFechaHasta.Value.ToShortDateString());
                        filtroFecha = true;
                    }
                    else
                    {
                        filtroFecha = false;
                        const string message2 = "Atención: Debe seleccionar una fecha Desde.";
                        const string caption2 = "Atención";
                        var result2 = MessageBox.Show(message2, caption2,
                                                     MessageBoxButtons.OK,
                                                     MessageBoxIcon.Exclamation);
                    }
                }
                if (cmbTipoMovimiento.Text != "Seleccione")
                {
                    filtroTipo = true;
                    TipoMovimiento = cmbTipoMovimiento.Text;
                    if (TipoMovimiento == "Entrada")
                    {
                        TipoMovimiento = "E";
                    }
                    if (TipoMovimiento == "Salida")
                    {
                        TipoMovimiento = "S";
                    }
                }

                if (filtroFecha == true)
                {
                    if (filtroTipo == true)
                    {
                        List<Stock> ListaStock = StockNeg.ListarMovimientosStockPorFechaTipoMovimiento(idProductoSeleccionado, FechaDesde, FechaHasta, TipoMovimiento);
                        if (ListaStock.Count > 0)
                        {
                            foreach (var item in ListaStock)
                            {
                                string Movimiento = "";
                                if (item.TipoMovimiento == "E")
                                {
                                    Movimiento = "Entrada";
                                }
                                if (item.TipoMovimiento == "S")
                                {
                                    Movimiento = "Salida";
                                }
                                dgvLista.Rows.Add(item.idProducto, item.Descripcion, item.FechaFactura, item.Cantidad, item.ValorUnitario, item.PrecioNeto, Movimiento);
                                ArmarGraficos();
                            }
                        }
                        dgvLista.ReadOnly = true;
                    }
                    else
                    {
                        List<Stock> ListaStock = StockNeg.ListarMovimientosStockPorFecha(idProductoSeleccionado, FechaDesde, FechaHasta);
                        if (ListaStock.Count > 0)
                        {
                            foreach (var item in ListaStock)
                            {
                                string Movimiento = "";
                                if (item.TipoMovimiento == "E")
                                {
                                    Movimiento = "Entrada";
                                }
                                if (item.TipoMovimiento == "S")
                                {
                                    Movimiento = "Salida";
                                }
                                dgvLista.Rows.Add(item.idProducto, item.Descripcion, item.FechaFactura, item.Cantidad, item.ValorUnitario, item.PrecioNeto, Movimiento);
                                ArmarGraficos();
                            }
                        }
                        dgvLista.ReadOnly = true;

                        List<Stock> ListaStockDisponible = StockNeg.ListarMovimientosStockDisponiblePorFecha(idProductoSeleccionado, FechaDesde, FechaHasta);
                        if (ListaStockDisponible.Count > 0)
                        {
                            ArmarGrillaStockDisponiblePorFecha(ListaStockDisponible);
                        }
                        dgvLista.ReadOnly = true;
                    }
                }
                if (filtroTipo == true)
                {
                    List<Stock> ListaStock = StockNeg.ListarMovimientosStockPorTipoMovimiento(idProductoSeleccionado, TipoMovimiento);
                    if (ListaStock.Count > 0)
                    {
                        foreach (var item in ListaStock)
                        {
                            string Movimiento = "";
                            if (item.TipoMovimiento == "E")
                            {
                                Movimiento = "Entrada";
                            }
                            if (item.TipoMovimiento == "S")
                            {
                                Movimiento = "Salida";
                            }
                            dgvLista.Rows.Add(item.idProducto, item.Descripcion, item.FechaFactura, item.Cantidad, item.ValorUnitario, item.PrecioNeto, Movimiento);
                            ArmarGraficos();
                        }
                    }
                    dgvLista.ReadOnly = true;
                }
            }
            catch (Exception ex)
            { }
        }

        private void ArmarGrillaStockDisponiblePorFecha(List<Stock> ListaStock)
        {
            int idEntrada = 0;
            int TotalEntrada = 0;
            int TotalSalida = 0;
            int Total = 0;
            List<Stock> ListaStockFinal = new List<Stock>();
            List<Stock> ListaStockFinal2 = new List<Stock>();
            dgvStockDisponible.Rows.Clear();
            int contadorElementos = 0;
            foreach (var item in ListaStock)
            {
                contadorElementos = contadorElementos + 1;
                if (idEntrada != 0)
                {
                    if (idEntrada == item.idMovimientoEntrada)
                    {
                        if (item.TipoMovimiento == "E")
                        {
                            TotalEntrada = item.Cantidad;

                        }
                        if (item.TipoMovimiento == "S")
                        {
                            TotalSalida = TotalSalida + item.Cantidad;
                        }
                        Total = TotalEntrada - TotalSalida;
                        item.Cantidad = Total;
                        //ListaStockFinal = ListaStock;
                        Stock _lista = new Stock();

                        _lista.idProducto = item.idProducto;
                        _lista.Descripcion = item.Descripcion;
                        _lista.FechaFactura = item.FechaFactura;
                        _lista.Cantidad = Total;
                        _lista.ValorUnitario = item.ValorUnitario;
                        _lista.PrecioNeto = item.PrecioNeto;
                        _lista.TipoMovimiento = item.TipoMovimiento;
                        _lista.idMovimientoEntrada = item.idMovimientoEntrada;
                        ListaStockFinal.Add(_lista);
                        idEntrada = item.idMovimientoEntrada;
                        if (!ListaStockFinal2.Any(x => x.idMovimientoEntrada == idEntrada) && contadorElementos == ListaStock.Count)
                        {
                            ListaStockFinal2.Add(ListaStockFinal[ListaStockFinal.Count - 1]);
                        }
                    }
                    else
                    {
                        if (!ListaStockFinal2.Any(x => x.idMovimientoEntrada == idEntrada))
                        {
                            if (ListaStockFinal[ListaStockFinal.Count - 1].Cantidad > 0)
                            {
                                ListaStockFinal2.Add(ListaStockFinal[ListaStockFinal.Count - 1]);
                            }
                        }

                        TotalEntrada = 0;
                        TotalSalida = 0;
                        Total = 0;
                        if (item.TipoMovimiento == "E")
                        {
                            TotalEntrada = item.Cantidad;

                        }
                        if (item.TipoMovimiento == "S")
                        {
                            TotalSalida = TotalSalida + item.Cantidad;
                        }
                        Total = TotalEntrada - TotalSalida;
                        item.Cantidad = Total;
                        //ListaStockFinal = ListaStock;
                        Stock _lista = new Stock();

                        _lista.idProducto = item.idProducto;
                        _lista.Descripcion = item.Descripcion;
                        _lista.FechaFactura = item.FechaFactura;
                        _lista.Cantidad = Total;
                        _lista.ValorUnitario = item.ValorUnitario;
                        _lista.PrecioNeto = item.PrecioNeto;
                        _lista.TipoMovimiento = item.TipoMovimiento;
                        _lista.idMovimientoEntrada = item.idMovimientoEntrada;
                        ListaStockFinal.Add(_lista);
                        idEntrada = item.idMovimientoEntrada;
                        ///// Valido si ya existe el material en la lista Final.
                        if (!ListaStockFinal2.Any(x => x.idMovimientoEntrada == idEntrada) && contadorElementos == ListaStock.Count)
                        {
                            ListaStockFinal2.Add(ListaStockFinal[ListaStockFinal.Count - 1]);
                        }
                    }
                }
                else
                {
                    TotalEntrada = 0;
                    TotalSalida = 0;
                    Total = 0;
                    if (item.TipoMovimiento == "E")
                    {
                        TotalEntrada = item.Cantidad;
                    }
                    Total = TotalEntrada - TotalSalida;
                    item.Cantidad = Total;
                    //ListaStockFinal = ListaStock;
                    Stock _lista = new Stock();

                    _lista.idProducto = item.idProducto;
                    _lista.Descripcion = item.Descripcion;
                    _lista.FechaFactura = item.FechaFactura;
                    _lista.Cantidad = Total;
                    _lista.ValorUnitario = item.ValorUnitario;
                    _lista.PrecioNeto = item.PrecioNeto;
                    _lista.TipoMovimiento = item.TipoMovimiento;
                    _lista.idMovimientoEntrada = item.idMovimientoEntrada;
                    ListaStockFinal.Add(_lista);
                    idEntrada = item.idMovimientoEntrada;
                }              
            }
            foreach (var item in ListaStockFinal2)
            {
                dgvStockDisponible.Rows.Add(item.idProducto, item.Descripcion, item.FechaFactura, item.Cantidad, item.ValorUnitario, item.PrecioNeto, "", "");
            }
            dgvLista.ReadOnly = true;
        }

        private void chcFechaHasta_CheckedChanged(object sender, EventArgs e)
        {
            if (chcFechaHasta.Checked == true)
            {
                dtFechaHasta.Enabled = true;
                dtFechaHasta.Value = DateTime.Now;
            }
            else
            {
                dtFechaHasta.Value = Convert.ToDateTime("1 / 1 / 1900 00:00:00");
                dtFechaHasta.Enabled = false;
            }
        }
        private void chcFechaDesde_CheckedChanged(object sender, EventArgs e)
        {
            if (chcFechaDesde.Checked == true)
            {
                dtFechaDesde.Enabled = true;
                dtFechaDesde.Value = DateTime.Now;
            }
            else
            {
                dtFechaDesde.Value = Convert.ToDateTime("1 / 1 / 1900 00:00:00");
                dtFechaDesde.Enabled = false;
            }
        }
    }
}
