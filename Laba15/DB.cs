using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using MySql.Data.MySqlClient;
using System.Windows.Forms;
using System.Data;




namespace Db
{
    public enum ConType
    {
        Local,
        Remote
    }

    public class fieldDef
    {
        public string name;
        public string type;
        public string constraint;
    };
    public class dbManager
    {

        private dynamic con;
        private dynamic cmd;
        private dynamic adapter;
        private ConType conType;


        public dbManager()
        {
            Connect(ConType.Local);

        }
        public dbManager(ConType typs)
        {
            Connect(typs);

        }


        public void Connect(ConType type)
        {
            try
            {
                switch (type)
                {
                    case ConType.Local:
                        {
                            con = new SqlConnection(@"Your connection string");
                            cmd = new SqlCommand();
                        }
                        break;
                    case ConType.Remote:
                        {
                            con = new MySqlConnection(@"Your connection string");
                            cmd = new MySqlCommand();
                        }
                        break;

                }

                cmd.Connection = con;
                conType = type;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        ~dbManager()
        {
            con.Close();
        }

        public void execute(string tablename,string command)
        {
            try
            {
                con.Open();
                cmd.CommandText = $"create table {tablename}( {command} )";
                cmd.ExecuteNonQuery();
                MessageBox.Show("Successful create");     
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                con.Close();
            }
        }
        public void execute(string tablename, List<string> command)
        {
            try
            {
                con.Open();
                cmd.CommandText = $"create table {tablename}( ";
                foreach (string c in command)
                {
                    cmd.CommandText += c + ", ";
                }
                cmd.CommandText = cmd.CommandText.Remove(cmd.CommandText.Length - 2) + " )";
                cmd.ExecuteNonQuery();
                MessageBox.Show("Successful create");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                con.Close();
            }
        }
        public void execute(string tablename, List<fieldDef> command)
        {
            try
            {
                con.Open();
                cmd.CommandText = $"create table {tablename}( ";
                foreach (fieldDef c in command)
                {
                    cmd.CommandText += $"{c.name} {c.type} {c.constraint}, ";
                }
                cmd.CommandText = cmd.CommandText.Remove(cmd.CommandText.Length - 2) + " )";
                cmd.ExecuteNonQuery();
                MessageBox.Show("Successful create");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                con.Close();
            }
        }

        public void Drop_table(string tablename)
        {
            try
            {
                con.Open();
                cmd.CommandText = $"drop table {tablename};";
                cmd.ExecuteNonQuery();
                MessageBox.Show("Successful drop");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                con.Close();
            }

        }

        public List<List<object>> SelectAll(string tablename)
        {
            try
            {
                con.Open();
                var res = new List<List<object>>(); 
                cmd.CommandText = $"select * from "+ tablename;
                dynamic r = cmd.ExecuteReader();
                while (r.Read())
                {
                    List<object> row = new List<object>();
                    for (int i = 0; i < r.FieldCount; i++)
                    {
                        row.Add(r[i]);
                    }
                    res.Add(row);

                }
                r.Close();
                return res;
               

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
            finally
            {   
                con.Close();
               
            }
        }
        public void fillGrid(DataGridView dg, SqlDataReader r)
        {
            dg.Rows.Clear();

            while (r.Read())
            {
                string[] s = new string[r.FieldCount];
                for (int i = 0; i < r.FieldCount; i++)
                {
                   s[i]=r[i].ToString();    
                }
                dg.Rows.Add(s);

            }
            
        }

        public void SelectAll(string tablename, DataGridView dg)
        {
            try
            {
                con.Open();
                cmd.CommandText = $"select * from " + tablename;
               dynamic r = cmd.ExecuteReader();

                fillGrid(dg, r);
                r.Close();
               
                

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {

                con.Close();
            }
        }

        private void GetAdapter(string query)
        {
            if (conType == 0)
            {
                adapter = new SqlDataAdapter(query, con);
            }
            else
            {
                adapter = new MySqlDataAdapter(query, con);
            }

        }
        public DataTable Select(String query)
        {
            try
            {
                con.Open();
                GetAdapter(query);
                DataSet ds = new DataSet(); 
                adapter.Fill(ds);    
                con.Close();    
                return ds.Tables[0];           
            
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
        }

 

        public DataTable Select(List<string> values, List<string> from, List<string> where)
        {
                try
                {


                    con.Open();

                    var query = new StringBuilder("select ");
                
                    foreach (var item in values)
                    {
                    query.Append(item + ", "); 
                    }

                    query.Remove(query.Length - 2, 2);
                    query.Append(" from ");
                    

                    foreach (var item in from)
                    {
                    query.Append(item + ", ");
                    }
                    query.Remove(query.Length - 2, 2);
                    query.Append(" where ");

                    foreach (var item in where)
                    {
                    query.Append(item + " and ");
                    }
                    query.Remove(query.Length - 5, 5);

                    

                GetAdapter(query.ToString());
                    DataSet ds = new DataSet();
                    adapter.Fill(ds);
                    con.Close();
                    return ds.Tables[0];

                }
                catch (Exception ex)
                {

                    throw ex;
                }
        }




        public void ExecSQL(string query)
        {
            try
            {
                con.Open();
                cmd.CommandText = query;
                cmd.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {

                con.Close();
            }

        }
        public void Insert(string tablename, String[] fields, String[] values)
        {
            try
            {
                con.Open();
                cmd.CommandText = $"insert into {tablename}({String.Join(",", fields)}) values ({String.Join(",", values)})";
                cmd.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {

                con.Close();
            }

        }

        public void Delete(string tablename, string cound)
        {
            try
            {

                if (cound!="")
                {
                    cmd.CommandText = $"delete from {tablename} where {cound}";
                }
                else
                {
                    cmd.CommandText = $"delete from {tablename}";
                }
                con.Open();
                cmd.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {

                con.Close();
            }

        }
        public void Update(string tablename, string cound, String[] fields, String[] values)
        {
            try
            {

                if (cound != "")
                {
                    cmd.CommandText = $"update {tablename} set";
                    for (int i = 0; i < fields.Length; i++)
                    {
                        cmd.CommandText += $" {fields[i]} = {values[i]},";
                    }
                    cmd.CommandText = cmd.CommandText.Remove(cmd.CommandText.Length - 1)+ $" where {cound}";

                }
                else
                {
                    cmd.CommandText = $"update {tablename} set";
                    for (int i = 0; i < fields.Length; i++)
                    {

                        cmd.CommandText += $" {fields[i]} = {values[i]}";
                    }
                }
                con.Open();
                cmd.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {

                con.Close();
            }

        }
        
        public void dismiss(string id)
        {


            dynamic trans = null;

            try
            {
                con.Open();
                trans = con.BeginTransaction();
               
                cmd.Transaction = trans;

               
                cmd.CommandText = "insert into EmployeeArchive(id, pip, contact, sex, birthday)" +
                    $" select  id, pip, contact, sex, birthday from Employee where Employee.id = {id}";

                cmd.ExecuteNonQuery();
                cmd.CommandText =$"update EmployeeArchive  set rupture = '{DateTime.Now.ToString("MM.dd.yyyy")}' where id = {id}" ;

                cmd.ExecuteNonQuery();
                cmd.CommandText = $"delete from Contracts where emploee = {id}";

                cmd.ExecuteNonQuery();
                cmd.CommandText = $"delete from Employee where id = {id}";

                cmd.ExecuteNonQuery();

                trans.Commit();
            }
            catch (Exception ex)
            {
                trans.Rollback();
                MessageBox.Show(ex.Message);
            }
            finally
            {

                con.Close();
            }
        } 

    }
}
