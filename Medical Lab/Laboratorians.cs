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
    public partial class Laboratorians : Form
    {
        public Laboratorians()
        {
            InitializeComponent();
            ShowLab();
        }
        
        SqlConnection conn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Dell\Documents\MedicalLabDB.mdf;Integrated Security=True;Connect Timeout=30");
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            Gendercbx.Height = 30;

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void Reset()
        {
            LabNametxtbox.Text = string.Empty;
            Gendercbx.SelectedItem = -1;
            LabAddresstxtbox.Text = string.Empty;
            LabPhonetxtbox.Text = string.Empty;
            Qualcbx.SelectedItem = -1;
            LabDate.Text = string.Empty;
            key = 0;

        }
        private void ShowLab()
        {
            conn.Open();
            string query = "Select * from Lab";
            SqlDataAdapter adapter = new SqlDataAdapter(query,conn);
            SqlCommandBuilder builder = new SqlCommandBuilder(adapter);
            var ds = new DataSet();
            adapter.Fill(ds);
            LabDGV.DataSource = ds.Tables[0];
            conn.Close();
        }
        private void Savebtn_Click(object sender, EventArgs e)
        {
            if(LabNametxtbox.Text == "" || LabAddresstxtbox.Text == "" || LabPhonetxtbox.Text == "" || LabPasswordTb.Text == "" || Qualcbx.SelectedIndex == -1 || Gendercbx.SelectedIndex == -1)
            {
                MessageBox.Show("Missing Information");
            } 
            else
            {
                try
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("insert into Lab(name,address,gender,qual,phone,dob,password) values(@LN,@LA,@LG,@LQ,@LP,@LD,@LPASS)", conn);
                    cmd.Parameters.AddWithValue("@LN", LabNametxtbox.Text);
                    cmd.Parameters.AddWithValue("@LA", LabAddresstxtbox.Text);
                    cmd.Parameters.AddWithValue("@LP", LabPhonetxtbox.Text);
                    cmd.Parameters.AddWithValue("@LQ", Qualcbx.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("@LD", LabDate.Value.Date);
                    cmd.Parameters.AddWithValue("@LG", Gendercbx.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("@LPASS", LabPasswordTb.Text);

                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Saved");
                    conn.Close();
                    ShowLab();
                    Reset();

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void Editbtn_Click(object sender, EventArgs e)
        {
            if (LabNametxtbox.Text == "" || LabAddresstxtbox.Text == "" || LabPhonetxtbox.Text == "" || Qualcbx.SelectedIndex == -1 || Gendercbx.SelectedIndex == -1)
            {
                MessageBox.Show("Missing Information");
            }
            else
            {
                try
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("Update Lab Set name=@LN,address=@LA,phone=@LP,qual=@LQ,dob=@LD,gender=@LG where id=@LID", conn);
                    cmd.Parameters.AddWithValue("@LN", LabNametxtbox.Text);
                    cmd.Parameters.AddWithValue("@LA", LabAddresstxtbox.Text);
                    cmd.Parameters.AddWithValue("@LP", LabPhonetxtbox.Text);
                    cmd.Parameters.AddWithValue("@LQ", Qualcbx.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("@LD", LabDate.Value.Date);
                    cmd.Parameters.AddWithValue("@LG", Gendercbx.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("@LID", key);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Updated");
                    conn.Close();
                    ShowLab();
                    Reset();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        int key = 0;
        private void LabDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            LabNametxtbox.Text = LabDGV.SelectedRows[0].Cells[1].Value.ToString();
            Gendercbx.SelectedItem = LabDGV.SelectedRows[0].Cells[2].Value.ToString();
            LabAddresstxtbox.Text = LabDGV.SelectedRows[0].Cells[3].Value.ToString();
            LabPhonetxtbox.Text = LabDGV.SelectedRows[0].Cells[4].Value.ToString();
            Qualcbx.SelectedItem = LabDGV.SelectedRows[0].Cells[5].Value.ToString();
            LabDate.Text = LabDGV.SelectedRows[0].Cells[6].Value.ToString();
            if (LabNametxtbox.Text == "")
            {
                key = 0;
            }
            else
            {
                key = Convert.ToInt32(LabDGV.SelectedRows[0].Cells[0].Value.ToString());
            }

        }

        private void Deletebtn_Click(object sender, EventArgs e)
        {
            if(key == 0)
            {
                MessageBox.Show("Select a Lab");
            }
            else
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("Delete from Lab Where id=@LID", conn);
                cmd.Parameters.AddWithValue("@LID", key);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Deleted");
                conn.Close();
                ShowLab();
                Reset();
            }
        }

        private void Laboratorians_Load(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Laboratorians Obj = new Laboratorians();
            Obj.Show();
            this.Hide();
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

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged_1(object sender, EventArgs e)
        {

        }
    }
}
