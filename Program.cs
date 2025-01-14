using System.Security.Cryptography;

namespace PruebaCriptografia
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string texto = "";
            char opt;
            using RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
            do
            {
                Mostrar(texto);
                opt = Console.ReadKey().KeyChar;
                switch (opt)
                {
                    case '1':
                        Console.Clear();
                        Console.WriteLine("1-Algoritmo AES\n2-Algoritmo RSA");
                        char opt1 = Console.ReadKey().KeyChar;
                        if(opt1 == '1')
                        {
                            texto = Encriptador.EncriptarAES(texto, IngresarLlave());
                        }
                        else if(opt1 == '2')
                        {
                            texto = Encriptador.EncriptarRSA(texto,  rsa.ToXmlString(false));
                        }
                        break;
                    case '2':
                        Console.Clear();
                        Console.WriteLine("1-Algoritmo AES\n2-Algoritmo RSA");
                        char opt2 = Console.ReadKey().KeyChar;
                        string aux = "";
                        if(opt2 == '1')
                        {
                            aux = Encriptador.DesencriptarAES(texto, IngresarLlave());
                        }
                        else if(opt2 == '2')
                        {
                            aux = Encriptador.DesencriptarRSA(texto, rsa.ToXmlString(true));
                        }
                        Console.WriteLine($"\nTu mensaje no encriptado es: \"{aux}\"?\nS-Si\nN-No\n");
                        opt2 = Console.ReadKey().KeyChar;
                        texto = (opt2 == 'S' || opt2 == 's') ? aux : texto;
                        Console.ReadKey();
                        break;
                    case '3':
                        Console.Clear();
                        Console.WriteLine("Ingrese el mensaje:");
                        texto = Console.ReadLine();
                        break;
                    case '4':
                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine("Llave pública:");
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.WriteLine(rsa.ToXmlString(false) + "\n");
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine("Llave privada:");
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.WriteLine(rsa.ToXmlString(true));
                        Console.ReadKey();
                        break;
                    default:
                        break;
                }
                Console.Clear();
            } while (opt != '0');
        }

        private static void Mostrar(string texto)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"Texto actual = {texto}\n\n");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("1-Encriptar mensaje\n2-Desencriptar mensaje\n3-Ingresar nuevo mensaje\n4-Mostrar claves algoritmo RSA\n0-Salir");
        }
        private static string IngresarLlave()
        {
            string key;
            //Recibe la llave de encriptación
            do
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("\nIngrese la clave de encriptación");
                Console.ForegroundColor = ConsoleColor.White;
                key = Console.ReadLine();
            } while (string.IsNullOrWhiteSpace(key));

            return key;
        }
    }
}
