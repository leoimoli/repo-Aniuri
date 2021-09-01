using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Data;
using System.IO;
using Añuri.Entidades;
using Añuri.Clases_Maestras;

namespace Añuri.Dao
{
    public class UsuarioDao
    {
        private static MySql.Data.MySqlClient.MySqlConnection connection = new MySqlConnection(Properties.Settings.Default.db);
        public static List<Usuario> LoginUsuario(string usuario, string contraseña)
        {
            connection.Close();
            string estado = "1";
            connection.Open();
            List<Entidades.Usuario> lista = new List<Entidades.Usuario>();
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = connection;
            DataTable Tabla = new DataTable();
            MySqlParameter[] oParam = {
                                      new MySqlParameter("Dni_in", usuario),
                                       new MySqlParameter("Contrasenia_in", contraseña),
             new MySqlParameter("Estado_in", estado)};
            string proceso = "LoginUsuario";
            MySqlDataAdapter dt = new MySqlDataAdapter(proceso, connection);
            dt.SelectCommand.CommandType = CommandType.StoredProcedure;
            dt.SelectCommand.Parameters.AddRange(oParam);
            dt.Fill(Tabla);
            if (Tabla.Rows.Count > 0)
            {
                foreach (DataRow item in Tabla.Rows)
                {
                    Entidades.Usuario listaUsuario = new Entidades.Usuario();
                    listaUsuario.idUsuario = Convert.ToInt32(item["idUsuario"].ToString());
                    listaUsuario.Apellido = item["Apellido"].ToString();
                    listaUsuario.Nombre = item["Nombre"].ToString();
                    listaUsuario.Dni = item["Dni"].ToString();
                    listaUsuario.FechaDeAlta = Convert.ToDateTime(item["FechaDeAlta"].ToString());
                    listaUsuario.FechaUltimaConexion = Convert.ToDateTime(item["UltimoInicioSesion"].ToString());
                    listaUsuario.Contraseña = item["Contrasena"].ToString();
                    listaUsuario.Estado = Convert.ToInt32(item["Estado"].ToString());
                    listaUsuario.idPerfil = Convert.ToInt32(item["idPerfil"].ToString());
                    lista.Add(listaUsuario);
                }
            }
            connection.Close();
            return lista;
        }
        public static bool EditarUsuario(Usuario usuario, int idUsuarioSeleccionado)
        {
            bool exito = false;
            connection.Close();
            connection.Open();
            string Actualizar = "EditarUsuario";
            MySqlCommand cmd = new MySqlCommand(Actualizar, connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("idUsuario_in", idUsuarioSeleccionado);
            cmd.Parameters.AddWithValue("Apellido_in", usuario.Apellido);
            cmd.Parameters.AddWithValue("Nombre_in", usuario.Nombre);
            cmd.Parameters.AddWithValue("Perfil_in", usuario.Perfil);
            cmd.Parameters.AddWithValue("Estado_in", usuario.Estado);
            cmd.ExecuteNonQuery();
            exito = true;
            connection.Close();
            return exito;
        }
        public static List<string> CargarComboPerfiles()
        {
            connection.Close();
            connection.Open();
            List<string> _listaPerfiles = new List<string>();
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = connection;
            DataTable Tabla = new DataTable();
            MySqlParameter[] oParam = { };
            string proceso = "CargarComboPerfiles";
            MySqlDataAdapter dt = new MySqlDataAdapter(proceso, connection);
            dt.SelectCommand.CommandType = CommandType.StoredProcedure;
            dt.SelectCommand.Parameters.AddRange(oParam);
            dt.Fill(Tabla);
            if (Tabla.Rows.Count > 0)
            {
                foreach (DataRow item in Tabla.Rows)
                {
                    string Perfil = item["Nombre"].ToString();
                    _listaPerfiles.Add(Perfil);
                }
            }
            connection.Close();
            return _listaPerfiles;
        }
        public static List<Usuario> ListarUsuarios()
        {
            connection.Close();
            connection.Open();
            List<Usuario> _listaUsuarios = new List<Usuario>();
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = connection;
            DataTable Tabla = new DataTable();
            MySqlParameter[] oParam = { };
            string proceso = "ListarUsuarios";
            MySqlDataAdapter dt = new MySqlDataAdapter(proceso, connection);
            dt.SelectCommand.CommandType = CommandType.StoredProcedure;
            dt.SelectCommand.Parameters.AddRange(oParam);
            dt.Fill(Tabla);
            if (Tabla.Rows.Count > 0)
            {
                foreach (DataRow item in Tabla.Rows)
                {
                    Entidades.Usuario listaUsuario = new Entidades.Usuario();
                    listaUsuario.idUsuario = Convert.ToInt32(item["idUsuario"].ToString());
                    listaUsuario.Apellido = item["Apellido"].ToString();
                    listaUsuario.Nombre = item["Nombre"].ToString();
                    listaUsuario.Dni = item["Dni"].ToString();
                    listaUsuario.FechaDeAlta = Convert.ToDateTime(item["FechaDeAlta"].ToString());
                    listaUsuario.FechaUltimaConexion = Convert.ToDateTime(item["UltimoInicioSesion"].ToString());
                    listaUsuario.Contraseña = item["Contrasena"].ToString();
                    int estado = Convert.ToInt32(item["Estado"].ToString());
                    string estadoString = "";
                    if (estado == 1)
                    { estadoString = "Activo"; }
                    listaUsuario.Estado = estado;
                    listaUsuario.idPerfil = Convert.ToInt32(item["idPerfil"].ToString());
                    _listaUsuarios.Add(listaUsuario);
                }
            }
            connection.Close();
            return _listaUsuarios;
        }
        public static List<Usuario> BuscarUsuarioPorApellidoNombre(string ape, string nom)
        {
            connection.Close();
            connection.Open();
            List<Usuario> _listaUsuarios = new List<Usuario>();
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = connection;
            DataTable Tabla = new DataTable();
            MySqlParameter[] oParam = { new MySqlParameter("Apellido_in", ape),
            new MySqlParameter("Nombre_in", nom)};
            string proceso = "BuscarUsuarioPorApellidoNombre";
            MySqlDataAdapter dt = new MySqlDataAdapter(proceso, connection);
            dt.SelectCommand.CommandType = CommandType.StoredProcedure;
            dt.SelectCommand.Parameters.AddRange(oParam);
            dt.Fill(Tabla);
            if (Tabla.Rows.Count > 0)
            {
                foreach (DataRow item in Tabla.Rows)
                {
                    Usuario listaUsuarios = new Usuario();
                    listaUsuarios.idUsuario = Convert.ToInt32(item["idUsuario"].ToString());
                    listaUsuarios.Apellido = item["Apellido"].ToString();
                    listaUsuarios.Nombre = item["Nombre"].ToString();
                    listaUsuarios.Dni = item["Dni"].ToString();
                    listaUsuarios.FechaDeAlta = Convert.ToDateTime(item["FechaDeAlta"].ToString());
                    listaUsuarios.Contraseña = item["Contrasena"].ToString();
                    listaUsuarios.Perfil = item["idPerfil"].ToString();
                    listaUsuarios.Estado = Convert.ToInt32(item["Estado"].ToString());
                    _listaUsuarios.Add(listaUsuarios);
                }
            }
            connection.Close();
            return _listaUsuarios;
        }
        public static bool ResetearClave(string claveCifrada)
        {
            bool Exito = false;
            connection.Close();
            connection.Open();
            ///PROCEDIMIENTO
            string proceso = "ResetearClave";
            MySqlCommand cmd = new MySqlCommand(proceso, connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("idUsuario_in", Sesion.UsuarioLogueado.idUsuario);
            DateTime Fecha = DateTime.Now;
            cmd.Parameters.AddWithValue("claveCifrada_in", claveCifrada);
            cmd.ExecuteNonQuery();
            Exito = true;
            connection.Close();
            return Exito;
        }
        public static bool InsertUsuario(Usuario usuario)
        {
            bool exito = false;
            connection.Close();
            connection.Open();
            string proceso = "AltaUsuario";
            MySqlCommand cmd = new MySqlCommand(proceso, connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("Dni_in", usuario.Dni);
            cmd.Parameters.AddWithValue("Apellido_in", usuario.Apellido);
            cmd.Parameters.AddWithValue("Nombre_in", usuario.Nombre);
            cmd.Parameters.AddWithValue("FechaDeNacimiento_in", usuario.FechaDeNacimiento);
            cmd.Parameters.AddWithValue("Contraseña_in", usuario.Contraseña);
            cmd.Parameters.AddWithValue("FechaDeAlta_in", usuario.FechaDeAlta);
            cmd.Parameters.AddWithValue("FechaUltimaConexion_in", usuario.FechaUltimaConexion);
            cmd.Parameters.AddWithValue("Perfil_in", usuario.Perfil);
            cmd.Parameters.AddWithValue("Estado_in", usuario.Estado);
            cmd.Parameters.AddWithValue("idUsuarioLogueado_in", Sesion.UsuarioLogueado.idUsuario);
            cmd.ExecuteNonQuery();
            exito = true;
            connection.Close();
            return exito;
        }
        public static bool ValidarUsuarioExistente(string dni)
        {
            connection.Close();
            bool Existe = false;
            connection.Open();
            List<Usuario> lista = new List<Usuario>();
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = connection;
            DataTable Tabla = new DataTable();
            MySqlParameter[] oParam = {
                                      new MySqlParameter("Dni_in", dni) };
            string proceso = "ValidarUsuarioExistente";
            MySqlDataAdapter dt = new MySqlDataAdapter(proceso, connection);
            dt.SelectCommand.CommandType = CommandType.StoredProcedure;
            dt.SelectCommand.Parameters.AddRange(oParam);
            dt.Fill(Tabla);
            DataSet ds = new DataSet();
            if (Tabla.Rows.Count > 0)
            {
                Existe = true;
            }
            connection.Close();
            return Existe;
        }
        public static void ActualizarUltimaConexion(int idUsuario)
        {
            connection.Close();
            connection.Open();
            ///PROCEDIMIENTO
            string proceso = "ActualizarUltimaConexion";
            MySqlCommand cmd = new MySqlCommand(proceso, connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("idUsuario_in", idUsuario);
            DateTime Fecha = DateTime.Now;
            cmd.Parameters.AddWithValue("FechaUltimaConexion_in", Fecha);
            cmd.ExecuteNonQuery();
            connection.Close();
        }
        public static List<MenuPorPerfilUsuario> BuscarMenuPorPerfilUsuario(int idPerfil)
        {
            connection.Close();
            connection.Open();
            List<Entidades.MenuPorPerfilUsuario> lista = new List<Entidades.MenuPorPerfilUsuario>();
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = connection;
            DataTable Tabla = new DataTable();
            MySqlParameter[] oParam = {
                                      new MySqlParameter("idPerfil_in", idPerfil),
                                      };
            string proceso = "BuscarMenuPorPerfilUsuario";
            MySqlDataAdapter dt = new MySqlDataAdapter(proceso, connection);
            dt.SelectCommand.CommandType = CommandType.StoredProcedure;
            dt.SelectCommand.Parameters.AddRange(oParam);
            dt.Fill(Tabla);
            if (Tabla.Rows.Count > 0)
            {
                foreach (DataRow item in Tabla.Rows)
                {
                    Entidades.MenuPorPerfilUsuario listaMenu = new Entidades.MenuPorPerfilUsuario();
                    listaMenu.idMenuPorPerfil = Convert.ToInt32(item["idMenuPorPerfil"].ToString());
                    listaMenu.idPerfil = Convert.ToInt32(item["idPerfil"].ToString());
                    listaMenu.NombreMenu = item["NombreMenu"].ToString();
                    lista.Add(listaMenu);
                }
            }
            connection.Close();
            return lista;
        }
    }
}
