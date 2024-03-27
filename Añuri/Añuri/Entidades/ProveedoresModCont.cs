using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Añuri.Entidades
{
    public class ProveedoresModCont
    {
        public int idProveedor { get; set; }
        public string RazonSocial { get; set; }
        public string Dni { get; set; }
        public int idPais { get; set; }
        public string Pais { get; set; }
        public int idProvincia { get; set; }
        public string Provincia { get; set; }
        public int idLocalalidad { get; set; }
        public string Localidad { get; set; }
        public string Domicilio { get; set; }
        public string Altura { get; set; }
        public string Piso { get; set; }
        public string Departamento { get; set; }
        public string CodigoPostal { get; set; }
        public int idSitIva { get; set; }
        public string SitIva { get; set; }
        public int idIngresosBrutos { get; set; }
        public string IngresosBrutos { get; set; }
        public string NumeroIB { get; set; }
    }
}
