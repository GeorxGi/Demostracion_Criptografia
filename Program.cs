namespace PruebaCriptografia
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string texto = "";
            char opt;
            do
            {
                Mostrar(texto);
                opt = Console.ReadKey().KeyChar;
                switch (opt)
                {

                    case '1':
                        Console.Clear();
                        texto = Encriptador.Encriptar(texto, IngresarLlave());
                        break;
                    case '2':
                        Console.Clear();
                        string aux = Encriptador.Desencriptar(texto, IngresarLlave());
                        Console.WriteLine($"Tu mensaje no encriptado es: \"{aux}\"?\nS-Si\nN-No\n");
                        char opt2 = Console.ReadKey().KeyChar;
                        texto = (opt2 == 'S' || opt2 == 's') ? aux : texto;
                        break;
                    case '3':
                        Console.Clear();
                        Console.WriteLine("Ingrese el mensaje:");
                        texto = Console.ReadLine();
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
            Console.WriteLine($"Texto actual = {texto}\n");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("1-Encriptar mensaje\n2-Desencriptar mensaje\n3-Ingresar nuevo mensaje\n0-Salir");
        }
        private static string IngresarLlave()
        {
            string key;
            //Recibe la llave de encriptación
            do
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Ingrese la clave de encriptación");
                Console.ForegroundColor = ConsoleColor.White;
                key = Console.ReadLine();
            } while (string.IsNullOrWhiteSpace(key));

            return key;
        }
    }
}
