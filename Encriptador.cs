using System.Security.Cryptography;
using System.Text;

namespace PruebaCriptografia
{
    public static class Encriptador
    {
        public static string EncriptarAES(string plainText, string key)
        {
            try
            {
                if (string.IsNullOrEmpty(plainText) || string.IsNullOrEmpty(key)) throw new ArgumentNullException();
                // Instancia el objeto que posee el algoritmo de encriptación Aes
                using Aes aes = Aes.Create();
                //Asigna la llave de encriptación a partir de la llave ingresada
                aes.Key = SHA256.HashData(Encoding.UTF8.GetBytes(key));
                //Genera un vector de inicialización aleatorio
                aes.GenerateIV();
                //Asigna el modo de relleno
                aes.Padding = PaddingMode.PKCS7;
                //Crea un objeto que encripta el flujo de datos
                using ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);
                //Crea un flujo de memoria
                using MemoryStream ms = new MemoryStream();
                //Escribe el vector de inicialización al flujo de memoria
                ms.Write(aes.IV, 0, aes.IV.Length);
                //Crea un flujo de encriptación
                using (CryptoStream cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
                //Crea un escritor que escribe en el flujo de encriptación
                using (StreamWriter sw = new StreamWriter(cs))
                {
                    //Escribe el texto plano en el flujo de encriptación
                    sw.Write(plainText);
                }
                //Convierte el flujo de memoria a un arreglo de bytes y lo convierte a una cadena en base 64
                return Convert.ToBase64String(ms.ToArray());
            }
            catch (Exception e)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(e.Message);
                Console.ForegroundColor = ConsoleColor.White;
                return "";
            }
        }

        public static string DesencriptarAES(string cipherText, string key)
        {
            try
            {
                if(string.IsNullOrEmpty(cipherText) || string.IsNullOrEmpty(key)) throw new ArgumentNullException();
                //Convierte el string de base 64 a un arreglo de bytes
                byte[] fullCipher = Convert.FromBase64String(cipherText);
                //Crea un arreglo de bytes para el vector de inicialización
                byte[] iv = new byte[16];
                //Copia los primeros 16 bytes del arreglo de bytes encriptado al vector de inicialización
                Array.Copy(fullCipher, 0, iv, 0, iv.Length);
                //Crea un arreglo de bytes para el texto encriptado
                byte[] cipher = new byte[fullCipher.Length - iv.Length];
                //Copia los bytes restantes del arreglo de bytes encriptado al arreglo de bytes del texto encriptado
                Array.Copy(fullCipher, iv.Length, cipher, 0, cipher.Length);

                //Instancia el objeto que posee el algoritmo de encriptación Aes
                using Aes aes = Aes.Create();
                //Asigna la llave de encriptación a partir de la llave ingresada
                aes.Key = SHA256.HashData(Encoding.UTF8.GetBytes(key));
                //Asigna el vector de inicialización
                aes.IV = iv;
                //Asigna el modo de relleno
                aes.Padding = PaddingMode.PKCS7;

                //Crea un objeto que desencripta el flujo de datos
                using ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);
                //Crea un flujo de memoria
                using MemoryStream ms = new MemoryStream(cipher);
                //Crea un flujo de desencriptación
                using CryptoStream cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read);
                //Crea un lector que lee del flujo de desencriptación
                using StreamReader sr = new StreamReader(cs);
                //Devuelve el texto desencriptado
                return sr.ReadToEnd();
            }
            catch (Exception e)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(e.Message);
                Console.ForegroundColor = ConsoleColor.White;
                return "";
            }
        }

        public static string EncriptarRSA(string textoplano, string llavePublica)
        {
            try
            {
                if(string.IsNullOrEmpty(textoplano) || string.IsNullOrEmpty(llavePublica)) throw new ArgumentNullException(nameof(textoplano));
                using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider())
                {
                    rsa.FromXmlString(llavePublica);
                    byte[] DatosEncriptar = Encoding.UTF8.GetBytes(textoplano);
                    return Convert.ToBase64String(rsa.Encrypt(DatosEncriptar, false));
                }
            }
            catch (Exception e)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(e.Message);
                Console.ForegroundColor = ConsoleColor.White;
                Console.ReadKey();
                return "";
            }
            
        }

        public static string DesencriptarRSA(string textoEncriptado, string llavePrivada)
        {
            try
            {
                if (string.IsNullOrEmpty(textoEncriptado) || string.IsNullOrEmpty(llavePrivada)) throw new ArgumentNullException(nameof(textoEncriptado));
                using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider())
                {
                    rsa.FromXmlString(llavePrivada);
                    byte[] DatosDesencriptar = Convert.FromBase64String(textoEncriptado);
                    byte[] DatosDesencriptados = rsa.Decrypt(DatosDesencriptar, false);
                    return Encoding.UTF8.GetString(DatosDesencriptados);
                }
            }
            catch (Exception e)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(e.Message);
                Console.ForegroundColor = ConsoleColor.White;
                return "";
            }
        }
    }
}
