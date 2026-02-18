using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Servicio4
{
    internal class Servidor4
    {

        public bool ServerRunning { get; set; } = true;
        int port = puertoLibre(31416);
        object lockListas = new object();
        Socket s;
        Random rd = new Random();
        List<string> palabras;
        List<Record> records;
        string pathArchivoPalabras = Environment.GetEnvironmentVariable("userprofile") + "\\palabras.txt";
        string pathArchivoRecords = Environment.GetEnvironmentVariable("userprofile") + "\\records.txt";



        public static int puertoLibre(int puertoDefecto)
        {
            bool flag = false;
            IPEndPoint ie = new IPEndPoint(IPAddress.Any, puertoDefecto);
            do
            {
                using (Socket sPrueba = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp))
                {
                    try
                    {
                        sPrueba.Bind(ie);
                        sPrueba.Listen(10);
                        flag = true;
                    }
                    catch (SocketException)
                    {
                        puertoDefecto++;
                    }
                }
            }
            while (!flag && puertoDefecto <= IPEndPoint.MaxPort);
            return puertoDefecto;
        }

        public void StopServer()
        {
            ServerRunning = false;
            s.Close();
        }
        public void InitServer()
        {
            IPEndPoint ie = new IPEndPoint(IPAddress.Any, port);
            using (s = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp))
            {
                s.Bind(ie);
                s.Listen(10);
                while (ServerRunning)
                {
                    try
                    {
                        Socket sCliente = s.Accept();
                        Thread thread = new Thread(() => protocoloCliente(sCliente));
                    }
                    catch (SocketException)
                    {
                        Console.WriteLine("Servidor Cerrado");
                    }
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
                sw.AutoFlush = true;
                sw.WriteLine("Escribe un comando ('gw' | 'sw + (palabra)' | 'gr' | 'sr + (record)' | 'close + (clave)')");
                string comando = sr.ReadLine();
                while (comando != null)
                {
                    string[] comando2 = comando.Split(' ');

                    switch (comando2[0])
                    {
                        case "gw":
                            lock (lockListas)
                            {
                                palabras = leerArchivoPalabras();
                                int numeroRandom = rd.Next(0, palabras.Count);
                                sw.WriteLine(palabras[numeroRandom]);
                            }
                            break;

                        case "sw":
                            if (comando2.Length == 2)
                            {
                                lock (lockListas)
                                {

                                    if (guardarEnArchivo(comando2[1]))
                                    {
                                        palabras.Add(comando2[1]);
                                        sw.WriteLine("OK");
                                    }
                                    else
                                    {
                                        sw.WriteLine("ERROR");
                                    }
                                }
                            }
                            else
                            {
                                sw.WriteLine("ERROR de comando");
                            }
                            break;

                        case "gr":
                            lock (lockListas)
                            {
                                records = leerArchivoRecords();
                                foreach (Record cadaRecord in records)
                                {
                                    sw.WriteLine($"{cadaRecord.Nombre} {cadaRecord.Segundos}");
                                }
                            }
                            break;

                        case "sr":
                            if (comando2.Length == 2)
                            {
                                lock (lockListas)
                                {
                                    sw.WriteLine("Escribe tu nombre:");
                                    string nombre = sr.ReadLine();
                                    sw.WriteLine("Escribe tu tiempo:");
                                    if (int.TryParse(sr.ReadLine(), out int tiempo))
                                    {
                                        Record rc = new Record(nombre, tiempo);
                                        comparaRecords(records, rc);
                                    }

                                }
                            }
                            break;

                        case "close":
                            if (comando2.Length == 2)
                            {
                                if (comando2[1] == "1234")
                                {
                                    StopServer();
                                }
                            }
                            break;

                        default:
                            sw.WriteLine("Comando no valido");

                            break;
                    }
                }
            }
        }

        public List<string> leerArchivoPalabras()
        {
            List<string> todasPalabras = new List<string>();
            try
            {
                using (StreamReader sr = new StreamReader(pathArchivoPalabras, true))
                {
                    todasPalabras = sr.ReadToEnd().Split(',').ToList();
                }
            }
            catch (IOException e)
            {
                Console.WriteLine("Error leer archivo");
            }
            return todasPalabras;
        }

        public bool guardarEnArchivo(string nuevaPalabra)
        {
            try
            {
                using (StreamWriter sw = new StreamWriter(pathArchivoPalabras, true))
                {
                    sw.WriteLine($",{nuevaPalabra}");
                }
            }
            catch (IOException e)
            {
                Console.WriteLine("Error escribir archivo");
                return false;
            }
            return true;
        }

        public List<Record> leerArchivoRecords()
        {
            List<Record> records = new List<Record>();
            try
            {
                using (FileStream fs = new FileStream(pathArchivoRecords, FileMode.Open))
                using (BinaryReader br = new BinaryReader(fs))
                {
                    while (br.BaseStream.Position < br.BaseStream.Length)
                    {
                        string nombre = br.ReadString();
                        int segundos = br.ReadInt32();
                        records.Add(new Record(nombre, segundos));
                    }
                }
            }
            catch (IOException e)
            {
                Console.WriteLine("Error leer archivo");
            }
            return records;
        }

        public List<Record> comparaRecords(List<Record> records, Record recordComparar)
        {
            for (int i = 0; i < records.Count; i++)
            {
                if (records[i].Segundos > recordComparar.Segundos)
                {
                    records[i] = recordComparar;
                }
            }
            return records;
        }
        public bool escribirArchivoRecords(Record record)
        {
            try
            {
                using (FileStream fs = new FileStream(pathArchivoRecords, FileMode.OpenOrCreate))
                using (BinaryWriter br = new BinaryWriter(fs))
                {
                    comparaRecords(records, record);
                    foreach (Record cadaRecord in records)
                    {
                        br.Write($"{cadaRecord.Nombre} {cadaRecord.Segundos}");

                    }
                }
            }
            catch (IOException e)
            {
                Console.WriteLine("Error escribir archivo");
                return false;
            }
            return true;
        }
    }
}

