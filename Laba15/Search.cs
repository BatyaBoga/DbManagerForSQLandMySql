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
    public partial class Search : Form
    {
        dbManager db;
        public Search()
        {
            InitializeComponent();
            db= new dbManager();
        }

        private void checkedListBox1_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (e.NewValue == CheckState.Checked && checkedListBox1.CheckedItems.Count > 0)
            {
                checkedListBox1.ItemCheck -= checkedListBox1_ItemCheck;
                checkedListBox1.SetItemChecked(checkedListBox1.CheckedIndices[0], false);
                checkedListBox1.ItemCheck += checkedListBox1_ItemCheck;
            }
        }

        private void Search_Load(object sender, EventArgs e)
        {
           
        }

        private void button2_Click(object sender, EventArgs e)
        {
            List<string> values = new List<string>() { "Employee.pip", "Contracts.code_", "Statement.summ" };
            List<string> from = new List<string>() { "Employee", "Statement", "Contracts" };
            List<string> where;

            switch (checkedListBox1.SelectedIndex)
            {
                case 0:
                       where = new List<string>() { "Contracts.emploee = Employee.id",
                      "Statement.cod = Contracts.code_", "summ = (select min(summ) from Statement)" };  
                       break;
                case 1:
                     where = new List<string>() { "Contracts.emploee = Employee.id",
                     "Statement.cod = Contracts.code_", "summ = (select max(summ) from Statement)" };
                     break;
                    default: where = new List<string>() { "" }; break;

            }

            dataGridView1.DataSource = db.Select(values, from, where);


        }

        private void button1_Click(object sender, EventArgs e)
        {
            List<string> values = new List<string>() { "Employee.pip", "Contracts.code_", "Statement.summ" };
            List<string> from = new List<string>() { "Employee", "Statement", "Contracts", "Job" };
            List<string> where = new List<string>() { "Contracts.emploee = Employee.id",
                                                     "Statement.cod = Contracts.code_",
                                                      "Contracts.job = Job.cod",
                                                      $"Job.job_title ='{textBox1.Text}'"};
            dataGridView1.DataSource = db.Select(values, from, where);


        }

        private void button3_Click(object sender, EventArgs e)
        {
            List<string> values = new List<string>() { "Employee.pip", "Contracts.code_", "Statement.month_", "Statement.year_" };
            List<string> from = new List<string>() { "Employee", "Statement", "Contracts"};
            List<string> where = new List<string>() { "Contracts.emploee = Employee.id",
                                                     "Statement.cod = Contracts.code_",
                                                      $"Statement.year_ >= {textBox2.Text}",
                                                      $" Statement.month_ >= {textBox3.Text}",
                                                      $"Statement.year_ <= {textBox5.Text}",
                                                      $" Statement.month_ <= {textBox4.Text}"};
            dataGridView1.DataSource = db.Select(values, from, where);

        }

        private void button4_Click(object sender, EventArgs e)
        {
            List<string> values = new List<string>() { "Employee.pip", "Contracts.code_", "Statement.summ" };
            List<string> from = new List<string>() { "Employee", "Statement", "Contracts"};
            List<string> where = new List<string>() { "Contracts.emploee = Employee.id",
                                                     "Statement.cod = Contracts.code_",
                                                      $"summ >= {textBox6.Text}"};
            dataGridView1.DataSource = db.Select(values, from, where);
        }
    }
}
