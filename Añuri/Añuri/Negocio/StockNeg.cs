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
    }
}
