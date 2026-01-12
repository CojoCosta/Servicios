using System.Data;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Ejercicio1
{
    public class Servidor1
    {
        public bool ServerRunning { set; get; } = true;
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
                catch (SocketException ex)
                {
                    Console.WriteLine("Servidor cerrado");
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
                    string welcome = "Welcome to my server";
                    sw.WriteLine(welcome);
                    string? msg = "";
                    try
                    {
                        //msg = sr.ReadLine();
                        if (msg != null)
                        {
                            if (msg != "time" || msg != "date" || msg != "all" || msg != "close")
                            {
                                Console.WriteLine($"Error de comando: {msg}");
                            }

                        }
                        else
                        {
                            switch (msg)
                            {
                                case "time":
                                    //DateTime
                                    break;
                                case "date":
                                    //DataSetDateTime

                                    break;
                                case "all":

                                    break;
                                case "close":

                                    break;
                            }
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
        string pass = "";
        public void Password(string contra)
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
                        pass = sr.ReadToEnd();
                    }
                }
            }
            catch (FileNotFoundException ex)
            {
                Console.WriteLine("Error de archivo");
            }

        }
    }
}
