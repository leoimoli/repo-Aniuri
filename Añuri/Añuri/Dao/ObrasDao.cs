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

        public static List<Stock> GraficoMaterialesEnPesos(int idObraSeleccionada)
        {
            List<Stock> listaMateriales = new List<Stock>();
            connection.Close();
            connection.Open();
            List<Stock> _listaStocks = new List<Stock>();
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = connection;
            DataTable Tabla = new DataTable();
            MySqlParameter[] oParam = { new MySqlParameter("idObraSeleccionada_in", idObraSeleccionada) };
            string proceso = "GraficoMaterialesEnPesos";
            MySqlDataAdapter dt = new MySqlDataAdapter(proceso, connection);
            dt.SelectCommand.CommandType = CommandType.StoredProcedure;
            dt.SelectCommand.Parameters.AddRange(oParam);
            dt.Fill(Tabla);
            if (Tabla.Rows.Count > 0)
            {
                foreach (DataRow item in Tabla.Rows)
                {
                    Stock _listaMateriales = new Stock();
                    _listaMateriales.Cantidad = Convert.ToInt32(item["Cantidad"].ToString());
                    _listaMateriales.PrecioNeto = Convert.ToDecimal(item["PrecioNeto"].ToString());
                    _listaMateriales.idProducto = Convert.ToInt32(item["idProducto"].ToString());
                    _listaMateriales.Descripcion = item["DescripcionProducto"].ToString();
                    listaMateriales.Add(_listaMateriales);
                }
            }
            connection.Close();
            return listaMateriales;
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
            string proceso = "ObtenerEntradaAbierta";
            MySqlDataAdapter dt = new MySqlDataAdapter(proceso, connection);
            dt.SelectCommand.CommandType = CommandType.StoredProcedure;
            dt.SelectCommand.Parameters.AddRange(oParam);
            dt.Fill(Tabla);
            if (Tabla.Rows.Count > 0)
            {
                foreach (DataRow item in Tabla.Rows)
                {

                    int idEntrada = Convert.ToInt32(item["idEntrada"].ToString());

                }
            }
            connection.Close();
            return _listaStocks;
        }
        public static bool ReservarStockSeleccionada(int idProducto)
        {
            bool exito = false;
            connection.Close();
            connection.Open();
            string Actualizar = "ReservarStockSeleccionada";
            MySqlCommand cmd = new MySqlCommand(Actualizar, connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("idProducto_in", idProducto);
            cmd.Parameters.AddWithValue("Estado_in", 3);
            cmd.ExecuteNonQuery();
            exito = true;
            connection.Close();
            return exito;
        }
        public static List<Stock> ObtenerProductoDisponible(int idProducto, int cantidadIngresada, int EstadoEntrada, int idEntrada)
        {
            connection.Close();
            connection.Open();
            List<Stock> _listaStocks = new List<Stock>();
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = connection;
            DataTable Tabla = new DataTable();
            MySqlParameter[] oParam = { new MySqlParameter("idProducto_in", idProducto) };
            string proceso = "ObtenerProductoDisponible";
            MySqlDataAdapter dt = new MySqlDataAdapter(proceso, connection);
            dt.SelectCommand.CommandType = CommandType.StoredProcedure;
            dt.SelectCommand.Parameters.AddRange(oParam);
            dt.Fill(Tabla);
            if (Tabla.Rows.Count > 0)
            {
                foreach (DataRow item in Tabla.Rows)
                {
                    Stock lista = new Stock();
                    lista.idProducto = Convert.ToInt32(item["idProducto"].ToString());
                    lista.Descripcion = item["NombreProducto"].ToString();
                    lista.ValorUnitario = Convert.ToDecimal(item["PrecioUnitario"].ToString());
                    lista.PrecioNeto = Convert.ToDecimal(item["PrecioNeto"].ToString());
                    lista.Cantidad = cantidadIngresada;
                    lista.EstadoEntrada = EstadoEntrada;
                    lista.idMovimientoEntrada = idEntrada;
                    _listaStocks.Add(lista);
                }
            }
            connection.Close();
            return _listaStocks;
        }
        public static List<Stock> ObtenerMovientosStock(int idEntrada, int idProducto)
        {
            connection.Close();
            connection.Open();
            List<Stock> _listaStocks = new List<Stock>();
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = connection;
            DataTable Tabla = new DataTable();
            MySqlParameter[] oParam = { new MySqlParameter("idEntrada_in", idEntrada),
            new MySqlParameter("idProducto_in", idProducto)};
            string proceso = "ObtenerMovientosStock";
            MySqlDataAdapter dt = new MySqlDataAdapter(proceso, connection);
            dt.SelectCommand.CommandType = CommandType.StoredProcedure;
            dt.SelectCommand.Parameters.AddRange(oParam);
            dt.Fill(Tabla);
            if (Tabla.Rows.Count > 0)
            {
                foreach (DataRow item in Tabla.Rows)
                {
                    Stock lista = new Stock();
                    lista.idProducto = Convert.ToInt32(item["idProducto"].ToString());
                    lista.Descripcion = item["NombreProducto"].ToString();
                    lista.Cantidad = Convert.ToInt32(item["Cantidad"].ToString());
                    lista.TipoMovimiento = item["TipoMovimiento"].ToString();
                    lista.ValorUnitario = Convert.ToDecimal(item["PrecioUnitario"].ToString());
                    lista.PrecioNeto = Convert.ToDecimal(item["PrecioNeto"].ToString());
                    lista.idMovimientoEntrada = Convert.ToInt32(item["idMovimientoEntradaSalida"].ToString());
                    _listaStocks.Add(lista);
                }
            }
            connection.Close();
            return _listaStocks;
        }
        public static List<int> ObtenerEntradaAbierta(int idProducto)
        {
            List<int> listaIdEntrada = new List<int>();
            connection.Close();
            connection.Open();
            List<Stock> _listaStocks = new List<Stock>();
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = connection;
            DataTable Tabla = new DataTable();
            MySqlParameter[] oParam = { new MySqlParameter("idProducto_in", idProducto) };
            string proceso = "ObtenerEntradaAbierta";
            MySqlDataAdapter dt = new MySqlDataAdapter(proceso, connection);
            dt.SelectCommand.CommandType = CommandType.StoredProcedure;
            dt.SelectCommand.Parameters.AddRange(oParam);
            dt.Fill(Tabla);
            if (Tabla.Rows.Count > 0)
            {
                foreach (DataRow item in Tabla.Rows)
                {
                    int idEntrada = Convert.ToInt32(item["idEntrada"].ToString());
                    listaIdEntrada.Add(idEntrada);
                }
            }
            connection.Close();
            return listaIdEntrada;
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

        public static List<Stock> ListaMaterialesExistentesPorFecha(int idObraSeleccionada, DateTime fechaDesde, DateTime fechaHasta)
        {
            List<Stock> listaMateriales = new List<Stock>();
            connection.Close();
            connection.Open();
            List<Stock> _listaStocks = new List<Stock>();
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = connection;
            DataTable Tabla = new DataTable();
            MySqlParameter[] oParam = { new MySqlParameter("idObraSeleccionada_in", idObraSeleccionada),
            new MySqlParameter("FechaDesde_in", fechaDesde),
            new MySqlParameter("FechaHasta_in", fechaHasta)};
            string proceso = "ListaMaterialesExistentesPorFecha";
            MySqlDataAdapter dt = new MySqlDataAdapter(proceso, connection);
            dt.SelectCommand.CommandType = CommandType.StoredProcedure;
            dt.SelectCommand.Parameters.AddRange(oParam);
            dt.Fill(Tabla);
            if (Tabla.Rows.Count > 0)
            {
                foreach (DataRow item in Tabla.Rows)
                {
                    Stock _listaMateriales = new Stock();
                    _listaMateriales.Cantidad = Convert.ToInt32(item["Cantidad"].ToString());
                    // _listaMateriales.ValorUnitario = Convert.ToDecimal(item["PrecioUnitario"].ToString());
                    _listaMateriales.PrecioNeto = Convert.ToDecimal(item["PrecioNeto"].ToString());
                    _listaMateriales.idProducto = Convert.ToInt32(item["idProducto"].ToString());
                    _listaMateriales.Descripcion = item["DescripcionProducto"].ToString();
                    //_listaMateriales.idMovimientoEntrada = Convert.ToInt32(item["idMovimientoEntradaSalida"].ToString());
                    _listaMateriales.FechaFactura = Convert.ToDateTime(item["FechaSalidaIngresada"].ToString());
                    listaMateriales.Add(_listaMateriales);
                }
            }
            connection.Close();
            return listaMateriales;
        }

        public static bool LiberarSotckReservado(List<int> ListaIdProd)
        {
            bool exito = false;
            List<int> listaEntrada = new List<int>();
            string idsListaProducto = string.Join(",", ListaIdProd);

            //listaEntrada = SeleccionarIdEntradaParaProducto(idsListaProducto);

            //string idsListaEntradas = string.Join(",", listaEntrada);

            foreach (var item in ListaIdProd)
            {
                connection.Close();
                connection.Open();
                string Actualizar = "LiberarSotckReservado";
                MySqlCommand cmd = new MySqlCommand(Actualizar, connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("idProducto_in", item);
                cmd.Parameters.AddWithValue("Estado_in", 1);
                cmd.ExecuteNonQuery();
                exito = true;
            }

            connection.Close();
            return exito;
        }

        private static List<int> SeleccionarIdEntradaParaProducto(string idsListaProducto)
        {
            List<int> listaIdEntrada = new List<int>();
            connection.Close();
            connection.Open();
            List<Stock> _listaStocks = new List<Stock>();
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = connection;
            DataTable Tabla = new DataTable();
            MySqlParameter[] oParam = { new MySqlParameter("idProducto_in", idsListaProducto) };
            string proceso = "SeleccionarIdEntradaParaProducto";
            MySqlDataAdapter dt = new MySqlDataAdapter(proceso, connection);
            dt.SelectCommand.CommandType = CommandType.StoredProcedure;
            dt.SelectCommand.Parameters.AddRange(oParam);
            dt.Fill(Tabla);
            if (Tabla.Rows.Count > 0)
            {
                foreach (DataRow item in Tabla.Rows)
                {
                    int idEntrada = Convert.ToInt32(item["idEntrada"].ToString());
                    listaIdEntrada.Add(idEntrada);
                }
            }
            connection.Close();
            return listaIdEntrada;
        }
        public static List<Stock> ListaMaterialesExistentes(int idObraSeleccionada)
        {
            List<Stock> listaMateriales = new List<Stock>();
            connection.Close();
            connection.Open();
            List<Stock> _listaStocks = new List<Stock>();
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = connection;
            DataTable Tabla = new DataTable();
            MySqlParameter[] oParam = { new MySqlParameter("idObraSeleccionada_in", idObraSeleccionada) };
            string proceso = "ListaMaterialesExistentes";
            MySqlDataAdapter dt = new MySqlDataAdapter(proceso, connection);
            dt.SelectCommand.CommandType = CommandType.StoredProcedure;
            dt.SelectCommand.Parameters.AddRange(oParam);
            dt.Fill(Tabla);
            if (Tabla.Rows.Count > 0)
            {
                foreach (DataRow item in Tabla.Rows)
                {
                    Stock _listaMateriales = new Stock();
                    _listaMateriales.Cantidad = Convert.ToInt32(item["Cantidad"].ToString());
                    _listaMateriales.ValorUnitario = Convert.ToDecimal(item["PrecioUnitario"].ToString());
                    _listaMateriales.PrecioNeto = Convert.ToDecimal(item["PrecioNeto"].ToString());
                    _listaMateriales.idProducto = Convert.ToInt32(item["idProducto"].ToString());
                    _listaMateriales.Descripcion = item["DescripcionProducto"].ToString();
                    _listaMateriales.idMovimientoEntrada = Convert.ToInt32(item["idMovimientoEntradaSalida"].ToString());
                    _listaMateriales.FechaFactura = Convert.ToDateTime(item["FechaSalidaIngresada"].ToString());
                    _listaMateriales.EstadoEntrada = Convert.ToInt32(item["Estado"].ToString());
                    listaMateriales.Add(_listaMateriales);
                }
            }
            connection.Close();
            return listaMateriales;
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
