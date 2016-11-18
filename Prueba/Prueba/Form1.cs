using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.IO.Ports;

namespace Prueba
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        bool conectado = false;

        private void btnConexion_Click(object sender, EventArgs e)
        {
            if (!conectado)  //Si no esta conectado el puerto, realizar la conexión
            {
                try
                {
                    //Definir los parámetros de la comunicación serial 
                    serialPort1.PortName = comboBox1.Text;
                    serialPort1.BaudRate = 38400;
                    serialPort1.Parity = Parity.None;
                    serialPort1.DataBits = 8;
                    serialPort1.StopBits = StopBits.One;
                    serialPort1.Open();  //Abrir la conexión
                    btnConexion.Text = "Cerrar conexión serie";
                    conectado = true;
                }
                catch (ArgumentException ex)   // Código que se ejecuta en caso de error
                {
                    MessageBox.Show("Error en la conexión " + ex.Message);
                    serialPort1.Close(); //Cerrar la conexión
                }
                conectado = true;
            }
            else   //Cerrar conexión.
            {
                btnConexion.Text = "Iniciar Conexión ";
                conectado = false;
                if (serialPort1 != null)
                    serialPort1.Close();
            }
        }

        private void btnMensaje_Click(object sender, EventArgs e)
        {
            if (conectado)
                serialPort1.WriteLine("carretera");  //Escribir le letra "hola" por el puerto serie
        }

        void comPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            if (serialPort1.IsOpen == true)
            {
                string msg = serialPort1.ReadExisting();
                //textBox1.Text = msg;
                MessageBox.Show(msg.Trim());
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (serialPort1.IsOpen == true)
            {
                string msg = serialPort1.ReadExisting();
                textBox1.Text = msg;
               // MessageBox.Show(msg.Trim());
            }
        }
    }
}
