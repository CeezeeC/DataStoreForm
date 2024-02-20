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

namespace DataStoreForm
{
    public partial class HomeForm : Form
    {
        SqlConnection conn = new SqlConnection("Data Source=CECIL\\SQLEXPRESS;Initial Catalog=DataDB;Integrated Security=True;Encrypt=False");

        public HomeForm()
        {
            InitializeComponent();
        }

        private void HomeForm_Load(object sender, EventArgs e)
        {

        }

        private void rbMale_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            
        }

        private void btAdd_Click(object sender, EventArgs e)
        {
            if (tbPersonID.Text==""|| tbName.Text == "" || tbSurname.Text == "" || comboBoxRace.Text == "" || tbAddress.Text == "" )
            {
                MessageBox.Show("Fill all the blanks","Error Message", MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
            else
            {
                try
                {
                    string gender;
                    if (rbMale.Checked)
                    {
                        gender = "Male";
                    }
                    else
                    {
                        gender = "Female";
                    }
                    conn.Open();
                    string insertData = "INSERT INTO PersonDetail(PersonID,Name,Surname,Race,Gender,Address,Date)" +
                        "VALUES('" + tbPersonID.Text + "','" + tbName.Text + "','" + tbSurname.Text + "','" + comboBoxRace.Text + "','" + gender + "','" + tbAddress.Text + "','" + dateTimePicker1.Value + "')";

                    using (SqlCommand cmd = new SqlCommand(insertData, conn))
                    {
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Successfully inserted ", "Information Message ", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    }

                }
                catch (Exception ex)
                {

                    MessageBox.Show("Connection Error" + ex, "Error Message ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    conn.Close();
                    Clear();
                    Display();
                }
            }
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }
      
       
        void Display()
        {
           
            string selectData = "SELECT * FROM PersonDetail";
            using (SqlCommand cmd = new SqlCommand(selectData, conn))
            {

                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataTable data = new DataTable();
                adapter.Fill(data);
                grindView.DataSource = data;
                conn.Close();

            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (tbPersonID.Text =="" )
            {
                MessageBox.Show("Fill the blanks ", "Errort Message ", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
            else 
            {
                try
                {
                    conn.Open();
                    string selectData = " UPDATE PersonDetail SET Name = '" + tbName.Text + "', Surname = '" + tbSurname.Text + "', Address = '" + tbAddress.Text + "', Date = '" + dateTimePicker1.Value + "'WHERE PersonID = '" + tbPersonID.Text + "' ";
                    using (SqlCommand cmd = new SqlCommand(selectData, conn))
                    {

                        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                        DataTable data = new DataTable();
                        adapter.Fill(data);

                       
                        MessageBox.Show("Successfully update", "Information Message ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                       ;
                    }
                }
                catch (Exception ex)
                {

                    MessageBox.Show("Database failed",ex + "Error Message ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    ;
                }
                finally
                {
                    conn.Close();
                    Clear();
                    Display();
                }
                
            }


            
            
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (tbPersonID.Text == "")
            {
                MessageBox.Show("Enter PersonID to Delete","Error Message ", MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
            else
            {
                try
                {
                    conn.Open();
                    string selectData = " DELETE FROM  PersonDetail WHERE PersonID = '" + tbPersonID.Text + "' ";
                    using (SqlCommand cmd = new SqlCommand(selectData, conn))
                    {
                        cmd.ExecuteNonQuery();           
                        MessageBox.Show("Successfully deleted", "Information Message ", MessageBoxButtons.OK, MessageBoxIcon.Information);    
                    }
                }
                catch (Exception ex)
                {

                    MessageBox.Show(" Database failed "+ex , " Error Messagea  ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    ;
                }
                finally
                {
                    conn.Close();
                   
                    Display();
                }
               
            }
        }
        public void Clear()
        {
            tbPersonID.Clear();
            tbName.Clear();
            tbSurname.Clear();
            comboBoxRace.Text = string.Empty;
            rbMale.Text = string.Empty;
            tbAddress.Clear();
           

        }

        private void btnView_Click(object sender, EventArgs e)
        {
            string selectData = "SELECT * FROM PersonDetail";
            conn.Open();
            using (SqlCommand cmd = new SqlCommand(selectData, conn))
            {

                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataTable data = new DataTable();
                adapter.Fill(data);
                grindView.DataSource = data;
                conn.Close();
            }
        }

        private void btnResert_Click(object sender, EventArgs e)
        {
            conn.Open();
            string selectData = " DELETE  FROM PersonDetail";
            using (SqlCommand cmd = new SqlCommand(selectData, conn))
            {

                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataTable data = new DataTable();
                adapter.Fill(data);
                grindView.DataSource = data;

                conn.Close();
                MessageBox.Show("Successfully Reserted", "Information Message ", MessageBoxButtons.OK, MessageBoxIcon.Information);
              


            }
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
