using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Añuri.Negocio
{
    public class ContabilidadNeg
    {
        public static List<string> CargarComboOrganismo()
        {
            List<string> _listaOrganismos = new List<string>();
            try
            {
                _listaOrganismos = Dao.ContabilidadDao.CargarComboOrganismo();
            }
            catch (Exception ex)
            {
                throw new Exception();
            }
            return _listaOrganismos;
        }
    }
}
