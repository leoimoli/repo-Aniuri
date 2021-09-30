using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Añuri.Entidades
{
    public class Reporte_Obras
    {
        public string anno { get; set; }
        public string mes { get; set; }
        public int TotalVentasPorMes { get; set; }
        public int TotalDeVentasGenerales { get; set; }
        public decimal CajaDeVentas { get; set; }
        public string DescripcionProducto { get; set; }
        public int ProductoMasVendido { get; set; }
        public DateTime Fecha { get; set; }
        public decimal Precio { get; set; }
    }
}
