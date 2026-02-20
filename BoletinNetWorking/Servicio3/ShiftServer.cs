using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Servicio3//Varias lineas en readnames. Readpin revisar. Probar.
{
    internal class ShiftServer
    {
        string[] users = new string[0];
        List<string> waitQueue = new List<string>();

        public bool serverRunning = true;
        Socket s;
        int puerto = 31416;


        public bool ComprobacionPuerto(int puerto)
        {
            IPEndPoint iPEndPoint = new IPEndPoint(IPAddress.Any, puerto);
            using (Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp))
            {
                try
                {
                    socket.Bind(iPEndPoint);
                    socket.Listen(1);
                }
                catch (SocketException)
                {
                    return false;
                }
                return true;
            }
        }
        public int NuevoPuertoLibre(int PuertoInicial)
        {
            IPEndPoint iP = new IPEndPoint(IPAddress.Any, PuertoInicial);
            using (Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp))
            {
                bool isFree = false;
                do
                {
                    try
                    {
                        socket.Bind(iP);
                        socket.Listen(1);
                        isFree = true;
                    }
                    catch (SocketException)
                    {
                        PuertoInicial++;
                    }
                }
                while (!isFree && PuertoInicial < IPEndPoint.MaxPort); // Comprobar lógica del bucle
                return PuertoInicial;
            }
        }

        public void InitServer()
        {
            if (!ComprobacionPuerto(puerto))
            {
                puerto = NuevoPuertoLibre(1024);
            }
            using (s = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp))
            {
                try
                {
                    IPEndPoint ie = new IPEndPoint(IPAddress.Any, puerto);
                    Console.WriteLine($"Puerto: {puerto}");
                    s.Bind(ie);
                    s.Listen(10);
                    Console.WriteLine($"Servidor iniciado. " + $"Escuchando en {ie.Address}:{ie.Port}");
                    Console.WriteLine("Esperando conexiones... (Ctrl+C para salir)");
                    cargarWaitQueue();
                    while (serverRunning)
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
            ReadNames($"{Environment.GetEnvironmentVariable("userprofile")}\\usuarios.txt");
            IPEndPoint ieCliente = (IPEndPoint)sCliente.RemoteEndPoint;
            Console.WriteLine($"Cliente conectado:{ieCliente.Address} " + $"en puerto {ieCliente.Port}");
            Encoding codificacion = Console.OutputEncoding;
            using (NetworkStream ns = new NetworkStream(sCliente))
            using (StreamReader sr = new StreamReader(ns))
            using (StreamWriter sw = new StreamWriter(ns))
            {
                sw.AutoFlush = true;
                sw.WriteLine("Welcome");
                sw.Write("Nombre: ");
                try
                {
                    string nombreUsuario = sr.ReadLine();
                    if (usuarioEnLista(users, nombreUsuario) || nombreUsuario == "admin")
                    {
                        if (nombreUsuario == "admin")
                        {
                            sw.WriteLine("Introduce un pin: ");
                            int pin = int.Parse(sr.ReadLine());
                            int pinCorrecto;
                            try
                            {
                                pinCorrecto = ReadPin($"{Environment.GetEnvironmentVariable("%userprofile%")}\\pin.txt");
                            }
                            catch
                            {
                                pinCorrecto = 1234;
                            }
                            if (pin == pinCorrecto)
                            {
                                string[] comandoFinal = new string[0];
                                do
                                {
                                    sw.WriteLine($"Escribe un comando ('del' | 'chpin' | 'exit' | 'shutdown'):");
                                    string comando = sr.ReadLine();
                                    if (comando != null)
                                    {
                                        comandoFinal = comando.Split(' ');
                                    }
                                    switch (comandoFinal[0])
                                    {
                                        case "del":
                                            bool flagnumero = int.TryParse(comando out int pos);
                                            if (!flagnumero || pos < 0 || pos > waitQueue.Count)
                                            {
                                                sw.WriteLine("delete rror");
                                            }
                                            else
                                            {

                                                waitQueue.RemoveAt(pos);
                                                sw.WriteLine($"Usuario {pos} eliminado correctamente");
                                            }
                                            break;
                                        case "chpin":
                                            if (comandoFinal.Length == 2)
                                            {
                                                if (compruebaPin(comandoFinal[1]).Item1)
                                                {
                                                    using (StreamWriter swPin = new StreamWriter($"{Environment.GetEnvironmentVariable("userprofile")}\\pin.txt"))
                                                    {
                                                        swPin.Write(compruebaPin(comandoFinal[1]).Item2);
                                                    }
                                                    sw.WriteLine("Pin guardado en archivo");
                                                }
                                                else
                                                {
                                                    sw.WriteLine("Error al guardar en archivo");
                                                }
                                            }
                                            break;
                                        case "exit":
                                            sCliente.Close();
                                            break;
                                        case "shutdown":
                                            using (StreamWriter swPin = new StreamWriter($"{Environment.GetEnvironmentVariable("userprofile")}\\waitQueue.txt"))
                                            {
                                                foreach (string usuarios in waitQueue)
                                                {
                                                    swPin.Write($"{usuarios};");
                                                }
                                            }
                                            Stop();
                                            break;
                                        default:
                                            sw.WriteLine("Comando no valido");
                                            break;
                                    }
                                }
                                while (comandoFinal[0] != "exit");
                            }
                            else
                            {
                                sCliente.Close();
                            }

                        }
                        else
                        {
                            string comando = "";
                            do
                            {
                                sw.WriteLine("Introduce un comando (list | add): ");
                                comando = sr.ReadLine();
                                switch (comando)
                                {
                                    case "list":
                                        foreach (string usuario in waitQueue)
                                        {
                                            sw.WriteLine($"{usuario}");
                                        }
                                        break;
                                    case "add":
                                        foreach (string usuario in waitQueue)
                                        {
                                            if (!usuarioEnLista(waitQueue.ToArray(), nombreUsuario))
                                            {
                                                waitQueue.Add($"{nombreUsuario} - {DateTime.Now.ToString("dd/MM/yyyy  HH:mm")}");
                                                sw.WriteLine("OK");
                                            }
                                        }
                                        break;
                                    default:
                                        sw.WriteLine("Comando no valido");
                                        break;
                                }
                            }
                            while (comando != "list" && comando != "add");
                        }
                    }
                    else
                    {
                        sw.WriteLine("No existe ese usuario");
                        sCliente.Close();
                    }
                }
                catch (IOException e)
                {

                }
            }
        }

        public void Stop()
        {
            serverRunning = false;
            s.Close();
        }
        public void cargarWaitQueue()
        {
            using (StreamReader srPin = new StreamReader($"{Environment.GetEnvironmentVariable("userprofile")}\\waitQueue.txt"))
            {
                string[] usuarios = srPin.ReadToEnd().Split(';');
                for (int i = 0; i < usuarios.Length; i++)
                {
                    waitQueue.Add(usuarios[i]);
                }
            }
        }

        public void ReadNames(string path)
        {
            try
            {
                using (StreamReader sr = new StreamReader(path))
                {
                    while (sr.ReadLine() != null)
                    {
                        users = sr.ReadToEnd().Split(';');
                    }
                }
            }
            catch (IOException e)
            {
                Console.WriteLine("Error de archivo");
            }
        }

        public int ReadPin(string path)
        {
            string pin = "";
            try
            {
                using (StreamReader sr = new StreamReader(path))
                {
                    string content = sr.ReadToEnd().Trim();
                    if (content.Length >= 4)
                    {
                        for (int i = 0; i <= 3; i++)
                        {
                            pin += content[i];
                        }
                    }
                    else
                    {
                        pin = "-1";
                    }
                }
            }
            catch (IOException e)
            {
                Console.WriteLine("Error de archivo");
                return -1;
            }
            return int.Parse(pin) | -1;
        }


        public bool usuarioEnLista(string[] users, string nombreUsuario)
        {
            for (int i = 0; i < users.Length; i++)
            {
                if (users[i] == nombreUsuario)
                {
                    return true;
                }
            }
            return false;
        }

        public (bool, int) compruebaPin(string pin)
        {
            if (pin.Length == 4 && int.TryParse(pin, out int pinFinal))
            {
                return (true, pinFinal);
            }
            return (false, 0);
        }
    }
}