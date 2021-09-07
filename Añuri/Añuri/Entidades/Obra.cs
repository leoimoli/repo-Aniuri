using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Añuri.Entidades
{
    public class Obra
    {
        public int idObra { get; set; }
        public string NombreObra { get; set; }
        public string Contacto { get; set; }
        public string Email { get; set; }
        public string SitioWeb { get; set; }
        public string Calle { get; set; }
        public string Altura { get; set; }
        public string Telefono { get; set; }
        public byte[] Foto { get; set; }
        public DateTime FechaDeAlta { get; set; }
        public int idUsuario { get; set; }
        public int idProvincia { get; set; }
        public int idLocalidad { get; set; }
        public string NombreProvincia { get; set; }
        public string NombreLocalidad { get; set; }
        public string CodigoPostalLocalidad { get; set; }
        public int Estado { get; set; }
    }
}
