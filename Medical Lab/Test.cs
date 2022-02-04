using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Medical_Lab
{
    public partial class Tests : Form
    {
        public Tests()
        {
            InitializeComponent();
            ShowTest();
        }

        SqlConnection conn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Dell\Documents\MedicalLabDB.mdf;Integrated Security=True;Connect Timeout=30");
        private void ShowTest()
        {
            conn.Open();
            string query = "Select * from Test";
            SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
            SqlCommandBuilder builder = new SqlCommandBuilder(adapter);
            var ds = new DataSet();
            adapter.Fill(ds);
            TestDGV.DataSource = ds.Tables[0];
            conn.Close();
        }

        private void Reset()
        {
            TestCostTb.Text = string.Empty;
            TestNameTb.Text = string.Empty;
            key = 0;

        }
        private void SaveBtn_Click(object sender, EventArgs e)
        {
            if (TestNameTb.Text == "" || TestCostTb.Text == "")
            {
                MessageBox.Show("Missing Information");
            }
            else
            {
                try
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("insert into Test(name,cost) values (@TN,@TC)", conn);
                    cmd.Parameters.AddWithValue("@TN", TestNameTb.Text);
                    cmd.Parameters.AddWithValue("@TC", TestCostTb.Text);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Saved");
                    conn.Close();
                    ShowTest();
                    Reset();

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        int key = 0;
        private void EditBtn_Click(object sender, EventArgs e)
        {
            if (TestNameTb.Text == "" || TestCostTb.Text == "")
            {
                MessageBox.Show("Missing Information");
            }
            else
            {
                try
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("Update Test Set name=@TN,cost=@TC where code=@TID", conn);
                    cmd.Parameters.AddWithValue("@TN", TestNameTb.Text);
                    cmd.Parameters.AddWithValue("@TC", TestCostTb.Text);
                    cmd.Parameters.AddWithValue("@TID", key);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Updated");
                    conn.Close();
                    ShowTest();
                    Reset();

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void DeleteBtn_Click(object sender, EventArgs e)
        {
            if (key == 0)
            {
                MessageBox.Show("Select a Test");
            }
            else
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("Delete from Test Where code=@TID", conn);
                cmd.Parameters.AddWithValue("@TID", key);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Deleted");
                conn.Close();
                ShowTest();
                Reset();
            }
        }

        private void TestDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            TestNameTb.Text = TestDGV.SelectedRows[0].Cells[1].Value.ToString();
            TestCostTb.Text = TestDGV.SelectedRows[0].Cells[2].Value.ToString();
            if (TestNameTb.Text == "")
            {
                key = 0;
            }
            else
            {
                key = Convert.ToInt32(TestDGV.SelectedRows[0].Cells[0].Value.ToString());
            }
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            Results results = new Results();
            results.Show();
            this.Hide();
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            Patients Obj = new Patients();
            Obj.Show();
            this.Hide();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Tests Obj = new Tests();
            Obj.Show();
            this.Hide();
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            Login Obj = new Login();
            Obj.Show();
            this.Hide();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Laboratorians Obj = new Laboratorians();
            Obj.Show();
            this.Hide();
        }
    }
}
