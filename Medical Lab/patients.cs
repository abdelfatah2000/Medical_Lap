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


namespace Medical_Lab
{
    public partial class Patients : Form
    {
        public Patients()
        {
            InitializeComponent();
            ShowPatient();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        SqlConnection conn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Dell\Documents\MedicalLabDB.mdf;Integrated Security=True;Connect Timeout=30");
        private void ShowPatient()
        {
            conn.Open();
            string query = "Select * from Patient";
            SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
            SqlCommandBuilder builder = new SqlCommandBuilder(adapter);
            var ds = new DataSet();
            adapter.Fill(ds);
            patientDGV.DataSource = ds.Tables[0];
            conn.Close();
        }

        private void SaveBtn_Click(object sender, EventArgs e)
        {
            if (PatNameTb.Text == "" || PatPhoneTb.Text == "" || PatAddressTb.Text == "" || GenCb.SelectedIndex == -1)
            {
                MessageBox.Show("Missing Information");
            }
            else
            {
                try
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("insert into Patient(name,address,gender,phone,dof) values(@PN,@PA,@PG,@PP,@PD)", conn);
                    cmd.Parameters.AddWithValue("@PN", PatNameTb.Text);
                    cmd.Parameters.AddWithValue("@PP", PatPhoneTb.Text);
                    cmd.Parameters.AddWithValue("@PA", PatAddressTb.Text);
                    cmd.Parameters.AddWithValue("@PD", PatDOB.Value.Date);
                    cmd.Parameters.AddWithValue("@PG", GenCb.SelectedItem.ToString());
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Saved");
                    conn.Close();
                    ShowPatient();
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
            if (PatNameTb.Text == "" || PatPhoneTb.Text == "" || PatAddressTb.Text == "" || GenCb.SelectedIndex == -1)
            {
                MessageBox.Show("Missing Information");
            }
            else
            {
                try
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("Update Patient Set name=@PN,address=@PA,phone=@PP,dob=@PD,gender=@PG where id=@PID", conn);
                    cmd.Parameters.AddWithValue("@PN", PatNameTb.Text);
                    cmd.Parameters.AddWithValue("@PP", PatPhoneTb.Text);
                    cmd.Parameters.AddWithValue("@PA", PatAddressTb.Text);
                    cmd.Parameters.AddWithValue("@PD", PatDOB.Value.Date);
                    cmd.Parameters.AddWithValue("@PG", GenCb.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("@PID", key);

                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Updated");
                    conn.Close();
                    ShowPatient();
                    Reset();

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void patientDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            PatNameTb.Text = patientDGV.SelectedRows[0].Cells[1].Value.ToString();
            GenCb.SelectedItem = patientDGV.SelectedRows[0].Cells[2].Value.ToString();
            PatAddressTb.Text = patientDGV.SelectedRows[0].Cells[3].Value.ToString();
            PatPhoneTb.Text = patientDGV.SelectedRows[0].Cells[4].Value.ToString();
            PatDOB.Text = patientDGV.SelectedRows[0].Cells[6].Value.ToString();
            if (PatNameTb.Text == "")
            {
                key = 0;
            }
            else
            {
                key = Convert.ToInt32(patientDGV.SelectedRows[0].Cells[0].Value.ToString());
            }
        }

        private void DeleteBtn_Click(object sender, EventArgs e)
        {

            if (key == 0)
            {
                MessageBox.Show("Select a Patient");
            }
            else
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("Delete from Patient Where id=@PID", conn);
                cmd.Parameters.AddWithValue("@PID", key);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Deleted");
                conn.Close();
                ShowPatient();
                Reset();
            }
        }
        private void Reset()
        {
            PatNameTb.Text = string.Empty;
            GenCb.SelectedItem = -1;
            PatAddressTb.Text = string.Empty;
            PatPhoneTb.Text = string.Empty;
            PatDOB.Text = string.Empty;
            key = 0;

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Tests Obj = new Tests();
            Obj.Show();
            this.Hide();
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            Patients Obj = new Patients();
            Obj.Show();
            this.Hide();
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            Results Obj = new Results();
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
