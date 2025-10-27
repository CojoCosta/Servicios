namespace Ejemplo6
{
    internal class Program
    {
        static void addition(int a, int b)
        {
            Console.WriteLine("* Thread {0} thinking: ", Thread.CurrentThread.Name);
            for (int i = 0; i < 18; i++) //Working simulation loop
            {
                Thread.Sleep(100);
                Console.Write("*");
            }
            Console.WriteLine("\nResult {0}: {1}", Thread.CurrentThread.Name, a + b);
        }
        static void factorial(int n)
        {
            long result = 1;
            Console.WriteLine(". Thread {0} thinking: ", Thread.CurrentThread.Name);
            for (int i = 2; i <= n; i++)
            {
                Console.Write('.');
                Thread.Sleep(100); //Working simulation Sleep
                result *= i;
            }
            Console.WriteLine("\nResult {0}: {1}", Thread.CurrentThread.Name, result);
        }

        static void factorialWrapper(object number)
        { // Need the wrapper to have an object param
            if (number is int)
            {
                factorial((int)number);
            }
        }
        static void Main(string[] args)
        {
            //Thread thread1 = new Thread(() => factorial(20));
            //Thread thread2 = new Thread(factorialWrapper);
            //Thread thread3 = new Thread(() => addition(44, 23));
            //thread1.Name = "Factorial";
            //thread2.Name = "FactorialWrapper";
            //thread3.Name = "Addition";
            //thread1.Start();
            //thread2.Start(20);
            //thread3.Start();
            //Console.ReadKey();

            for (char i = 'A'; i < 'D'; i++)
            {
                char oneChar = i;//variable temporal necesaria para guardar bien un dato
                new Thread(() =>
                {
                    for (int j = 0; j < 5; j++)
                        Console.Write(oneChar);
                        //Console.Write(i);//Asi va mal, escribe el ultimo valor de i en todos los hilos
                }).Start();
            }
        }
    }
}
