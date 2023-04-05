using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.Serialization.Json;
using System.IO;

namespace lab4
{
    public partial class MainForm : Form
    {
        private TeacherSubjectManager manager;

        public MainForm(TeacherSubjectManager manager)
        {
            this.manager = manager;
            InitializeComponent();
            Load += MainForm_Load;
            BindingList<Teacher> teacherList = new BindingList<Teacher>(this.manager.GetAllTeachers());

            dataGridView1.AutoGenerateColumns = false;

            DataGridViewTextBoxColumn nameColumn = new DataGridViewTextBoxColumn();
            nameColumn.Name = "Name";
            nameColumn.DataPropertyName = "Name";
            dataGridView1.Columns.Add(nameColumn);

            DataGridViewTextBoxColumn subjectsColumn = new DataGridViewTextBoxColumn();
            subjectsColumn.Name = "Subjects";
            subjectsColumn.DataPropertyName = "FormattedSubjectNames";
            subjectsColumn.DefaultCellStyle.NullValue = "None";
            subjectsColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView1.Columns.Add(subjectsColumn);

            dataGridView1.DataSource = teacherList;
        }


        private void addTeacherButton_Click(object sender, EventArgs e)
        {
            var dialog = new AddTeacherDialog();
            var result = dialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                var teacherName = dialog.TeacherChosenName;
                var newTeacher = new Teacher(teacherName);
                this.manager.AddTeacher(newTeacher);
                UpdateDataGridView();
            }
        }
        private void UpdateDataGridView()
        {
            BindingList<Teacher> teacherList = new BindingList<Teacher>(manager.GetAllTeachers());

            DataGridViewTextBoxColumn subjectsColumn = dataGridView1.Columns["Subjects"] as DataGridViewTextBoxColumn;
            subjectsColumn.DefaultCellStyle.NullValue = "None";
            subjectsColumn.DataPropertyName = "FormattedSubjectNames";
            dataGridView1.DataSource = teacherList;
        }

        private void removeTeacherButton_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a teacher to remove.");
                return;
            }

            var selectedRow = dataGridView1.SelectedRows[0];
            var selectedTeacher = selectedRow.DataBoundItem as Teacher;
            if (selectedTeacher == null)
            {
                MessageBox.Show("Please select a valid teacher to remove.");
                return;
            }

            var dialog = new RemoveTeacherDialog(selectedTeacher.Name);
            var result = dialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                manager.RemoveTeacher(selectedTeacher);
                UpdateDataGridView();
            }
        }

        private void AddSubjectButton_Click(object sender, EventArgs e)
        {
            var dialog = new AddSubjectDialog();
            var result = dialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                var subjectName = dialog.SubjectName;
                var newSubject = new Subject(subjectName);
                this.manager.AddSubject(newSubject);
                UpdateDataGridView();
            }
        }

        private void removeSubjectButton_Click(object sender, EventArgs e)
        {
            var dialog = new RemoveSubjectDialog(this.manager.GetAllSubjects());
            var result = dialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                var selectedSubject = dialog.SelectedSubject;
                this.manager.RemoveSubject(selectedSubject);
                UpdateDataGridView();
            }
        }

        private void connectTeacherSubjectButton_Click(object sender, EventArgs e)
        {
            var dialog = new SelectTeacherSubjectDialog(manager.GetAllTeachers(), manager.GetAllSubjects());
            dialog.ShowDialog();
            this.manager.AssignTeacherToSubject(dialog.selectedTeacher, dialog.selectedSubject);
            UpdateDataGridView();
        }

        private void removeConnectionButton_Click(object sender, EventArgs e)
        {
            var dialog = new SelectTeacherSubjectDialog(manager.GetAllTeachers(), manager.GetAllSubjects());
            dialog.ShowDialog();
            this.manager.RemoveConnection(dialog.selectedTeacher, dialog.selectedSubject);
            UpdateDataGridView();
        }

        private void sortNameButton_Click(object sender, EventArgs e)
        {
            manager.SortTeachersByName();
            UpdateDataGridView();
        }

        private void sortCountButton_Click(object sender, EventArgs e)
        {
            manager.SortTeachersBySubjectCount();
            UpdateDataGridView();
        }
        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(List<Teacher>));
            using (var file = new FileStream("teachers.json", FileMode.OpenOrCreate))
            {
               
                    serializer.WriteObject(file, this.manager.GetAllTeachers());
                
            }

            serializer = new DataContractJsonSerializer(typeof(List<Subject>));
            using (var file = new FileStream("subjects.json", FileMode.OpenOrCreate))
            {
               serializer.WriteObject(file,manager.GetAllSubjects());
            }
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(List<Teacher>));
            using (var file = new FileStream("teachers.json",FileMode.OpenOrCreate))
            {
                foreach (Teacher teacher in serializer.ReadObject(file) as List<Teacher>)
                {
                    this.manager.AddTeacher(teacher);
                }
            }

            serializer = new DataContractJsonSerializer(typeof(List<Subject>));
            using (var file = new FileStream("subjects.json", FileMode.OpenOrCreate))
            {
               foreach (Subject subject in serializer.ReadObject(file) as List<Subject>)
                {
                    this.manager.AddSubject(subject);
                }
            }
            UpdateDataGridView();
        }

    }
}
