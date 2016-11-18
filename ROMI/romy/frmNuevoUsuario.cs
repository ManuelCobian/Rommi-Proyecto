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
    public partial class frmNuevoUsuario : Form
    {
        public frmNuevoUsuario()
        {
            InitializeComponent();
        }

        private void bntRegistrar_Click(object sender, EventArgs e)
        {
            if (txtPass.Text == txtPass2.Text)
            {
                database db = new database();
                encriptar en = new encriptar();
                int alta = 0;
                String pass = en.SHA256Encrypt(txtPass.Text.ToString());
                alta = db.altaUser(txtUser.Text.ToString(), pass);
                if (alta > 0)
                {
                    MessageBox.Show("Usuario dado de alta correctamente", "ROMY", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Dispose();
                }
            }
            else
            {
                MessageBox.Show("Las contraseñas no coinciden", "ROMY", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

        }
    }
}
