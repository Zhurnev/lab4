using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace lab4
{
    public partial class AddSubjectDialog : Form
    {
        public string SubjectName { get; set; }
        public AddSubjectDialog()
        {
            InitializeComponent();
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            SubjectName = subjectNameTextBox.Text;
            DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
