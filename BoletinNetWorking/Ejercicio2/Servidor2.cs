using System.Collections;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Ejercicio2
{
    internal class Servidor2
    {
        Socket s;
        List<Cliente> clientes = new();
        static object l = new object();
        public bool ServerRunning { get; set; } = true;
        public int[] Port = { 31416, 31416, 135 };

        public void InitServer()
        {
            using (s = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp))
            {
                try
                {
                    IPEndPoint ie = new IPEndPoint(IPAddress.Any, puertoEnUso(Port));
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

        Cliente cliente;
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
                        cliente = new Cliente(id, ieCliente.Address, nombreCliente, sw);
                        clientes.Add(cliente);
                        id++;
                    }
                    else
                    {
                        msg = null;
                    }
                    sw.WriteLine("Ya puede empezar a chatear");
                    while (msg != null)
                    {
                        msg = sr.ReadLine();

                        switch (msg)
                        {
                            case "list":
                                lock (l)
                                {
                                    foreach (Cliente cadaCliente in clientes)
                                    {
                                        if (cadaCliente != cliente)
                                        {
                                            cliente.Sw.WriteLine($"{cliente.Nombre}@{cliente.Ip}");
                                        }
                                    }
                                }
                                break;
                            case "exit":
                                lock (l)
                                {
                                    for (int i = id; i > 0; i--)
                                    {
                                        clientes.RemoveAt(cliente.Id);
                                    }
                                }
                                msg = null;
                                break;
                            default:
                                lock (l)
                                {
                                    foreach (Cliente cadaCliente in clientes)
                                    {
                                        if (cliente != cadaCliente)
                                        {
                                            cliente.Sw.WriteLine($"{cliente.Id}-{cliente.Nombre}@{cliente.Ip}:{msg}");
                                        }
                                    }

                                }
                                //sw.WriteLine($"{cliente.nombre}@{cliente.ip}: {msg}");
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
        public int puertoEnUso(int[] puertos)
        {
            int j = 0;
            bool flag = true;
            while (flag && j < puertos.Length)
            {
                using (Socket socketComprobacion = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp))
                {
                    try
                    {
                        IPEndPoint comprobacion = new IPEndPoint(IPAddress.Any, puertos[j]);
                        socketComprobacion.Bind(comprobacion);
                        socketComprobacion.Listen();
                        flag = false;
                        return puertos[j];
                    }
                    catch (SocketException ex) when (ex.ErrorCode == (int)SocketError.AddressAlreadyInUse)
                    {
                        Console.WriteLine("Sin puerto libre");
                        j++;
                    }
                }
            }
            j--;
            return puertos[j];
        }
    }
}