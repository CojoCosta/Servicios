namespace Ejemplo1
{
    internal class Program
    {
        static void charA()
        {
            for (int i = 1; i < 1500; i++)
            {
                Console.Write('A');
            }
        }
        static void Main(string[] args)
        {
            Thread thread = new Thread(charA);
            thread.Priority = ThreadPriority.Highest;
            thread.Start();
            //char B
            for (int i = 1; i < 1500; i++)
            {
                Console.Write('B');
            }
            Console.ReadKey();
        }
    }
}