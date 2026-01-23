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
        private int id;
        private IPAddress ip;
        private string nombre;
        private StreamWriter sw;
        public int Id { get; set; }

        public IPAddress Ip { get; set; }
        public string Nombre { get; set; }
        public StreamWriter Sw {  get; set; }

        public Cliente(int id, IPAddress ip, string nombre, StreamWriter sw)
        {
            this.id = id;
            this.ip = ip;
            this.nombre = nombre;
            this.sw = sw;
        }
    }
}
