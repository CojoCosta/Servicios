using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicio5
{
    internal class FlyRunner
    {
        public StreamWriter Sw {  get; set; }
        public int KilledFlies { get; set; }
        public int Bites { get; set; }
        public FlyRunner(StreamWriter sw)
        {
            Sw = sw;
            KilledFlies = 0;
            Bites = 0;
        }

    }
}
