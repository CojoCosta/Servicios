using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Security.Policy;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static System.Net.WebRequestMethods;

namespace Servicio5
{
    internal class FlyServer
    {
        List<FlyRunner> clients = new List<FlyRunner>();
        public bool ServerRunning { get; set; } = true;
        Socket s;
        int defaultPort = 31416;
        object lockFlies = new Object();

        public int GetPort(int port)
        {
            bool flag = true;
            try
            {
                using (Socket s = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp))
                {

                    while (flag)
                    {
                        IPEndPoint ie = new IPEndPoint(IPAddress.Any, port);
                        s.Bind(ie);
                        s.Listen(10);
                    }
                }
            }
            catch (SocketException)
            {
                port++;
            }
            return port;
        }

        public void InitServer()
        {
            IPEndPoint ie = new IPEndPoint(IPAddress.Any, 31416);
            using (Socket s = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp))
            {
                s.Bind(ie);
                s.Listen(10);
                while (ServerRunning)
                {
                    try
                    {
                        Socket sClient = s.Accept();
                        Thread thread = new Thread(() => FlyRunnerThread(sClient));
                        thread.Start();
                    }
                    catch (SocketException e)
                    {

                    }
                }
            }
        }

        public void StopServer()
        {
            ServerRunning = false;
            s.Close();
        }

        public void FlyRunnerThread(Socket sClient)
        {
            IPEndPoint ieCliente = (IPEndPoint)sClient.RemoteEndPoint;
            Console.WriteLine($"Cliente conectado:{ieCliente.Address} en puerto {ieCliente.Port}");
            Encoding codificacion = Console.OutputEncoding;
            try
            {
                using (NetworkStream ns = new NetworkStream(sClient))
                using (StreamReader sr = new StreamReader(ns))
                using (StreamWriter sw = new StreamWriter(ns))
                {
                    FlyRunner fr = new FlyRunner(sw);
                    lock (lockFlies)
                    {
                        clients.Add(fr);
                    }
                    sw.AutoFlush = true;
                    sw.WriteLine("Fly");
                    string comando = "";
                    while (comando != null)
                    {
                        sw.WriteLine("Escribe un comando ('fsw' | 'quit'): ");
                        comando = sr.ReadLine();
                        switch (comando)
                        {
                            case "fsw":
                                switch (GetRandom(3))
                                {
                                    case 0:
                                        lock (lockFlies)
                                        {
                                            fr.KilledFlies++;
                                            sw.WriteLine($"Killed {fr.KilledFlies} fly/flies!!");
                                        }
                                        break;
                                    case 1:
                                        lock (lockFlies)
                                        {
                                            fr.Bites++;
                                            sw.WriteLine($"You have been bitten. Number of bites: {fr.Bites}.");
                                        }
                                        break;
                                    case 2:
                                        lock (lockFlies)
                                        {
                                            int moscaAleatoria = 0;
                                            bool flag = true;
                                            moscaAleatoria = GetRandom(clients.Count);
                                            while (flag)
                                            {
                                                if (clients[moscaAleatoria].Sw != fr.Sw)
                                                {
                                                    clients[moscaAleatoria].Bites++;
                                                    sw.WriteLine("Other fly bites you!!");
                                                    flag = false;
                                                }
                                            }
                                        }
                                        break;
                                }
                                break;

                            case "quit":
                                lock (lockFlies)
                                {
                                    foreach (FlyRunner cadaMosca in clients)
                                    {
                                        if (cadaMosca.Sw != sw)
                                        {
                                            cadaMosca.Sw.WriteLine($"Someone leaves with {fr.Bites} bites and {fr.KilledFlies} flies killed.");
                                        }
                                    }
                                }
                                comando = null;
                                break;
                            default:
                                lock (lockFlies)
                                {
                                    fr.Bites += 2;
                                    sw.WriteLine($"Big mistake, you were bitten twice.Number of bites: {fr.Bites}.");
                                    int aleatoria = GetRandom(clients.Count);
                                    clients[aleatoria].Bites--;
                                }
                                break;
                        }
                    }
                }
            }
            catch (SocketException e)
            {

            }
        }

        static Random rd = new Random();
        public static int GetRandom(int limite)
        {
            return rd.Next(limite);
        }
    }
}
