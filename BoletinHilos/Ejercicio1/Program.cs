using System.Security;

namespace Ejercicio1
{
    public class Program//TODO el main espera y luego indica quien gana. Uso de locks y mas cosas...
    {
        static readonly object l = new object();
        static void Main(string[] args)
        {
            bool running = true;
            int i = 0;
            Thread suma = new Thread(() =>
            {
                while (running)
                {
                    lock (l)
                    {
                        if (running)
                        {
                            if (i > 499)
                            {
                                running = false;
                                break;
                            }
                            Console.ForegroundColor = ConsoleColor.Green;
                            i++;
                            Console.Write($"{i,10}");
                        }
                    }
                }
            });
            Thread resta = new Thread(() =>
            {
                while (running)
                {
                    lock (l)
                    {
                        if (running)
                        {
                            if (i < -499)
                            {
                                running = false;
                                break;
                            }
                            Console.ForegroundColor = ConsoleColor.Red;
                            i--;
                            Console.Write($"{i,10}");
                        }
                    }
                }
            });
            resta.Start();
            suma.Start();
            resta.Join();
            suma.Join();
            Console.WriteLine();
            Console.WriteLine($"El ganador es : ");
        }
    }
}
