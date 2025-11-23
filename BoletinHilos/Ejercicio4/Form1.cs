using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Ejercicio4
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        public string buscaPalabra(string dirección, string palabraBuscada)
        {
            int contador = 0;
            using (StreamReader sr = new StreamReader(dirección))
            {
                string textoArchivo = sr.ReadToEnd();
                for (int i = 0; i < (textoArchivo.Length - palabraBuscada.Length); i++)
                {
                    if (textoArchivo.Substring(i, palabraBuscada.Length) == palabraBuscada)
                    {
                        contador++;
                    }
                }
            }
            return $"Archivo:{Path.GetFileName(dirección)}, {palabraBuscada}, {contador}";
        }

        List<Task<String>> tareasBusqueda = new List<Task<String>>();
        private async void btnBusqueda_click(object sender, EventArgs e)
        {
            DirectoryInfo directorioActual = new DirectoryInfo(txtUrl.Text);
            foreach (FileInfo archivo in directorioActual.GetFiles())
            {
                if (archivo.Extension == ".txt")
                {
                    tareasBusqueda.Add(Task.Run(() => buscaPalabra(archivo.FullName, txtComun.Text)));
                }
            }
            while (tareasBusqueda.Count > 0)
            {
                Task<string> tareaAcabada = await Task.WhenAny(tareasBusqueda);
                tareasBusqueda.Remove(tareaAcabada);
                listaBusqueda.Items.Add(tareaAcabada.Result);
            }
        }

        public string primeraPosicionPalabra(string ruta, string palabraBuscada)
        {
            using (StreamReader sr = new StreamReader(ruta))
            {
                string textoArchivo = sr.ReadToEnd();
                for (int i = 0; i < textoArchivo.Length; i++)
                {
                    if (textoArchivo.Substring(i, palabraBuscada.Length) == palabraBuscada)
                    {
                        return $"{Path.GetFileName(ruta)}, {palabraBuscada}, {i}";
                    }
                }
            }
            return $"{Path.GetFileName(ruta)}, {palabraBuscada}, -1";
        }

        List<Task<String>> tareasPosicion = new List<Task<String>>();
        private async void btnPosicion_Click(object sender, EventArgs e)
        {
            DirectoryInfo directorioActual = new DirectoryInfo(txtUrl.Text);
            foreach (FileInfo archivo in directorioActual.GetFiles())
            {
                if (archivo.Extension == ".txt")
                {
                    tareasPosicion.Add(Task.Run(() => primeraPosicionPalabra(archivo.FullName, txtComun.Text)));
                }
            }

            string[] tareasRealizadas = await Task.WhenAll(tareasPosicion);
            while (tareasPosicion.Count > 0)
            {
                foreach (string tareaRealizada in tareasRealizadas)
                {
                    listaBusqueda.Items.Add(tareaRealizada);
                }
            }
            tareasPosicion.Clear();
        }

        private async void btnHttp_Click(object sender, EventArgs e)
        {
            HttpClient http = new HttpClient();
            try
            {
                Task<HttpResponseMessage> tareaPorRealizar = http.GetAsync(txtComun.Text);
                listaBusqueda.Items.Add(await tareaPorRealizar);
            }
            catch (Exception)
            {
            }
        }
    }
}
