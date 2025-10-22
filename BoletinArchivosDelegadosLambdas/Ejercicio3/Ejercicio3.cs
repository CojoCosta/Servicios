namespace Ejercicio3
{
    internal class Ejercicio3
    {
        double num;
        public delegate double Operation(double x);

        public static double pedirDouble()
        {
            double num;
            Console.WriteLine("Dime un puto numero");
            if (double.TryParse(Console.ReadLine(), out num))
            {
                return num;
            }
            return 0;
        }

        static void Main(string[] args)
        {
            double num = pedirDouble();
            int numero = 0;
            Console.WriteLine("Elija hacer el cuadrado o cubo(2 o 3)¿?");
            while (numero != 2 && numero != 3)
            {
                Operation resultados;
                bool flag = int.TryParse(Console.ReadLine(), out numero);
                if (numero == 2)
                {
                    resultados = num => num * num;
                    Console.WriteLine($"Resultado: {resultados(num)}");
                }
                else if (numero == 3)
                {
                    resultados = num => num * num * num;
                    Console.WriteLine($"Resultado: {resultados(num)}");
                }
                else
                {
                    Console.WriteLine("Nº incorrecto");
                }
            }
        }
    }
}
