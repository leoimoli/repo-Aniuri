﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Añuri.Clases_Maestras;
using Añuri.Entidades;
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
                        cmd2.Parameters.AddWithValue("Fecha_in", fechaactual);
                        ///// TipoMovimiento_in es parametro si entrada o salida de stock
                        cmd2.Parameters.AddWithValue("TipoMovimiento_in", "E");
                        cmd2.Parameters.AddWithValue("idMovimientoEntrada_in", idMovimiento);
                        cmd2.Parameters.AddWithValue("ValorUnitario_in", item.ValorUnitario);
                        cmd2.Parameters.AddWithValue("PrecioNeto_in", item.PrecioNeto);
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
                        cmd2.Parameters.AddWithValue("Fecha_in", fechaactual);
                        ///// TipoMovimiento_in es parametro si entrada o salida de stock
                        cmd2.Parameters.AddWithValue("TipoMovimiento_in", "S");
                        cmd2.Parameters.AddWithValue("idMovimientoEntrada_in", item.idMovimientoEntrada);
                        cmd2.Parameters.AddWithValue("ValorUnitario_in", item.ValorUnitario);
                        cmd2.Parameters.AddWithValue("PrecioNeto_in", item.PrecioNeto);
                        cmd2.Parameters.AddWithValue("idSalida_in", idMovimiento);
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
            connection.Close();
            connection.Open();
            string Actualizar = "ModificarEstadoEntrada";
            MySqlCommand cmd = new MySqlCommand(Actualizar, connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("idMovimientoEntrada_in", idMovimientoEntrada);
            cmd.Parameters.AddWithValue("EstadoEntrada_in", 0);
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
