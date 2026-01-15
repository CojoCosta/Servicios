using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ejercicio2
{
    internal class Cliente
    {
        private int id { get; set; }
        private string nombre { get; set; }
        private StreamWriter sw {  get; set; }

        public Cliente(int id, string nombre, StreamWriter sw)
        {
            this.id = id;
            this.nombre = nombre;
            this.sw = sw;
        }
    }
}
