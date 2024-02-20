using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace DataStoreForm
{
    public partial class SignUpForm : Form
    {

        SqlConnection conn = new SqlConnection("Data Source=CECIL\\SQLEXPRESS;Initial Catalog=DataDB;Integrated Security=True;Encrypt=False");

        public SignUpForm()
        {
            InitializeComponent();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();

        }

        private void registerLink_Click(object sender, EventArgs e)
        {
            Form1 form = new Form1();
            form.Show();
            this.Hide();
        }

        private void btnSignIn_Click(object sender, EventArgs e)
        {
            if (tbEmail.Text =="" || tbUsername.Text=="" || tbPassword.Text=="")
            {
                MessageBox.Show("Fill the blanks", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                if (conn.State != ConnectionState.Open)
                {
                    try
                    {
                        conn.Open();
                        string selectData = " SELECT * FROM userTb WHERE username= '" +tbUsername.Text+ "'   ";
                        using (SqlCommand command = new SqlCommand(selectData,conn))
                        {
                            SqlDataAdapter adapter = new SqlDataAdapter(command);
                            DataTable data = new DataTable();
                            adapter.Fill(data);
                            if (data.Rows.Count >= 1)
                            {
                                MessageBox.Show("User already exist","Error Message",MessageBoxButtons.OK,MessageBoxIcon.Error);
                            }
                            else
                            {
                                string insertData = "INSERT INTO userTb(email,username,password)VALUES('"+tbEmail.Text+"','"+tbUsername.Text+"','"+tbPassword.Text+"')";
                                using (SqlCommand cmd = new SqlCommand(insertData,conn))
                                {
                                    cmd.ExecuteNonQuery();
                                    MessageBox.Show("Successful registered", "Information Message", MessageBoxButtons.OK, MessageBoxIcon.Information);

                                    Form1 form = new Form1();
                                    form.Show();
                                    this.Hide();
                                }
                            }
                        }
                    }
                    catch (Exception)
                    {

                        throw;
                    }
                }
            }
        }

        private void tbUsername_TextChanged(object sender, EventArgs e)
        {

        }

        private void SignUpForm_Load(object sender, EventArgs e)
        {

            tbPassword.UseSystemPasswordChar = true;

        }

        private void cbPassword_CheckedChanged(object sender, EventArgs e)
        {
            if (cbPassword.Checked)
            {
                tbPassword.UseSystemPasswordChar = false;
            }
            else
            {
                tbPassword.UseSystemPasswordChar = true;
            }
        }
    }
}
