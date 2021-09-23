using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Añuri.Entidades
{
    public class Stock
    {
        public int idUsuario { get; set; }
        public int idProducto { get; set; }
        public string CodigoProducto { get; set; }
        public string Descripcion { get; set; }
        public int idProveedor { get; set; }
        public string Proveedor { get; set; }
        public string Marca { get; set; }
        public string Remito { get; set; }
        public int Cantidad { get; set; }
        public DateTime FechaFactura { get; set; }
        public Decimal ValorUnitario { get; set; }
        public Decimal PrecioNeto { get; set; }
        public string TipoMovimiento { get; set; }
        public int idMovimientoEntrada { get; set; }
        public int EstadoEntrada { get; set; }
      
    }
}
