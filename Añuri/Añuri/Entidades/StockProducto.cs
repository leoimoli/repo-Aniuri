using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Añuri.Entidades
{
    public class StockProducto
    {
        public Decimal SumaEntrada { get; set; }
        public Decimal SumaSalida { get; set; }
        public Decimal Valor { get; set; }
        public int SumaEntradaKilos { get; set; }
        public int SumaSalidaKilos { get; set; }
        public int kilos { get; set; }
        public string TipoMovimiento { get; set; }
       
    }
}
