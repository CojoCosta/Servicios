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
        private int id { get; set; }

        private IPAddress ip { get; set; }
        private string nombre { get; set; }
        private StreamWriter sw {  get; set; }

        public Cliente(int id, IPAddress ip, string nombre, StreamWriter sw)
        {
            this.id = id;
            this.ip = ip;
            this.nombre = nombre;
            this.sw = sw;
        }
    }
}
