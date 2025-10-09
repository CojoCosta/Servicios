using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace BoletinArchivosDelegadosLambdas
{
    public class Ejercicio1B
    {
        public static void funcionCat(string[] args)
        {
            StreamReader sr;
            string[] modificador;
            int numMod = 0;
            try
            {
                if (args.Length <= 2 && args.Length > 0)
                {
                    if (args.Length == 1)
                    {
                        sr = new StreamReader(args[0]);
                        Console.WriteLine(sr.ReadToEnd());
                    }
                    else
                    {
                        sr = new StreamReader(args[1]);
                        modificador = args[0].Split("-n");
                        numMod = int.Parse(modificador[1]);
                        for (int i = 0; i < numMod; i++)
                        {
                            if (sr.ReadLine != null)
                            {
                                Console.WriteLine(sr.ReadLine());
                            }
                        }
                    }
                    sr.Close();
                }
                else
                {
                    Console.WriteLine("No hay nada");
                }
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("NO EXISTE EL ARCHIVO");
            }
        }
    }
}
