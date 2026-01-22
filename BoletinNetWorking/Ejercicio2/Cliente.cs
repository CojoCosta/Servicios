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
        public int id { get; set; }

        public IPAddress ip { get; set; }
        public string nombre { get; set; }
        public StreamWriter sw {  get; set; }

        public Cliente(int id, IPAddress ip, string nombre, StreamWriter sw)
        {
            this.id = id;
            this.ip = ip;
            this.nombre = nombre;
            this.sw = sw;
        }
    }
}
