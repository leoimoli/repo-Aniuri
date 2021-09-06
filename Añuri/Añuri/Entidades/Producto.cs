using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Añuri.Entidades
{
    public class Producto
    {
        public int idProducto { get; set; }
        public string CodigoProducto { get; set; }
        public string DescripcionProducto { get; set; }
        public DateTime FechaDeAlta { get; set; }
        public int idUsuario { get; set; }
        public string Usuario { get; set; }
        public int Stock { get; set; }
        public int idProveedor { get; set; }
        public int Unidades { get; set; }
    }
}
