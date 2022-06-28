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
using System.Globalization;
using System.IO;
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
        private int CalcularTotalKilos(List<Stock> listaMateriales)
        {
            int totalKilos = 0;
            foreach (var item in listaMateriales)
            {
                totalKilos += item.Cantidad;
            }
            return totalKilos;
        }
        private decimal CalculaPrecioNeto(List<Stock> listaMateriales)
        {
            decimal totalMonto = 0;
            decimal MontoNegativo = 0;
            foreach (var item in listaMateriales)
            {
                totalMonto += item.PrecioNeto;

            }
            decimal valor = decimal.Round(totalMonto - MontoNegativo, 2);
            return valor;
        }
        private void ArmarGrillaStockDisponible(List<Stock> ListaStock)
        {
            int idEntrada = 0;
            int TotalEntrada = 0;
            int TotalSalida = 0;
            int Total = 0;
            List<Stock> ListaStockFinal = new List<Stock>();
            List<Stock> ListaStockFinal2 = new List<Stock>();
            int contadorElementos = 0;
            dgvStockDisponible.Rows.Clear();
            foreach (var item in ListaStock)
            {
                if (item.Descripcion == "TOTALES")
                {
                    ListaStockFinal2.Add(item);
                    break;
                }
                else
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
                            _lista.PrecioNeto = Total * item.ValorUnitario;
                            //_lista.PrecioNeto = item.PrecioNeto;
                            _lista.TipoMovimiento = item.TipoMovimiento;
                            _lista.idMovimientoEntrada = item.idMovimientoEntrada;
                            ListaStockFinal.Add(_lista);
                            idEntrada = item.idMovimientoEntrada;
                            if (contadorElementos == ListaStock.Count)
                            {
                                if (!ListaStockFinal2.Any(x => x.idMovimientoEntrada == idEntrada))
                                {
                                    ListaStockFinal2.Add(ListaStockFinal[ListaStockFinal.Count - 1]);
                                }
                            }
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
                            _lista.PrecioNeto = Total * item.ValorUnitario;
                            // _lista.PrecioNeto = item.PrecioNeto;
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
                        if (contadorElementos == ListaStock.Count)
                        {
                            if (!ListaStockFinal2.Any(x => x.idMovimientoEntrada == idEntrada))
                            {
                                ListaStockFinal2.Add(ListaStockFinal[ListaStockFinal.Count - 1]);
                            }
                        }
                    }
                }
            }
            string fecha = "";
            Stock ultimo = new Stock();
            ultimo.Descripcion = "TOTALES";
            int Kilos = CalcularTotalKilos(ListaStockFinal2);
            decimal TotalPrecioNeto = CalculaPrecioNeto(ListaStockFinal2);
            ultimo.Cantidad = Convert.ToInt32(Kilos);
            ultimo.PrecioNeto = Convert.ToDecimal(TotalPrecioNeto);
            ultimo.ValorUnitario = 0;
            ListaStockFinal2.Add(ultimo);
            ListaMaterialesStockDisponibleStatic = ListaStockFinal2;
            foreach (var item in ListaStockFinal2)
            {
                if (item.FechaFactura == Convert.ToDateTime("1/1/0001 00:00:00"))
                {
                    fecha = " ";
                }
                else
                {
                    fecha = item.FechaFactura.ToShortDateString();
                }
                //Agrego Punto De Miles...
                string ValorUnitario = item.ValorUnitario.ToString("N", new CultureInfo("es-CL"));
                string ValorNeto = item.PrecioNeto.ToString("N", new CultureInfo("es-CL"));
                dgvStockDisponible.Rows.Add(item.idProducto, item.Descripcion, fecha, item.Cantidad, ValorUnitario, ValorNeto, "", "");
            }
            dgvLista.ReadOnly = true;
        }
        private void ArmarGraficos()
        {
            //////// Grafico en Pesos
            List<StockProducto> GraficoProductoEnPesos = new List<StockProducto>();
            GraficoProductoEnPesos = DatosParaGraficoEnPesos();
            if (dgvLista.Rows.Count > 0)
            {
                if (GraficoProductoEnPesos.Count > 0)
                {
                    DiseñoGraficoProductoEnPesos(GraficoProductoEnPesos);
                }
            }
            else
            {
                chartEnPesos.Visible = false;
                btnPdf.Visible = false;
                btnExcel.Visible = false;
                btnPdf2.Visible = false;
                btnExcel2.Visible = false;
            }
            //////// Grafico en Kilos
            List<StockProducto> GraficoProductoEnKilos = new List<StockProducto>();
            if (dgvLista.Rows.Count > 0)
            {
                GraficoProductoEnKilos = DatosParaGraficoKilos();
                if (GraficoProductoEnPesos.Count > 0)
                {
                    DiseñoGraficoProductoEnKilos(GraficoProductoEnKilos);
                }
            }
            else
            {
                chartEnKilos.Visible = false;
                btnPdf.Visible = false;
                btnExcel.Visible = false;
                btnPdf2.Visible = false;
                btnExcel2.Visible = false;
            }

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
                //Agrego Punto De Miles...
                string ValorConPuntos = item.Valor.ToString("N", new CultureInfo("es-CL"));
                //string total = Convert.ToString(item.Valor);
                string totalFinal = "$" + " " + ValorConPuntos;

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
                ListaMaterialesStatic = ListaStock;
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
                    //Agrego Punto De Miles...
                    string ValorUnitario = item.ValorUnitario.ToString("N", new CultureInfo("es-CL"));
                    string ValorNeto = item.PrecioNeto.ToString("N", new CultureInfo("es-CL"));
                    dgvLista.Rows.Add(item.idProducto, item.Descripcion, item.FechaFactura, item.Cantidad, ValorUnitario, ValorNeto, Movimiento);
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
                if (chcFechaDesde.Checked == true && chcFechaHasta.Checked == true)
                {
                    if (dtFechaDesde.Value > dtFechaHasta.Value)
                    {
                        const string message2 = "Atención: La Fecha desde no puede ser mayor a la fecha Hasta.";
                        const string caption2 = "Atención";
                        var result2 = MessageBox.Show(message2, caption2,
                                                     MessageBoxButtons.OK,
                                                     MessageBoxIcon.Exclamation);
                        throw new Exception();
                    }
                }
                dgvLista.Rows.Clear();
                dgvStockDisponible.Rows.Clear();
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
                            ListaMaterialesStatic = ListaStock;
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
                        else { ArmarGraficos(); }
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
                        else { ArmarGraficos(); }
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
                    else
                    {
                        ArmarGraficos();
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
                if (item.Descripcion == "TOTALES")
                {
                    ListaStockFinal2.Add(item);
                    break;
                }
                else
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
            }
            string fecha = "";
            Stock ultimo = new Stock();
            ultimo.Descripcion = "TOTALES";
            int Kilos = CalcularTotalKilos(ListaStockFinal2);
            decimal TotalPrecioNeto = CalculaPrecioNeto(ListaStockFinal2);
            ultimo.Cantidad = Convert.ToInt32(Kilos);
            ultimo.PrecioNeto = Convert.ToDecimal(TotalPrecioNeto);
            ultimo.ValorUnitario = 0;
            ListaStockFinal2.Add(ultimo);
            ListaMaterialesStockDisponibleStatic = ListaStockFinal2;
            foreach (var item in ListaStockFinal2)
            {
                if (item.FechaFactura == Convert.ToDateTime("1/1/0001 00:00:00"))
                {
                    fecha = " ";
                }
                else
                {
                    fecha = item.FechaFactura.ToShortDateString();
                }
                dgvStockDisponible.Rows.Add(item.idProducto, item.Descripcion, fecha, item.Cantidad, item.ValorUnitario, item.PrecioNeto, "", "");
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
                Caluculate2(j);
                progressBar2.PerformStep();
            }
        }
        private void Caluculate2(int i)
        {
            double pow = Math.Pow(i, i);
        }
        private void btnExcel_Click(object sender, EventArgs e)
        {
            ProgressBar();
            dgvLista.ClipboardCopyMode = DataGridViewClipboardCopyMode.EnableAlwaysIncludeHeaderText;
            dgvLista.MultiSelect = true;
            dgvLista.SelectAll();
            DataObject dataObj = dgvLista.GetClipboardContent();
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
        public static List<Stock> ListaMaterialesStatic;
        public static List<Stock> ListaMaterialesStockDisponibleStatic;
        private void btnPdf_Click(object sender, EventArgs e)
        {
            MemoryStream m = new MemoryStream();
            Document doc = new Document(PageSize.LETTER);

            string folderPath = "C:\\Añuri-Archivos\\PDFs\\Reporte Stock\\";
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }
            string replaceWith = "";
            material = material.Replace("\r\n", replaceWith).Replace("\n", replaceWith).Replace("\r", replaceWith);
            string ruta = folderPath;
            //string Periodo = "Reporte de Obra";
            PdfWriter writer = PdfWriter.GetInstance(doc,
                                        new FileStream(ruta + material + ".pdf", FileMode.Create));
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
            string TextoInicial = "Informe de movimientos historicos de - " + material;
            //string DomicilioTexto = "Domicilio:" + Domicilio;
            //string replaceWith = "";
            //DomicilioTexto = DomicilioTexto.Replace("\r\n", replaceWith).Replace("\n", replaceWith).Replace("\r", replaceWith);

            doc.Add(new Paragraph(" "));
            //Paragraph p2 = new Paragraph(new Chunk(DomicilioTexto));
            Paragraph p1 = new Paragraph(new Chunk(TextoInicial));
            p1.Alignment = Element.ALIGN_LEFT;

            //if (Domicilio.Length <= 30)
            //{ p2 = new Paragraph(new Chunk(DomicilioTexto, DomicilioFontMenos30)); }
            //if (Domicilio.Length >= 30 && Domicilio.Length <= 40)
            //{ p2 = new Paragraph(new Chunk(DomicilioTexto, DomicilioFont30a40)); }
            //if (Domicilio.Length >= 40 && Domicilio.Length <= 50)
            //{ p2 = new Paragraph(new Chunk(DomicilioTexto, DomicilioFontHasta40a50)); }
            //if (Domicilio.Length >= 50 && Domicilio.Length <= 60)
            //{ p2 = new Paragraph(new Chunk(DomicilioTexto, DomicilioFontHasta50a60)); }
            //p2.Alignment = Element.ALIGN_LEFT;

            doc.Add(new Paragraph(p1));
            //doc.Add(new Paragraph(p2));
            doc.Add(new Paragraph(" "));

            //doc.Add(Chunk.NEWLINE);

            // Creamos una tabla que contendrá las cabeceras
            // de nuestros visitante.
            PdfPTable tblPrueba = new PdfPTable(6);
            tblPrueba.WidthPercentage = 110;

            // Configuramos el título de las columnas de la tabla
            PdfPCell clMaterial = new PdfPCell(new Phrase("Material", _standardFont));
            clMaterial.BorderWidth = 0;
            clMaterial.BorderWidthBottom = 0.50f;
            clMaterial.BorderWidthLeft = 0.50f;
            clMaterial.BorderWidthRight = 0.50f;
            clMaterial.BorderWidthTop = 0.50f;

            PdfPCell clFecha = new PdfPCell(new Phrase("Fecha", _standardFont));
            clFecha.BorderWidth = 0;
            clFecha.BorderWidthBottom = 0.50f;
            clFecha.BorderWidthLeft = 0.50f;
            clFecha.BorderWidthRight = 0.50f;
            clFecha.BorderWidthTop = 0.50f;

            PdfPCell clKilos = new PdfPCell(new Phrase("Kilos", _standardFont));
            clKilos.BorderWidth = 0;
            clKilos.BorderWidthBottom = 0.50f;
            clKilos.BorderWidthLeft = 0.50f;
            clKilos.BorderWidthRight = 0.50f;
            clKilos.BorderWidthTop = 0.50f;


            PdfPCell clPrecioUnitario = new PdfPCell(new Phrase("Precio Unitario", _standardFont));
            clPrecioUnitario.BorderWidth = 0;
            clPrecioUnitario.BorderWidthBottom = 0.50f;
            clPrecioUnitario.BorderWidthLeft = 0.50f;
            clPrecioUnitario.BorderWidthRight = 0.50f;
            clPrecioUnitario.BorderWidthTop = 0.50f;

            PdfPCell clPrecioNeto = new PdfPCell(new Phrase("Precio Neto", _standardFont));
            clPrecioNeto.BorderWidth = 0;
            clPrecioNeto.BorderWidthBottom = 0.50f;
            clPrecioNeto.BorderWidthLeft = 0.50f;
            clPrecioNeto.BorderWidthRight = 0.50f;
            clPrecioNeto.BorderWidthTop = 0.50f;

            PdfPCell clTipoMovimiento = new PdfPCell(new Phrase("Tipo de Movimiento", _standardFont));
            clTipoMovimiento.BorderWidth = 0;
            clTipoMovimiento.BorderWidthBottom = 0.50f;
            clTipoMovimiento.BorderWidthLeft = 0.50f;
            clTipoMovimiento.BorderWidthRight = 0.50f;
            clTipoMovimiento.BorderWidthTop = 0.50f;

            // Añadimos las celdas a la tabla
            tblPrueba.AddCell(clMaterial);
            tblPrueba.AddCell(clFecha);
            tblPrueba.AddCell(clKilos);
            tblPrueba.AddCell(clPrecioUnitario);
            tblPrueba.AddCell(clPrecioNeto);
            tblPrueba.AddCell(clTipoMovimiento);

            // Llenamos la tabla con información
            int TotalDeElementos = ListaMaterialesStatic.Count;
            int Contador = 0;
            foreach (var item in ListaMaterialesStatic)
            {
                Contador = Contador + 1;
                if (item.Descripcion != "")
                {
                    if (TotalDeElementos == Contador)
                    {
                        clMaterial = new PdfPCell(new Phrase(item.Descripcion, letraContenido));
                        clMaterial.BorderWidth = 0;

                        string Fecha = Convert.ToString(item.FechaFactura.ToShortDateString());
                        clFecha = new PdfPCell(new Phrase(Fecha, letraContenido));
                        clFecha.BorderWidth = 0;

                        string Kilos = Convert.ToString(item.Cantidad);
                        clKilos = new PdfPCell(new Phrase(Kilos, letraContenido));
                        clKilos.BorderWidth = 0;

                        string PrecioUnitario = Convert.ToString(item.ValorUnitario);
                        clPrecioUnitario = new PdfPCell(new Phrase(PrecioUnitario, letraContenido));
                        clPrecioUnitario.BorderWidth = 0;

                        string PrecioNeto = Convert.ToString(item.PrecioNeto);
                        clPrecioNeto = new PdfPCell(new Phrase(PrecioNeto, letraContenido));
                        clPrecioNeto.BorderWidth = 0;

                        string TipoMovimiento = Convert.ToString(item.TipoMovimiento);
                        clTipoMovimiento = new PdfPCell(new Phrase(TipoMovimiento, letraContenido));
                        clTipoMovimiento.BorderWidth = 0;


                        tblPrueba.AddCell(clMaterial);
                        tblPrueba.AddCell(clFecha);
                        tblPrueba.AddCell(clKilos);
                        tblPrueba.AddCell(clPrecioUnitario);
                        tblPrueba.AddCell(clPrecioNeto);
                        tblPrueba.AddCell(clTipoMovimiento);
                    }
                    else
                    {
                        clMaterial = new PdfPCell(new Phrase(item.Descripcion, letraContenido));
                        clMaterial.BorderWidth = 0;

                        string Fecha = Convert.ToString(item.FechaFactura.ToShortDateString());
                        clFecha = new PdfPCell(new Phrase(Fecha, letraContenido));
                        clFecha.BorderWidth = 0;

                        string Kilos = Convert.ToString(item.Cantidad);
                        clKilos = new PdfPCell(new Phrase(Kilos, letraContenido));
                        clKilos.BorderWidth = 0;

                        string PrecioUnitario = Convert.ToString(item.ValorUnitario);
                        clPrecioUnitario = new PdfPCell(new Phrase(PrecioUnitario, letraContenido));
                        clPrecioUnitario.BorderWidth = 0;

                        string PrecioNeto = Convert.ToString(item.PrecioNeto);
                        clPrecioNeto = new PdfPCell(new Phrase(PrecioNeto, letraContenido));
                        clPrecioNeto.BorderWidth = 0;

                        string TipoMovimiento = Convert.ToString(item.TipoMovimiento);
                        clTipoMovimiento = new PdfPCell(new Phrase(TipoMovimiento, letraContenido));
                        clTipoMovimiento.BorderWidth = 0;

                        tblPrueba.AddCell(clMaterial);
                        tblPrueba.AddCell(clFecha);
                        tblPrueba.AddCell(clKilos);
                        tblPrueba.AddCell(clPrecioUnitario);
                        tblPrueba.AddCell(clPrecioNeto);
                        tblPrueba.AddCell(clTipoMovimiento);

                    }
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

        private void btnExcel2_Click(object sender, EventArgs e)
        {
            ProgressBar2();
            dgvStockDisponible.ClipboardCopyMode = DataGridViewClipboardCopyMode.EnableAlwaysIncludeHeaderText;
            dgvStockDisponible.MultiSelect = true;
            dgvStockDisponible.SelectAll();
            DataObject dataObj = dgvStockDisponible.GetClipboardContent();
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
            progressBar2.Value = Convert.ToInt32(null);
            progressBar2.Visible = false;
        }
        private void btnPdf2_Click(object sender, EventArgs e)
        {
            MemoryStream m = new MemoryStream();
            Document doc = new Document(PageSize.LETTER);

            string folderPath = "C:\\Añuri-Archivos\\PDFs\\Reporte Stock\\";
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }
            string replaceWith = "";
            material = material.Replace("\r\n", replaceWith).Replace("\n", replaceWith).Replace("\r", replaceWith);
            string ruta = folderPath;
            //string Periodo = "Reporte de Obra";
            PdfWriter writer = PdfWriter.GetInstance(doc,
                                        new FileStream(ruta + material + " Stock Disponible" + ".pdf", FileMode.Create));
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
            string TextoInicial = "Informe detalle Stock disponible de - " + material;
            doc.Add(new Paragraph(" "));

            Paragraph p1 = new Paragraph(new Chunk(TextoInicial));
            p1.Alignment = Element.ALIGN_LEFT;
            doc.Add(new Paragraph(p1));
            doc.Add(new Paragraph(" "));
            // Creamos una tabla que contendrá las cabeceras
            // de nuestros visitante.
            PdfPTable tblPrueba = new PdfPTable(5);
            tblPrueba.WidthPercentage = 110;
            // Configuramos el título de las columnas de la tabla
            PdfPCell clMaterial = new PdfPCell(new Phrase("Material", _standardFont));
            clMaterial.BorderWidth = 0;
            clMaterial.BorderWidthBottom = 0.50f;
            clMaterial.BorderWidthLeft = 0.50f;
            clMaterial.BorderWidthRight = 0.50f;
            clMaterial.BorderWidthTop = 0.50f;

            PdfPCell clFecha = new PdfPCell(new Phrase("Fecha", _standardFont));
            clFecha.BorderWidth = 0;
            clFecha.BorderWidthBottom = 0.50f;
            clFecha.BorderWidthLeft = 0.50f;
            clFecha.BorderWidthRight = 0.50f;
            clFecha.BorderWidthTop = 0.50f;

            PdfPCell clKilos = new PdfPCell(new Phrase("Kilos", _standardFont));
            clKilos.BorderWidth = 0;
            clKilos.BorderWidthBottom = 0.50f;
            clKilos.BorderWidthLeft = 0.50f;
            clKilos.BorderWidthRight = 0.50f;
            clKilos.BorderWidthTop = 0.50f;


            PdfPCell clPrecioUnitario = new PdfPCell(new Phrase("Precio Unitario", _standardFont));
            clPrecioUnitario.BorderWidth = 0;
            clPrecioUnitario.BorderWidthBottom = 0.50f;
            clPrecioUnitario.BorderWidthLeft = 0.50f;
            clPrecioUnitario.BorderWidthRight = 0.50f;
            clPrecioUnitario.BorderWidthTop = 0.50f;

            PdfPCell clPrecioNeto = new PdfPCell(new Phrase("Precio Neto", _standardFont));
            clPrecioNeto.BorderWidth = 0;
            clPrecioNeto.BorderWidthBottom = 0.50f;
            clPrecioNeto.BorderWidthLeft = 0.50f;
            clPrecioNeto.BorderWidthRight = 0.50f;
            clPrecioNeto.BorderWidthTop = 0.50f;

            // Añadimos las celdas a la tabla
            tblPrueba.AddCell(clMaterial);
            tblPrueba.AddCell(clFecha);
            tblPrueba.AddCell(clKilos);
            tblPrueba.AddCell(clPrecioUnitario);
            tblPrueba.AddCell(clPrecioNeto);

            // Llenamos la tabla con información
            int TotalDeElementos = ListaMaterialesStockDisponibleStatic.Count;
            int Contador = 0;
            foreach (var item in ListaMaterialesStockDisponibleStatic)
            {
                Contador = Contador + 1;
                if (item.Descripcion != "")
                {
                    if (TotalDeElementos == Contador)
                    {
                        clMaterial = new PdfPCell(new Phrase(item.Descripcion, UltimoRegistro));
                        clMaterial.BorderWidth = 0;

                        string Fecha = Convert.ToString(item.FechaFactura.ToShortDateString());
                        clFecha = new PdfPCell(new Phrase(Fecha, UltimoRegistro));
                        clFecha.BorderWidth = 0;

                        string Kilos = Convert.ToString(item.Cantidad);
                        clKilos = new PdfPCell(new Phrase(Kilos, UltimoRegistro));
                        clKilos.BorderWidth = 0;

                        string PrecioUnitario = Convert.ToString(item.ValorUnitario);
                        clPrecioUnitario = new PdfPCell(new Phrase(PrecioUnitario, UltimoRegistro));
                        clPrecioUnitario.BorderWidth = 0;

                        string PrecioNeto = Convert.ToString(item.PrecioNeto);
                        clPrecioNeto = new PdfPCell(new Phrase(PrecioNeto, UltimoRegistro));
                        clPrecioNeto.BorderWidth = 0;

                        tblPrueba.AddCell(clMaterial);
                        tblPrueba.AddCell(clFecha);
                        tblPrueba.AddCell(clKilos);
                        tblPrueba.AddCell(clPrecioUnitario);
                        tblPrueba.AddCell(clPrecioNeto);
                    }
                    else
                    {
                        clMaterial = new PdfPCell(new Phrase(item.Descripcion, letraContenido));
                        clMaterial.BorderWidth = 0;

                        string Fecha = Convert.ToString(item.FechaFactura.ToShortDateString());
                        clFecha = new PdfPCell(new Phrase(Fecha, letraContenido));
                        clFecha.BorderWidth = 0;

                        string Kilos = Convert.ToString(item.Cantidad);
                        clKilos = new PdfPCell(new Phrase(Kilos, letraContenido));
                        clKilos.BorderWidth = 0;

                        string PrecioUnitario = Convert.ToString(item.ValorUnitario);
                        clPrecioUnitario = new PdfPCell(new Phrase(PrecioUnitario, letraContenido));
                        clPrecioUnitario.BorderWidth = 0;

                        string PrecioNeto = Convert.ToString(item.PrecioNeto);
                        clPrecioNeto = new PdfPCell(new Phrase(PrecioNeto, letraContenido));
                        clPrecioNeto.BorderWidth = 0;

                        tblPrueba.AddCell(clMaterial);
                        tblPrueba.AddCell(clFecha);
                        tblPrueba.AddCell(clKilos);
                        tblPrueba.AddCell(clPrecioUnitario);
                        tblPrueba.AddCell(clPrecioNeto);
                    }
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
