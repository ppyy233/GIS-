using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _20232633035
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }

        public int SearchType = 0;
        private void button1_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }
        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            SearchType = 1;
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            SearchType = 2;
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            SearchType = 3;
        }

        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {
            SearchType = 4;
        }

        private void radioButton5_CheckedChanged(object sender, EventArgs e)
        {
            SearchType = 5;
        }

        private void radioButton6_CheckedChanged(object sender, EventArgs e)
        {
            SearchType = 6;
        }
    }
}
