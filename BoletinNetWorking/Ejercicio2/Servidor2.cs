using System.Collections;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Ejercicio2
{
    internal class Servidor2
    {
        Socket s;
        Cliente cliente;
        List<Cliente> clientes = new();
        public bool ServerRunning { get; set; }
        public int Port = 31416;
        //TODO Añadir logs al tener recursos compratidos en diferentes hilos
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
                    while (ServerRunning)
                    {
                        Socket cadaCliente = s.Accept();
                        Thread hilo = new Thread(() => protocoloCliente(cadaCliente));
                        //hilo.IsBackground = true; //Lo activo si quiero echar a todos los clientes
                        hilo.Start();
                    }
                }
                catch (SocketException ex)
                {
                    Console.WriteLine("Servidor Cerrado");
                }
            }
        }

        public void protocoloCliente(Socket sCliente)
        {
            IPEndPoint ieCliente = (IPEndPoint)sCliente.RemoteEndPoint;
            Console.WriteLine($"Cliente conectado:{ieCliente.Address} " + $"en puerto {ieCliente.Port}");
            Encoding codificacion = Console.OutputEncoding;
            using (NetworkStream ns = new NetworkStream(sCliente))
            using (StreamReader sr = new StreamReader(ns))
            using (StreamWriter sw = new StreamWriter(ns))
            {
                int id = 1;
                string nombreCliente = "";
                string? msg = "";
                sw.AutoFlush = true;
                sw.WriteLine("Indique su nombre: ");
                try
                {
                    nombreCliente = sr.ReadLine();
                    if (nombreCliente != null)
                    {
                        cliente = new Cliente(id, nombreCliente, sw);
                        clientes.Add(cliente);
                        id++;
                    }
                    sw.WriteLine("Ya puede empezar a chatear");
                    msg = sr.ReadLine();
                    while (msg != null)
                    {
                        switch (msg)
                        {
                            case "#list":
                                foreach (Cliente cadaCliente in clientes)
                                {
                                    sw.WriteLine($"{nombreCliente}@{ieCliente.Address}");
                                }
                                break;

                            case "#exit":
                                msg = null;
                                break;

                            default:
                                sw.WriteLine($"{nombreCliente}@{ieCliente.Address}: {msg}");
                                break;
                        }
                    }
                }
                catch (Exception ex)
                {
                    msg = null;
                    Console.WriteLine($"Cliente {nombreCliente} se ha desconectado");
                }
            }
        }
    }
}