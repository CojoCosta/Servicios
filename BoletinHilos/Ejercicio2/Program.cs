namespace Ejercicio2
{
    internal class Program//Revisar condición de carrera, repetición de carrera
    {
        static string bicho = ",O,^·";
        static bool running = true;
        static readonly object l = new object();
        static Random rd = new Random();
        static Random rdSleep = new Random();
        static int first;
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


        public static void carrera(object y)
        {
            int winner = (int)y;
            int x = 0;
            int nuevaY = (int)y;
            nuevaY += 1;
            while (running)
            {
                lock (l)
                {
                    Console.Write(bicho);
                    if (x >= 100)
                    {
                        running = false;
                        first = winner;
                    }
                    Console.SetCursorPosition(x, nuevaY);
                    x += 1;// rd.Next(1, 11);
                }
                Thread.Sleep(50);// rdSleep.Next(100,500));
            }
        }
        static void Main(string[] args)
        {
            first = -1;
            int jugar = 0;
            Thread[] caballos;
            running = true;
            int y = 1;
            int ganador;
            do
            {
                int eleccion;
                Console.WriteLine("~~~~BIENVENIDO AL HIPÓDROMO VIVAS~~~~");
                do
                {
                    Console.WriteLine("Elige cuantos caballos");
                    eleccion = pedirEntero();
                } while (eleccion < 1 || eleccion > 10);
                caballos = new Thread[eleccion];
                do
                {
                    Console.WriteLine("Elige tu caballo");
                    ganador = pedirEntero();
                } while (ganador < 1 || ganador > eleccion);
                for (int i = 0; i < eleccion; i++)
                {
                    caballos[i] = new Thread(carrera);
                }
                Console.Clear();
                for (int i = 0; i < caballos.Length; i++)
                {
                    caballos[i].Start(y + i);
                }
                for (int i = 0; i < caballos.Length; i++)
                {
                    caballos[i].Join();
                }
                Console.ReadKey();
                Console.WriteLine($"Ganador: {first}");
                if (ganador == first)
                {
                    Console.WriteLine("Enhorabuena aún no te has arruinado");
                }
                Console.WriteLine("Quieres volver a perder tu dinero¿?");
                Console.WriteLine("1.- No");
                jugar = pedirEntero();
            } while (jugar != 1);
        }
    }
}