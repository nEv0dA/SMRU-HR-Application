using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace HR
{
    public partial class Form3 : Form
    {
        SqlConnection con;
        //SqlCommand cmd;
        //SqlDataReader dr;
        ConnectionTestDb db = new ConnectionTestDb();
        Form1 f3;

        int page = 0;

        public Form3(Form1 f3)
        {
            InitializeComponent();

            con = new SqlConnection(db.GetConnection());
            this.f3 = f3;

            ShowEmployee(page);

            //con.Open();
            //SqlCommand cmd = new SqlCommand();
            //cmd.Connection = con;
            //cmd.CommandText = "SELECT * FROM Test";

            //DataTable table = new DataTable();
            //SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            //adapter.Fill(table);

            //lbEmployeeID.Text = table.Rows[0]["employee_id"].ToString();
            //lbName.Text = table.Rows[0]["name"].ToString();
            //lbAge.Text = table.Rows[0]["age"].ToString();
            //lbAddress.Text = table.Rows[0]["address"].ToString();

            //cmd.ExecuteNonQuery();
            //cmd.Dispose();
            //con.Close();
        }

        void ShowEmployee(int counter)
        {
            try
            {
                if (con.State != ConnectionState.Open)
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = con;
                    cmd.CommandText = "SELECT * FROM Test";

                    DataTable table = new DataTable();
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    adapter.Fill(table);

                    lbEmployeeID.Text = table.Rows[counter]["employee_id"].ToString();
                    lbName.Text = table.Rows[counter]["name"].ToString();
                    lbAge.Text = table.Rows[counter]["age"].ToString();
                    lbAddress.Text = table.Rows[counter]["address"].ToString();

                    cmd.ExecuteNonQuery();
                    cmd.Dispose();
                    con.Close();
                }
            }
            catch (Exception error)
            {
                MessageBox.Show("Error: " + error);
            }

        }

        private void btnFirst_Click(object sender, EventArgs e)
        {
            page = 0;
            ShowEmployee(page);
        }

        int rows;
        private void btnLast_Click(object sender, EventArgs e)
        {
            try
            {
                if(con.State == ConnectionState.Closed)
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = con;
                    cmd.CommandText = "SELECT * FROM Test";

                    DataTable table = new DataTable();
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    adapter.Fill(table);

                    lbEmployeeID.Text = table.Rows[table.Rows.Count-1]["employee_id"].ToString();
                    lbName.Text = table.Rows[table.Rows.Count - 1]["name"].ToString();
                    lbAge.Text = table.Rows[table.Rows.Count - 1]["age"].ToString();
                    lbAddress.Text = table.Rows[table.Rows.Count - 1]["address"].ToString();
                    rows = int.Parse(table.Rows[table.Rows.Count - 1]["employee_id"].ToString());

                    cmd.ExecuteNonQuery();
                    cmd.Dispose();
                    con.Close();
                }
            }catch(Exception error)
            {
                MessageBox.Show("Error: " + error);
            }
        }

        
        private void btnPrevious_Click(object sender, EventArgs e)
        {
            if (page > 0)
            {
                page--;
                ShowEmployee(page);
            }
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            //page++;

            //if (page >= table.Rows.Count())
            //{

            //}

            if (page < rows - 1)
            {
                page++;
                ShowEmployee(page);
            }
        }

        //private void Form3_Load(object sender, EventArgs e)
        //{
        //    ShowEmployee();
        //}
    }
}
