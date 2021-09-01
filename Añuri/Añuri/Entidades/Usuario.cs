using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Añuri.Entidades
{
    public class Usuario
    {
        public int idUsuario { get; set; }
        public string Dni { get; set; }
        public string Apellido { get; set; }
        public string Nombre { get; set; }
        public DateTime FechaDeNacimiento { get; set; }
        public DateTime FechaDeAlta { get; set; }
        public DateTime FechaUltimaConexion { get; set; }
        public string Contraseña { get; set; }
        public string Contraseña2 { get; set; }
        public string Perfil { get; set; }
        public int idPerfil { get; set; }
        public int Estado { get; set; }
    }
}
