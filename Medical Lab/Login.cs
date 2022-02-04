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
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        SqlConnection conn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Dell\Documents\MedicalLabDB.mdf;Integrated Security=True;Connect Timeout=30");

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click_1(object sender, EventArgs e)
        {

        }

        private void bunifuThinButton21_Click(object sender, EventArgs e)
        {
            if (UserNameTb.Text == "" || PasswordTb.Text == "")
            {
                MessageBox.Show("Enter Username and Password");
            }
            else
            {
                conn.Open();
                SqlDataAdapter adapter = new SqlDataAdapter("Select count(*) from Lab where name='" + UserNameTb.Text + "' and password='" + PasswordTb.Text + "'", conn);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                if (dt.Rows[0][0].ToString()=="1")
                {
                    Patients Obj = new Patients();
                    Obj.Show();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("Wrong Username and Password");
                }
                conn.Close();
            }
        }
    }
}
