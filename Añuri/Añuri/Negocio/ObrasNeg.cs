using Añuri.Dao;
using Añuri.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Añuri.Negocio
{
    public class ObrasNeg
    {
        public static int BuscarIdProvincia(string provincia)
        {
            int idProvincia = 0;
            try
            {
                idProvincia = ObrasDao.BuscarIdProvincia(provincia);
            }
            catch (Exception ex)
            {
            }
            return idProvincia;
        }
        public static List<Obra> BuscarInformacionLocalidad(string localidad, int idProvincia)
        {
            List<Obra> _lista = new List<Obra>();
            try
            {
                _lista = ObrasDao.BuscarInformacionLocalidad(localidad, idProvincia);
            }
            catch (Exception ex)
            {
            }
            return _lista;
        }
        public static bool EditarObra(Obra obra, int idObraSeleccionada)
        {
            bool exito = false;
            try
            {
                ValidarDatos(obra);
                exito = ObrasDao.EditarObra(obra, idObraSeleccionada);
            }
            catch (Exception ex)
            {
            }
            return exito;
        }
        private static void ValidarDatos(Obra obra)
        {
            if (String.IsNullOrEmpty(obra.NombreObra))
            {
                const string message = "El campo nombre de obra es obligatorio.";
                const string caption = "Error";
                var result = MessageBox.Show(message, caption,
                                             MessageBoxButtons.OK,
                                           MessageBoxIcon.Exclamation);
                throw new Exception();
            }
        }
        public static bool InsertObra(Obra obra)
        {
            bool exito = false;
            try
            {
                ValidarDatos(obra);
                exito = ObrasDao.InsertObra(obra);
            }
            catch (Exception ex)
            { }
            return exito;
        }
        public static List<Obra> ListaDeObras()
        {
            List<Entidades.Obra> _listaObras = new List<Entidades.Obra>();
            try
            {
                _listaObras = ObrasDao.ListaDeObras();
            }
            catch (Exception ex)
            {
            }
            return _listaObras;
        }
        public static List<Obra> BuscarObraPorID(int idObraSeleccionada)
        {
            List<Obra> _listaObra = new List<Obra>();
            try
            {
                _listaObra = ObrasDao.BuscarObraPorID(idObraSeleccionada);
            }
            catch (Exception ex)
            {
            }
            return _listaObra;
        }
        public static List<Obra> ListaDeObrasPorNombre(string obra)
        {
            List<Entidades.Obra> _listaObras = new List<Entidades.Obra>();
            try
            {
                _listaObras = ObrasDao.ListaDeObrasPorNombre(obra);
            }
            catch (Exception ex)
            {
            }
            return _listaObras;
        }
        public static List<Stock> VerificarDisponibilidadDeMaterial(string material)
        {
            List<Entidades.Stock> _listaObras = new List<Entidades.Stock>();
            try
            {
                _listaObras = ObrasDao.VerificarDisponibilidadDeMaterial(material);
            }
            catch (Exception ex)
            {
            }
            return _listaObras;
        }
        public static List<Stock> ObtenerStockDisponible(int idProducto, int cantidadIngresada)
        {
            List<Entidades.Stock> _listaObras = new List<Entidades.Stock>();
            List<Stock> listaStock = new List<Stock>();
            List<Stock> listaStockAcumuladora = new List<Stock>();
            List<int> listaIdEntrada = new List<int>();
            int Entrada = 0;
            int Salidas = 0;
            int ValorEnStock = 0;
            int stockDiferencial = 0;
            bool exito = false;
            try
            {
                listaIdEntrada = BuscarIdEntrada(idProducto);
                if (listaIdEntrada.Count > 0)
                {
                    int contador = 1;
                    foreach (var idEntrada in listaIdEntrada)
                    {
                        listaStock = ObrasDao.ObtenerMovientosStock(idEntrada, idProducto);
                        if (listaStock.Count > 0)
                        {
                            if (idEntrada > 0)
                            {
                                exito = ObrasDao.ReservarStockSeleccionada(idProducto);
                            }
                            if (exito == true)
                            {
                                foreach (var item in listaStock)
                                {
                                    if (item.TipoMovimiento == "E")
                                    {
                                        Entrada = Entrada + item.Cantidad;
                                    }
                                    if (item.TipoMovimiento == "S")
                                    {
                                        Salidas = Salidas + item.Cantidad;
                                    }
                                }
                                ValorEnStock = Entrada - Salidas;
                                foreach (var item in listaStock)
                                {
                                    Stock stock = new Stock();
                                    stock.idProducto = Convert.ToInt32(item.idProducto);
                                    stock.Descripcion = item.Descripcion;
                                    stock.ValorUnitario = Convert.ToDecimal(item.ValorUnitario);
                                    stock.PrecioNeto = Convert.ToDecimal(item.PrecioNeto);
                                    stock.Cantidad = ValorEnStock;
                                    stock.TipoMovimiento = item.TipoMovimiento;
                                    stock.idMovimientoEntrada = idEntrada;
                                    listaStockAcumuladora.Add(stock);
                                    break;
                                }
                                Entrada = 0;
                                Salidas = 0;
                            }
                        }
                        if (ValorEnStock >= cantidadIngresada && contador <= 1)
                        {
                            _listaObras = ObrasDao.ObtenerProductoDisponible(idProducto, cantidadIngresada);
                            ValorEnStock = 0;
                            break;
                        }
                        else
                        {
                            if (ValorEnStock < cantidadIngresada)
                            {
                                stockDiferencial = cantidadIngresada - ValorEnStock;
                            }
                            ValorEnStock = 0;
                        }
                        contador = contador + 1;
                    }
                    if (_listaObras.Count == 0 && listaStockAcumuladora.Count > 0)
                    {
                        int StockAcumulado = 0;
                        int StockSumado = 0;
                        foreach (var Valor in listaStockAcumuladora)
                        {
                            StockSumado = StockSumado + Valor.Cantidad;
                            if (StockSumado <= cantidadIngresada)
                            {
                                Stock _lista = new Stock();
                                _lista.idProducto = Convert.ToInt32(Valor.idProducto);
                                _lista.Descripcion = Valor.Descripcion;
                                _lista.ValorUnitario = Convert.ToDecimal(Valor.ValorUnitario);
                                _lista.PrecioNeto = Convert.ToDecimal(Valor.PrecioNeto);
                                _lista.Cantidad = Convert.ToInt32(Valor.Cantidad);
                                _lista.idMovimientoEntrada = Convert.ToInt32(Valor.idMovimientoEntrada);
                                StockAcumulado = StockAcumulado + Valor.Cantidad;
                                _listaObras.Add(_lista);
                            }
                            else
                            {
                                stockDiferencial = cantidadIngresada - StockAcumulado;
                                if (stockDiferencial <= Valor.Cantidad)
                                {
                                    Stock _lista = new Stock();
                                    _lista.idProducto = Convert.ToInt32(Valor.idProducto);
                                    _lista.Descripcion = Valor.Descripcion;
                                    _lista.ValorUnitario = Convert.ToDecimal(Valor.ValorUnitario);
                                    _lista.PrecioNeto = Convert.ToDecimal(Valor.PrecioNeto);
                                    _lista.idMovimientoEntrada = Convert.ToInt32(Valor.idMovimientoEntrada);
                                    _lista.Cantidad = stockDiferencial;
                                    //StockAcumulado = StockAcumulado + Valor.Cantidad;
                                    _listaObras.Add(_lista);
                                    break;
                                }
                            }
                        }

                    }
                }
                else
                {
                    const string message2 = "Atención: El producto ingresado en estos momentos se encuentra siendo utilizado en otra obra. Aguarde un momento y vuelva intentarlo.";
                    const string caption2 = "Atención";
                    var result2 = MessageBox.Show(message2, caption2,
                                                 MessageBoxButtons.OK,
                                                 MessageBoxIcon.Exclamation);
                }
            }
            catch (Exception ex)
            { }


            return _listaObras;
        }
        private static List<int> BuscarIdEntrada(int idProducto)
        {
            List<int> listaIdEntrada = new List<int>();
            int idEntrada = 0;
            bool exito = false;
            try
            {
                listaIdEntrada = ObrasDao.ObtenerEntradaAbierta(idProducto);
            }
            catch (Exception ex)
            {
            }
            return listaIdEntrada;
        }
        public static bool LiberarSotckReservado(List<int> ListaIdProd)
        {
            bool exito = false;
            exito = ObrasDao.LiberarSotckReservado(ListaIdProd);
            return exito;
        }
        public static bool GuardarDetalleObra(List<Stock> stockObra, int idObraSeleccionada)
        {
            bool exito = false;
            try
            {
                exito = StockDao.InsertSalidaStock(stockObra, idObraSeleccionada);
            }
            catch (Exception ex)
            { }
            return exito;
        }
    }
}
