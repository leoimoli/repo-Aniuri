using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Añuri.Entidades
{
    public class SaldoInicialEnPesos
    {
        public int idProducto { get; set; }
        public string Producto { get; set; }
        public decimal Saldo { get; set; }
        public int CantidadSaldo { get; set; }
    }
}
