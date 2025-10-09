using System;
using System.ComponentModel;

namespace BoletinArchivosDelegadosLambdas
{
    internal class Ejercicio1A
    {
        public static void funcionLs(string[] args)
        {
            if (args.Length > 0 && Directory.Exists(args[0]))
            {
                DirectoryInfo directoryInfo = new DirectoryInfo(args[0]);
                Console.ForegroundColor = ConsoleColor.Blue;
                foreach (var directorios in directoryInfo.GetDirectories())
                {
                    Console.WriteLine(directorios.Name);
                }
                Console.ForegroundColor = ConsoleColor.Magenta;
                foreach (var archivos in directoryInfo.GetFiles())
                {
                    Console.WriteLine($"{archivos.Name} \t TAMAÑO {archivos.Length}");
                }
                Console.ResetColor();
            }
            else
            {
                Console.WriteLine("NO EXISTEEEEE");
            }
        }
    }
}
