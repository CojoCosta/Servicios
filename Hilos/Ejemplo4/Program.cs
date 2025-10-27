namespace Ejemplo4
{
    internal class Program
    {
        static object l = new object();
        static void Main(string[] args) // Hilo principal, foreground por defecto.
        {
            Thread thread = new Thread(writeDown);
            thread.IsBackground = true; // Punto clave. Prueba a cambiarlo a false.
            thread.Start();
            for (int i = 1; i < 50; i++)
            {
                lock (l)
                {
                    Console.SetCursorPosition(1, 1);
                    Console.Write("{0,4}", i);
                }
                Thread.Sleep(50);
            }
        } // Cuando acaba el programa se cierra interrumpiendo el hilo background.
        static void writeDown()
        {
            for (int i = 1; i < 50; i++)
            {
                lock (l)
                {
                    Console.SetCursorPosition(1, 20);
                    Console.Write("{0,4}", i);
                }
                Thread.Sleep(200);
            }
        }
    }
}
