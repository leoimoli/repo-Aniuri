using Añuri.Dao;
using Añuri.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace Añuri.Negocio
{
    public class UsuarioNeg
    {
        public static List<Usuario> LoginUsuario(string usuario, string contraseña)
        {
            List<Entidades.Usuario> lista = new List<Entidades.Usuario>();
            lista = UsuarioDao.LoginUsuario(usuario, contraseña);
            if (lista.Count > 0)
            {
                int idUsuario = Convert.ToInt32(lista[0].idUsuario.ToString());
                UsuarioDao.ActualizarUltimaConexion(idUsuario);
            }
            return lista;
        }
        public static List<MenuPorPerfilUsuario> BuscarMenuPorPerfilUsuario(int idPerfil)
        {
            List<Entidades.MenuPorPerfilUsuario> lista = new List<Entidades.MenuPorPerfilUsuario>();
            lista = UsuarioDao.BuscarMenuPorPerfilUsuario(idPerfil);
            return lista;
        }
        public static List<Usuario> ListarUsuarios()
        {
            List<Usuario> _listaUsuarios = new List<Usuario>();
            try
            {
                _listaUsuarios = Dao.UsuarioDao.ListarUsuarios();
            }
            catch (Exception ex)
            {

            }
            return _listaUsuarios;
        }
        public static List<string> CargarComboPerfiles()
        {
            List<string> _listaPerfiles = new List<string>();
            try
            {
                _listaPerfiles = Dao.UsuarioDao.CargarComboPerfiles();
            }
            catch (Exception ex)
            {

            }
            return _listaPerfiles;
        }
        public static bool EditarUsuario(Usuario usuario, int idUsuarioSeleccionado)
        {
            bool exito = false;
            try
            {
                ValidarDatos(usuario);
                exito = UsuarioDao.EditarUsuario(usuario, idUsuarioSeleccionado);
            }
            catch (Exception ex)
            {

            }
            return exito;
        }
        private static void ValidarDatos(Usuario usuario)
        {
            if (String.IsNullOrEmpty(usuario.Dni))
            {
                const string message = "El campo dni es obligatorio.";
                const string caption = "Error";
                var result = MessageBox.Show(message, caption,
                                             MessageBoxButtons.OK,
                                           MessageBoxIcon.Exclamation);
                throw new Exception();
            }
            if (String.IsNullOrEmpty(usuario.Apellido))
            {
                const string message = "El campo apellido es obligatorio.";
                const string caption = "Error";
                var result = MessageBox.Show(message, caption,
                                             MessageBoxButtons.OK,
                                           MessageBoxIcon.Exclamation);
                throw new Exception();
            }
            if (String.IsNullOrEmpty(usuario.Nombre))
            {
                const string message = "El campo nombre es obligatorio.";
                const string caption = "Error";
                var result = MessageBox.Show(message, caption,
                                             MessageBoxButtons.OK,
                                           MessageBoxIcon.Exclamation);
                throw new Exception();
            }

            if (String.IsNullOrEmpty(usuario.Perfil))
            {
                const string message = "El perfil es un campo obligatorio.";
                const string caption = "Error";
                var result = MessageBox.Show(message, caption,
                                             MessageBoxButtons.OK,
                                           MessageBoxIcon.Exclamation);
                throw new Exception();
            }
            if (usuario.Perfil != "2" & usuario.Perfil != "3")
            {
                const string message = "El perfil ingresado es inexistente.";
                const string caption = "Error";
                var result = MessageBox.Show(message, caption,
                                             MessageBoxButtons.OK,
                                           MessageBoxIcon.Exclamation);
                throw new Exception();
            }
        }
        public static bool CargarUsuario(Usuario usuario)
        {
            bool exito = false;
            try
            {
                ValidarDatos(usuario);
                bool UsuarioExistente = ValidarUsuarioExistente(usuario.Dni);
                if (UsuarioExistente == true)
                {
                    const string message = "Ya existe un usuario registrado con el dni ingresado.";
                    const string caption = "Error";
                    var result = MessageBox.Show(message, caption,
                                                 MessageBoxButtons.OK,
                                               MessageBoxIcon.Exclamation);
                    throw new Exception();
                }
                else
                {
                    exito = UsuarioDao.InsertUsuario(usuario);
                }
            }
            catch (Exception ex)
            {

            }
            return exito;
        }
        private static bool ValidarUsuarioExistente(string dni)
        {
            bool existe = UsuarioDao.ValidarUsuarioExistente(dni);
            return existe;
        }
        public static List<Usuario> BuscarUsuarioPorApellidoNombre(string ape, string nom)
        {
            List<Usuario> _lista = new List<Usuario>();
            try
            {
                _lista = UsuarioDao.BuscarUsuarioPorApellidoNombre(ape, nom);
            }
            catch (Exception ex)
            {
            }
            return _lista;
        }
    }
}

