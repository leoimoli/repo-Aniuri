using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Añuri.Entidades
{
   public class ResultadoGrillaEnKilos
    {
        public int idProducto { get; set; }
        public string nombre { get; set; }
        public int valorInicial { get; set; }
        public List<Stock> movimientos { get; set; }
    }
}
