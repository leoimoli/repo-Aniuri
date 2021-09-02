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
    public class ProveedoresNeg
    {
        public static List<Proveedores> ListaDeProveedores()
        {
            List<Entidades.Proveedores> _listaProveedores = new List<Entidades.Proveedores>();
            try
            {
                _listaProveedores = ProveedoresDao.ListarProveedores();
            }
            catch (Exception ex)
            {
            }
            return _listaProveedores;
        }
        public static List<Proveedores> BuscarProveedorPorID(int idProveedorSeleccionado)
        {
            List<Entidades.Proveedores> _listaProveedores = new List<Entidades.Proveedores>();
            try
            {
                _listaProveedores = ProveedoresDao.BuscarProveedorPorID(idProveedorSeleccionado);
            }
            catch (Exception ex)
            {
            }
            return _listaProveedores;
        }
        public static bool EditarProveedor(Proveedores proveedor, int idProveedorSeleccionado)
        {
            bool exito = false;
            try
            {
                ValidarDatos(proveedor);
                exito = ProveedoresDao.EditarProveedor(proveedor, idProveedorSeleccionado);
            }
            catch (Exception ex)
            {
            }
            return exito;
        }
        public static bool CargarProveedor(Proveedores proveedor)
        {
            bool exito = false;
            try
            {
                ValidarDatos(proveedor);
                bool ProveedorExistente = ValidarProveedorExistente(proveedor.NombreEmpresa);
                if (ProveedorExistente == true)
                {
                    const string message = "Ya existe un proveedor registrado con el nombre ingresado.";
                    const string caption = "Error";
                    var result = MessageBox.Show(message, caption,
                                                 MessageBoxButtons.OK,
                                               MessageBoxIcon.Exclamation);
                    throw new Exception();
                }
                else
                {
                    exito = ProveedoresDao.InsertProveedor(proveedor);
                }
            }
            catch (Exception ex)
            { }
            return exito;
        }
        private static bool ValidarProveedorExistente(string nombreEmpresa)
        {
            bool existe = ProveedoresDao.ValidarProveedorExistente(nombreEmpresa);
            return existe;
        }
        private static void ValidarDatos(Entidades.Proveedores _proveedor)
        {
            if (String.IsNullOrEmpty(_proveedor.NombreEmpresa))
            {
                const string message = "El campo nombre es obligatorio.";
                const string caption = "Error";
                var result = MessageBox.Show(message, caption,
                                             MessageBoxButtons.OK,
                                           MessageBoxIcon.Exclamation);
                throw new Exception();
            }
        }
        public static List<Proveedores> BuscarProvedorPorNombre(string NombreProveedor)
        {
            List<Proveedores> _listaProveedores = new List<Proveedores>();
            try
            {
                _listaProveedores = ProveedoresDao.BuscarProvedorPorNombre(NombreProveedor);
            }
            catch (Exception ex)
            {                
            }
            return _listaProveedores;
        }
    }
}
