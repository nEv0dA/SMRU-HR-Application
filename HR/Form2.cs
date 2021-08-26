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
    public partial class Form2 : Form
    {
        SqlConnection con;
        SqlCommand cmd;
        ConnectionTestDb db = new ConnectionTestDb();
        Form1 f;
         
        public Form2(Form1 f)
        {
            InitializeComponent();

            con = new SqlConnection(db.GetConnection());
            this.f = f;
        }

        private void Clear()
        {
            txtEmployeeID.Clear();
            txtName.Clear();
            txtAge.Clear();
            txtAddress.Clear();
            btnSave.Enabled = true;
            btnUpdate.Enabled = false;
            txtEmployeeID.Focus();
        }

        private void bntSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtEmployeeID.Text == "" || txtName.Text == "" || txtAge.Text == "" || txtAddress.Text == "")
                {
                    MessageBox.Show("REQUIRED MISSING FIELD!", "MESSAGE", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                con.Open();
                cmd = new SqlCommand("INSERT INTO Test (employee_id, name, age, address) VALUES (@employee_id, @name, @age, @address)", con);
                cmd.Parameters.AddWithValue("@employee_id", txtEmployeeID.Text);
                cmd.Parameters.AddWithValue("@name", txtName.Text);
                cmd.Parameters.AddWithValue("@age", txtAge.Text);
                cmd.Parameters.AddWithValue("@address", txtAddress.Text);
                cmd.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("NEW EMPLOYEE HAS BEEN SUCCESSFULLY ADDED.", "MESSAGE", MessageBoxButtons.OK, MessageBoxIcon.Information);
                f.LoadRecords();
                Clear();
            }
            catch (Exception ex)
            {
                con.Close();
                MessageBox.Show(ex.Message, "WARNING", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtEmployeeID.Text == "" || txtName.Text == "" || txtAge.Text == "" || txtAddress.Text == "")
                {
                    MessageBox.Show("REQUIRED MISSING FIELD!", "MESSAGE", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                if (MessageBox.Show("WANT TO UPDATE THIS RECORD?", "MESSAGE", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    con.Open();
                    cmd = new SqlCommand("UPDATE Test SET name = @name, age = @age, address = @address WHERE employee_id = @employee_id", con);
                    cmd.Parameters.AddWithValue("@employee_id", txtEmployeeID.Text);
                    cmd.Parameters.AddWithValue("@name", txtName.Text);
                    cmd.Parameters.AddWithValue("@age", txtAge.Text);
                    cmd.Parameters.AddWithValue("@address", txtAddress.Text);
                    cmd.ExecuteNonQuery();
                    con.Close();
                    MessageBox.Show("NEW EMPLOYEE HAS BEEN SUCCESSFULLY UPDATED.", "MESSAGE", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    f.LoadRecords();
                    Clear();
                }
            }
            catch (Exception ex)
            {
                con.Close();
                MessageBox.Show(ex.Message, "WARNING", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            this.Dispose();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void btnCancel_MouseHover(object sender, EventArgs e)
        {

        }

        //private void X_MouseHover(object sender, EventArgs e)
        //{
        //    X.BackColor = Color.Red;
        //}

        //private void X_MouseLeave(object sender, EventArgs e)
        //{
        //    X.BackColor = Color.Transparent;
        //}

        //private void btnClose_MouseHover(object sender, EventArgs e)
        //{
        //    btnClose.BackColor = Color.Red;
        //}

        //private void btnClose_MouseLeave(object sender, EventArgs e)
        //{
        //    btnClose.BackColor = Color.Transparent;
        //}
    }
}
