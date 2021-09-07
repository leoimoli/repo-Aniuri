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
    public class ProductoNeg
    {
        public static bool EditarProducto(Producto producto, int idProductoSeleccionado)
        {
            bool exito = false;
            try
            {
                ValidarDatos(producto);
                exito = ProductoDao.EditarProducto(producto, idProductoSeleccionado);
            }
            catch (Exception ex)
            {
            }
            return exito;
        }
        private static void ValidarDatos(Producto producto)
        {
            if (String.IsNullOrEmpty(producto.DescripcionProducto))
            {

                const string message = "El campo Descripción es obligatorio.";
                const string caption = "Error";
                var result = MessageBox.Show(message, caption,
                                             MessageBoxButtons.OK,
                                           MessageBoxIcon.Exclamation);
                throw new Exception();
            }
        }
        internal static bool CargarProducto(Producto producto)
        {
            bool exito = false;
            try
            {
                ValidarDatos(producto);
                bool UsuarioExistente = ValidarProductoExistente(producto.DescripcionProducto);
                if (UsuarioExistente == true)
                {
                    const string message = "Ya existe un producto registrado la descripción ingresada.";
                    const string caption = "Error";
                    var result = MessageBox.Show(message, caption,
                                                 MessageBoxButtons.OK,
                                               MessageBoxIcon.Exclamation);
                    throw new Exception();
                }
                else
                {
                    exito = ProductoDao.InsertarProducto(producto);
                }
            }
            catch (Exception ex)
            {

            }
            return exito;
        }
        private static bool ValidarProductoExistente(string descripcionProducto)
        {
            bool existe = ProductoDao.ValidarProductoExistente(descripcionProducto);
            return existe;
        }
        public static List<Producto> ListaDeProductos()
        {
            List<Producto> _listaProductos = new List<Producto>();
            try
            {
                _listaProductos = ProductoDao.ListarProductos();
            }
            catch (Exception ex)
            {
            }
            return _listaProductos;
        }
        public static List<Producto> ListaProductoPorDescripcion(string descripcion)
        {
            List<Producto> _listaStock = new List<Producto>();
            try
            {
                _listaStock = ProductoDao.ListaProductoPorDescripcion(descripcion);
            }
            catch (Exception ex)
            {
            }
            return _listaStock;
        }
        public static List<Producto> BuscarProductoPorDescripcion(string descripcion)
        {
            List<Producto> _listaStock = new List<Producto>();
            try
            {
                _listaStock = ProductoDao.BuscarProductoPorDescripcion(descripcion);
            }
            catch (Exception ex)
            {
            }
            return _listaStock;
        }
    }
}
