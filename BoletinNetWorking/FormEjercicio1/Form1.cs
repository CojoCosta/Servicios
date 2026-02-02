using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FormEjercicio1
{
    public partial class Form1 : Form//Título, icono.
    {
        public Form1()
        {
            InitializeComponent();
        }
        IPAddress ip = IPAddress.Parse("127.0.0.1");
        int puerto = 31416;
        int pMax = IPEndPoint.MaxPort;
        private void btnElegir_Click(object sender, EventArgs e)
        {
            bool flag = true;
            PuertoIP eleccion = new PuertoIP();
            lblIP.Text = "IP: ";
            lblPuerto.Text = "Puerto: ";
            DialogResult dr;
            dr = eleccion.ShowDialog();
            if (dr == DialogResult.OK)
            {
                ip = IPAddress.Parse("127.0.0.1");
                puerto = 31416;
                lblPorDefecto.Text = "Por defecto";
                if (eleccion.txtPuerto.Text.Trim() != "" && eleccion.txtIP.Text.Trim() != "")
                {
                    if (!int.TryParse(eleccion.txtPuerto.Text.Trim(), out int puertoCorrecto) || !IPAddress.TryParse(eleccion.txtIP.Text.Trim(), out IPAddress ipCorrecta))
                    {
                        MessageBox.Show("No se ha elegido una IP o un puerto");
                        lblPorDefecto.Text = "Por defecto";
                    }
                    else
                    {
                        if (puertoCorrecto > 0 && puertoCorrecto < pMax)
                        {
                            ip = ipCorrecta;
                            puerto = puertoCorrecto;
                            lblPorDefecto.Text = "Por eleccion";
                        }
                    }
                } 
            }
            lblIP.Text += ip;
            lblPuerto.Text += puerto;
            eleccion.Close();
        }
        private async Task<string> EnvioYRecepcionDeDatos(string msg)
        {
            try
            {
                using (Socket s = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp))
                {
                    IPEndPoint ep = new IPEndPoint(ip, puerto);
                    await s.ConnectAsync(ep);

                    Encoding codificacion = Console.OutputEncoding;
                    using (NetworkStream ns = new NetworkStream(s))
                    using (StreamReader sr = new StreamReader(ns, codificacion))
                    using (StreamWriter sw = new StreamWriter(ns, codificacion))
                    {
                        sw.AutoFlush = true;

                        await sw.WriteLineAsync(msg);

                        msg = await sr.ReadLineAsync();

                        return msg;
                    }
                }
            }
            catch (Exception ex) when (ex is SocketException || ex is IOException)
            {
                return ex.Message;
            }
            catch (Exception ex)
            {
                return $"Error inesperado: {ex.GetType().Name}. Contacte con soporte.";
            }
        }
        private async void btnTime_Click(object sender, EventArgs e)
        {
            lblResultado.Text = "Resultado del comando: ";
            if ((Button)sender == btnClose)
            {
                string msg2 = await EnvioYRecepcionDeDatos($"{((Button)sender).Text.ToLower()} {txtPassword.Text}");
            }
            else
            {
                string msg3 = await EnvioYRecepcionDeDatos(((Button)sender).Text.ToLower());
                lblResultado.Text += msg3;
            }
        }

    }
}
