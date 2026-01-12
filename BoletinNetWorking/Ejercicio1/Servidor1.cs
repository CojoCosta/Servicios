using System.Data;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Ejercicio1
{
    public class Servidor1
    {
        public bool ServerRunning { set; get; } = true;
        public int[] Port1 { get; set; } = { 31416, 31417, 31418 };
        public int Port { get; set; } = 31416;
        public void InitServer()
        {
            IPEndPoint ie = new IPEndPoint(IPAddress.Any, Port);
            using (Socket s = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp))
            {
                s.Bind(ie);
                s.Listen(10);
                Console.WriteLine($"Servidor iniciado. " + $"Escuchando en {ie.Address}:{ie.Port}");
                Console.WriteLine("Esperando conexiones... (Ctrl+C para salir)");
                try
                {
                    while (ServerRunning)
                    {
                        Socket cliente = s.Accept();
                        Thread hilo = new Thread(() => ProtocoloCliente(cliente));
                        hilo.Start();
                    }
                }
                catch (SocketException e) when(e.ErrorCode == (int)SocketError.AddressAlreadyInUse)
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
                    string pass = Password();
                    string welcome = "Welcome to my server";
                    sw.WriteLine(welcome);
                    string? msg = "";
                    string comando;
                    DateTime fechaYHora;
                    try
                    {
                        msg = sr.ReadLine();
                        comando = msg.Split(" ")[0];
                        if (msg != null)
                        {
                            switch (comando)
                            {
                                case "Time":
                                    fechaYHora = DateTime.Now;
                                    sw.WriteLine(fechaYHora.ToString("HH:mm:ss"));
                                    break;
                                case "Date":
                                    fechaYHora = DateTime.Now;
                                    sw.WriteLine(fechaYHora.ToString("dd/MM/yyyy"));
                                    break;
                                case "All":
                                    fechaYHora = DateTime.Now;
                                    sw.WriteLine(fechaYHora.ToString("dd/MM/yyyy -- HH:mm:ss"));
                                    break;
                                case "Close":
                                    if (msg == $"Close {pass}")
                                    {
                                        ServerRunning = false;
                                    }
                                    else
                                    {
                                        if (msg.Trim() == "Close")
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
                            Console.WriteLine($"El cliente dijo: {msg}");
                        }
                    }
                    catch (IOException ex)
                    {
                        msg = null;
                    }
                    Console.WriteLine("Cliente desconectado.\nConexión cerrada");
                }
            }
        }
        string programdata = Environment.GetEnvironmentVariable("Programdata");
        string passRead = "";
        public string Password()
        {
            try
            {
                StreamReader sr;
                Directory.SetCurrentDirectory(programdata);
                DirectoryInfo dir = new DirectoryInfo(Directory.GetCurrentDirectory());
                foreach (FileInfo doc in dir.GetFiles())
                {
                    if (doc.FullName == "password.txt")
                    {
                        sr = new StreamReader(doc.FullName);
                        passRead = sr.ReadToEnd();
                        return passRead;
                    }
                }
            }
            catch (Exception ex) when (ex is FileNotFoundException || ex is IOException)
            {
                Console.WriteLine("Error de archivo");
            }
            return passRead;
        }
    }
}