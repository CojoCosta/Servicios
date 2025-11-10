namespace Ejercicio2
{
    internal class Program
    {
        static string bicho = ",-,^·";
        public static int pedirEntero()
        {
            bool flag = true;
            int numero;
            do
            {
                flag = int.TryParse(Console.ReadLine(), out numero);
                if (numero > 0)
                {
                    return numero;
                }
                else
                {
                    flag = false;
                    Console.WriteLine("Prueba otra vez");
                }
            } while (!flag);
            return numero;
        }
        public static void carrera()
        {
            Random rd = new Random();
            int x = rd.Next(1, 11);
            for (int i = 0; i < 3; i++)
            {
                Console.WriteLine(bicho);
                Console.SetCursorPosition(x, 10);
                x += x;
            }

        }
        static void Main(string[] args)
        {
            int jugar = 0;
            do
            {
                Thread[] caballos = new Thread[5];
                int eleccion;
                Console.WriteLine("~~~~BIENVENIDO AL HIPÓDROMO VIVAS~~~~");
                do
                {
                    Console.WriteLine("Elige un caballo (1-5)");
                    eleccion = pedirEntero();
                } while (eleccion < 1 || eleccion > 5);
                for (int i = 0; i < 5; i++)
                {
                    caballos[i] = new Thread(carrera);
                    caballos[i].Start();
                }
                for (int i = 0; i < caballos.Length; i++)
                {
                    
                }
                Console.ReadKey();


                Console.WriteLine("Quieres volver a perder tu dinero¿?");
                Console.WriteLine("1.- No");
                jugar = pedirEntero();
            } while (jugar != 1);
        }
    }
}