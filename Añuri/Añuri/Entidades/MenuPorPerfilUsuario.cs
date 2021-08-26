using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Añuri.Entidades
{
   public class MenuPorPerfilUsuario
    {
        public int idMenuPorPerfil { get; set; }
        public int idPerfil { get; set; }
        public string NombreMenu { get; set; }
    }
}
