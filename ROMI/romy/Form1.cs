using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace romy
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnEntrar_Click(object sender, EventArgs e)
        {
            database db = new database();
            encriptar en = new encriptar();
            bool entro = false;
            String pass=en.SHA256Encrypt(textBox2.Text.ToString());
            entro = db.login(textBox1.Text.ToString(), pass);
            if (entro)
            {
                this.Hide();
                frmPrincipal prin = new frmPrincipal();
                prin.user = textBox1.Text.ToString();
                prin.Show();
            }
            else
            {
                MessageBox.Show("Verifique sus datos", "ROMY", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            db.closeConnection();
        }
    }
}
