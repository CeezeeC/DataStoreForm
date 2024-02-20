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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;

namespace DataStoreForm
{
    public partial class Form1 : Form
    {
        SqlConnection conn = new SqlConnection("Data Source=CECIL\\SQLEXPRESS;Initial Catalog=DataDB;Integrated Security=True;Encrypt=False");

        public Form1()
        {
            InitializeComponent();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void registerLink_Click(object sender, EventArgs e)
        {
            SignUpForm sign = new SignUpForm();
            sign.Show();
            this.Hide();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {

            if (tbUsername.Text == "" || tbPassword.Text == "")
            {
                MessageBox.Show(" Fill all the blanks", "Error Messge", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                if (conn.State != ConnectionState.Open)
                {
                    try
                    {
                        conn.Open();
                        string selectData = "SELECT * FROM userTb WHERE username= @username AND password=@password";
                        using (SqlCommand cmd = new SqlCommand(selectData, conn))
                        {
                            cmd.Parameters.AddWithValue("@username", tbUsername.Text.Trim());
                            cmd.Parameters.AddWithValue("@password", tbPassword.Text.Trim());
                            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                            DataTable table = new DataTable();
                            adapter.Fill(table);
                            if (table.Rows.Count >= 1)
                            {
                                MessageBox.Show(" Successfully loged in ", "Information Messege", MessageBoxButtons.OK, MessageBoxIcon.Information);

                                HomeForm home = new HomeForm();
                                home.Show();
                                this.Hide();
                            }
                            else
                            {
                                MessageBox.Show("Wrong username/password", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                    catch (Exception ex)
                    {

                        MessageBox.Show("Error connection " + ex, "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    }
                    finally
                    {

                        conn.Close();
                    }

                }
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

            tbPassword.UseSystemPasswordChar = true;
        }

      

        private void checkBox_CheckedChanged(object sender, EventArgs e)
        {

            if (checkBox.Checked)
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
