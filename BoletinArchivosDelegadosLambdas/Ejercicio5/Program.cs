namespace Ejercicio5
{
    internal class Program
    {
        
        public static int pedirEntero()
        {
            bool flag;
            int numero;
            do
            {
                flag = int.TryParse(Console.ReadLine(), out numero);
                if (numero >= 0)
                {
                    return numero;
                }
                else
                {
                    flag = false;
                    Console.WriteLine("Numero entero");
                }
            } while (!flag);
            return numero;
        }
        public delegate void MyDelegate();
        public static bool MenuGenerator(String[] opciones, MyDelegate[] funciones)
        {
            int opcion;

            do
            {
                for (int i = 0; i < opciones.Length; i++)
                {
                    Console.WriteLine($"{i+1}.- {opciones[i]}");
                }
                Console.WriteLine($"{opciones.Length + 1} Salir");
                Console.WriteLine("Elige una opcion: ");
                opcion = pedirEntero();
                if (opcion >= 1 && opcion <= opciones.Length)
                {
                    funciones[opcion - 1]();
                }
                else if (opcion == opciones.Length + 1)
                {
                    Console.WriteLine("Gracias por usar el programa");
                }
                else
                {
                    Console.WriteLine("Opcion no válida");
                }
            }
            while (opcion != opciones.Length +1);
            return true;
        }
        static void f1()
        {
            Console.WriteLine("A");
        }
        static void f2()
        {
            Console.WriteLine("B");
        }
        static void f3()
        {
            Console.WriteLine("C");
        }
        static void Main(string[] args)
        {
            MenuGenerator(new string[] { "Op1", "Op2", "Op3" },
            new MyDelegate[] { () => Console.WriteLine("A"), ()=> Console.WriteLine("B"), ()=> Console.WriteLine("C") });
            Console.ReadKey();
        }
    }
}
