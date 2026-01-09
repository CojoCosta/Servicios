namespace ServidorHilos
{
    internal class ServidorHilos
    {
        static void Main(string[] args)
        {
            (new EchoServer()).InitServer();
        }
    }
}
