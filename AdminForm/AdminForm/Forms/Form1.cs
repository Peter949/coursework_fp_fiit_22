using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AdminForm.Forms;
using telegram_bot_c_.Models;

namespace AdminForm
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void выходToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void openForm2(Button button)
        {
            Form2 form2 = new Form2(button.Text);
            this.Hide();
            form2.ShowDialog();
            this.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            openForm2(button1);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            openForm2(button2);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            openForm2(button3);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            openForm2(button4);
        }

    }
}
