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
            try
            {
                _listaObras = ObrasDao.ObtenerStockDisponible(idProducto, cantidad);
            }
            catch (Exception ex)
            {
            }
            return _listaObras;
        }
    }
}
