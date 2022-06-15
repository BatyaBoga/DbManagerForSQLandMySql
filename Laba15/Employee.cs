using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Db;

namespace Laba15
{
    public partial class Employee : Form
    {
        dbManager db;
        public string[] f ={ "id","pip", "contact", "sex", "birthday"};
        public string cound;

        public Employee(ConType type)
        {
            InitializeComponent();
            db = new dbManager(type);
            


        }

        private void Employee_Load(object sender, EventArgs e)
        {
            dataGridView1.DataSource = db.Select($"Select * from {this.Text}");
            textBox1.Text = dataGridView1.Rows[0].Cells[0].Value?.ToString();
            textBox2.Text = dataGridView1.Rows[0].Cells[1].Value?.ToString();
            textBox3.Text = dataGridView1.Rows[0].Cells[2].Value?.ToString();
            textBox4.Text = dataGridView1.Rows[0].Cells[3].Value?.ToString();
            textBox5.Text = dataGridView1.Rows[0].Cells[4].Value?.ToString();
            cound = $"id = {textBox1.Text}";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string[] v = {textBox1.Text,"'" + textBox2.Text+ "'", "'" + textBox3.Text + "'", "'" + textBox4.Text + "'", "'" + textBox5.Text + "'" };
            db.Insert(this.Text, f, v);
            dataGridView1.DataSource = db.Select($"Select * from {this.Text}");

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            textBox1.Text = dataGridView1.Rows[e.RowIndex].Cells[0].Value?.ToString();
            textBox2.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value?.ToString();
            textBox3.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value?.ToString();
            textBox4.Text = dataGridView1.Rows[e.RowIndex].Cells[3].Value?.ToString();
            textBox5.Text = dataGridView1.Rows[e.RowIndex].Cells[4].Value?.ToString();
            cound = $"id = {textBox1.Text}";

        }

        private void button2_Click(object sender, EventArgs e)
        {
            db.Delete(this.Text, cound);
            dataGridView1.DataSource = db.Select($"Select * from {this.Text}");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string[] v = { textBox1.Text, "'" + textBox2.Text + "'", "'" + textBox3.Text + "'", "'" + textBox4.Text + "'", "'" + textBox5.Text + "'" };
            db.Update(this.Text, cound, f, v);
            dataGridView1.DataSource = db.Select($"Select * from {this.Text}");
        }

        private void button4_Click(object sender, EventArgs e)
        {
            db.dismiss(textBox1.Text);
            dataGridView1.DataSource = db.Select($"Select * from {this.Text}");
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (!checkBox1.Checked)
            {
                dataGridView1.DataSource = db.Select($"Select * from {this.Text}");
            }
            else
            {
                dataGridView1.DataSource = db.Select("Select * from EmployeeArchive");
            }
        }
    }
}
