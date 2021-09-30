using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Añuri.Entidades
{
    public class Reporte_Proveedores
    {
        public int idProveedor { get; set; }
        public string NombreEmpresa { get; set; }
        public int TotalCompras { get; set; }
        public DateTime Fecha { get; set; }
        public string Proveedor { get; set; }
        public string Remito { get; set; }

        public decimal TotalComprasEnPesos { get; set; }
    }
}
