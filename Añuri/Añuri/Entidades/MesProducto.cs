using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Añuri.Entidades
{
   public class MesProducto
    {
        public int idProducto { get; set; }
        public string Producto { get; set; }
        public decimal Monto { get; set; }
        public int Mes { get; set; }
        public string NombreMes { get; set; }

        public decimal SaldoInicial { get; set; }

        public int CantidadSaldoInicial { get; set; }

        public int Cantidad { get; set; }
    }
}
