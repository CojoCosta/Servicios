using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoletinArchivosDelegadosLambdas
{
    public class Ejercicio1C
    {
        public static void newFile(string[] args)
        {
            try
            {

                if (args.Length == 2)
                {
                    Console.WriteLine("Sin modificador");
                    using (StreamWriter sw = new StreamWriter(args[0]))
                    {
                        sw.WriteLine(args[1]);
                    }
                }
                else if (args.Length == 3)
                {
                    Console.WriteLine("Con modificador -a");
                    if (args[0] == "-a")
                    {
                        using (StreamWriter sw = new StreamWriter(args[1], true))
                        {
                            sw.WriteLine(args[2]);
                        }
                    }
                    else
                    {
                        Console.WriteLine("Modificador incorrecto");
                    }
                }
                else
                {
                    Console.WriteLine("Comando no valido");
                }
            }
            catch(IOException)
            {

            }
        }
    }
}
