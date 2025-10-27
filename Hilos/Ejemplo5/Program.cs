namespace Ejemplo5
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Thread[] hilos = new Thread[10];
            for (int i = 0; i < 10; i++)
            {
                hilos[i] = new Thread(writeALotOfChars);
                char c = (char)('A' + i);
                hilos[i].Start(c);
            }
            for (int i = 1; i < 1000; i++)
            {
                Console.Write("-Main-");
            }
            Console.ReadKey();
        }
        static void writeALotOfChars(object a)
        {
            for (int i = 1; i < 1000; i++)
            {
                Console.Write((char)a);
            }
        }
    }
}
