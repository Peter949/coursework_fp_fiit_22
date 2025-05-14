using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AdminForm.Controllers;

namespace AdminForm.Forms
{
    public partial class Form3 : Form
    {
        string name;
        int price;
        byte[] img_bytes;
        DataGridView dg;
        public Form3(DataGridView dg)
        {
            InitializeComponent();
            this.dg = dg;
        }

        private void Form3_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void button2_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "Image Files(*.JPG)|*.JPG";
            if(openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    name = textBox1.Text;
                    price = Convert.ToInt32(textBox2.Text);
                    if(name != "" && textBox2.Text != "")
                    {
                        string filePath = openFileDialog1.FileName;
                        img_bytes = File.ReadAllBytes(filePath);
                        HairController.addHair(name, price, img_bytes);
                        dg.DataSource = HairController.getHairs();
                    }
                    else
                    {
                        MessageBox.Show("Поля не должны быть пустыми!");
                    }
                }
                catch(Exception er)
                {
                    MessageBox.Show(er.Message);
                }
            }
        }
    }
}
