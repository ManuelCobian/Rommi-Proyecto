using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Windows.Forms;

namespace romy
{
    class database
    {
        public MySqlConnection connection;
        public MySqlCommand cmd;
        public MySqlDataReader leer;

        public database(){
            connection = new MySqlConnection("server=127.0.0.1; database=romy; Uid=root; pwd=;");
        }

        public bool login(String user, String pass)
        {
            connection.Open();
            bool existe = false;
            cmd = new MySqlCommand("SELECT * FROM Usuarios WHERE Usuario='" + user + "' AND Pass='" + pass + "'",connection);
            try
            {
                leer = cmd.ExecuteReader();
                if (leer.Read())
                {
                    existe = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error en la consulta: " + ex.Message, "ROMY", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            return existe;
        }

        public int altaUser(String user, String pass)
        {
            connection.Open();
            int creado = 0;
            cmd = new MySqlCommand("INSERT INTO Usuarios values(null, '"+user+"', '"+pass+"', 0)", connection);
            try
            {
                creado = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al crear al usuario. "+ex.Message, "ROMY", MessageBoxButtons.OK, MessageBoxIcon.Error);
            
            }
            return creado;
        }

        public void closeConnection()
        {
            connection.Close();
        }

    }
}
