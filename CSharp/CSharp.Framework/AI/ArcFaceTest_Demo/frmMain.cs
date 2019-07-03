using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ArcFaceTest
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form1 frm = new Form1();
            frm.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            frmFreeCompare frm = new frmFreeCompare();
            frm.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string sTemp1 = Environment.CurrentDirectory;
            string sTemp2 = "\\22\\22";
            MessageBox.Show(Path.Combine(sTemp1, sTemp2.TrimStart('\\')));

        }
    }
}
