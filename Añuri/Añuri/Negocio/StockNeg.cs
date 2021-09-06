using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Añuri.Dao;
using Añuri.Entidades;

namespace Añuri.Negocio
{
    public class StockNeg
    {
        public static bool CargarlistaStock(List<Stock> listaStock)
        {
            bool exito = false;
            try
            {
                exito = StockDao.InsertarStock(listaStock);
            }
            catch (Exception ex)
            { }
            return exito;
        }
    }
}
