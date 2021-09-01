using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace Añuri
{
   public class Prueba
    {
        private string clave = "cadenadecifrado"; // Clave de cifrado. NOTA: Puede ser cualquier combinación de carácteres.
        // Función para cifrar una cadena.
        string CadenaCifrada = "";
        public string cifrar(string cadena)
        {
            if (cadena != "")
            {
                byte[] llave; //Arreglo donde guardaremos la llave para el cifrado 3DES.

                byte[] arreglo = UTF8Encoding.UTF8.GetBytes(cadena); //Arreglo donde guardaremos la cadena descifrada.

                // Ciframos utilizando el Algoritmo MD5.
                MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
                llave = md5.ComputeHash(UTF8Encoding.UTF8.GetBytes(clave));
                md5.Clear();
                md5.Dispose();
                //Ciframos utilizando el Algoritmo 3DES.
                TripleDESCryptoServiceProvider tripledes = new TripleDESCryptoServiceProvider();
                tripledes.Key = llave;
                tripledes.Mode = CipherMode.ECB;
                tripledes.Padding = PaddingMode.PKCS7;
                ICryptoTransform convertir = tripledes.CreateEncryptor(); // Iniciamos la conversión de la cadena
                byte[] resultado = convertir.TransformFinalBlock(arreglo, 0, arreglo.Length); //Arreglo de bytes donde guardaremos la cadena cifrada.
                tripledes.Clear();
                tripledes.Dispose();
                CadenaCifrada = Convert.ToBase64String(resultado, 0, resultado.Length); // Convertimos la cadena y la regresamos.
            }
            return CadenaCifrada;
        }

        // Función para descifrar una cadena.
        string cadena_descifrada = "";
        public string descifrar(string cadena)
        {
            if (cadena != "")
            {
                byte[] llave;

                byte[] arreglo = Convert.FromBase64String(cadena); // Arreglo donde guardaremos la cadena descovertida.

                // Ciframos utilizando el Algoritmo MD5.
                MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
                llave = md5.ComputeHash(UTF8Encoding.UTF8.GetBytes(clave));
                md5.Clear();
                md5.Dispose();
                //Ciframos utilizando el Algoritmo 3DES.
                TripleDESCryptoServiceProvider tripledes = new TripleDESCryptoServiceProvider();
                tripledes.Key = llave;
                tripledes.Mode = CipherMode.ECB;
                tripledes.Padding = PaddingMode.PKCS7;
                ICryptoTransform convertir = tripledes.CreateDecryptor();
                byte[] resultado = convertir.TransformFinalBlock(arreglo, 0, arreglo.Length);
                tripledes.Clear();
                tripledes.Dispose();
                cadena_descifrada = UTF8Encoding.UTF8.GetString(resultado); // Obtenemos la cadena
            }
            return cadena_descifrada; // Devolvemos la cadena
        }
    }
}
