namespace Ejercicio5
{
    internal class Program
    {
        public static Random rd = new Random();
        public static int numeroRandom(int min, int max)
        {
            return rd.Next(min, max);
        }

        public static List<int> numeros = new List<int>();
        public static int contPrimos = 0;
        public static bool runnning = true;
        public static object l = new object();
        public static void Productor()
        {
            while (runnning)
            {
                lock (l)
                {
                    if (runnning)
                    {
                        int numero = numeroRandom(1000, 10001);
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        Console.WriteLine(numero);
                        numeros.Add(numero);
                    }
                }
            }
        }

        public static void Consumidor()
        {
            //while (runnning)
            {
                while (runnning||numeros.Count > 0)
                {
                    lock (l)
                    {
                    //            Console.ForegroundColor = ConsoleColor.Green;
                    //Console.WriteLine(runnning);
                    //Console.WriteLine(numeros.Count
                    //    );
                        int numero = numeros[0];
                        numeros.RemoveAt(0);
                        if (esPrimo(numero))
                        {
                            contPrimos++;

                            if (contPrimos == 5)
                            {
                                contPrimos = 0;
                                Console.WriteLine("5 primos!");
                                runnning = false;
                            }
                        }
                    }
                }
                Thread.Sleep(50);
            }
        }

        public static bool esPrimo(int numero)
        {
            for (int i = 2; i < numero; i++)
            {
                if (numero % i == 0)
                {
                    return false;
                }
            }
            return true;
        }

        static void Main(string[] args)
        {
            Thread productor = new Thread(Productor);
            Thread consumidor = new Thread(Consumidor);

            productor.Priority = ThreadPriority.Highest;

            productor.Start();
            consumidor.Start();

            productor.Join();
            consumidor.Join();

            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(numeros.Count);
        }
    }
}

