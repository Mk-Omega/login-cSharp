using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SQLite;

namespace IMS
{
    public partial class ims : Form
    {
        public string username;
        Authentication auth;

        public ims()
        {
            InitializeComponent();
        }

        private void ims_Load(object sender, EventArgs e)
        {

        }

        private void btn_login_Click(object sender, EventArgs e)
        {
            if(txt_uname.Text.Trim() != string.Empty && txt_pass.Text.Trim() != string.Empty)
            {
                checkAccount(txt_uname.Text, txt_pass.Text);
            }
        }

        private void txt_pass_TextChanged(object sender, EventArgs e)
        {

        }

        private void btn_reg_Click(object sender, EventArgs e)
        {
            register reg = new register();
            reg.ShowDialog();
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void checkAccount(string uname, string pass)
        {
            auth = new Authentication();
            auth.getConnection();

            try
            {
                using (SQLiteConnection con = new SQLiteConnection(auth.connectionString))
                {
                    con.Open();
                    SQLiteCommand cmd = new SQLiteCommand();

                    string query = @"SELECT * FROM admin WHERE username='" + uname + "' AND password='" + pass + "'";
                    cmd.CommandText = query;
                    cmd.Connection = con;

                    int count = 0;

                    SQLiteDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        count++;
                    }
                    if (count == 1)
                    {
                        DialogResult result = MessageBox.Show("Login successful", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        if (result == DialogResult.OK)
                        {
                            username = uname;
                            this.Close();
                        }
                    }
                    else
                    {
                        MessageBox.Show("Admin not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
    }
}
