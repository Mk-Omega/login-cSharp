using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.Windows.Forms;

namespace IMS
{
    public class Authentication
    {
        public string connectionString { get; set; }
        public string connection;

        public void getConnection()
        {
            connection = @"Data Source=account.db; Version=3";
            connectionString = connection;
        }

        public void createDB()
        {
            if (!File.Exists("account.db"))
            {
                try
                {
                    File.Create("account.db");

                    createTable();
                }
                catch (Exception ex)
                {
                    Console.Write(ex);
                }
            }
            else
            {
                createTable();
            }
        }

        private void createTable()
        {
            try
            {
                getConnection();

                using (SQLiteConnection con = new SQLiteConnection(connection))
                {
                    con.Open();
                    SQLiteCommand cmd = new SQLiteCommand();


                    string query = @"CREATE TABLE admin (id INTEGER PRIMARY KEY AUTOINCREMENT, username TEXT(25), password TEXT(25))";
                    cmd.CommandText = query;
                    cmd.Connection = con;

                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error" + ex.Message);
            }

        }
    }
}
