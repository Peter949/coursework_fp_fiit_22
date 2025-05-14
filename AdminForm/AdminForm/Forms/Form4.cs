using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AdminForm.Controllers;

namespace AdminForm.Forms
{
    public partial class Form4 : Form
    {
        string fio;
        string phone;
        DataGridView dg;
        public Form4(DataGridView dg)
        {
            InitializeComponent();
            this.dg = dg;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                fio = textBox1.Text;
                phone = textBox2.Text;
                if(fio != "" && phone != "")
                {
                    MasterController.addMaster(fio, phone);
                    dg.DataSource = MasterController.getMasters();
                }
                else
                {
                    MessageBox.Show("Поля не должны быть пустыми!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
