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
    public partial class Statement : Form
    {
        dbManager db;
        public string[] f = { "cod", "nomer", "month_", "year_", "summ" };
        string cound;
        public Statement(ConType conType)
        {
            InitializeComponent();
            db = new dbManager(conType);
            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
        }

        private void Statement_Load(object sender, EventArgs e)
        {
            dataGridView1.DataSource = db.Select($"Select * from {this.Text}");

            textBox1.Text = dataGridView1.Rows[0].Cells[0].Value?.ToString();
            textBox2.Text = dataGridView1.Rows[0].Cells[1].Value?.ToString();
            textBox3.Text = dataGridView1.Rows[0].Cells[2].Value?.ToString();
            textBox4.Text = dataGridView1.Rows[0].Cells[3].Value?.ToString();
            textBox5.Text = dataGridView1.Rows[0].Cells[4].Value?.ToString();
            cound = $"cod = {textBox1.Text}";
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            textBox1.Text = dataGridView1.Rows[e.RowIndex].Cells[0].Value?.ToString();
            textBox2.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value?.ToString();
            textBox3.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value?.ToString();
            textBox4.Text = dataGridView1.Rows[e.RowIndex].Cells[3].Value?.ToString();
            textBox5.Text = dataGridView1.Rows[e.RowIndex].Cells[4].Value?.ToString();
            cound = $"cod = {textBox1.Text}";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string[] v = { textBox1.Text, textBox2.Text, textBox3.Text, textBox4.Text, textBox5.Text};
            db.Insert(this.Text, f, v);
            dataGridView1.DataSource = db.Select($"Select * from {this.Text}");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            db.Delete(this.Text, $"cod = {textBox1.Text}");
            dataGridView1.DataSource = db.Select($"Select * from {this.Text}");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string[] v = { textBox1.Text, textBox2.Text, textBox3.Text, textBox4.Text, textBox5.Text };
            db.Update(this.Text, cound, f, v);
            dataGridView1.DataSource = db.Select($"Select * from {this.Text}");
        }
    }
}
