using System.Security;

namespace Ejercicio1
{
    public class Program//TODO el main espera y luego indica quien gana. Uso de locks y mas cosas...
    {
        static void Main(string[] args)
        {
            bool running = true;
            int i = 0;
            Thread suma = new Thread(() =>
            {
                while (running)
                {
                    if (running)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.Write(i);
                        i++;
                        if (i >= 500)
                        {
                            running = false;
                        }
                    }
                }
            });
            Thread resta = new Thread(() =>
            {
                while (running)
                {
                    if (running)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write(i);
                        i--;
                        if (i <= -500)
                        {
                            running = false;
                        }
                    }
                }
            });

            resta.Start();
            suma.Start();
        }
    }
}
