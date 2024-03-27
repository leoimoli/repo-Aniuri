using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Añuri.Clases_Maestras;
using Añuri.Entidades;
using Añuri.Negocio;
using MySql.Data.MySqlClient;

namespace Añuri.Dao
{
    public class StockDao
    {
        private static MySql.Data.MySqlClient.MySqlConnection connection = new MySqlConnection(Properties.Settings.Default.db);
        public static bool InsertarStock(List<Stock> listaStock)
        {
            bool exito = false;
            int idMovimiento = 0;
            foreach (var item in listaStock)
            {
                connection.Close();
                connection.Open();
                string proceso = "AltaStockMovimientoEntrada";
                MySqlCommand cmd = new MySqlCommand(proceso, connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("Proveedor_in", listaStock[0].idProveedor);
                cmd.Parameters.AddWithValue("FechaCompra_in", listaStock[0].FechaFactura);
                cmd.Parameters.AddWithValue("Remito_in", listaStock[0].Remito);
                DateTime fecha = Convert.ToDateTime(DateTime.Now);
                cmd.Parameters.AddWithValue("FechaActual_in", fecha);
                cmd.Parameters.AddWithValue("idUsuario_in", listaStock[0].idUsuario);
                cmd.Parameters.AddWithValue("Cantidad_in", listaStock[0].Cantidad);
                cmd.Parameters.AddWithValue("Estado_in", "1");

                MySqlDataReader r = cmd.ExecuteReader();
                while (r.Read())
                {
                    idMovimiento = Convert.ToInt32(r["ID"].ToString());
                }
                if (idMovimiento > 0)
                {
                    exito = true;
                }
                else
                {
                    exito = false;
                }

                if (exito == true)
                {
                    int CantidadTotal = 0;
                    List<int> stockExistente = new List<int>();
                    //item.idProducto = ProductoDao.BuscarIDProductoPorDescripcion(item.);
                    stockExistente = ValidarStockExistente(item.idProducto);
                    if (stockExistente.Count > 0)
                    {
                        int cant = Convert.ToInt32(stockExistente[0].ToString());
                        CantidadTotal = item.Cantidad + cant;
                        exito = ActualizarStock(item.idProducto, CantidadTotal);
                    }
                    else
                    {
                        exito = InsertarStockNuevo(item.idProducto, item.Cantidad, item.CodigoProducto);
                    }

                    if (exito == true)
                    {
                        connection.Close();
                        connection.Open();
                        string proceso2 = "AltaMovimientoStock";
                        MySqlCommand cmd2 = new MySqlCommand(proceso2, connection);
                        cmd2.CommandType = CommandType.StoredProcedure;
                        cmd2.Parameters.AddWithValue("idProducto_in", item.idProducto);
                        cmd2.Parameters.AddWithValue("Cantidad_in", item.Cantidad);
                        DateTime fechaactual = Convert.ToDateTime(DateTime.Now);
                        cmd2.Parameters.AddWithValue("Fecha_in", item.FechaFactura);
                        ///// TipoMovimiento_in es parametro si entrada o salida de stock
                        cmd2.Parameters.AddWithValue("TipoMovimiento_in", "E");
                        cmd2.Parameters.AddWithValue("idMovimientoEntrada_in", idMovimiento);
                        cmd2.Parameters.AddWithValue("ValorUnitario_in", item.ValorUnitario);
                        cmd2.Parameters.AddWithValue("PrecioNeto_in", item.PrecioNeto);
                        cmd2.Parameters.AddWithValue("Observaciones_in", item.Observaciones);
                        cmd2.Parameters.AddWithValue("idSalida_in", 0);
                        cmd2.ExecuteNonQuery();
                        exito = true;
                        connection.Close();
                    }
                    else
                    {
                        exito = false;
                    }
                }

            }
            return exito;
        }
        public static List<Stock> ListarMaterialesPorKilos()
        {
            connection.Close();
            connection.Open();
            List<Stock> _lista = new List<Stock>();
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = connection;
            DataTable Tabla = new DataTable();
            MySqlParameter[] oParam = { };
            string proceso = "ListarIdMateriales";
            MySqlDataAdapter dt = new MySqlDataAdapter(proceso, connection);
            dt.SelectCommand.CommandType = CommandType.StoredProcedure;
            dt.SelectCommand.Parameters.AddRange(oParam);
            dt.Fill(Tabla);
            if (Tabla.Rows.Count > 0)
            {
                foreach (DataRow item in Tabla.Rows)
                {
                    Stock _listaMateriales = new Stock();
                    _listaMateriales.idProducto = Convert.ToInt32(item["idProducto"].ToString());
                    _listaMateriales.Descripcion = item["DescripcionProducto"].ToString();
                    _listaMateriales.Cantidad = Convert.ToInt32(item["Cantidad"].ToString());
                    _listaMateriales.StockTotal = Convert.ToInt32(item["StockTotal"].ToString());
                    _listaMateriales.ValorUnitario = Convert.ToDecimal(item["PrecioUnitario"].ToString());
                    _listaMateriales.PrecioNeto = Convert.ToDecimal(item["PrecioNeto"].ToString());
                    _listaMateriales.idMovimientoEntrada = Convert.ToInt32(item["idEntrada"].ToString());
                    _listaMateriales.EstadoEntrada = Convert.ToInt32(item["Estado"].ToString());
                    _listaMateriales.TipoMovimiento = item["TipoMovimiento"].ToString();
                    _listaMateriales.FechaFactura = Convert.ToDateTime(item["Fecha"].ToString());
                    if (item["FechaCierre"].ToString() != "" && item["FechaCierre"].ToString() != null)
                    {
                        _listaMateriales.FechaCierre = Convert.ToDateTime(item["FechaCierre"].ToString());
                    }
                    _listaMateriales.FechaMovimiento = Convert.ToDateTime(item["FechaMovimiento"].ToString());
                    _lista.Add(_listaMateriales);
                }
            }
            connection.Close();
            return _lista;
        }
        public static List<Stock> ListarSaldoInicialEnKilos(int idProducto, string año)
        {
            List<Stock> lista = new List<Stock>();
            connection.Close();
            connection.Open();
            List<Stock> _listaStocks = new List<Stock>();
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = connection;
            DataTable Tabla = new DataTable();
            MySqlParameter[] oParam = { new MySqlParameter("idMaterial_in", idProducto),
            new MySqlParameter("Anio_in", año)};
            string proceso = "ListarSaldoInicial";
            MySqlDataAdapter dt = new MySqlDataAdapter(proceso, connection);
            dt.SelectCommand.CommandType = CommandType.StoredProcedure;
            dt.SelectCommand.Parameters.AddRange(oParam);
            dt.Fill(Tabla);
            if (Tabla.Rows.Count > 0)
            {
                foreach (DataRow item in Tabla.Rows)
                {
                    Stock _listaMateriales = new Stock();
                    _listaMateriales.idProducto = Convert.ToInt32(item["idProducto"].ToString());
                    _listaMateriales.Descripcion = item["DescripcionProducto"].ToString();
                    _listaMateriales.Cantidad = Convert.ToInt32(item["Cantidad"].ToString());
                    _listaMateriales.StockTotal = Convert.ToInt32(item["StockTotal"].ToString());
                    _listaMateriales.ValorUnitario = Convert.ToDecimal(item["PrecioUnitario"].ToString());
                    _listaMateriales.PrecioNeto = Convert.ToDecimal(item["PrecioNeto"].ToString());
                    _listaMateriales.idMovimientoEntrada = Convert.ToInt32(item["idEntrada"].ToString());
                    _listaMateriales.EstadoEntrada = Convert.ToInt32(item["Estado"].ToString());
                    _listaMateriales.TipoMovimiento = item["TipoMovimiento"].ToString();
                    _listaMateriales.FechaFactura = Convert.ToDateTime(item["Fecha"].ToString());
                    if (item["FechaCierre"].ToString() != "" && item["FechaCierre"].ToString() != null)
                    {
                        _listaMateriales.FechaCierre = Convert.ToDateTime(item["FechaCierre"].ToString());
                    }
                    _listaMateriales.FechaMovimiento = Convert.ToDateTime(item["FechaMovimiento"].ToString());
                    lista.Add(_listaMateriales);
                }
            }
            connection.Close();
            return lista;
        }
        public static List<string> CargarComboGrupo()
        {
            connection.Close();
            connection.Open();
            List<string> _Grupos = new List<string>();
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = connection;
            DataTable Tabla = new DataTable();
            MySqlParameter[] oParam = { };
            string proceso = "CargarComboGrupo";
            MySqlDataAdapter dt = new MySqlDataAdapter(proceso, connection);
            dt.SelectCommand.CommandType = CommandType.StoredProcedure;
            dt.SelectCommand.Parameters.AddRange(oParam);
            dt.Fill(Tabla);
            if (Tabla.Rows.Count > 0)
            {
                foreach (DataRow item in Tabla.Rows)
                {
                    string Perfil = item["Nombre"].ToString();
                    _Grupos.Add(Perfil);
                }
            }
            connection.Close();
            return _Grupos;
        }
        public static List<Stock> ListarSaldoInicial(int id, string año)
        {
            List<Stock> lista = new List<Stock>();
            connection.Close();
            connection.Open();
            List<Stock> _listaStocks = new List<Stock>();
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = connection;
            DataTable Tabla = new DataTable();
            MySqlParameter[] oParam = { new MySqlParameter("idMaterial_in", id),
            new MySqlParameter("Anio_in", año)};
            string proceso = "ListarSaldoInicial";
            MySqlDataAdapter dt = new MySqlDataAdapter(proceso, connection);
            dt.SelectCommand.CommandType = CommandType.StoredProcedure;
            dt.SelectCommand.Parameters.AddRange(oParam);
            dt.Fill(Tabla);
            if (Tabla.Rows.Count > 0)
            {
                foreach (DataRow item in Tabla.Rows)
                {
                    Stock _listaMateriales = new Stock();
                    _listaMateriales.idProducto = Convert.ToInt32(item["idProducto"].ToString());
                    _listaMateriales.Descripcion = item["DescripcionProducto"].ToString();
                    _listaMateriales.Cantidad = Convert.ToInt32(item["Cantidad"].ToString());
                    _listaMateriales.StockTotal = Convert.ToInt32(item["StockTotal"].ToString());
                    _listaMateriales.ValorUnitario = Convert.ToDecimal(item["PrecioUnitario"].ToString());
                    _listaMateriales.PrecioNeto = Convert.ToDecimal(item["PrecioNeto"].ToString());
                    _listaMateriales.idMovimientoEntrada = Convert.ToInt32(item["idEntrada"].ToString());
                    _listaMateriales.EstadoEntrada = Convert.ToInt32(item["Estado"].ToString());
                    _listaMateriales.TipoMovimiento = item["TipoMovimiento"].ToString();
                    _listaMateriales.FechaFactura = Convert.ToDateTime(item["Fecha"].ToString());
                    if (item["FechaCierre"].ToString() != "" && item["FechaCierre"].ToString() != null)
                    {
                        _listaMateriales.FechaCierre = Convert.ToDateTime(item["FechaCierre"].ToString());
                    }
                    _listaMateriales.FechaMovimiento = Convert.ToDateTime(item["FechaMovimiento"].ToString());
                    lista.Add(_listaMateriales);
                }
            }
            connection.Close();
            return lista;
        }
        public static List<Stock> ListarIdMateriales()
        {
            connection.Close();
            connection.Open();
            List<Stock> _lista = new List<Stock>();
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = connection;
            DataTable Tabla = new DataTable();
            MySqlParameter[] oParam = { };
            string proceso = "ListarIdMateriales";
            MySqlDataAdapter dt = new MySqlDataAdapter(proceso, connection);
            dt.SelectCommand.CommandType = CommandType.StoredProcedure;
            dt.SelectCommand.Parameters.AddRange(oParam);
            dt.Fill(Tabla);
            if (Tabla.Rows.Count > 0)
            {
                foreach (DataRow item in Tabla.Rows)
                {
                    Stock _listaMateriales = new Stock();
                    _listaMateriales.idProducto = Convert.ToInt32(item["idProducto"].ToString());
                    _listaMateriales.Descripcion = item["DescripcionProducto"].ToString();
                    _listaMateriales.Cantidad = Convert.ToInt32(item["Cantidad"].ToString());
                    _listaMateriales.StockTotal = Convert.ToInt32(item["StockTotal"].ToString());
                    _listaMateriales.ValorUnitario = Convert.ToDecimal(item["PrecioUnitario"].ToString());
                    _listaMateriales.PrecioNeto = Convert.ToDecimal(item["PrecioNeto"].ToString());
                    _listaMateriales.idMovimientoEntrada = Convert.ToInt32(item["idEntrada"].ToString());
                    _listaMateriales.EstadoEntrada = Convert.ToInt32(item["Estado"].ToString());
                    _listaMateriales.TipoMovimiento = item["TipoMovimiento"].ToString();
                    _listaMateriales.FechaFactura = Convert.ToDateTime(item["Fecha"].ToString());
                    if (item["FechaCierre"].ToString() != "" && item["FechaCierre"].ToString() != null)
                    {
                        _listaMateriales.FechaCierre = Convert.ToDateTime(item["FechaCierre"].ToString());
                    }
                    _listaMateriales.FechaMovimiento = Convert.ToDateTime(item["FechaMovimiento"].ToString());
                    _lista.Add(_listaMateriales);
                    //int idMaterial = Convert.ToInt32(item["idProducto"].ToString());
                    //_lista.Add(idMaterial);
                }
            }
            connection.Close();
            return _lista;
        }
        public static bool ValidarProductoExistente(string text)
        {
            connection.Close();
            bool existe = false;
            connection.Open();
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = connection;
            DataTable Tabla = new DataTable();
            MySqlParameter[] oParam = {
                                      new MySqlParameter("DescripcionProducto_in", text) };
            string proceso = "ValidarProductoExistente";
            MySqlDataAdapter dt = new MySqlDataAdapter(proceso, connection);
            dt.SelectCommand.CommandType = CommandType.StoredProcedure;
            dt.SelectCommand.Parameters.AddRange(oParam);
            dt.Fill(Tabla);
            if (Tabla.Rows.Count > 0)
            {
                existe = true;
            }
            connection.Close();
            return existe;
        }
        public static List<Stock> ListarSaldoInicialInventarioMaterialesPorKilosPorMaterial(string año, string material)
        {
            List<Stock> lista = new List<Stock>();
            connection.Close();
            connection.Open();
            List<Stock> _listaStocks = new List<Stock>();
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = connection;
            DataTable Tabla = new DataTable();
            MySqlParameter[] oParam = { new MySqlParameter("Anio_in", año),
           new MySqlParameter("Material_in", material) };
            string proceso = "ListarSaldoInicialInventarioMaterialesPorKilosPorMaterial";
            MySqlDataAdapter dt = new MySqlDataAdapter(proceso, connection);
            dt.SelectCommand.CommandType = CommandType.StoredProcedure;
            dt.SelectCommand.Parameters.AddRange(oParam);
            dt.Fill(Tabla);
            if (Tabla.Rows.Count > 0)
            {
                foreach (DataRow item in Tabla.Rows)
                {
                    Stock _listaMateriales = new Stock();
                    _listaMateriales.idProducto = Convert.ToInt32(item["idProducto"].ToString());
                    _listaMateriales.Descripcion = item["DescripcionProducto"].ToString();
                    _listaMateriales.Cantidad = Convert.ToInt32(item["Cantidad"].ToString());
                    _listaMateriales.StockTotal = Convert.ToInt32(item["StockTotal"].ToString());
                    _listaMateriales.ValorUnitario = Convert.ToDecimal(item["PrecioUnitario"].ToString());
                    _listaMateriales.PrecioNeto = Convert.ToDecimal(item["PrecioNeto"].ToString());
                    _listaMateriales.idMovimientoEntrada = Convert.ToInt32(item["idEntrada"].ToString());
                    _listaMateriales.EstadoEntrada = Convert.ToInt32(item["Estado"].ToString());
                    _listaMateriales.TipoMovimiento = item["TipoMovimiento"].ToString();
                    _listaMateriales.FechaFactura = Convert.ToDateTime(item["Fecha"].ToString());
                    if (item["FechaCierre"].ToString() != "" && item["FechaCierre"].ToString() != null)
                    {
                        _listaMateriales.FechaCierre = Convert.ToDateTime(item["FechaCierre"].ToString());
                    }
                    _listaMateriales.FechaMovimiento = Convert.ToDateTime(item["FechaMovimiento"].ToString());
                    lista.Add(_listaMateriales);
                }
            }
            connection.Close();
            return lista;
        }
        public static List<Stock> ListaStockFaltante()
        {
            connection.Close();
            connection.Open();
            List<Stock> _listaStocks = new List<Stock>();
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = connection;
            DataTable Tabla = new DataTable();

            string variable = DaoConsultasGenerales.ConsultaVariableStockFaltante();

            MySqlParameter[] oParam = { new MySqlParameter("variable_in", variable) };
            string proceso = "ListaStockFaltante";
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
                    listaStock.Descripcion = item["DescripcionProducto"].ToString();
                    listaStock.Cantidad = Convert.ToInt32(item["Cantidad"].ToString());
                    _listaStocks.Add(listaStock);
                }
            }
            connection.Close();
            return _listaStocks;
        }
        public static List<Stock> ListarSaldoInicialInventarioMaterialesPorKilos(string año)
        {
            List<Stock> lista = new List<Stock>();
            connection.Close();
            connection.Open();
            List<Stock> _listaStocks = new List<Stock>();
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = connection;
            DataTable Tabla = new DataTable();
            MySqlParameter[] oParam = { new MySqlParameter("Anio_in", año) };
            string proceso = "ListarSaldoInicialInventarioMaterialesPorKilos";
            MySqlDataAdapter dt = new MySqlDataAdapter(proceso, connection);
            dt.SelectCommand.CommandType = CommandType.StoredProcedure;
            dt.SelectCommand.Parameters.AddRange(oParam);
            dt.Fill(Tabla);
            if (Tabla.Rows.Count > 0)
            {
                foreach (DataRow item in Tabla.Rows)
                {
                    Stock _listaMateriales = new Stock();
                    _listaMateriales.idProducto = Convert.ToInt32(item["idProducto"].ToString());
                    _listaMateriales.Descripcion = item["DescripcionProducto"].ToString();
                    _listaMateriales.Cantidad = Convert.ToInt32(item["Cantidad"].ToString());
                    _listaMateriales.StockTotal = Convert.ToInt32(item["StockTotal"].ToString());
                    _listaMateriales.ValorUnitario = Convert.ToDecimal(item["PrecioUnitario"].ToString());
                    _listaMateriales.PrecioNeto = Convert.ToDecimal(item["PrecioNeto"].ToString());
                    _listaMateriales.idMovimientoEntrada = Convert.ToInt32(item["idEntrada"].ToString());
                    _listaMateriales.EstadoEntrada = Convert.ToInt32(item["Estado"].ToString());
                    _listaMateriales.TipoMovimiento = item["TipoMovimiento"].ToString();
                    _listaMateriales.FechaFactura = Convert.ToDateTime(item["Fecha"].ToString());
                    if (item["FechaCierre"].ToString() != "" && item["FechaCierre"].ToString() != null)
                    {
                        _listaMateriales.FechaCierre = Convert.ToDateTime(item["FechaCierre"].ToString());
                    }
                    _listaMateriales.FechaMovimiento = Convert.ToDateTime(item["FechaMovimiento"].ToString());
                    lista.Add(_listaMateriales);
                }
            }
            connection.Close();
            return lista;
        }
        public static List<Stock> ListarInventarioMaterialesPorKilos(string año)
        {
            List<Stock> lista = new List<Stock>();
            connection.Close();
            connection.Open();
            List<Stock> _listaStocks = new List<Stock>();
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = connection;
            DataTable Tabla = new DataTable();
            MySqlParameter[] oParam = { new MySqlParameter("Anio_in", año) };
            string proceso = "ListarInventarioMaterialesPorKilos";
            MySqlDataAdapter dt = new MySqlDataAdapter(proceso, connection);
            dt.SelectCommand.CommandType = CommandType.StoredProcedure;
            dt.SelectCommand.Parameters.AddRange(oParam);
            dt.Fill(Tabla);
            if (Tabla.Rows.Count > 0)
            {
                foreach (DataRow item in Tabla.Rows)
                {
                    Stock _listaMateriales = new Stock();
                    _listaMateriales.idProducto = Convert.ToInt32(item["idProducto"].ToString());
                    _listaMateriales.Descripcion = item["DescripcionProducto"].ToString();
                    _listaMateriales.Cantidad = Convert.ToInt32(item["Cantidad"].ToString());
                    _listaMateriales.StockTotal = Convert.ToInt32(item["StockTotal"].ToString());
                    _listaMateriales.ValorUnitario = Convert.ToDecimal(item["PrecioUnitario"].ToString());
                    _listaMateriales.PrecioNeto = Convert.ToDecimal(item["PrecioNeto"].ToString());
                    _listaMateriales.idMovimientoEntrada = Convert.ToInt32(item["idEntrada"].ToString());
                    _listaMateriales.EstadoEntrada = Convert.ToInt32(item["Estado"].ToString());
                    _listaMateriales.TipoMovimiento = item["TipoMovimiento"].ToString();
                    _listaMateriales.FechaFactura = Convert.ToDateTime(item["Fecha"].ToString());
                    if (item["FechaCierre"].ToString() != "" && item["FechaCierre"].ToString() != null)
                    {
                        _listaMateriales.FechaCierre = Convert.ToDateTime(item["FechaCierre"].ToString());
                    }
                    _listaMateriales.FechaMovimiento = Convert.ToDateTime(item["FechaMovimiento"].ToString());
                    lista.Add(_listaMateriales);
                }
            }
            connection.Close();
            return lista;
        }
        public static List<Stock> ListarInventarioMaterialesPorKilosPorMaterial(string año, string material)
        {
            List<Stock> lista = new List<Stock>();
            connection.Close();
            connection.Open();
            List<Stock> _listaStocks = new List<Stock>();
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = connection;
            DataTable Tabla = new DataTable();
            MySqlParameter[] oParam = { new MySqlParameter("Anio_in", año),
            new MySqlParameter("Material_in", material)};
            string proceso = "ListarInventarioMaterialesPorKilosPorMaterial";
            MySqlDataAdapter dt = new MySqlDataAdapter(proceso, connection);
            dt.SelectCommand.CommandType = CommandType.StoredProcedure;
            dt.SelectCommand.Parameters.AddRange(oParam);
            dt.Fill(Tabla);
            if (Tabla.Rows.Count > 0)
            {
                foreach (DataRow item in Tabla.Rows)
                {
                    Stock _listaMateriales = new Stock();
                    _listaMateriales.idProducto = Convert.ToInt32(item["idProducto"].ToString());
                    _listaMateriales.Descripcion = item["DescripcionProducto"].ToString();
                    _listaMateriales.Cantidad = Convert.ToInt32(item["Cantidad"].ToString());
                    _listaMateriales.StockTotal = Convert.ToInt32(item["StockTotal"].ToString());
                    _listaMateriales.ValorUnitario = Convert.ToDecimal(item["PrecioUnitario"].ToString());
                    _listaMateriales.PrecioNeto = Convert.ToDecimal(item["PrecioNeto"].ToString());
                    _listaMateriales.idMovimientoEntrada = Convert.ToInt32(item["idEntrada"].ToString());
                    _listaMateriales.EstadoEntrada = Convert.ToInt32(item["Estado"].ToString());
                    _listaMateriales.TipoMovimiento = item["TipoMovimiento"].ToString();
                    _listaMateriales.FechaFactura = Convert.ToDateTime(item["Fecha"].ToString());
                    if (item["FechaCierre"].ToString() != "" && item["FechaCierre"].ToString() != null)
                    {
                        _listaMateriales.FechaCierre = Convert.ToDateTime(item["FechaCierre"].ToString());
                    }
                    _listaMateriales.FechaMovimiento = Convert.ToDateTime(item["FechaMovimiento"].ToString());
                    lista.Add(_listaMateriales);
                }
            }
            connection.Close();
            return lista;
        }
        public static List<Stock> ListarSaldoInicialInventarioMaterialesPesosPorMaterial(string año, string material)
        {
            List<Stock> lista = new List<Stock>();
            connection.Close();
            connection.Open();
            List<Stock> _listaStocks = new List<Stock>();
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = connection;
            DataTable Tabla = new DataTable();
            MySqlParameter[] oParam = { new MySqlParameter("Anio_in", año),
           new MySqlParameter("Material_in", material) };
            string proceso = "ListarSaldoInicialInventarioMaterialesPesosPorMaterial";
            MySqlDataAdapter dt = new MySqlDataAdapter(proceso, connection);
            dt.SelectCommand.CommandType = CommandType.StoredProcedure;
            dt.SelectCommand.Parameters.AddRange(oParam);
            dt.Fill(Tabla);
            if (Tabla.Rows.Count > 0)
            {
                foreach (DataRow item in Tabla.Rows)
                {
                    Stock _listaMateriales = new Stock();
                    _listaMateriales.idProducto = Convert.ToInt32(item["idProducto"].ToString());
                    _listaMateriales.Descripcion = item["DescripcionProducto"].ToString();
                    _listaMateriales.Cantidad = Convert.ToInt32(item["Cantidad"].ToString());
                    _listaMateriales.StockTotal = Convert.ToInt32(item["StockTotal"].ToString());
                    _listaMateriales.ValorUnitario = Convert.ToDecimal(item["PrecioUnitario"].ToString());
                    _listaMateriales.PrecioNeto = Convert.ToDecimal(item["PrecioNeto"].ToString());
                    _listaMateriales.idMovimientoEntrada = Convert.ToInt32(item["idEntrada"].ToString());
                    _listaMateriales.EstadoEntrada = Convert.ToInt32(item["Estado"].ToString());
                    _listaMateriales.TipoMovimiento = item["TipoMovimiento"].ToString();
                    _listaMateriales.FechaFactura = Convert.ToDateTime(item["Fecha"].ToString());
                    if (item["FechaCierre"].ToString() != "" && item["FechaCierre"].ToString() != null)
                    {
                        _listaMateriales.FechaCierre = Convert.ToDateTime(item["FechaCierre"].ToString());
                    }
                    _listaMateriales.FechaMovimiento = Convert.ToDateTime(item["FechaMovimiento"].ToString());
                    lista.Add(_listaMateriales);
                }
            }
            connection.Close();
            return lista;
        }
        public static List<Stock> ListarSaldoInicialInventarioMaterialesPesos(string año)
        {
            List<Stock> lista = new List<Stock>();
            connection.Close();
            connection.Open();
            List<Stock> _listaStocks = new List<Stock>();
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = connection;
            DataTable Tabla = new DataTable();
            MySqlParameter[] oParam = { new MySqlParameter("Anio_in", año) };
            string proceso = "ListarSaldoInicialInventarioMaterialesPesos";
            MySqlDataAdapter dt = new MySqlDataAdapter(proceso, connection);
            dt.SelectCommand.CommandType = CommandType.StoredProcedure;
            dt.SelectCommand.Parameters.AddRange(oParam);
            dt.Fill(Tabla);
            if (Tabla.Rows.Count > 0)
            {
                foreach (DataRow item in Tabla.Rows)
                {
                    Stock _listaMateriales = new Stock();
                    _listaMateriales.idProducto = Convert.ToInt32(item["idProducto"].ToString());
                    _listaMateriales.Descripcion = item["DescripcionProducto"].ToString();
                    _listaMateriales.Cantidad = Convert.ToInt32(item["Cantidad"].ToString());
                    _listaMateriales.StockTotal = Convert.ToInt32(item["StockTotal"].ToString());
                    _listaMateriales.ValorUnitario = Convert.ToDecimal(item["PrecioUnitario"].ToString());
                    _listaMateriales.PrecioNeto = Convert.ToDecimal(item["PrecioNeto"].ToString());
                    _listaMateriales.idMovimientoEntrada = Convert.ToInt32(item["idEntrada"].ToString());
                    _listaMateriales.EstadoEntrada = Convert.ToInt32(item["Estado"].ToString());
                    _listaMateriales.TipoMovimiento = item["TipoMovimiento"].ToString();
                    _listaMateriales.FechaFactura = Convert.ToDateTime(item["Fecha"].ToString());
                    if (item["FechaCierre"].ToString() != "" && item["FechaCierre"].ToString() != null)
                    {
                        _listaMateriales.FechaCierre = Convert.ToDateTime(item["FechaCierre"].ToString());
                    }
                    _listaMateriales.FechaMovimiento = Convert.ToDateTime(item["FechaMovimiento"].ToString());
                    lista.Add(_listaMateriales);
                }
            }
            connection.Close();
            return lista;
        }
        public static List<Stock> ListarInventarioMaterialesPesosPorAño(string año)
        {
            List<Stock> lista = new List<Stock>();
            connection.Close();
            connection.Open();
            List<Stock> _listaStocks = new List<Stock>();
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = connection;
            DataTable Tabla = new DataTable();
            MySqlParameter[] oParam = { new MySqlParameter("Anio_in", año) };
            string proceso = "ListarInventarioMaterialesPesosPorAnio";
            MySqlDataAdapter dt = new MySqlDataAdapter(proceso, connection);
            dt.SelectCommand.CommandType = CommandType.StoredProcedure;
            dt.SelectCommand.Parameters.AddRange(oParam);
            dt.Fill(Tabla);
            if (Tabla.Rows.Count > 0)
            {
                foreach (DataRow item in Tabla.Rows)
                {
                    Stock _listaMateriales = new Stock();
                    _listaMateriales.idProducto = Convert.ToInt32(item["idProducto"].ToString());
                    _listaMateriales.Descripcion = item["DescripcionProducto"].ToString();
                    _listaMateriales.Cantidad = Convert.ToInt32(item["Cantidad"].ToString());
                    _listaMateriales.StockTotal = Convert.ToInt32(item["StockTotal"].ToString());
                    _listaMateriales.ValorUnitario = Convert.ToDecimal(item["PrecioUnitario"].ToString());
                    _listaMateriales.PrecioNeto = Convert.ToDecimal(item["PrecioNeto"].ToString());
                    _listaMateriales.idMovimientoEntrada = Convert.ToInt32(item["idEntrada"].ToString());
                    _listaMateriales.EstadoEntrada = Convert.ToInt32(item["Estado"].ToString());
                    _listaMateriales.TipoMovimiento = item["TipoMovimiento"].ToString();
                    _listaMateriales.FechaFactura = Convert.ToDateTime(item["Fecha"].ToString());
                    if (item["FechaCierre"].ToString() != "" && item["FechaCierre"].ToString() != null)
                    {
                        _listaMateriales.FechaCierre = Convert.ToDateTime(item["FechaCierre"].ToString());
                    }
                    _listaMateriales.FechaMovimiento = Convert.ToDateTime(item["FechaMovimiento"].ToString());
                    lista.Add(_listaMateriales);
                }
            }
            connection.Close();
            return lista;
        }
        public static List<Stock> ListarInventarioMaterialesPesosPorAñoPorMaterial(string año, string material)
        {
            List<Stock> lista = new List<Stock>();
            connection.Close();
            connection.Open();
            List<Stock> _listaStocks = new List<Stock>();
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = connection;
            DataTable Tabla = new DataTable();
            MySqlParameter[] oParam = { new MySqlParameter("Anio_in", año),
            new MySqlParameter("Material_in", material)};
            string proceso = "ListarInventarioMaterialesPesosPorAnioPorMaterial";
            MySqlDataAdapter dt = new MySqlDataAdapter(proceso, connection);
            dt.SelectCommand.CommandType = CommandType.StoredProcedure;
            dt.SelectCommand.Parameters.AddRange(oParam);
            dt.Fill(Tabla);
            if (Tabla.Rows.Count > 0)
            {
                foreach (DataRow item in Tabla.Rows)
                {
                    Stock _listaMateriales = new Stock();
                    _listaMateriales.idProducto = Convert.ToInt32(item["idProducto"].ToString());
                    _listaMateriales.Descripcion = item["DescripcionProducto"].ToString();
                    _listaMateriales.Cantidad = Convert.ToInt32(item["Cantidad"].ToString());
                    _listaMateriales.StockTotal = Convert.ToInt32(item["StockTotal"].ToString());
                    _listaMateriales.ValorUnitario = Convert.ToDecimal(item["PrecioUnitario"].ToString());
                    _listaMateriales.PrecioNeto = Convert.ToDecimal(item["PrecioNeto"].ToString());
                    _listaMateriales.idMovimientoEntrada = Convert.ToInt32(item["idEntrada"].ToString());
                    _listaMateriales.EstadoEntrada = Convert.ToInt32(item["Estado"].ToString());
                    _listaMateriales.TipoMovimiento = item["TipoMovimiento"].ToString();
                    _listaMateriales.FechaFactura = Convert.ToDateTime(item["Fecha"].ToString());
                    if (item["FechaCierre"].ToString() != "" && item["FechaCierre"].ToString() != null)
                    {
                        _listaMateriales.FechaCierre = Convert.ToDateTime(item["FechaCierre"].ToString());
                    }
                    _listaMateriales.FechaMovimiento = Convert.ToDateTime(item["FechaMovimiento"].ToString());
                    lista.Add(_listaMateriales);
                }
            }
            connection.Close();
            return lista;
        }
        public static List<Stock> ListarMovimientosStockInventarioPorFecha(DateTime fecha)
        {
            List<Stock> lista = new List<Stock>();
            connection.Close();
            connection.Open();
            List<Stock> _listaStocks = new List<Stock>();
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = connection;
            DataTable Tabla = new DataTable();
            MySqlParameter[] oParam = { new MySqlParameter("FechaHasta_in", fecha) };
            string proceso = "ListarMovimientosStockInventarioPorFecha";
            MySqlDataAdapter dt = new MySqlDataAdapter(proceso, connection);
            dt.SelectCommand.CommandType = CommandType.StoredProcedure;
            dt.SelectCommand.Parameters.AddRange(oParam);
            dt.Fill(Tabla);
            if (Tabla.Rows.Count > 0)
            {
                foreach (DataRow item in Tabla.Rows)
                {
                    Stock _listaMateriales = new Stock();
                    _listaMateriales.idProducto = Convert.ToInt32(item["idProducto"].ToString());
                    _listaMateriales.Descripcion = item["DescripcionProducto"].ToString();
                    _listaMateriales.Cantidad = Convert.ToInt32(item["Cantidad"].ToString());
                    _listaMateriales.StockTotal = Convert.ToInt32(item["StockTotal"].ToString());
                    _listaMateriales.ValorUnitario = Convert.ToDecimal(item["PrecioUnitario"].ToString());
                    _listaMateriales.PrecioNeto = Convert.ToDecimal(item["PrecioNeto"].ToString());
                    _listaMateriales.idMovimientoEntrada = Convert.ToInt32(item["idEntrada"].ToString());
                    _listaMateriales.EstadoEntrada = Convert.ToInt32(item["Estado"].ToString());
                    _listaMateriales.TipoMovimiento = item["TipoMovimiento"].ToString();
                    _listaMateriales.FechaFactura = Convert.ToDateTime(item["Fecha"].ToString());
                    if (item["FechaCierre"].ToString() != "" && item["FechaCierre"].ToString() != null)
                    {
                        _listaMateriales.FechaCierre = Convert.ToDateTime(item["FechaCierre"].ToString());
                    }
                    _listaMateriales.FechaMovimiento = Convert.ToDateTime(item["FechaMovimiento"].ToString());
                    //_listaMateriales.Mes = item["mes"].ToString();
                    //_listaMateriales.Año = item["anno"].ToString();
                    lista.Add(_listaMateriales);
                }
            }
            connection.Close();
            return lista;
        }
        public static List<Stock> ListarMovimientosStockDisponiblePorFecha(int idProductoSeleccionado, DateTime fechaDesde, DateTime fechaHasta)
        {
            List<Stock> lista = new List<Stock>();
            connection.Close();
            connection.Open();
            List<Stock> _listaStocks = new List<Stock>();
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = connection;
            DataTable Tabla = new DataTable();
            MySqlParameter[] oParam = { new MySqlParameter("idProductoSeleccionado_in", idProductoSeleccionado),
            new MySqlParameter("FechaDesde_in", fechaDesde),
            new MySqlParameter("FechaHasta_in", fechaHasta),};
            string proceso = "ListarMovimientosStockDisponiblePorFecha";
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
                    _listaMateriales.idMovimientoEntrada = Convert.ToInt32(item["idEntrada"].ToString());
                    _listaMateriales.Descripcion = item["DescripcionProducto"].ToString();
                    _listaMateriales.TipoMovimiento = item["TipoMovimiento"].ToString();
                    _listaMateriales.FechaFactura = Convert.ToDateTime(item["Fecha"].ToString());
                    lista.Add(_listaMateriales);
                }
            }
            connection.Close();
            return lista;
        }
        public static List<Stock> ListarStockDisponible(int idProductoSeleccionado)
        {
            List<Stock> lista = new List<Stock>();
            connection.Close();
            connection.Open();
            List<Stock> _listaStocks = new List<Stock>();
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = connection;
            DataTable Tabla = new DataTable();
            MySqlParameter[] oParam = { new MySqlParameter("idProductoSeleccionado_in", idProductoSeleccionado) };
            string proceso = "ListarStockDisponible";
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
                    _listaMateriales.idMovimientoEntrada = Convert.ToInt32(item["idEntrada"].ToString());
                    _listaMateriales.Descripcion = item["DescripcionProducto"].ToString();
                    _listaMateriales.TipoMovimiento = item["TipoMovimiento"].ToString();
                    _listaMateriales.FechaFactura = Convert.ToDateTime(item["Fecha"].ToString());
                    lista.Add(_listaMateriales);
                }
            }
            connection.Close();
            return lista;

        }
        public static List<Stock> ListarMovimientosStockPorTipoMovimiento(int idProductoSeleccionado, string tipoMovimiento)
        {
            List<Stock> lista = new List<Stock>();
            connection.Close();
            connection.Open();
            List<Stock> _listaStocks = new List<Stock>();
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = connection;
            DataTable Tabla = new DataTable();
            MySqlParameter[] oParam = { new MySqlParameter("idProductoSeleccionado_in", idProductoSeleccionado),
            new MySqlParameter("tipoMovimiento_in", tipoMovimiento)};
            string proceso = "ListarMovimientosStockPorTipoMovimiento";
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
                    //_listaMateriales.idProducto = Convert.ToInt32(item["idProducto"].ToString());
                    _listaMateriales.Descripcion = item["DescripcionProducto"].ToString();
                    _listaMateriales.TipoMovimiento = item["TipoMovimiento"].ToString();
                    _listaMateriales.FechaFactura = Convert.ToDateTime(item["Fecha"].ToString());
                    lista.Add(_listaMateriales);
                }
            }
            connection.Close();
            return lista;
        }
        public static List<Stock> ListarMovimientosStockPorFecha(int idProductoSeleccionado, DateTime fechaDesde, DateTime fechaHasta)
        {
            List<Stock> lista = new List<Stock>();
            connection.Close();
            connection.Open();
            List<Stock> _listaStocks = new List<Stock>();
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = connection;
            DataTable Tabla = new DataTable();
            MySqlParameter[] oParam = { new MySqlParameter("idProductoSeleccionado_in", idProductoSeleccionado),
            new MySqlParameter("fechaDesde_in", fechaDesde),
            new MySqlParameter("fechaHasta_in", fechaHasta)};
            string proceso = "ListarMovimientosStockPorFecha";
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
                    //_listaMateriales.idProducto = Convert.ToInt32(item["idProducto"].ToString());
                    _listaMateriales.Descripcion = item["DescripcionProducto"].ToString();
                    _listaMateriales.TipoMovimiento = item["TipoMovimiento"].ToString();
                    _listaMateriales.FechaFactura = Convert.ToDateTime(item["Fecha"].ToString());
                    lista.Add(_listaMateriales);
                }
            }
            connection.Close();
            return lista;
        }
        public static List<Stock> ListarMovimientosStockPorFechaTipoMovimiento(int idProductoSeleccionado, DateTime fechaDesde, DateTime fechaHasta, string tipoMovimiento)
        {

            List<Stock> lista = new List<Stock>();
            connection.Close();
            connection.Open();
            List<Stock> _listaStocks = new List<Stock>();
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = connection;
            DataTable Tabla = new DataTable();
            MySqlParameter[] oParam = { new MySqlParameter("idProductoSeleccionado_in", idProductoSeleccionado),
            new MySqlParameter("fechaDesde_in", fechaDesde),
            new MySqlParameter("fechaHasta_in", fechaHasta),
             new MySqlParameter("tipoMovimiento_in", tipoMovimiento),};
            string proceso = "ListarMovimientosStockPorFechaTipoMovimiento";
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
                    //_listaMateriales.idProducto = Convert.ToInt32(item["idProducto"].ToString());
                    _listaMateriales.Descripcion = item["DescripcionProducto"].ToString();
                    _listaMateriales.TipoMovimiento = item["TipoMovimiento"].ToString();
                    _listaMateriales.FechaFactura = Convert.ToDateTime(item["Fecha"].ToString());
                    lista.Add(_listaMateriales);
                }
            }
            connection.Close();
            return lista;
        }
        public static List<Stock> ListarMovimientosStockPorProducto(int idProductoSeleccionado)
        {
            List<Stock> lista = new List<Stock>();
            connection.Close();
            connection.Open();
            List<Stock> _listaStocks = new List<Stock>();
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = connection;
            DataTable Tabla = new DataTable();
            MySqlParameter[] oParam = { new MySqlParameter("idProductoSeleccionado_in", idProductoSeleccionado) };
            string proceso = "ListarMovimientosStockPorProducto";
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
                    _listaMateriales.idMovimiento = Convert.ToInt32(item["idMovimientoEntradaSalida"].ToString());
                    _listaMateriales.Descripcion = item["DescripcionProducto"].ToString();
                    _listaMateriales.TipoMovimiento = item["TipoMovimiento"].ToString();
                    _listaMateriales.FechaFactura = Convert.ToDateTime(item["Fecha"].ToString());
                    _listaMateriales.NombreObra = item["NombreObra"].ToString();
                    lista.Add(_listaMateriales);
                }
            }
            connection.Close();
            return lista;
        }
        private static bool InsertarStockNuevo(int idProducto, int cantidad, string codigoProducto)
        {
            bool exito = false;
            connection.Close();
            connection.Open();
            string proceso = "AltaStock";
            MySqlCommand cmd = new MySqlCommand(proceso, connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("idProducto_in", idProducto);
            cmd.Parameters.AddWithValue("Cantidad_in", cantidad);
            cmd.Parameters.AddWithValue("Estado_in", 1);
            cmd.ExecuteNonQuery();
            exito = true;
            connection.Close();
            return exito;
        }
        private static bool ActualizarStock(int idProducto, int cantidadTotal)
        {
            bool exito = false;
            connection.Close();
            connection.Open();
            string Actualizar = "EditarStock";
            MySqlCommand cmd = new MySqlCommand(Actualizar, connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("idProducto_in", idProducto);
            cmd.Parameters.AddWithValue("cantidad_in", cantidadTotal);
            cmd.ExecuteNonQuery();
            exito = true;
            connection.Close();
            return exito;
        }
        public static string ObteneridStock(int idProducto)
        {
            connection.Close();
            string Stock = "0";
            connection.Open();
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = connection;
            DataTable Tabla = new DataTable();
            MySqlParameter[] oParam = {
                                      new MySqlParameter("idProducto_in", idProducto) };
            string proceso = "ObteneridStock";
            MySqlDataAdapter dt = new MySqlDataAdapter(proceso, connection);
            dt.SelectCommand.CommandType = CommandType.StoredProcedure;
            dt.SelectCommand.Parameters.AddRange(oParam);
            dt.Fill(Tabla);
            if (Tabla.Rows.Count > 0)
            {
                foreach (DataRow item in Tabla.Rows)
                {
                    Stock = item["idStock"].ToString() + "," + item["Cantidad"].ToString();
                }
            }
            connection.Close();
            return Stock;
        }
        private static List<int> ValidarStockExistente(int idProducto)
        {
            connection.Close();
            List<int> cantidad = new List<int>();
            connection.Open();
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = connection;
            DataTable Tabla = new DataTable();
            MySqlParameter[] oParam = {
                                      new MySqlParameter("idProducto_in", idProducto) };
            string proceso = "ValidarStockExistente";
            MySqlDataAdapter dt = new MySqlDataAdapter(proceso, connection);
            dt.SelectCommand.CommandType = CommandType.StoredProcedure;
            dt.SelectCommand.Parameters.AddRange(oParam);
            dt.Fill(Tabla);
            if (Tabla.Rows.Count > 0)
            {
                foreach (DataRow item in Tabla.Rows)
                {
                    cantidad.Add(Convert.ToInt32(item["Cantidad"].ToString()));
                }
            }
            connection.Close();
            return cantidad;
        }
        public static bool InsertSalidaStock(List<Stock> stockObra, int idObraSeleccionada)
        {
            bool exito = false;
            int idMovimiento = 0;
            foreach (var item in stockObra)
            {
                connection.Close();
                connection.Open();
                string proceso = "AltaStockMovimientoSalida";
                MySqlCommand cmd = new MySqlCommand(proceso, connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("idProducto_in", item.idProducto);
                cmd.Parameters.AddWithValue("Cantidad_in", item.Cantidad);
                DateTime fecha = Convert.ToDateTime(DateTime.Now);
                cmd.Parameters.AddWithValue("Fecha_in", fecha);
                cmd.Parameters.AddWithValue("idObra_in", idObraSeleccionada);
                cmd.Parameters.AddWithValue("idUsuario_in", Sesion.UsuarioLogueado.idUsuario);
                cmd.Parameters.AddWithValue("FechaSalidaIngresada_in", item.FechaFactura);
                MySqlDataReader r = cmd.ExecuteReader();
                while (r.Read())
                {
                    idMovimiento = Convert.ToInt32(r["ID"].ToString());
                }
                if (idMovimiento > 0)
                {
                    exito = true;
                }
                else
                {
                    exito = false;
                }
                if (exito == true)
                {
                    int CantidadTotal = 0;
                    List<int> stockExistente = new List<int>();
                    stockExistente = ValidarStockExistente(item.idProducto);
                    if (stockExistente.Count > 0)
                    {
                        int cant = Convert.ToInt32(stockExistente[0].ToString());
                        CantidadTotal = cant - item.Cantidad;
                        exito = ActualizarStock(item.idProducto, CantidadTotal);
                    }
                    if (exito == true)
                    {
                        connection.Close();
                        connection.Open();
                        string proceso2 = "AltaMovimientoStock";
                        MySqlCommand cmd2 = new MySqlCommand(proceso2, connection);
                        cmd2.CommandType = CommandType.StoredProcedure;
                        cmd2.Parameters.AddWithValue("idProducto_in", item.idProducto);
                        cmd2.Parameters.AddWithValue("Cantidad_in", item.Cantidad);
                        DateTime fechaactual = Convert.ToDateTime(DateTime.Now);
                        cmd2.Parameters.AddWithValue("Fecha_in", item.FechaFactura);
                        ///// TipoMovimiento_in es parametro si entrada o salida de stock
                        cmd2.Parameters.AddWithValue("TipoMovimiento_in", "S");
                        cmd2.Parameters.AddWithValue("idMovimientoEntrada_in", item.idMovimientoEntrada);
                        cmd2.Parameters.AddWithValue("ValorUnitario_in", item.ValorUnitario);
                        cmd2.Parameters.AddWithValue("PrecioNeto_in", item.PrecioNeto);
                        cmd2.Parameters.AddWithValue("idSalida_in", idMovimiento);
                        cmd2.Parameters.AddWithValue("Observaciones_in", item.Observaciones);
                        cmd2.ExecuteNonQuery();
                        exito = true;
                        connection.Close();
                    }
                    else
                    {
                        exito = false;
                    }
                    if (exito == true && item.EstadoEntrada == 0)
                    {
                        exito = ModificarEstadoEntrada(item.idMovimientoEntrada);
                    }
                }
            }
            return exito;
        }
        private static bool ModificarEstadoEntrada(int idMovimientoEntrada)
        {
            bool exito = false;
            int Estado = 0;
            connection.Close();
            connection.Open();
            string Actualizar = "ModificarEstadoEntrada";
            MySqlCommand cmd = new MySqlCommand(Actualizar, connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("idMovimientoEntrada_in", idMovimientoEntrada);
            cmd.Parameters.AddWithValue("FechaCierre_in", DateTime.Now);
            cmd.Parameters.AddWithValue("EstadoEntrada_in", Estado);
            cmd.ExecuteNonQuery();
            exito = true;
            connection.Close();
            return exito;
        }
        public static bool FinalizarObra(int idObraSeleccionada)
        {
            bool exito = false;
            connection.Close();
            connection.Open();
            string Actualizar = "FinalizarObra";
            MySqlCommand cmd = new MySqlCommand(Actualizar, connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("idObraSeleccionada_in", idObraSeleccionada);
            cmd.Parameters.AddWithValue("Estado_in", 0);
            cmd.ExecuteNonQuery();
            exito = true;
            connection.Close();
            return exito;
        }
        public static int ValidaEstadoObra(int idObraSeleccionada)
        {
            connection.Close();
            int idObra = 0;
            connection.Open();
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = connection;
            DataTable Tabla = new DataTable();
            MySqlParameter[] oParam = {
                                      new MySqlParameter("idObraSeleccionada_in", idObraSeleccionada) };
            string proceso = "ValidaEstadoObra";
            MySqlDataAdapter dt = new MySqlDataAdapter(proceso, connection);
            dt.SelectCommand.CommandType = CommandType.StoredProcedure;
            dt.SelectCommand.Parameters.AddRange(oParam);
            dt.Fill(Tabla);
            if (Tabla.Rows.Count > 0)
            {
                foreach (DataRow item in Tabla.Rows)
                {
                    idObra = Convert.ToInt32(item["Estado"].ToString());
                }
            }
            connection.Close();
            return idObra;
        }
    }
}
