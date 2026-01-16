using System.Data;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Ejercicio1
{
    public class Servidor1//puertos ocupados  
    {
        public bool ServerRunning { set; get; } = true;
        public int[] Port1 { get; set; } = { 135, 135, 135};
        Socket s;

        public void InitServer()
        {
            using (s = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp))
            {
                try
                {
                    IPEndPoint ie = new IPEndPoint(IPAddress.Any, puertoEnUso(Port1));
                    s.Bind(ie);
                    s.Listen(10);
                    Console.WriteLine($"Servidor iniciado. " + $"Escuchando en {ie.Address}:{ie.Port}");
                    Console.WriteLine("Esperando conexiones... (Ctrl+C para salir)");
                    while (ServerRunning)
                    {
                        Socket cliente = s.Accept();
                        Thread hilo = new Thread(() => ProtocoloCliente(cliente));
                        hilo.IsBackground = true;
                        hilo.Start();
                    }
                }
                catch (SocketException e)
                {
                    Console.WriteLine("Servidor cerrado \nPuerto en uso");
                }
            }
        }

        public void ProtocoloCliente(Socket sCliente)
        {
            using (sCliente)
            {
                IPEndPoint ieCliente = (IPEndPoint)sCliente.RemoteEndPoint;
                Console.WriteLine($"Cliente conectado:{ieCliente.Address} " + $"en puerto {ieCliente.Port}");
                Encoding codificacion = Console.OutputEncoding;
                using (NetworkStream ns = new NetworkStream(sCliente))
                using (StreamReader sr = new StreamReader(ns))
                using (StreamWriter sw = new StreamWriter(ns))
                {
                    sw.AutoFlush = true;
                    string pass = Password("password");
                    string? msg = "";
                    string comando;
                    DateTime fechaYHora;
                    try
                    {
                        msg = sr.ReadLine();
                        if (msg != null)
                        {
                            comando = msg.Split(" ")[0];
                            switch (comando.Trim())
                            {
                                case "time":
                                    fechaYHora = DateTime.Now;
                                    sw.WriteLine(fechaYHora.ToString("HH:mm:ss"));
                                    break;
                                case "date":
                                    fechaYHora = DateTime.Now;
                                    sw.WriteLine(fechaYHora.ToString("dd/MM/yyyy"));
                                    break;
                                case "all":
                                    fechaYHora = DateTime.Now;
                                    sw.WriteLine(fechaYHora.ToString("dd/MM/yyyy -- HH:mm:ss"));
                                    break;
                                case "close":
                                    if (msg == $"close {pass}")
                                    {
                                        ServerRunning = false;
                                        s.Close();
                                    }
                                    else
                                    {
                                        if (msg.Trim() == "close")
                                        {
                                            sw.WriteLine("No ha escrito ninguna contraseña");
                                        }
                                        else
                                        {
                                            sw.WriteLine("Contraseña incorrecta");
                                        }
                                    }
                                    break;

                                default:
                                    Console.WriteLine($"Error de comando: {msg}");
                                    break;
                            }
                            Console.WriteLine($"El cliente escribió: {msg}");
                        }
                    }
                    catch (Exception ex) when (ex is IOException || ex is SocketException)
                    {
                        msg = null;
                    }
                    Console.WriteLine("Cliente desconectado.\nConexión cerrada");
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

        string programdata = Environment.GetEnvironmentVariable("Programdata");
        string passRead = "";
        public string Password(string fileName)
        {
            try
            {
                string path = $"{Environment.GetEnvironmentVariable("programdata")}\\{fileName}.txt";
                DirectoryInfo dir = new DirectoryInfo(programdata);
                using (StreamReader sr = new StreamReader(path))
                {
                    passRead = sr.ReadToEnd();
                    return passRead;
                }
            }
            catch (Exception ex) when (ex is FileNotFoundException || ex is IOException || ex is UnauthorizedAccessException)
            {
                Console.WriteLine("Error de archivo");
            }
            return passRead;
        }
    }
}