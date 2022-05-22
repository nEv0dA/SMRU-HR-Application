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
    public partial class Form1 : Form
    {
        SqlConnection con;
        SqlCommand cmd;
        SqlDataReader dr;
        ConnectionTestDb db = new ConnectionTestDb();

        public Form1()
        {
            InitializeComponent();

            con = new SqlConnection(db.GetConnection());
            LoadRecords();
        }

        public void LoadRecords()
        {
            dgvEmployees.Rows.Clear();
            int i = 0;
            con.Open();
            cmd = new SqlCommand("SELECT * FROM Test", con);
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                i++;
                dgvEmployees.Rows.Add(i, dr["employee_id"].ToString(), dr["name"].ToString(), dr["age"].ToString(), dr["address"].ToString());
            }
            dr.Close();
            con.Close();
        }

        private void btnAddNew_Click(object sender, EventArgs e)
        {
            Form2 f = new Form2(this);
            f.btnSave.Enabled = true;
            f.btnUpdate.Enabled = false;
            f.ShowDialog();
        }

        private void dgvEmployees_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            string colName = dgvEmployees.Columns[e.ColumnIndex].Name;

            if (colName == "colEdit")
            {
                Form2 f = new Form2(this);
                f.txtEmployeeID.Text = dgvEmployees.Rows[e.RowIndex].Cells[1].Value.ToString();
                f.txtName.Text = dgvEmployees.Rows[e.RowIndex].Cells[2].Value.ToString();
                f.txtAge.Text = dgvEmployees.Rows[e.RowIndex].Cells[3].Value.ToString();
                f.txtAddress.Text = dgvEmployees.Rows[e.RowIndex].Cells[4].Value.ToString();
                f.txtEmployeeID.Enabled = false;
                f.btnSave.Enabled = false;
                f.btnUpdate.Enabled = true;
                f.ShowDialog();
            }
            else if (colName == "colDelete")
            {
                if (MessageBox.Show("WANT TO DELETE THIS RECORD?", "MESSAGE", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    con.Open();
                    cmd = new SqlCommand("DELETE FROM Test WHERE employee_id = '" + dgvEmployees.Rows[e.RowIndex].Cells[1].Value.ToString() + "'", con);
                    cmd.ExecuteNonQuery();
                    con.Close();
                    MessageBox.Show("EMPLOYEE RECORD HAS BEEN SUCCESSFULLY DELETED.", "MESSAGE", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadRecords();
                }
            }
        }

        private void btnDetails_Click(object sender, EventArgs e)
        {
            Form3 f3 = new Form3(this);
            f3.ShowDialog();
        }
    }
}
