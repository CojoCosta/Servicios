using System.Security;

namespace Ejercicio1
{
    internal class Program
    {
        static bool running = true;
        public static void contadorSuma()
        {
            int i = 0;
            while (running)
            {
                Console.SetCursorPosition(1, 1);
                Console.Write(i);
                i++;
                if (i>=500)
                {
                    running = false;
                }
            }
        }
        public static void contadorResta()
        {
            int i = 500;
            while (running)
            {
                Console.SetCursorPosition(1, 20);
                Console.Write(i);
                i--;
                if (i <= 0)
                {
                    running = false;
                }
            }
        }
        static void Main(string[] args)
        {
            Thread suma = new Thread(contadorSuma);
            Thread resta = new Thread(contadorResta);
           
            resta.Start();
            suma.Start();
        }
    }
}
