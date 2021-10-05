using Añuri.Entidades;
using Añuri.Negocio;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Añuri
{
    public partial class InventarioStockWF : Form
    {
        public InventarioStockWF()
        {
            InitializeComponent();
        }
        private void btnBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                DateTime Fecha = dtFechaHasta.Value;
                List<Stock> ListaStock = StockNeg.ListarMovimientosStockInventarioPorFecha(Fecha);
                if (ListaStock.Count > 0)
                {
                    var GrupoPorProducto = ListaStock.GroupBy(l => l.idProducto)
                            .Select(la => new
                            {
                                IdGrupo = la.Key,
                                //NoArticulos = la.Count(),
                                //SumaKilos = la.Sum(s => s.Cantidad),

                            }).ToList();
                    int contadorDeProductos = GrupoPorProducto.Count;
                    int idProducto = 0;                 
                    //var GrupoPorProducto = ListaStock.GroupBy(l => l.idProducto)
                    //        .Select(la => new
                    //        {
                    //            IdGrupo = la.Key,
                    //            Material = la.ToString()
                    //            NoArticulos = la.Count(),
                    //            SumaKilos = la.Sum(s => s.Cantidad),

                    //        }).ToList();
                    //var GrupoPorProductoFecha = GrupoPorProducto.GroupBy(l => l.idProducto)
                    //        .Select(la => new
                    //        {
                    //            IdGrupo = la.Key,
                    //            //NoArticulos = la.Count(),
                    //            //SumaKilos = la.Sum(s => s.Cantidad),

                    //        }).ToList();
                }
            }
            catch (Exception ex)
            { }
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void InventarioStockWF_Load(object sender, EventArgs e)
        {

        }
    }
}
