using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Añuri.Entidades;
using MySql.Data.MySqlClient;

namespace Añuri.Dao
{
    public class ProductoDao
    {
        private static MySql.Data.MySqlClient.MySqlConnection connection = new MySqlConnection(Properties.Settings.Default.db);
        public static bool EditarProducto(Producto producto, int idProductoSeleccionado)
        {
            bool exito = false;
            connection.Close();
            connection.Open();
            string Actualizar = "EditarProducto";
            MySqlCommand cmd = new MySqlCommand(Actualizar, connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("idProducto_in", idProductoSeleccionado);       
            cmd.Parameters.AddWithValue("DescripcionProducto_in", producto.DescripcionProducto);
            cmd.Parameters.AddWithValue("TipoMedicion_in", producto.TipoMedicion);
            cmd.ExecuteNonQuery();
            exito = true;
            connection.Close();
            return exito;
        }
        public static bool InsertarProducto(Producto producto)
        {
            bool exito = false;
            connection.Close();
            connection.Open();
            string proceso = "AltaProducto";
            MySqlCommand cmd = new MySqlCommand(proceso, connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("TipoMedicion_in", producto.TipoMedicion);
            cmd.Parameters.AddWithValue("DescripcionProducto_in", producto.DescripcionProducto);
            cmd.Parameters.AddWithValue("FechaDeAlta_in", producto.FechaDeAlta);
            cmd.Parameters.AddWithValue("idUsuario_in", producto.idUsuario);
            cmd.ExecuteNonQuery();
            exito = true;
            connection.Close();
            return exito;
        }
        public static bool ValidarProductoExistente(string descripcionProducto)
        {
            connection.Close();
            bool Existe = false;
            connection.Open();
            List<Producto> lista = new List<Producto>();
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = connection;
            DataTable Tabla = new DataTable();
            MySqlParameter[] oParam = {
                                      new MySqlParameter("DescripcionProducto_in", descripcionProducto) };
            string proceso = "ValidarProductoExistente";
            MySqlDataAdapter dt = new MySqlDataAdapter(proceso, connection);
            dt.SelectCommand.CommandType = CommandType.StoredProcedure;
            dt.SelectCommand.Parameters.AddRange(oParam);
            dt.Fill(Tabla);
            if (Tabla.Rows.Count > 0)
            {
                Existe = true;
            }
            connection.Close();
            return Existe;
        }
        public static List<Producto> ListarProductos()
        {
            connection.Close();
            connection.Open();
            List<Producto> _listaProductos = new List<Producto>();
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = connection;
            DataTable Tabla = new DataTable();
            MySqlParameter[] oParam = { };
            string proceso = "ListarProductosStock";
            MySqlDataAdapter dt = new MySqlDataAdapter(proceso, connection);
            dt.SelectCommand.CommandType = CommandType.StoredProcedure;
            dt.SelectCommand.Parameters.AddRange(oParam);
            dt.Fill(Tabla);
            if (Tabla.Rows.Count > 0)
            {
                foreach (DataRow item in Tabla.Rows)
                {
                    Producto listaProducto = new Producto();
                    listaProducto.idProducto = Convert.ToInt32(item["idProducto"].ToString());
                    listaProducto.DescripcionProducto = item["DescripcionProducto"].ToString();
                    listaProducto.Stock = Convert.ToInt32(item["Stock"].ToString());
                    _listaProductos.Add(listaProducto);
                }
            }
            connection.Close();
            return _listaProductos;
        }
        public static List<Producto> ListaProductoPorDescripcion(string descripcion)
        {
            connection.Close();
            connection.Open();
            List<Producto> _listaStocks = new List<Producto>();
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = connection;
            DataTable Tabla = new DataTable();
            MySqlParameter[] oParam = { new MySqlParameter("descripcion_in", descripcion) };
            string proceso = "ListaProductoPorDescripcion";
            MySqlDataAdapter dt = new MySqlDataAdapter(proceso, connection);
            dt.SelectCommand.CommandType = CommandType.StoredProcedure;
            dt.SelectCommand.Parameters.AddRange(oParam);
            dt.Fill(Tabla);
            if (Tabla.Rows.Count > 0)
            {
                foreach (DataRow item in Tabla.Rows)
                {
                    Producto listaStock = new Producto();
                    listaStock.idProducto = Convert.ToInt32(item["idProducto"].ToString());
                    _listaStocks.Add(listaStock);
                }
            }
            connection.Close();
            return _listaStocks;
        }
        public static List<Producto> BuscarProductoPorDescripcion(string descripcion)
        {
            connection.Close();
            connection.Open();
            List<Producto> _listaStocks = new List<Producto>();
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = connection;
            DataTable Tabla = new DataTable();
            MySqlParameter[] oParam = { new MySqlParameter("descripcion_in", descripcion) };
            string proceso = "ListarProductosPorDescripcionStock";
            MySqlDataAdapter dt = new MySqlDataAdapter(proceso, connection);
            dt.SelectCommand.CommandType = CommandType.StoredProcedure;
            dt.SelectCommand.Parameters.AddRange(oParam);
            dt.Fill(Tabla);
            if (Tabla.Rows.Count > 0)
            {
                foreach (DataRow item in Tabla.Rows)
                {
                    Producto listaStock = new Producto();
                    listaStock.idProducto = Convert.ToInt32(item["idProducto"].ToString());
                    listaStock.DescripcionProducto = item["DescripcionProducto"].ToString();
                    listaStock.Stock = Convert.ToInt32(item["Stock"].ToString());
                    _listaStocks.Add(listaStock);
                }
            }
            connection.Close();
            return _listaStocks;
        }

        public static int GuardarCargaMasivaProductos(List<Producto> listaStatic)
        {
            int Exito = 0;
            foreach (var item in listaStatic)
            {
                bool ProductoExistente = Negocio.ProductoNeg.ValidarProductoExistente(item.DescripcionProducto);
                if (ProductoExistente == true)
                {
                    continue;
                }
                else
                {
                    connection.Close();
                    connection.Open();
                    string proceso = "AltaProducto";
                    MySqlCommand cmd = new MySqlCommand(proceso, connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("DescripcionProducto_in", item.DescripcionProducto);
                    cmd.Parameters.AddWithValue("FechaDeAlta_in", item.FechaDeAlta);
                    cmd.Parameters.AddWithValue("idUsuario_in", item.idUsuario);
                    cmd.ExecuteNonQuery();
                }
                Exito = 1;
            }
            connection.Close();
            return Exito;
        }

        public static string BuscarTipoMedicionPorIdProducto(int idProductoSeleccionado)
        {
            connection.Close();
            connection.Open();
            string TipoMedicion = "";
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = connection;
            DataTable Tabla = new DataTable();
            MySqlParameter[] oParam = { new MySqlParameter("idProductoSeleccionado_in", idProductoSeleccionado) };
            string proceso = "BuscarTipoMedicionPorIdProducto";
            MySqlDataAdapter dt = new MySqlDataAdapter(proceso, connection);
            dt.SelectCommand.CommandType = CommandType.StoredProcedure;
            dt.SelectCommand.Parameters.AddRange(oParam);
            dt.Fill(Tabla);
            if (Tabla.Rows.Count > 0)
            {
                foreach (DataRow item in Tabla.Rows)
                {
                    TipoMedicion = item["TipoMedicion"].ToString();
                }
            }
            connection.Close();
            return TipoMedicion;
        }
    }
}
