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
    public partial class frmPrincipal : Form
    {
        public String user;
        public frmPrincipal()
        {
            InitializeComponent();
        }

        private void frmPrincipal_Load(object sender, EventArgs e)
        {
            if (user == "admin")
                btnNuevo.Visible = true;
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            frmNuevoUsuario nuevo = new frmNuevoUsuario();
            nuevo.Show();
        }
    }
}
