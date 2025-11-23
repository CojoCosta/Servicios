using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Ejercicio3
{
    public partial class Form1 : System.Windows.Forms.Form
    {
        public Form1()
        {
            InitializeComponent();
            AcceptButton = btnDesc;
        }
        async Task<string> DownloadFileAsync(string fileName, int delayMs) //devolvemos el string
        { 
            await Task.Delay(delayMs);
            return $"File {fileName} downloaded in {delayMs} ms";
        }
        private async void btnDesc_Click(object sender, EventArgs e)
        {
            int aleatorio = nRandom(10);
            //texto es un string que al hacer el await de la tarea lo estamos igualando al resultado de ejecutar dicha tarea, si no hicieramos await no devolveria el string ya que devolveria la propia tarea (Task) sin completar pq el programa seguiria ejecutandose, de esta manera le mandamos esperar a acabar la tarea y obtenemos su resultado
            string resultadoTask = await DownloadFileAsync(txtFileName.Text, aleatorio);
            txtResults.Text += resultadoTask + Environment.NewLine;
        }
        Random rd = new Random();
        public int nRandom(int numMax)
        {
            return (int)rd.Next(numMax);
        }

    }
}