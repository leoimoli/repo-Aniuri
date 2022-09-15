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
        public string TipoMedicion { get; set; }
        public int idProveedor { get; set; }
        public string Proveedor { get; set; }
        public string Marca { get; set; }
        public string Remito { get; set; }
        public int Cantidad { get; set; }
        public int StockTotal { get; set; }
        public DateTime FechaFactura { get; set; }
        public Decimal ValorUnitario { get; set; }
        public Decimal PrecioNeto { get; set; }
        public string TipoMovimiento { get; set; }
        public int idMovimientoEntrada { get; set; }
        public int EstadoEntrada { get; set; }
        public string Mes { get; set; }
        public string Año { get; set; }
        public int CantidadEntradas { get; set; }
        public int CantidadSalidas { get; set; }
        public Decimal ImporteTotalMateriales { get; set; }
        public DateTime FechaCierre { get; set; }
        public DateTime FechaMovimiento { get; set; }
        public string Observaciones { get; set; }
        public string NombreObra { get; set; }
        public int idObra { get; set; }
        public int idMovimiento { get; set; }
        public int PosicionEnLista { get; set; }
    }
    public class ObraReporte
    {
        public string NombreObra { get; set; }
        public int idObra { get; set; }
        public List<KiloReporte> ListaKilos { get; set; }
        public List<UnidadReporte> ListaUnidad { get; set; }
    }
    public class KiloReporte
    {
        public int idProducto { get; set; }
        public string Descripcion { get; set; }
        public int Cantidad { get; set; }
        public Decimal PrecioNeto { get; set; }
    }
    public class UnidadReporte
    {
        public int idProducto { get; set; }
        public string Descripcion { get; set; }
        public int Cantidad { get; set; }
        public Decimal PrecioNeto { get; set; }
    }
}

