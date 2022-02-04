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
    public partial class Results : Form
    {
        public Results()
        {
            InitializeComponent();
            ShowResult();
            GetPatient();
            GetLab();
            GetTest();
            DateLb.Text = DateTime.Today.ToString();
        }

        SqlConnection conn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Dell\Documents\MedicalLabDB.mdf;Integrated Security=True;Connect Timeout=30");
        private void ShowResult()
        {
            conn.Open();
            string query = "Select * from Result";
            SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
            SqlCommandBuilder builder = new SqlCommandBuilder(adapter);
            var ds = new DataSet();
            adapter.Fill(ds);
            ResultDGV.DataSource = ds.Tables[0];
            conn.Close();
        }

        private void GetPatient()
        {
            conn.Open();
            SqlCommand cmd = new SqlCommand("Select name from Patient", conn);
            SqlDataReader dr = cmd.ExecuteReader();
            DataTable dt = new DataTable(); 
            dt.Columns.Add("name", typeof(string));
            dt.Load(dr);
            PatCb.ValueMember = "name";
            PatCb.DataSource = dt;
            conn.Close();
        }

        private void GetLab()
        {
            conn.Open();
            SqlCommand cmd = new SqlCommand("Select name from Lab", conn);
            SqlDataReader dr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Columns.Add("name", typeof(string));
            dt.Load(dr);
            LabCb.ValueMember = "name";
            LabCb.DataSource = dt;
            conn.Close();
        }

        private void GetTest()
        {
            conn.Open();
            SqlCommand cmd = new SqlCommand("Select name from Test", conn);
            SqlDataReader dr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Columns.Add("name", typeof(string));
            dt.Load(dr);
            TestCb.ValueMember = "name";
            TestCb.DataSource = dt;
            conn.Close();
        }
        int cost = 0;
        private void GetTestCost()
        {
            conn.Open ();
            string query = "Select * from Test Where name=" + TestCb.SelectedValue.ToString() + "";
            SqlCommand cmd = new SqlCommand (query, conn);
            DataTable dt = new DataTable();
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            sda.Fill(dt);
            foreach(DataRow dr in dt.Rows)
            {
                cost = Convert.ToInt32(dr["cost"].ToString());
            }
            conn.Close();
        }

        private void SaveBtn_Click(object sender, EventArgs e)
        {
            if (PatCb.SelectedIndex == -1 || LabCb.SelectedIndex == -1 || TestCb.SelectedIndex == -1 || ResultCb.SelectedIndex == -1 || TestTb.Text == ""|| CostTb.Text == "")
            {
                MessageBox.Show("Missing Information");
            }
            else
            {
                try
                {
                    string date = DateTime.Today.Date.ToString();
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("insert into Result(patient_name,lab_name,test_done,test_cost,result_date) values(@PN,@LN,@TD,@TC,@RD)", conn);
                    cmd.Parameters.AddWithValue("@PN", PatCb.SelectedValue.ToString());
                    cmd.Parameters.AddWithValue("@LN", LabCb.SelectedValue.ToString());
                    cmd.Parameters.AddWithValue("@TC", CostTb.Text);
                    cmd.Parameters.AddWithValue("@TD", TestTb.Text);
                    cmd.Parameters.AddWithValue("@RD", DateTime.Now);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Saved");
                    conn.Close();
                    ShowResult();
                    //Reset();

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        string testContent = "";
        int totalCost = 0;
        private void bunifuThinButton24_Click(object sender, EventArgs e)
        {
            if (TestCb.SelectedIndex == -1 || ResultCb.SelectedIndex==-1)
            {
                MessageBox.Show("Select Test and Result");
            }
            else
            {
                testContent = testContent + TestCb.SelectedValue.ToString() + " : " + ResultCb.SelectedItem.ToString() + Environment.NewLine;
                TestTb.Text = testContent;
                totalCost = totalCost + 400;
                CostTb.Text = "" + totalCost;
            }
        }

        private void ShowPatient()
        {
            conn.Open();
            string query = "Select * from Result";
            SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
            SqlCommandBuilder builder = new SqlCommandBuilder(adapter);
            var ds = new DataSet();
            adapter.Fill(ds);
            ResultDGV.DataSource = ds.Tables[0];
            conn.Close();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Tests tests = new Tests();
            tests.Show();
            this.Hide();
        }

        private void TestCb_SelectionChangeCommitted(object sender, EventArgs e)
        {
            GetTestCost();
        }

        private void PatCb_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void Results_Load(object sender, EventArgs e)
        {

        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            Patients Obj = new Patients();
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

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            Results Obj = new Results();
            Obj.Show();
            this.Hide();
        }
    }
}
