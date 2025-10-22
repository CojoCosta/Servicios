//#define ej1
namespace Ejercicio4
{
    internal class Ejercicio4
    {
        public delegate int Operation(int x);
        static void Main(string[] args)
        {
            int[] notas = { 5, 2, 8, 1, 9, 4 };
            string[] palabras = { "Sol", "Luna", "Estrella", "Cielo" };
//#if ej1
            Console.WriteLine("Saber de si hay algún aprobado");
            if (Array.Exists(notas, num => num >= 5))
            {
                Console.WriteLine("Hay aprobados");
            }
//#elif ej2
            Console.WriteLine("Mostrar los aprobados de notas.");
            foreach (var nota in Array.FindAll(notas, num => num >= 5))
            {
                Console.WriteLine(nota);
            }
//#elif ej3
            Console.WriteLine("Posicion ultimo aprobado");
            Console.WriteLine(Array.FindLastIndex(notas, num => num >= 5));
//#elif ej4
            Console.WriteLine("Mostrar la nota del último aprobado");
            Console.WriteLine(Array.FindLast(notas, num => num >= 5));
//#elif ej5
            Console.WriteLine("Cuantos tienen nota par");
            int pares = Array.FindAll(notas, num => num % 2 == 0).Length;
            Console.WriteLine($"Hay {pares} notas pares");
//#elif ej5
            Console.WriteLine("Cual es la primera palabra de más de 3 caracteres");
            string palabra = Array.Find(palabras, item => item.Length > 3);
            Console.Write($"{palabra}\n");
            
//#elif ej6
            Console.WriteLine("Mostrar todas las palabras en mayúsculas");
            Array.ForEach(palabras, item => Console.WriteLine(item.ToUpper()));
            
//elif ej7
            Console.WriteLine("Indica la posición de la primera palabra que empiece por E");
            string palabraPorE = Array.Find(palabras, item => item.StartsWith("E"));
            Console.Write($"{palabraPorE}");
            
//#endif
        }
    }
}
