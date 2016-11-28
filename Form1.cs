using System;
using System.IO.Ports;
using System.Windows.Forms;
using System.Collections.Generic;//NO OLVIDEN LAS LIBRERAS
using System.Text;



namespace TxRx_SerialPort
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            Control.CheckForIllegalCrossThreadCalls = false;

        }
        //TBN
        bool work_data = false;
        byte[] payload = new byte[256];
        int Mac_16_bytes;
        public byte checksum(byte[] payload_to_send, int size_payload_check)//ES NECESARIO PARA CALCULAR EL CHECKSUM
        {

            int SumCRC = 0;
            for (int x = 3; x <= size_payload_check; x++)
                SumCRC += payload_to_send[x];
           // int temp = SumCRC & 0xff;
           byte check= (byte)(0xFF - SumCRC);
            return check;

        }
        private void puertosDisponibles()
        {
            foreach (string puertoDis in System.IO.Ports.SerialPort.GetPortNames())
            {
                cmbPuertos.Items.Add(puertoDis);
            }
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            puertosDisponibles();
        }
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            serialPort1.Close();
        }
        int recived_package = 0;
        private void serialPort1_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {//Funcion par RECIBIR
            string datorx = serialPort1.ReadExisting().TrimStart().TrimEnd();

            byte[] hexFrame = new byte[datorx.Length];

            hexFrame = Encoding.ASCII.GetBytes(datorx);
            if(datorx.Length>=9)
            if (hexFrame[0]==0x7E && hexFrame.Length== (hexFrame[2]+4))
            {
                int payload_size = (byte)(hexFrame[2] - 0x05);

                for (int i = 0; i < payload_size; i++)
                    payload[i] = hexFrame[i + 8];//PAYLOAD ALMACENA LO QUE TE LEGO EN BITS AQUI BUSCAS LO QUE TE LLEGO EN HEX
                    //WICHO ALMACENA ESTO EN DIVERSAS VARIABLES ID DE NODO, TIPO DE TRAMA, CM, ETC
                string payloadstring = Encoding.ASCII.GetString(payload);

                Dialog_Datos_recibidos.Text += payloadstring;//payloadstring ALMACENA LO QUE TE LLEGO
                    recived_package++;
                for (int j = 0; j < 256; j++)
                    payload[j] = 0x00;
                
                
            }
            // else
            //   MessageBox.Show("Problema en la llegada detrama");
            work_data = false;// ES NECESARIO PARA EVITAR ALGUNOS CONFLICTOS DE ENVIO/RECEPCION
        }
        private void cmbPuertos_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            serialPort1.PortName = cmbPuertos.Text;
            cmbPuertos.Enabled = false;
            try
            {
                serialPort1.Open();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Selecciones otro puerto", "Puerto no disponible");
                cmbPuertos.Enabled = true;
            }
        }
        private void btnEnviar_Click_1(object sender, EventArgs e)//para eviar
        {
            if (!work_data)
            {
                string msj_to_send = txtTx.Text;

                byte size_payload = Convert.ToByte(msj_to_send.Length);

                byte[] Payload_to_send = Encoding.ASCII.GetBytes(msj_to_send);
                byte[] frame15_4 = new byte[msj_to_send.Length + 9];

                frame15_4[0] = 0x7E;
                frame15_4[1] = 0x00;
                frame15_4[2] = (byte)(size_payload + 0x05);
                frame15_4[3] = 0x01;
                frame15_4[4] = 0x01;
                if ((Mac16Bytes_H.TextLength > 0)&&(Mac16Bytes_L.TextLength > 0) && (Mac16Bytes_H.TextLength < 3) && (Mac16Bytes_L.TextLength < 3))
                {
                    int Mac_16_Dec_H = Convert.ToInt16(Mac16Bytes_H.Text);
                    int Mac_16_Dec_L = Convert.ToInt16(Mac16Bytes_L.Text);
                    frame15_4[5] =(byte) Mac_16_Dec_H;
                    frame15_4[6] = (byte)Mac_16_Dec_L;
                }
                else
                {
                    frame15_4[5] = 0xFF;
                    frame15_4[6] = 0xFF;
                }
                frame15_4[7] = 0x00;
                for (int i = 0; i < size_payload; i++)
                    frame15_4[i + 8] = Payload_to_send[i];
                frame15_4[size_payload + 8] = checksum(frame15_4, size_payload + 7);
                int size_frm = size_payload + 9;
                serialPort1.Write(frame15_4, 0, size_frm);

            
            }

                /* byte[] miBuffer = new byte[13] { 0x7E, 0x00, 0x09, 0x01, 0x01, 0xFF, 0xFF, 0x00, 0x68, 0x6F, 0x6C, 0x61, 0x5B };

                 serialPort1.Write(miBuffer, 0, 13);

                 txtTx.Clear();*/

        
        }

        private void txtRecibidos_Click(object sender, EventArgs e)
        {

        }

        private void txtRx_Click(object sender, EventArgs e)
        {

        }

        private void txtTx_TextChanged(object sender, EventArgs e)
        {

        }

       

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBox1.Items.Add("Hello");
            comboBox1.Items.Add("ASK");
        }
       
        private void textBox1_TextChanged_1(object sender, EventArgs e)
        {
            
        }

        private void label3_Click(object sender, EventArgs e)
        {
            Dialog_Datos_recibidos.Text="";
        }
    }

}




 



