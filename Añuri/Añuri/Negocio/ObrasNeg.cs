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
        public static List<Stock> ObtenerStockDisponible(int idProducto, int cantidad)
        {
            List<Entidades.Stock> _listaObras = new List<Entidades.Stock>();
            List<Stock> listaStock = new List<Stock>();
            List<Stock> listaStockAcumuladora = new List<Stock>();
            List<int> listaIdEntrada = new List<int>();
            int Entrada = 0;
            int Salidas = 0;
            int ValorEnStock = 0;
            //int idEntrada = 0;
            //if (idEntrada > 0)
            //{
            //    exito = ObrasDao.ReservarEntradaSeleccionada(idEntrada);
            //}
            //if (exito == false)
            //{ idEntrada = 0; }

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
                                Stock stock = new Stock();
                                stock.Cantidad = item.Cantidad;
                                stock.TipoMovimiento = item.TipoMovimiento;
                                listaStockAcumuladora.Add(stock);
                            }
                            ValorEnStock = Entrada - Salidas;
                            Entrada = 0;
                            Salidas = 0;
                        }
                        if (ValorEnStock > cantidad && contador <= 1)
                        {
                            _listaObras = ObrasDao.ObtenerProductoDisponible(idEntrada);
                            ValorEnStock = 0;
                            break;
                        }
                        else
                        {
                            ValorEnStock = 0;
                        }
                        contador = contador + 1;
                    }
                    if (_listaObras.Count == 0 && listaStockAcumuladora.Count > 0)
                    {
                        foreach (var Valor in listaStockAcumuladora)
                        {
                          
                        }
                        //_listaObras = ObrasDao.ObtenerProductoDisponible(idEntrada);
                    }
                }
                else
                {

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
    }
}
