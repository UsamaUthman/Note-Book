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
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace Project_iste
{
    public partial class Form1 : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\osama\source\repos\my_github\Usama-USKH\Project-iste\Project-iste\Database1.mdf;Integrated Security=True");
        SqlCommand com;

        public static string userid;
        public Form1()
        {
            InitializeComponent();
        }


        public static bool check(string str)
        {
            return (String.IsNullOrEmpty(str) || 
                str.Trim().Length == 0) ? true : false;
        }


        private void guna2ControlBox1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            
            
            userid = guna2TextBox1.Text;
            com = new SqlCommand();
            if (check(guna2TextBox1.Text) == true || check(guna2TextBox2.Text) == true)
            {
                MessageBox.Show("please enter the required data", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                guna2TextBox1.Clear(); guna2TextBox2.Clear(); guna2TextBox1.Focus();
            }
            else
            {
                con.Open();
                com.Connection = con;
                com.CommandText = "Select * from Login where Username='" + guna2TextBox1.Text +
                    "'And password='" + guna2TextBox2.Text + "'";
                SqlDataReader reader = com.ExecuteReader();
                if (reader.Read())
                {
                    this.Hide();
                    Account.account.Show();
                    //account.Show();
                    Account.account.label2.Text = guna2TextBox1.Text;
                }
                else
                {
                    MessageBox.Show("Invaild Login Details!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    guna2TextBox1.Clear(); guna2TextBox2.Clear(); guna2TextBox1.Focus();
                }
                con.Close();
            }
        }

        public static int passwordchar(string password)
        {
            int passwordchar = password.Length;
            return passwordchar;
        }

        public static int passworsupper(string password)
        {
            int value = password.Length - Regex.Replace(password, "[A-Z]", "").Length;
            return value;
        }

        public static int passworslower(string password)
        {
            int value = password.Length - Regex.Replace(password, "[a-z]", "").Length;
            return value;  
        }

        public static int passworsnumber(string password)
        {
            int value = password.Length - Regex.Replace(password, "[0-9]", "").Length;
            return value;
        }

        public static int passworsspecial(string password)
        {
            int value = password.Length - Regex.Replace(password, "[!-/:-@]", "").Length;
            return value;
        }

    

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            if (check(guna2TextBox1.Text) == true || check(guna2TextBox2.Text )== true)
            {
                MessageBox.Show("please enter the required data", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                guna2TextBox1.Clear(); guna2TextBox2.Clear(); guna2TextBox1.Focus();
            }
            else
            {
                DataTable dt = new DataTable();
                con.Open();
                string query = "select UserName From Login Where UserName = '" + guna2TextBox1.Text.ToString() + "'";
                SqlCommand cmd = new SqlCommand(query, con);
                SqlDataAdapter Adb = new SqlDataAdapter(cmd);
                Adb.Fill(dt);
                con.Close();
                if (dt.Rows.Count > 0)
                {
                    MessageBox.Show("UserName is Already Iaken", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    guna2TextBox1.Clear(); guna2TextBox2.Clear();
                }
                else
                {
                    if (passwordchar(guna2TextBox2.Text) < 8 || passwordchar(guna2TextBox2.Text) >= 16)
                    {
                        MessageBox.Show("your password must be 8-16 characters", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        guna2TextBox2.Clear(); guna2TextBox2.Focus();
                    }
                    else if (passworsupper(guna2TextBox2.Text) < 1)
                    {
                        MessageBox.Show("your password must have at least one capital", "eror", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        guna2TextBox2.Clear(); guna2TextBox2.Focus();
                    }
                    else if (passworslower(guna2TextBox2.Text) < 1)
                    {
                        MessageBox.Show("your password must have at least one lower case letter", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        guna2TextBox2.Clear(); guna2TextBox2.Focus();

                    }
                    else if (passworsnumber(guna2TextBox2.Text) < 1)
                    {
                        MessageBox.Show("your password must have at least one number", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        guna2TextBox2.Clear(); guna2TextBox2.Focus();

                    }
                    else if (passworsspecial(guna2TextBox2.Text) < 1)
                    {
                        MessageBox.Show("your password must have at least one Special characters ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        guna2TextBox2.Clear(); guna2TextBox2.Focus();

                    }
                    else
                    {
                        con.Open();
                        string query2 = "INSERT INTO Login (UserName , Password) VALUES(@UserName,@Password) ";
                        SqlCommand cmd2 = new SqlCommand(query2, con);
                        cmd2.Parameters.AddWithValue("@UserName", guna2TextBox1.Text);
                        cmd2.Parameters.AddWithValue("@Password", guna2TextBox2.Text);
                        cmd2.ExecuteNonQuery();
                        con.Close();
                        guna2TextBox1.Clear(); guna2TextBox2.Clear(); guna2TextBox1.Focus();
                        MessageBox.Show("user saved");
                    }
                }

            }
        }


        private void Form1_Shown(object sender, EventArgs e)
        {
            guna2TextBox1.Focus();
        }

      
    }
}
