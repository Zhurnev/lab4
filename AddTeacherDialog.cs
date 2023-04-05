using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace lab4
{
    public partial class AddTeacherDialog : Form
    {

        public string TeacherChosenName { get; set; }
        public AddTeacherDialog()
        {
            InitializeComponent();
        }

        private void OKButton_Click(object sender, EventArgs e)
        {
            TeacherChosenName =   TeacherNameTextBox.Text;
            DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
