using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace problem
{
    public partial class ask : Form
    {
        public ask()
        {
            InitializeComponent();
        }
        public string Mask
        {
            get
            {
                return maskedTextBox1.Text;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(maskedTextBox1.Text.Length != 0)
                this.Close();
        }
    }
}
