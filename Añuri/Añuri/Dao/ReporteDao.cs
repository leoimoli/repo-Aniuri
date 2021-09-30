using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Añuri.Entidades;
using MySql.Data.MySqlClient;
using System.Data;

namespace Añuri.Dao
{
    public class ReporteDao
    {
        private static MySql.Data.MySqlClient.MySqlConnection connection = new MySqlConnection(Properties.Settings.Default.db);
        public static List<Reporte_Proveedores> BuscarTotalComprasRealizadasProveedores()
        {
            connection.Close();
            connection.Open();
            List<Reporte_Proveedores> _listaProveedores = new List<Reporte_Proveedores>();
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = connection;
            DataTable Tabla = new DataTable();
            MySqlParameter[] oParam = { };
            string proceso = "BuscarTotalComprasRealizadasProveedores";
            MySqlDataAdapter dt = new MySqlDataAdapter(proceso, connection);
            dt.SelectCommand.CommandType = CommandType.StoredProcedure;
            dt.SelectCommand.Parameters.AddRange(oParam);
            dt.Fill(Tabla);
            if (Tabla.Rows.Count > 0)
            {
                foreach (DataRow item in Tabla.Rows)
                {
                    Entidades.Reporte_Proveedores listaProveedores = new Entidades.Reporte_Proveedores();
                    //listaProveedores.idProveedor = Convert.ToInt32(item["idProducto"].ToString());
                    listaProveedores.NombreEmpresa = item["Proveedor"].ToString();
                    if (Convert.ToDecimal(item["Total"].ToString()) > 0)
                    {
                        listaProveedores.TotalComprasEnPesos = Convert.ToDecimal(item["Total"].ToString());
                    }
                    else
                    {
                        listaProveedores.TotalCompras = 0;
                    }
                    _listaProveedores.Add(listaProveedores);
                }
            }
            connection.Close();
            return _listaProveedores;
        }

        public static List<Reporte_Stock> BuscarMaterialesMasUtilizados()
        {
            connection.Close();
            connection.Open();
            List<Reporte_Stock> _listaStock = new List<Reporte_Stock>();
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = connection;
            DataTable Tabla = new DataTable();
            MySqlParameter[] oParam = { };
            string proceso = "BuscarMaterialesMasUtilizados";
            MySqlDataAdapter dt = new MySqlDataAdapter(proceso, connection);
            dt.SelectCommand.CommandType = CommandType.StoredProcedure;
            dt.SelectCommand.Parameters.AddRange(oParam);
            dt.Fill(Tabla);
            if (Tabla.Rows.Count > 0)
            {
                foreach (DataRow item in Tabla.Rows)
                {
                    Reporte_Stock lista = new Reporte_Stock();
                    lista.Descripcion = item["Material"].ToString();
                    lista.Cantidad = Convert.ToInt32(item["Cantidad"].ToString());
                    _listaStock.Add(lista);
                }
            }
            connection.Close();
            return _listaStock;
        }

        public static List<Reporte_Stock> BuscarMaterialesConMasStock()
        {
            connection.Close();
            connection.Open();
            List<Reporte_Stock> _listaStock = new List<Reporte_Stock>();
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = connection;
            DataTable Tabla = new DataTable();
            MySqlParameter[] oParam = { };
            string proceso = "BuscarMaterialesConMasStock";
            MySqlDataAdapter dt = new MySqlDataAdapter(proceso, connection);
            dt.SelectCommand.CommandType = CommandType.StoredProcedure;
            dt.SelectCommand.Parameters.AddRange(oParam);
            dt.Fill(Tabla);
            if (Tabla.Rows.Count > 0)
            {
                foreach (DataRow item in Tabla.Rows)
                {
                    Reporte_Stock lista = new Reporte_Stock();
                    lista.Descripcion = item["Material"].ToString();
                    lista.Cantidad = Convert.ToInt32(item["Cantidad"].ToString());
                    _listaStock.Add(lista);
                }
            }
            connection.Close();
            return _listaStock;
        }
        public static List<Reporte_Obras> BuscarObrasPorMes()
        {
            String Año = DateTime.Now.Year.ToString();

            string FechaArmadaDesde = "01/01/" + Año;
            DateTime FechaDesde = Convert.ToDateTime(FechaArmadaDesde);

            string FechaArmadaHasta = "31/12/" + Año;
            DateTime FechaHasta = Convert.ToDateTime(FechaArmadaHasta);

            connection.Close();
            connection.Open();
            List<Reporte_Obras> _listaVentas = new List<Reporte_Obras>();
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = connection;
            DataTable Tabla = new DataTable();
            MySqlParameter[] oParam = { new MySqlParameter("FechaDesde_in", FechaDesde),
            new MySqlParameter("FechaHasta_in", FechaHasta)};
            string proceso = "BuscarObrasPorMes";
            MySqlDataAdapter dt = new MySqlDataAdapter(proceso, connection);
            dt.SelectCommand.CommandType = CommandType.StoredProcedure;
            dt.SelectCommand.Parameters.AddRange(oParam);
            dt.Fill(Tabla);
            if (Tabla.Rows.Count > 0)
            {
                foreach (DataRow item in Tabla.Rows)
                {
                    Entidades.Reporte_Obras listaVentas = new Entidades.Reporte_Obras();
                    //listaProveedores.idProveedor = Convert.ToInt32(item["idProducto"].ToString());
                    listaVentas.mes = item["mes"].ToString();
                    if (Convert.ToInt32(item["Obra"].ToString()) > 0)
                    {
                        listaVentas.TotalVentasPorMes = Convert.ToInt32(item["Obra"].ToString());
                    }
                    else
                    { listaVentas.TotalVentasPorMes = 0; }
                    _listaVentas.Add(listaVentas);
                }
            }
            connection.Close();
            return _listaVentas;
        }
    }
}
