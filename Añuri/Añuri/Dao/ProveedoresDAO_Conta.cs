using Añuri.Clases_Maestras;
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
    public class ProveedoresDAO_Conta
    {
        private static MySql.Data.MySqlClient.MySqlConnection connection = new MySqlConnection(Properties.Settings.Default.db);
        public static bool ValidarProveedorExistente(string dni)
        {
            bool Exito = false;
            try
            {
                connection.Close();
                connection.Open();
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = connection;
                DataTable Tabla = new DataTable();
                MySqlParameter[] oParam = {
                                      new MySqlParameter("Dni_in", dni) };
                string proceso = "Conta_ValidarProveedorExistente";
                MySqlDataAdapter dt = new MySqlDataAdapter(proceso, connection);
                dt.SelectCommand.CommandType = CommandType.StoredProcedure;
                dt.SelectCommand.Parameters.AddRange(oParam);
                dt.Fill(Tabla);
                DataSet ds = new DataSet();
                if (Tabla.Rows.Count > 0)
                {
                    Exito = true;
                }
                connection.Close();
            }
            catch (Exception ex)
            { }
            return Exito;
        }
        public static int ObteneridIngresosBrutos(string ingresosBrutos)
        {
            int Id = 0;
            try
            {
                ///// QUITO ESPACIOS Blancos
                ingresosBrutos = ingresosBrutos.Trim();
                connection.Close();
                connection.Open();
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = connection;
                DataTable Tabla = new DataTable();
                MySqlParameter[] oParam = {
                                      new MySqlParameter("ingresosBrutos_in", ingresosBrutos) };
                string proceso = "Conta_ObteneridIngresosBrutos";
                MySqlDataAdapter dt = new MySqlDataAdapter(proceso, connection);
                dt.SelectCommand.CommandType = CommandType.StoredProcedure;
                dt.SelectCommand.Parameters.AddRange(oParam);
                dt.Fill(Tabla);
                DataSet ds = new DataSet();
                if (Tabla.Rows.Count > 0)
                {
                    foreach (DataRow item in Tabla.Rows)
                    {
                        Id = Convert.ToInt32(item["idIngresosBrutos"].ToString());
                    }
                }
                connection.Close();
            }
            catch (Exception ex)
            { }
            return Id;
        }
        public static int RegistrarObteneridIngresosBrutos(string ingresosBrutos)
        {
            int Id = 0;
            try
            {
                ///// QUITO ESPACIOS Blancos
                ingresosBrutos = ingresosBrutos.Trim();
                connection.Close();
                connection.Open();
                string proceso = "Conta_RegistrarObteneridIngresosBrutos";
                MySqlCommand cmd = new MySqlCommand(proceso, connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("ingresosBrutos_in", ingresosBrutos);
                cmd.Parameters.AddWithValue("FechaRegistro_in", DateTime.Now);
                MySqlDataReader r = cmd.ExecuteReader();
                while (r.Read())
                {
                    Id = Convert.ToInt32(r["ID"].ToString());
                }
            }
            catch (Exception ex)
            { }
            return Id;
        }
        public static int ObteneridSitIva(string sitIva)
        {
            int Id = 0;
            try
            {
                connection.Close();
                connection.Open();
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = connection;
                DataTable Tabla = new DataTable();
                MySqlParameter[] oParam = {
                                      new MySqlParameter("sitIva_in", sitIva) };
                string proceso = "Conta_ObteneridSitIva";
                MySqlDataAdapter dt = new MySqlDataAdapter(proceso, connection);
                dt.SelectCommand.CommandType = CommandType.StoredProcedure;
                dt.SelectCommand.Parameters.AddRange(oParam);
                dt.Fill(Tabla);
                DataSet ds = new DataSet();
                if (Tabla.Rows.Count > 0)
                {
                    foreach (DataRow item in Tabla.Rows)
                    {
                        Id = Convert.ToInt32(item["idSitIva"].ToString());
                    }
                }
                connection.Close();
            }
            catch (Exception ex)
            { }
            return Id;
        }
        public static int RegistrarSitIva(string sitIva)
        {
            int Id = 0;
            try
            {
                ///// QUITO ESPACIOS Blancos
                sitIva = sitIva.Trim();
                connection.Close();
                connection.Open();
                string proceso = "Conta_RegistrarSitIva";
                MySqlCommand cmd = new MySqlCommand(proceso, connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("sitIva_in", sitIva);
                cmd.Parameters.AddWithValue("FechaRegistro_in", DateTime.Now);
                MySqlDataReader r = cmd.ExecuteReader();
                while (r.Read())
                {
                    Id = Convert.ToInt32(r["ID"].ToString());
                }
            }
            catch (Exception ex)
            { }
            return Id;
        }
        public static int ObteneridPais(string pais)
        {
            int Id = 0;
            try
            {
                ///// QUITO ESPACIOS Blancos
                pais = pais.Trim();
                connection.Close();
                connection.Open();
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = connection;
                DataTable Tabla = new DataTable();
                MySqlParameter[] oParam = {
                                      new MySqlParameter("pais_in", pais) };
                string proceso = "Conta_ObteneridPais";
                MySqlDataAdapter dt = new MySqlDataAdapter(proceso, connection);
                dt.SelectCommand.CommandType = CommandType.StoredProcedure;
                dt.SelectCommand.Parameters.AddRange(oParam);
                dt.Fill(Tabla);
                DataSet ds = new DataSet();
                if (Tabla.Rows.Count > 0)
                {
                    foreach (DataRow item in Tabla.Rows)
                    {
                        Id = Convert.ToInt32(item["idPais"].ToString());
                    }
                }
                connection.Close();
            }
            catch (Exception ex)
            { }
            return Id;
        }
        public static int RegistrarObteneridPais(string pais)
        {
            int Id = 0;
            try
            {
                ///// QUITO ESPACIOS Blancos
                pais = pais.Trim();
                connection.Close();
                connection.Open();
                string proceso = "Conta_RegistrarObteneridPais";
                MySqlCommand cmd = new MySqlCommand(proceso, connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("pais_in", pais);
                MySqlDataReader r = cmd.ExecuteReader();
                while (r.Read())
                {
                    Id = Convert.ToInt32(r["ID"].ToString());
                }
            }
            catch (Exception ex)
            { }
            return Id;
        }
        public static int ObteneridProvincia(string provincia, int idPais)
        {
            int Id = 0;
            try
            {
                ///// QUITO ESPACIOS Blancos
                provincia = provincia.Trim();
                ///// AGREGO SALTO DE LINEA FINAL POR VALOR PRECARGADO EN TABLA EXISTENTE....
                provincia = provincia + "\r";
                connection.Close();
                connection.Open();
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = connection;
                DataTable Tabla = new DataTable();
                MySqlParameter[] oParam = {
                                      new MySqlParameter("provincia_in", provincia),
                new MySqlParameter("idPais_in", idPais)};
                string proceso = "Conta_ObteneridProvincia";
                MySqlDataAdapter dt = new MySqlDataAdapter(proceso, connection);
                dt.SelectCommand.CommandType = CommandType.StoredProcedure;
                dt.SelectCommand.Parameters.AddRange(oParam);
                dt.Fill(Tabla);
                DataSet ds = new DataSet();
                if (Tabla.Rows.Count > 0)
                {
                    foreach (DataRow item in Tabla.Rows)
                    {
                        Id = Convert.ToInt32(item["idProvincia"].ToString());
                    }
                }
                connection.Close();
            }
            catch (Exception ex)
            { }
            return Id;
        }
        public static int RegistrarObteneridProvincia(string provincia, int idPais)
        {
            int Id = 0;
            try
            {
                ///// QUITO ESPACIOS Blancos
                provincia = provincia.Trim();
                connection.Close();
                connection.Open();
                string proceso = "Conta_RegistrarObteneridProvincia";
                MySqlCommand cmd = new MySqlCommand(proceso, connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("provincia_in", provincia);
                cmd.Parameters.AddWithValue("idPais_in", idPais);
                MySqlDataReader r = cmd.ExecuteReader();
                while (r.Read())
                {
                    Id = Convert.ToInt32(r["ID"].ToString());
                }
            }
            catch (Exception ex)
            { }
            return Id;
        }
        public static int ObtenerObteneridLocalidad(string localidad, int idProvincia)
        {
            int Id = 0;
            try
            {
                ///// QUITO ESPACIOS Blancos
                localidad = localidad.Trim();
                connection.Close();
                connection.Open();
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = connection;
                DataTable Tabla = new DataTable();
                MySqlParameter[] oParam = {
                                      new MySqlParameter("localidad_in", localidad),
                new MySqlParameter("idProvincia_in", idProvincia)};
                string proceso = "Conta_ObteneridLocalidad";
                MySqlDataAdapter dt = new MySqlDataAdapter(proceso, connection);
                dt.SelectCommand.CommandType = CommandType.StoredProcedure;
                dt.SelectCommand.Parameters.AddRange(oParam);
                dt.Fill(Tabla);
                DataSet ds = new DataSet();
                if (Tabla.Rows.Count > 0)
                {
                    foreach (DataRow item in Tabla.Rows)
                    {
                        Id = Convert.ToInt32(item["idLocalidad"].ToString());
                    }
                }
                connection.Close();
            }
            catch (Exception ex)
            { }
            return Id;
        }
        public static int RegistrarObteneridLocalidad(string localidad, int idProvincia)
        {
            int Id = 0;
            try
            {
                ///// QUITO ESPACIOS Blancos
                localidad = localidad.Trim();
                connection.Close();
                connection.Open();
                string proceso = "Conta_RegistrarObteneridLocalidad";
                MySqlCommand cmd = new MySqlCommand(proceso, connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("localidad_in", localidad);
                cmd.Parameters.AddWithValue("idProvincia_in", idProvincia);
                MySqlDataReader r = cmd.ExecuteReader();
                while (r.Read())
                {
                    Id = Convert.ToInt32(r["ID"].ToString());
                }
            }
            catch (Exception ex)
            { }
            return Id;
        }
        public static bool RegistrarProveedor(ProveedoresModCont item)
        {
            bool Exito = false;
            try
            {
                connection.Close();
                connection.Open();
                string proceso = "Conta_RegistrarProveedor";
                MySqlCommand cmd = new MySqlCommand(proceso, connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("RazonSocial_in", item.RazonSocial);
                cmd.Parameters.AddWithValue("idPais_in", item.idPais);
                cmd.Parameters.AddWithValue("idProvincia_in", item.idProvincia);
                cmd.Parameters.AddWithValue("idLocalalidad_in", item.idLocalalidad);
                cmd.Parameters.AddWithValue("Domicilio_in", item.Domicilio);
                cmd.Parameters.AddWithValue("Altura_in", item.Altura);
                cmd.Parameters.AddWithValue("Piso_in", item.Piso);
                cmd.Parameters.AddWithValue("Departamento_in", item.Departamento);
                cmd.Parameters.AddWithValue("CodigoPostal_in", item.CodigoPostal);
                cmd.Parameters.AddWithValue("idSitIva_in", item.idSitIva);
                cmd.Parameters.AddWithValue("Dni_in", item.Dni);
                cmd.Parameters.AddWithValue("idIngresosBrutos_in", item.idIngresosBrutos);
                cmd.Parameters.AddWithValue("NumeroIB_in", item.NumeroIB);
                cmd.ExecuteNonQuery();
                Exito = true;
                connection.Close();
                return Exito;
            }
            catch (Exception ex)
            { }
            return Exito;
        }
    }
}
