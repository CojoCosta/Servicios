namespace Ejercicio1
{
    internal class LanzarServidor1
    {
        public static void Main(string[] args)
        {
            Console.WriteLine((new Servidor1()).Password("password"));
            //(new Servidor1()).InitServer();
        }
    }
}
