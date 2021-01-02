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
    public partial class register : Form
    {

        Authentication auth;

        public register()
        {
            InitializeComponent();
        }

        private void register_Load(object sender, EventArgs e)
        {

        }

        private void btn_register_Click(object sender, EventArgs e)
        {
            if (txt_uname.Text.Trim() != string.Empty && txt_pass.Text.Trim() != string.Empty)
            {
                checkAccount(txt_uname.Text, txt_pass.Text);
            }
        }

        private void checkAccount(string uname, string pass)
        {
            auth = new Authentication();

            auth.createDB();
            auth.getConnection();

            try
            {
                using (SQLiteConnection con = new SQLiteConnection(auth.connectionString))
                {
                    SQLiteCommand cmd = new SQLiteCommand();
                    con.Open();
                    int count = 0;
                    string query = @"SELECT * FROM admin WHERE username='" + uname + "' AND password='" + pass + "'";
                    cmd.CommandText = query;
                    cmd.Connection = con;

                    SQLiteDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        count++;
                    }
                    if (count == 1)
                    {
                        MessageBox.Show("Error account already taken!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    else if (count == 0)
                    {
                        insertData(txt_uname.Text, txt_pass.Text);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void insertData(string uname, string pass)
        {
            auth = new Authentication();
            auth.getConnection();

            try
            {
                using (SQLiteConnection con = new SQLiteConnection(auth.connectionString))
                {
                    con.Open();
                    SQLiteCommand cmd = new SQLiteCommand();
                    string query = @"INSERT INTO admin(username, password) VALUES(@uname, @pass)";
                    cmd.CommandText = query;
                    cmd.Connection = con;
                    cmd.Parameters.Add(new SQLiteParameter("@uname", uname));
                    cmd.Parameters.Add(new SQLiteParameter("@pass", pass));
                    cmd.ExecuteNonQuery();

                    MessageBox.Show("Account register successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
            }
        }
    }
}
