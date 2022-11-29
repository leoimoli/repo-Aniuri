using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Añuri.Dao;
using Añuri.Entidades;

namespace Añuri.Negocio
{
    public class StockNeg
    {
        public static bool CargarlistaStock(List<Stock> listaStock)
        {
            bool exito = false;
            try
            {
                exito = StockDao.InsertarStock(listaStock);
            }
            catch (Exception ex)
            { }
            return exito;
        }
        public static List<Stock> ListarMovimientosStock(int idProductoSeleccionado)
        {
            List<Stock> _lista = new List<Stock>();
            try
            {
                _lista = StockDao.ListarMovimientosStockPorProducto(idProductoSeleccionado);
            }
            catch (Exception ex)
            {
            }
            return _lista;
        }

        public static List<Stock> ListarMovimientosStockInventarioPorFecha(DateTime fecha)
        {
            List<Stock> _lista = new List<Stock>();
            try
            {
                _lista = StockDao.ListarMovimientosStockInventarioPorFecha(fecha);
            }
            catch (Exception ex)
            {
            }
            return _lista;
        }

        public static List<Stock> ListarMovimientosStockPorFecha(int idProductoSeleccionado, DateTime fechaDesde, DateTime fechaHasta)
        {
            List<Stock> _lista = new List<Stock>();
            try
            {
                _lista = StockDao.ListarMovimientosStockPorFecha(idProductoSeleccionado, fechaDesde, fechaHasta);
            }
            catch (Exception ex)
            {
            }
            return _lista;
        }

        public static List<Stock> ListarStockDisponible(int idProductoSeleccionado)
        {
            List<Stock> _lista = new List<Stock>();
            try
            {
                _lista = StockDao.ListarStockDisponible(idProductoSeleccionado);
            }
            catch (Exception ex)
            {
            }
            return _lista;
        }

        public static List<Stock> ListarMovimientosStockPorFechaTipoMovimiento(int idProductoSeleccionado, DateTime fechaDesde, DateTime fechaHasta, string tipoMovimiento)
        {
            List<Stock> _lista = new List<Stock>();
            try
            {
                _lista = StockDao.ListarMovimientosStockPorFechaTipoMovimiento(idProductoSeleccionado, fechaDesde, fechaHasta, tipoMovimiento);
            }
            catch (Exception ex)
            {
            }
            return _lista;
        }

        public static List<Stock> ListarInventarioMaterialesPesos(string año, string material)
        {
            List<Stock> _lista = new List<Stock>();
            try
            {
                if (año != "")
                {
                    if (material != "")
                    { _lista = StockDao.ListarInventarioMaterialesPesosPorAñoPorMaterial(año, material); }
                    else
                    {
                        _lista = StockDao.ListarInventarioMaterialesPesosPorAño(año);
                    }
                }
            }
            catch (Exception ex)
            {
            }
            return _lista;
        }

        public static List<Stock> ListarSaldoInicialInventarioMaterialesPorKilos(string año, string material)
        {
            List<Stock> _lista = new List<Stock>();
            try
            {
                if (año != "")
                {
                    if (material != "")
                    { _lista = StockDao.ListarSaldoInicialInventarioMaterialesPorKilosPorMaterial(año, material); }
                    else
                    {
                        _lista = StockDao.ListarSaldoInicialInventarioMaterialesPorKilos(año);
                    }
                }
            }
            catch (Exception ex)
            {
            }
            return _lista;
        }

        public static List<Stock> ListarInventarioMaterialesPorKilos(string año, string material)
        {
            List<Stock> _lista = new List<Stock>();
            try
            {
                if (año != "")
                {
                    if (material != "")
                    { _lista = StockDao.ListarInventarioMaterialesPorKilosPorMaterial(año, material); }
                    else
                    {
                        _lista = StockDao.ListarInventarioMaterialesPorKilos(año);
                    }
                }
            }
            catch (Exception ex)
            {
            }
            return _lista;
        }
    
        public static decimal ListarSaldoInicial(int id, string año)
        {
            decimal valorInicial = 0;
            try
            {
                List<Stock> _lista = new List<Stock>();
                string añoFuturo = (int.Parse(año) + 1).ToString();
                _lista = StockDao.ListarSaldoInicial(id, año);

                foreach (var item in _lista)
                {
                    if (item.TipoMovimiento == "E" && item.FechaMovimiento.Year != int.Parse(año))
                    {
                        valorInicial = valorInicial + item.PrecioNeto;
                    }
                    if (item.TipoMovimiento == "S" && item.FechaMovimiento.Year != int.Parse(año))
                    {
                        valorInicial = valorInicial - item.PrecioNeto;
                    }
                }
            }
            catch (Exception ex)
            { }
            return valorInicial;
        }
       

        public static List<ResultadoGrillaEnPesos> ListarIdMateriales()
        {
            List<ResultadoGrillaEnPesos> resultado = new List<ResultadoGrillaEnPesos>();
            List<Stock> _lista = new List<Stock>();
            try
            {
                _lista = StockDao.ListarIdMateriales();
                foreach (var item in _lista)
                {
                    if(!resultado.Any(x => x.idProducto == item.idProducto))
                    {
                        ResultadoGrillaEnPesos nuevo = new ResultadoGrillaEnPesos();
                        nuevo.nombre = item.Descripcion;
                        nuevo.idProducto = item.idProducto;
                        nuevo.movimientos = new List<Stock>();
                        nuevo.movimientos.Add(item);
                        resultado.Add(nuevo);
                    }
                    else
                    {
                        resultado.FirstOrDefault(x => x.idProducto == item.idProducto).movimientos.Add(item);
                    }
                }
            }
            catch (Exception ex)
            {
            }
            return resultado;
        }

        public static List<Stock> ListaStockFaltante()
        {
            List<Stock> _lista = new List<Stock>();
            try
            {
                _lista = StockDao.ListaStockFaltante();
            }
            catch (Exception ex)
            {
            }
            return _lista;
        }
        public static List<Stock> ListarSaldoInicialInventarioMaterialesPesos(string año, string material)
        {
            List<Stock> _lista = new List<Stock>();
            try
            {
                if (año != "")
                {
                    if (material != "")
                    { _lista = StockDao.ListarSaldoInicialInventarioMaterialesPesosPorMaterial(año, material); }
                    else
                    {
                        _lista = StockDao.ListarSaldoInicialInventarioMaterialesPesos(año);
                    }
                }
            }
            catch (Exception ex)
            {
            }
            return _lista;
        }
        public static List<Stock> ListarMovimientosStockPorTipoMovimiento(int idProductoSeleccionado, string tipoMovimiento)
        {
            List<Stock> _lista = new List<Stock>();
            try
            {
                _lista = StockDao.ListarMovimientosStockPorTipoMovimiento(idProductoSeleccionado, tipoMovimiento);
            }
            catch (Exception ex)
            {
            }
            return _lista;
        }

        public static List<Stock> ListarMovimientosStockDisponiblePorFecha(int idProductoSeleccionado, DateTime fechaDesde, DateTime fechaHasta)
        {
            List<Stock> _lista = new List<Stock>();
            try
            {
                _lista = StockDao.ListarMovimientosStockDisponiblePorFecha(idProductoSeleccionado, fechaDesde, fechaHasta);
            }
            catch (Exception ex)
            {
            }
            return _lista;
        }
    }
}
