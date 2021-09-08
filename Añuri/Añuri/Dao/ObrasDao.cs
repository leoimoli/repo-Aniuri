using Añuri.Entidades;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Añuri.Dao
{
    public class ObrasDao
    {
        private static MySql.Data.MySqlClient.MySqlConnection connection = new MySqlConnection(Properties.Settings.Default.db);
        public static int BuscarIdProvincia(string provincia)
        {
            connection.Close();
            connection.Open();
            int idProvincia = 0;
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = connection;
            DataTable Tabla = new DataTable();
            MySqlParameter[] oParam = { new MySqlParameter("provincia_in", provincia) };
            string proceso = "BuscarIdProvincia";
            MySqlDataAdapter dt = new MySqlDataAdapter(proceso, connection);
            dt.SelectCommand.CommandType = CommandType.StoredProcedure;
            dt.SelectCommand.Parameters.AddRange(oParam);
            dt.Fill(Tabla);
            if (Tabla.Rows.Count > 0)
            {
                foreach (DataRow item in Tabla.Rows)
                {
                    idProvincia = Convert.ToInt32(item["idProvincia"].ToString());
                }
            }
            connection.Close();
            return idProvincia;
        }
        public static bool EditarObra(Obra obra, int idObraSeleccionada)
        {
            bool exito = false;
            connection.Close();
            connection.Open();
            string Actualizar = "EditarObra";
            MySqlCommand cmd = new MySqlCommand(Actualizar, connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("idObraSeleccionada_in", idObraSeleccionada);
            cmd.Parameters.AddWithValue("NombreObra_in", obra.NombreObra);
            cmd.Parameters.AddWithValue("Contacto_in", obra.Contacto);
            cmd.Parameters.AddWithValue("Email_in", obra.Email);
            cmd.Parameters.AddWithValue("idProvincia_in", obra.idProvincia);
            cmd.Parameters.AddWithValue("idLocalidad_in", obra.idLocalidad);
            cmd.Parameters.AddWithValue("Calle_in", obra.Calle);
            cmd.Parameters.AddWithValue("Altura_in", obra.Altura);
            cmd.Parameters.AddWithValue("Telefono_in", obra.Telefono);
            cmd.Parameters.AddWithValue("FechaAlta_in", obra.FechaDeAlta);
            cmd.Parameters.AddWithValue("idUsuario_in", obra.idUsuario);
            cmd.ExecuteNonQuery();
            exito = true;
            connection.Close();
            return exito;
        }
        public static List<Obra> BuscarInformacionLocalidad(string localidad, int idProvincia)
        {
            connection.Close();
            connection.Open();
            List<Obra> _lista = new List<Obra>();
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = connection;
            DataTable Tabla = new DataTable();
            MySqlParameter[] oParam = { new MySqlParameter("localidad_in", localidad),
            new MySqlParameter("idProvincia_in", idProvincia)};
            string proceso = "BuscarInformacionLocalidad";
            MySqlDataAdapter dt = new MySqlDataAdapter(proceso, connection);
            dt.SelectCommand.CommandType = CommandType.StoredProcedure;
            dt.SelectCommand.Parameters.AddRange(oParam);
            dt.Fill(Tabla);
            if (Tabla.Rows.Count > 0)
            {
                foreach (DataRow item in Tabla.Rows)
                {
                    Obra listaObra = new Obra();
                    listaObra.idLocalidad = Convert.ToInt32(item["idLocalidad"].ToString());
                    listaObra.NombreLocalidad = item["Nombre"].ToString();
                    listaObra.CodigoPostalLocalidad = item["CodigoPostal"].ToString();
                    _lista.Add(listaObra);
                }
            }
            connection.Close();
            return _lista;
        }
        public static List<Stock> ObtenerStockDisponible(int idProducto, int cantidad)
        {
            connection.Close();
            connection.Open();
            List<Stock> _listaStocks = new List<Stock>();
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = connection;
            DataTable Tabla = new DataTable();
            MySqlParameter[] oParam = { new MySqlParameter("idProducto_in", idProducto) };
            string proceso = "ObtenerStockDisponible";
            MySqlDataAdapter dt = new MySqlDataAdapter(proceso, connection);
            dt.SelectCommand.CommandType = CommandType.StoredProcedure;
            dt.SelectCommand.Parameters.AddRange(oParam);
            dt.Fill(Tabla);
            if (Tabla.Rows.Count > 0)
            {
                foreach (DataRow item in Tabla.Rows)
                {
                    Stock listaStock = new Stock();
                    listaStock.idProducto = Convert.ToInt32(item["idProducto"].ToString());
                    listaStock.Cantidad = Convert.ToInt32(item["Cantidad"].ToString());
                    listaStock.Cantidad = Convert.ToInt32(item["PrecioUnitario"].ToString());
                    _listaStocks.Add(listaStock);
                }
            }
            connection.Close();
            return _listaStocks;
        }
        public static List<Stock> VerificarDisponibilidadDeMaterial(string material)
        {
            connection.Close();
            connection.Open();
            List<Stock> _listaStocks = new List<Stock>();
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = connection;
            DataTable Tabla = new DataTable();
            MySqlParameter[] oParam = { new MySqlParameter("material_in", material) };
            string proceso = "VerificarDisponibilidadDeMaterial";
            MySqlDataAdapter dt = new MySqlDataAdapter(proceso, connection);
            dt.SelectCommand.CommandType = CommandType.StoredProcedure;
            dt.SelectCommand.Parameters.AddRange(oParam);
            dt.Fill(Tabla);
            if (Tabla.Rows.Count > 0)
            {
                foreach (DataRow item in Tabla.Rows)
                {
                    Stock listaStock = new Stock();
                    listaStock.idProducto = Convert.ToInt32(item["idProducto"].ToString());
                    listaStock.Cantidad = Convert.ToInt32(item["Cantidad"].ToString());
                    _listaStocks.Add(listaStock);
                }
            }
            connection.Close();
            return _listaStocks;
        }

        public static List<Obra> ListaDeObrasPorNombre(string obra)
        {
            connection.Close();
            connection.Open();
            List<Obra> _listaProveedores = new List<Obra>();
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = connection;
            DataTable Tabla = new DataTable();
            MySqlParameter[] oParam = { new MySqlParameter("obra_in", obra) };
            string proceso = "ListaDeObrasPorNombre";
            MySqlDataAdapter dt = new MySqlDataAdapter(proceso, connection);
            dt.SelectCommand.CommandType = CommandType.StoredProcedure;
            dt.SelectCommand.Parameters.AddRange(oParam);
            dt.Fill(Tabla);
            DataSet ds = new DataSet();
            if (Tabla.Rows.Count > 0)
            {

                foreach (DataRow item in Tabla.Rows)
                {
                    Obra listaObra = new Obra();
                    listaObra.idObra = Convert.ToInt32(item["idObra"].ToString());
                    listaObra.NombreObra = item["NombreObra"].ToString();
                    listaObra.Contacto = item["NombreContacto"].ToString();
                    listaObra.Email = item["Email"].ToString();
                    listaObra.NombreProvincia = item["Provincia"].ToString();
                    listaObra.NombreLocalidad = item["Localidad"].ToString();
                    listaObra.Calle = item["Calle"].ToString();
                    listaObra.Altura = item["Altura"].ToString();
                    listaObra.Telefono = item["Telefono"].ToString();
                    listaObra.Estado = Convert.ToInt32(item["Estado"].ToString());
                    _listaProveedores.Add(listaObra);
                }
            }
            connection.Close();
            return _listaProveedores;
        }
        public static List<Obra> BuscarObraPorID(int idObraSeleccionada)
        {
            connection.Close();
            connection.Open();
            List<Obra> lista = new List<Obra>();
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = connection;
            DataTable Tabla = new DataTable();
            MySqlParameter[] oParam = {
                                      new MySqlParameter("idObraSeleccionada_in", idObraSeleccionada)};
            string proceso = "BuscarObraPorID";
            MySqlDataAdapter dt = new MySqlDataAdapter(proceso, connection);
            dt.SelectCommand.CommandType = CommandType.StoredProcedure;
            dt.SelectCommand.Parameters.AddRange(oParam);
            dt.Fill(Tabla);
            //Dat
            if (Tabla.Rows.Count > 0)
            {
                foreach (DataRow item in Tabla.Rows)
                {
                    Obra listaObra = new Obra();
                    listaObra.idObra = Convert.ToInt32(item["idObra"].ToString());
                    listaObra.NombreObra = item["NombreObra"].ToString();
                    listaObra.Contacto = item["NombreContacto"].ToString();
                    listaObra.Email = item["Email"].ToString();
                    listaObra.NombreProvincia = item["Provincia"].ToString();
                    listaObra.NombreLocalidad = item["Localidad"].ToString();
                    listaObra.Calle = item["Calle"].ToString();
                    listaObra.Altura = item["Altura"].ToString();
                    listaObra.Telefono = item["Telefono"].ToString();
                    lista.Add(listaObra);
                }
            }
            connection.Close();
            return lista;
        }
        public static List<Obra> ListaDeObras()
        {
            connection.Close();
            connection.Open();
            List<Obra> _listaProveedores = new List<Obra>();
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = connection;
            DataTable Tabla = new DataTable();
            MySqlParameter[] oParam = { };
            string proceso = "ListaDeObras";
            MySqlDataAdapter dt = new MySqlDataAdapter(proceso, connection);
            dt.SelectCommand.CommandType = CommandType.StoredProcedure;
            dt.SelectCommand.Parameters.AddRange(oParam);
            dt.Fill(Tabla);
            DataSet ds = new DataSet();
            if (Tabla.Rows.Count > 0)
            {

                foreach (DataRow item in Tabla.Rows)
                {
                    Obra listaObra = new Obra();
                    listaObra.idObra = Convert.ToInt32(item["idObra"].ToString());
                    listaObra.NombreObra = item["NombreObra"].ToString();
                    listaObra.Contacto = item["NombreContacto"].ToString();
                    listaObra.Email = item["Email"].ToString();
                    listaObra.NombreProvincia = item["Provincia"].ToString();
                    listaObra.NombreLocalidad = item["Localidad"].ToString();
                    listaObra.Calle = item["Calle"].ToString();
                    listaObra.Altura = item["Altura"].ToString();
                    listaObra.Telefono = item["Telefono"].ToString();
                    listaObra.Estado = Convert.ToInt32(item["Estado"].ToString());
                    _listaProveedores.Add(listaObra);
                }
            }
            connection.Close();
            return _listaProveedores;
        }
        public static bool InsertObra(Obra obra)
        {
            bool exito = false;
            connection.Close();
            connection.Open();
            string proceso = "AltaObra";
            MySqlCommand cmd = new MySqlCommand(proceso, connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("NombreObra_in", obra.NombreObra);
            cmd.Parameters.AddWithValue("Contacto_in", obra.Contacto);
            cmd.Parameters.AddWithValue("Email_in", obra.Email);
            cmd.Parameters.AddWithValue("idProvincia_in", obra.idProvincia);
            cmd.Parameters.AddWithValue("idLocalidad_in", obra.idLocalidad);
            cmd.Parameters.AddWithValue("Calle_in", obra.Calle);
            cmd.Parameters.AddWithValue("Altura_in", obra.Altura);
            cmd.Parameters.AddWithValue("Telefono_in", obra.Telefono);
            cmd.Parameters.AddWithValue("FechaDeAlta_in", obra.FechaDeAlta);
            cmd.Parameters.AddWithValue("idUsuario_in", obra.idUsuario);
            cmd.Parameters.AddWithValue("Estado_in", 1);
            cmd.ExecuteNonQuery();
            exito = true;
            connection.Close();
            return exito;
        }
    }
}
