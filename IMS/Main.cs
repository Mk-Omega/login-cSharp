using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IMS
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
        }

        private void Main_Load(object sender, EventArgs e)
        {
            ims ims = new ims();
            ims.ShowDialog();

            string uname;
            uname = ims.username;
            lbl_name.Text = uname;
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}
