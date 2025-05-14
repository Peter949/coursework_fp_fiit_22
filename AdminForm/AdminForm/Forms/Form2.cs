using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AdminForm.Controllers;

namespace AdminForm.Forms
{
    public partial class Form2 : Form
    {
        string mem = "";
        public byte[] ImageData { get; set; }
        public Form2()
        {
            InitializeComponent();
        }

        public Form2(string text)
        {
            InitializeComponent();
            mem = text;
            this.Text = text;
            this.StartPosition = FormStartPosition.CenterScreen;
            switch (text)
            {
                case "Клиенты":
                    dataGridView1.DataSource = ClientController.getClients();
                    break;
                case "Прически":
                    //dataGridView1.DataSource = HairController.getHairs();
                    dataGridView1.AutoGenerateColumns = false;
                    dataGridView1.DataSource = HairController.getHairs();
                    dataGridView1.Columns.Add(new DataGridViewTextBoxColumn()
                    {
                        Name = "Id",
                        HeaderText = "Id",
                        DataPropertyName = "Id"
                    });
                    dataGridView1.Columns.Add(new DataGridViewTextBoxColumn()
                    {
                        HeaderText = "Name",
                        DataPropertyName = "Name"
                    });
                    dataGridView1.Columns.Add(new DataGridViewTextBoxColumn()
                    {
                        HeaderText = "Price",
                        DataPropertyName = "Price"
                    });
                    var imageColumn = new DataGridViewImageColumn()
                    {
                        HeaderText = "Image",
                        DataPropertyName = "img",
                        ImageLayout = DataGridViewImageCellLayout.Zoom
                    };
                    dataGridView1.Columns.Add(imageColumn);
                    dataGridView1.RowTemplate.Height = 100;
                    break;
                case "Парикмахеры":
                    dataGridView1.DataSource = MasterController.getMasters();
                    break;
                case "Записи":
                    dataGridView1.DataSource = NotesController.getNotes();
                    break;
                default:
                    break;
            }
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            switch (mem)
            {
                case "Парикмахеры":
                    try
                    {
                        int id;
                        id = Convert.ToInt32(dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells["Id"].Value);
                        MasterController.deleteMaster(id);
                        dataGridView1.DataSource = MasterController.getMasters();
                    }
                    catch(Exception er)
                    {
                        MessageBox.Show(er.Message);
                    }
                    break;
                case "Прически":
                    try
                    {
                        int id;
                        id = Convert.ToInt32(dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells["Id"].Value);
                        HairController.deleteHair(id);
                        dataGridView1.DataSource = HairController.getHairs();
                    }
                    catch(Exception er)
                    {
                        MessageBox.Show(er.Message);   
                    }
                    break;
                case "Клиенты":
                    try
                    {
                        int id;
                        id = Convert.ToInt32(dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells["Id"].Value);
                        ClientController.deleteClient(id);
                        dataGridView1.DataSource = ClientController.getClients();
                    }
                    catch (Exception er)
                    {
                        MessageBox.Show(er.Message);
                    }
                    break;
                default:
                    break;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            switch(mem)
            {
                case "Парикмахеры":
                    Form4 form4 = new Form4(dataGridView1);
                    form4.ShowDialog();
                    break;
                case "Прически":
                    Form3 form3 = new Form3(dataGridView1);
                    form3.ShowDialog();
                    break;
                default:
                    break;
            }
        }
    }
}
