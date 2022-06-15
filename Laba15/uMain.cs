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
    public partial class uMain : Form
    {
        dbManager db;
        List<string> command;
        private string tablename;
        private string mytable;
        public string[] f = { "code_", "emploee", "job", "d_laying", "rupture" };
        public string cound;
        ConType conType;
        public uMain()
        {
            InitializeComponent();
            db = new dbManager();
            tablename = "";
            command = new List<string>();

        }

        

        private void employeeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Employee emp = new Employee(conType);
            emp.ShowDialog();

        }

        private void jobToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Job job = new Job(conType);
            job.ShowDialog();
        }

        private void statementToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Statement statement = new Statement(conType);
            statement.ShowDialog();
        }

        private void timesheetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Timesheet timesheet = new Timesheet(conType);
            timesheet.ShowDialog();
        }

        private void uMain_Load(object sender, EventArgs e)
        {


            dataGridView1.DataSource = db.Select("Select * from Contracts");

            textBox4.Text = dataGridView1.Rows[0].Cells[0].Value?.ToString();
            textBox5.Text = dataGridView1.Rows[0].Cells[1].Value?.ToString();
            textBox6.Text = dataGridView1.Rows[0].Cells[2].Value?.ToString();
            textBox7.Text = dataGridView1.Rows[0].Cells[3].Value?.ToString();
            textBox8.Text = dataGridView1.Rows[0].Cells[4].Value?.ToString();
            cound = $"code_ = {textBox4.Text}";
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {

            textBox4.Text = dataGridView1.Rows[e.RowIndex].Cells[0].Value?.ToString();
            textBox5.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value?.ToString();
            textBox6.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value?.ToString();
            textBox7.Text = dataGridView1.Rows[e.RowIndex].Cells[3].Value?.ToString();
            textBox8.Text = dataGridView1.Rows[e.RowIndex].Cells[4].Value?.ToString();
            cound = $"code_ = {textBox4.Text}";
        }

        private void button19_Click(object sender, EventArgs e)
        {
            string[] v = { textBox4.Text, textBox5.Text,  "'" + textBox6.Text + "'", "'" + textBox7.Text + "'", "'" + textBox8.Text + "'" };
            db.Insert(this.Text, f, v);
            dataGridView1.DataSource = db.Select("Select * from Contracts");
        }

        private void button18_Click(object sender, EventArgs e)
        {
            db.Delete(this.Text, cound);
            dataGridView1.DataSource = db.Select("Select * from Contracts");
        }

        private void button16_Click(object sender, EventArgs e)
        {

            string[] v = { textBox4.Text, textBox5.Text, "'" + textBox6.Text + "'", "'" + textBox7.Text + "'", "'" + textBox8.Text + "'" };
            db.Update(this.Text, cound, f, v);
            dataGridView1.DataSource = db.Select("Select * from Contracts");

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Search search = new Search();
            search.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            conType = ConType.Remote;
            db.Connect(conType);
            try
            {
                dataGridView1.DataSource = db.Select("Select * from Contracts");
                MessageBox.Show("Connecting Remote DB");
            }
            catch (Exception ex) { }

            



        }

        private void button3_Click(object sender, EventArgs e)
        {
            conType = ConType.Local;
            db.Connect(conType);
            dataGridView1.DataSource = db.Select("Select * from Contracts");
            MessageBox.Show("Connecting Local DB");
        }
        private void create(object sender)
        {
            db.execute((sender as Button).Text, mytable);
        }
        private void button13_Click(object sender, EventArgs e)
        {
            mytable = "id int not null primary key,pip varchar(50) not null,contact char(12), sex char(1) not null, birthday char(10) not null";
            create(sender);
        }

        private void button12_Click(object sender, EventArgs e)
        {
            mytable = "cod char(5) not null primary key, job_title varchar(40) not null,pay decimal(6,2) not null, check(pay>10)";
            create(sender);
        }

        private void button11_Click(object sender, EventArgs e)
        {
            mytable = "cod int not null, nomer int not null, primary key(cod, nomer),month_ int not null, year_ int not null, umm decimal(6,2) not null,check(month_ >0 and month_ <= 12 and year_ > 1900)";
            create(sender);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            mytable = "date_ char(10) not null, contract int not null, primary key(date_, contract),hours int not null, check(hours <= 744)";
            create(sender);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            mytable = "code_ int not null primary key, emploee int not null, job char(5) not null,d_laying char(10) not null, rupture char(10)";
            create(sender);
        }

        private void button10_Click(object sender, EventArgs e)
        {
            db.Drop_table((sender as Button).Text);
        }

        private void button14_Click(object sender, EventArgs e)
        {

            
           
        }
    }
}
