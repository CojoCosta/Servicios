using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Ejemplo7_Task_
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Random g = new Random();
            this.BackColor = Color.FromArgb(255, g.Next(256), g.Next(256), g.Next(256));
        }
        public int SlowFind(int num, int[] vector)
        {
            Random g = new Random();
            int position;
            do
            {
                position = g.Next(vector.Length); // Random search :-/
            } while (num != vector[position]);
            return position;
        }
        //public void Init()
        //{
        //    Random g = new Random();
        //    int[] v = new int[100000000]; // Ojo, esto ocupa 400MB
        //    v[g.Next(v.Length)] = 1;
        //    Stopwatch time = new Stopwatch(); // Timing control, no necesario.
        //    time.Start();
        //    int result = SlowFind(1, v);
        //    time.Stop();
        //    Debug.WriteLine($"Time Elapsed: {time.ElapsedMilliseconds} ms.");
        //    Debug.WriteLine("I couldn't do anything during the search :-(");
        //    label1.Text = $"The 1 is in the position {result} of the vector.";
        //}
        public async void Init() // async ? “Puedo usar await dentro”
        {
            // Cada hilo crea su propia instancia de Random
            // Evita problemas de thread-safety sin necesidad de locks
            Random g = new Random();
            // Código síncrono normal
            int[] v = new int[100000000];
            v[g.Next(v.Length)] = 1;
            // Operación asíncrona - NO BLOQUEA
            Task<int> task = Task.Run(() => SlowFind(1, v));
            // Mientras se hace la operación previa, la UI responde.
            Debug.WriteLine("Doing things while waiting for the result. :-D");
            // await ? "espera sin bloquear, luego continúa aquí".
            int result = await task;
            label1.Text = $"The 1 is in the position {result} of the vector.";
        }
        private void btnSearch_Click(object sender, EventArgs e)
        {
            Init();
        }

        private readonly HttpClient _httpClient = new HttpClient();
        private async void button1_Click_1(object sender, EventArgs e)
        {
            try // Se puede hacer control de excepciones aquí.
            {
                textBox2.Text = await _httpClient.GetStringAsync(textBox1.Text);
            }
            catch (HttpRequestException ex)
            {
                textBox2.Text = $"Error de conexión: {ex.Message}";
            }
            catch (Exception ex)
            {
                textBox2.Text = $"Error inesperado: {ex.Message}";
            }
        }
    }
}
