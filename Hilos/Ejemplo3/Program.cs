using System;

namespace Ejemplo3
{
    internal class Program
    {
        //WHILE-LOCK-IF
        static bool running = true;
        static readonly object l = new();
        static void charA()
        {
            int contA = 1;
            while (running)
            {
                lock (l)
                {
                    if (running)//comprobador extra para que una vez acabe el otro no escriba de mas
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.Write($" A:{contA}");
                        contA++;
                        if (contA > 1000)
                        {
                            running = false;
                        }
                    }
                }
            }
        }
        static void charB()
        {
            int contB = 1;
            while (running)
            {
                lock (l)
                {
                    if (running)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write($" B:{contB}");
                        contB++;
                        if (contB > 1000)
                        {
                            running = false;
                        }
                    }
                }
            }
        }
        static void Main(string[] args)
        {
            Thread threadA = new Thread(charA);
            Thread threadB = new Thread(charB);
            threadA.Start();
            threadB.Start();

            threadA.Join();//Ahora la frase se escribe de ultimo, espera a que acabe el hilo
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("Cuando se ejecuta esto¿?");//Al principio pero no de primero o si

            Console.ReadKey();
        }
    }
}
