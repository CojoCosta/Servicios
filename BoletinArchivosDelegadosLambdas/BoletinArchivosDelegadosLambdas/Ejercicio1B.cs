using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace BoletinArchivosDelegadosLambdas
{
    public class Ejercicio1B
    {
        public static void funcionCat(string[] args)
        {
            string[] modificador;
            int numMod = 0;
            try
            {
                if (args.Length <= 2 && args.Length > 0)
                {
                    if (args.Length == 1)
                    {
                        using (StreamReader sr = new StreamReader(args[0]))//TODO using
                        {
                            Console.WriteLine(sr.ReadToEnd());
                        }
                    }
                    else
                    {
                        using (StreamReader sr = new StreamReader(args[1]))
                        {
                            modificador = args[0].Split("-n");
                            string linea = "";
                            if (int.TryParse(Console.ReadLine(), out numMod))
                            {
                                for (int i = 0; i < numMod && linea != null; i++)
                                {
                                    linea = sr.ReadLine();
                                    Console.WriteLine(linea);
                                }
                            }
                        }
                    }
                }
                else
                {
                    Console.WriteLine("No hay nada");
                }
            }
            catch (IOException)
            {
                Console.WriteLine("NO EXISTE EL ARCHIVO");
            }
        }
    }
}
