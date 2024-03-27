using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Añuri.Dao;
using Añuri.Entidades;

namespace Añuri.Negocio
{
    public class ProveedoresNeg_Conta
    {

        public static bool ValidarProveedorExistente(ProveedoresModCont item)
        {
            bool ValidarProveedorExistente = false;
            try
            {
                ValidarProveedorExistente = ProveedoresDAO_Conta.ValidarProveedorExistente(item.Dni);
            }
            catch (Exception ex)
            {
            }
            return ValidarProveedorExistente;
        }
        public static int ObteneridSitIva(string sitIva)
        {
            int idSitIva = 0;
            try
            {
                idSitIva = ProveedoresDAO_Conta.ObteneridSitIva(sitIva);
                if (idSitIva == 0)
                {
                    idSitIva = ProveedoresDAO_Conta.RegistrarSitIva(sitIva);
                }
            }
            catch (Exception ex)
            {
            }
            return idSitIva;
        }
        public static int ObteneridIngresosBrutos(string IngresosBrutos)
        {
            int idIngresosBrutos = 0;
            try
            {
                idIngresosBrutos = ProveedoresDAO_Conta.ObteneridIngresosBrutos(IngresosBrutos);
                if (idIngresosBrutos == 0)
                {
                    idIngresosBrutos = ProveedoresDAO_Conta.RegistrarObteneridIngresosBrutos(IngresosBrutos);
                }
            }
            catch (Exception ex)
            {
            }
            return idIngresosBrutos;
        }
        public static int ObteneridPais(string pais)
        {
            int idPais = 0;
            try
            {
                idPais = ProveedoresDAO_Conta.ObteneridPais(pais);
                if (idPais == 0)
                {
                    idPais = ProveedoresDAO_Conta.RegistrarObteneridPais(pais);
                }
            }
            catch (Exception ex)
            {
            }
            return idPais;
        }
        public static int ObteneridProvincia(string provincia, int idPais)
        {
            int idProvincia = 0;
            try
            {
                idProvincia = ProveedoresDAO_Conta.ObteneridProvincia(provincia, idPais);
                if (idProvincia == 0)
                {
                    idProvincia = ProveedoresDAO_Conta.RegistrarObteneridProvincia(provincia, idPais);
                }
            }
            catch (Exception ex)
            {
            }
            return idProvincia;
        }
        public static int ObteneridLocalidad(string localidad, int idProvincia)
        {
            int idLocalidad = 0;
            try
            {
                idLocalidad = ProveedoresDAO_Conta.ObtenerObteneridLocalidad(localidad, idProvincia);
                if (idLocalidad == 0)
                {
                    idLocalidad = ProveedoresDAO_Conta.RegistrarObteneridLocalidad(localidad, idProvincia);
                }
            }
            catch (Exception ex)
            {
            }
            return idLocalidad;
        }
        public static bool RegistrarProveedor(ProveedoresModCont item)
        {
            bool Exito = false;
            try
            {
                Exito = ProveedoresDAO_Conta.RegistrarProveedor(item);
            }
            catch (Exception ex)
            {
            }
            return Exito;
        }
    }
}
