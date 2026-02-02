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
        async Task<string> DownloadFileAsync(string fileName, int delayMs) 
        { 
            await Task.Delay(delayMs);
            return $"File {fileName} downloaded in {delayMs} ms";
        }
        Random rd = new Random();
        private async void btnDesc_Click(object sender, EventArgs e)
        {
            int aleatorio = rd.Next(5000);
            string resultadoTask = await DownloadFileAsync(txtFileName.Text, aleatorio);
            txtResults.Text += resultadoTask + Environment.NewLine;
        }
    }
}