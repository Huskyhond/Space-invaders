using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Game1
{
    public partial class Form1 : Form
    {
        public int i; 

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }

        private void button1_Click(object sender, EventArgs e)
        {
            i = Convert.ToInt32(comboBox1.SelectedItem);
            DialogResult = DialogResult.OK;
        }
    }
}
