using System.Collections;
using System.Net;
using System.Net.Sockets;

namespace Ejercicio2
{
    internal class Servidor2
    {
        Socket s;
        Cliente cliente;
        List<Cliente> clientes;
        public int Port = 31416;
        public void InitServer()
        {
            using (s = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp))
            {
                try
                {
                    IPEndPoint ie = new IPEndPoint(IPAddress.Any, Port);
                    s.Bind(ie);
                    s.Listen(10);
                    Console.WriteLine($"Servidor iniciado. " + $"Escuchando en {ie.Address}:{ie.Port}");
                    Console.WriteLine("Esperando conexiones... (Ctrl+C para salir)");

                }
                catch 
                {

                }

            }
        }
    }
}
