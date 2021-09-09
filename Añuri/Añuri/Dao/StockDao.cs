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
    public class StockDao
    {
        private static MySql.Data.MySqlClient.MySqlConnection connection = new MySqlConnection(Properties.Settings.Default.db);
        public static bool InsertarStock(List<Stock> listaStock)
        {
            bool exito = false;
            int idMovimiento = 0;
            connection.Close();
            connection.Open();
            string proceso = "AltaStockMovimientoEntrada";
            MySqlCommand cmd = new MySqlCommand(proceso, connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("Proveedor_in", listaStock[0].idProveedor);
            cmd.Parameters.AddWithValue("FechaCompra_in", listaStock[0].FechaFactura);
            cmd.Parameters.AddWithValue("Remito_in", listaStock[0].Remito);
            DateTime fecha = Convert.ToDateTime(DateTime.Now.ToShortDateString());
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
                foreach (var item in listaStock)
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
                        DateTime fechaactual = Convert.ToDateTime(DateTime.Now.ToShortDateString());
                        cmd2.Parameters.AddWithValue("Fecha_in", fechaactual);
                        ///// TipoMovimiento_in es parametro si entrada o salida de stock
                        cmd2.Parameters.AddWithValue("TipoMovimiento_in", "E");
                        cmd2.Parameters.AddWithValue("idMovimientoEntrada_in", idMovimiento);
                        cmd2.Parameters.AddWithValue("ValorUnitario_in", item.ValorUnitario);
                        cmd2.Parameters.AddWithValue("PrecioNeto_in", item.PrecioNeto);                       
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
    }
}
