namespace Ejemplo2
{
    internal class Program
    {
        static bool running = true;//Booleana compartida para controlar los bucles
        static readonly object l = new object();
        static void Main(string[] args)
        {
            Thread thread = new Thread(WriteDown);
            thread.Start();
            //writeUp
            int i = 1;
            while (running)
            {
                lock (l)
                {
                    Console.SetCursorPosition(1, 1);
                    Console.Write("{0,4}", i);
                    i++;
                    Thread.Sleep(100);
                    if (i >= 1000)
                    {
                        running = false;
                    }
                }
            }
            Console.ReadKey();
        }
        static void WriteDown()
        {
            int i = 1;
            while (running)
            {
                lock (l)
                {
                    Console.SetCursorPosition(1, 20);
                    Console.Write("{0,4}", i);
                    i++;
                    Thread.Sleep(1000);
                    if (i >= 1000)
                    {
                        running = false;
                    }
                }
            }
        }
    }
}
