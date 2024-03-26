using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Añuri.Clases_Maestras
{
    public class ValoresConstantes
    {
        public static string[] Version
        {
            get
            {
                return new string[] { "Añuri Hispanoamericana S.A.  V-1.1.0" };
            }
        }
        public static string[] TipoComprobante
        {
            get
            {
                return new string[] { "Prueba 1", "Prueba 2" , "Prueba 3" };
            }
        }
        public static string[] Meses
        {
            get
            {
                return new string[] { "Enero", "Febrero", "Marzo", "Abril", "Mayo", "Junio", "Julio", "Agosto", "Septiembre", "Octubre", "Noviembre", "Diciembre" };
            }
        }
    }
}
