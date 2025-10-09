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
        public static void funcionCat(string archivo, string[] args)
        {
            if (args[1] == null)
            {
                StreamReader sr;
                sr = new StreamReader(archivo);
                Console.WriteLine(sr.ReadToEnd());
                sr.Close();
            }
        }
    }
}
