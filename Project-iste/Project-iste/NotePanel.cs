using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Project_iste
{
    public partial class NotePanel : UserControl
    {

        static public string title;  
        public NotePanel()
        {
            InitializeComponent();
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            Write_Note.note = this;
            Write_Note note = new Write_Note();
            Account account = new Account();
            note.label1.Text = account.label2.Text;
            note.label5.Text = this.label1.Text;
            note.ShowDialog();
            
            
        }

        private void guna2ShadowPanel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
