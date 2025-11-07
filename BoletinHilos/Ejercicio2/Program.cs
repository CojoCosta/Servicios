namespace Ejercicio2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Thread[] caballos = new Thread[5];
            Console.WriteLine("~~~~BIENVENIDO AL HIPÓDROMO VIVAS~~~~");
            Console.WriteLine("Elija el caballo por el que apuesta (1 -5)");
            for (int i = 0; i < caballos.Length; i++)
            {
                caballos[i].Start();
            }

        }
    }
}
