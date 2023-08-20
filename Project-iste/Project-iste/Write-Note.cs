using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using System.Windows.Forms;

namespace Project_iste
{
    public partial class Write_Note : Form
    {
        public static NotePanel note;
        SqlConnection con = new SqlConnection(@"");
        DataTable dt = new DataTable();
        DataTable dt2 = new DataTable();
        public Write_Note()
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
            this.Close();
        }

        private void Write_Note_Load(object sender, EventArgs e)
        {
            label1.Text = Form1.userid;
            if(label5.Text != "")
            {
                guna2Button1.Visible = false;
                guna2Button2.Visible = true;
            }
            else
            {
                guna2Button1.Visible = true;
                guna2Button2.Visible = false;
            }
            if (label5.Text != "")
            {
                con.Open();
                string query2 = "select * from NoteWrite Where Title = '" + label5.Text.ToString() + "'";
                SqlCommand cmd2 = new SqlCommand(query2, con);
                SqlDataAdapter adb2 = new SqlDataAdapter(cmd2);
                adb2.Fill(dt2);
                con.Close();
                if (dt2.Rows.Count > 0)
                    richTextBox1.Text = dt2.Rows[0][3].ToString();
                else
                    MessageBox.Show("error");
                dt2.Clear();
            }
             
             /* string query = "select Title from NoteWrite Where UserName = '" + label1.Text +"'";
             SqlCommand cmd = new SqlCommand(query,con);
             SqlDataAdapter adb = new SqlDataAdapter(cmd);
             adb.Fill(dt);*/
            /* foreach (DataRow dr in dt.Rows)
             {
                 comboBox1.Items.Add(dr["Title"].ToString());
             }*/
        }


        private void guna2Button1_Click(object sender, EventArgs e)
        {
            
            if (check(guna2TextBox1.Text) == true || check(richTextBox1.Text) == true )
            {
                MessageBox.Show("please enter the required data", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                con.Open();
                string query2="select Title from NoteWrite Where Title = '"+guna2TextBox1.Text.ToString()+ "' And UserName = '"+ label1.Text.ToString()+"'";
                SqlCommand cmd2 = new SqlCommand(query2, con);
                SqlDataReader reader = cmd2.ExecuteReader();
                if (reader.Read())
                {
                    MessageBox.Show("Sorry,But An Title With This Name Already Exists", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    
                }
                else
                {
                    con.Close();
                    con.Open();
                    guna2TextBox1.Text = guna2TextBox1.Text.Replace('ğ', 'g');
                    string query = "insert into NoteWrite (UserName,Title,Subject) VALUES (@UserName,@Title,@Subject)";
                    SqlCommand cmd3 = new SqlCommand(query, con);
                    cmd3.Parameters.AddWithValue("@UserName", label1.Text);
                    cmd3.Parameters.AddWithValue("@Title", guna2TextBox1.Text);
                    cmd3.Parameters.AddWithValue("@Subject", richTextBox1.Text);
                    cmd3.ExecuteNonQuery();
                    MessageBox.Show("Your Note Saved in your sql table", "Information", MessageBoxButtons.OK,MessageBoxIcon.Information);
                    guna2TextBox1.Clear(); richTextBox1.Clear();
                    
                }
                con.Close();
                guna2TextBox1.Clear(); guna2TextBox1.Focus();
            }
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            
            if(guna2TextBox1.Text == "")
            {
                guna2TextBox1.Text = label5.Text;
                con.Open();
                string query3 = "update NoteWrite set  Title = @Title , Subject=@Subject Where Title = '" + label5.Text.ToString() + "'";
                SqlCommand cmd3 = new SqlCommand(query3, con);
                cmd3.Parameters.AddWithValue("@Title", guna2TextBox1.Text);
                cmd3.Parameters.AddWithValue("@Subject", richTextBox1.Text);
                cmd3.ExecuteNonQuery();
                con.Close();
                this.Close();
                MessageBox.Show("Your Note Is Successfully Updated ,Refresh Your Form");
            }
            else
            {
                con.Open();
                string query = "select Title from NoteWrite Where Title = '" + guna2TextBox1.Text.ToString() + "' And UserName = '" + label1.Text.ToString() + "'";
                SqlCommand cmd = new SqlCommand(query, con);
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    MessageBox.Show("Sorry,But An Title With This Name Already Exists", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
                else
                {
                    con.Close();
                    note.label1.Text = guna2TextBox1.Text;
                    note.richTextBox1.Text = richTextBox1.Text; 
                    con.Open();
                    string query2 = "update NoteWrite set  Title = @Title , Subject=@Subject Where Title = '" + label5.Text.ToString() + "'";
                    SqlCommand cmd2 = new SqlCommand(query2, con);
                    cmd2.Parameters.AddWithValue("@Title", guna2TextBox1.Text);
                    cmd2.Parameters.AddWithValue("@Subject", richTextBox1.Text);
                    cmd2.ExecuteNonQuery();
                    con.Close();
                    this.Close();
                    MessageBox.Show("Your Note Is Successfully ,Refresh Your Form");
                   
                }

                
            }
            
        }
    }
}
