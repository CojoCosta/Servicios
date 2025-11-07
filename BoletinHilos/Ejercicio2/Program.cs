namespace Ejercicio2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Thread[] caballos = new Thread[5];
            int eleccion;
            bool flag = false;
            Console.WriteLine("~~~~BIENVENIDO AL HIPÓDROMO VIVAS~~~~");
            while (!flag)
            {
                flag = int.TryParse("Elija el caballo por el que apuesta (1 -5)", out eleccion);
            }
            //for (int i = 0; i < caballos.Length; i++)
            //{
            //    caballos[i].Start();
            //}

        }
    }
}
