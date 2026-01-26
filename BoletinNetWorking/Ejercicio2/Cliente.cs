using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Ejercicio2
{
    internal class Cliente
    {
        public IPAddress Ip { get; set; }
        public string Nombre { get; set; }
        public StreamWriter Sw {  get; set; }

        public Cliente(IPAddress ip, string nombre, StreamWriter sw)
        {
            this.Ip = ip;
            this.Nombre = nombre;
            this.Sw = sw;
        }
    }
}
