using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicio4
{
    internal class Record
    {
        private string nombre;
        public string Nombre
        {
            set
            {
                nombre = value.Substring(0, 3);
            }
            get
            {
                return nombre;
            }
        }
        private int segundos;
        public int Segundos { set; get; }
        public Record() { }
        public Record(string nombre, int segundos)
        {
            this.nombre = nombre;
            this.segundos = segundos;
        }
    }
}
