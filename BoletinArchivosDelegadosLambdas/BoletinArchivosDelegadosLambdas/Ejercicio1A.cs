using System;
using System.ComponentModel;

namespace BoletinArchivosDelegadosLambdas
{
    internal class Ejercicio1A
    {
        public static void funcionLs(string[] args)
        {
            if (Directory.Exists(args[0]) && args.Length > 0)
            {
                DirectoryInfo directoryInfo = new DirectoryInfo(args[0]);
                foreach (var directorios in directoryInfo.GetDirectories())
                {
                    Console.ForegroundColor = ConsoleColor.DarkBlue;
                    Console.WriteLine(directorios.Name);
                    Console.ResetColor();
                }
                foreach (var archivos in directoryInfo.GetFiles())
                {
                    Console.ForegroundColor = ConsoleColor.Magenta;
                    Console.WriteLine($"{archivos.Name} TAMAÑO {archivos.Length}");
                    Console.ResetColor();
                }
            }
            else
            {
                Console.WriteLine("NO EXISTEEEEE");
            }
        }
    }
}
