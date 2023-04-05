using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab4
{
    public class TeacherSubjectManager
    {
        private List<Teacher> teachers;
        private List<Subject> subjects;

        public TeacherSubjectManager()
        {
            teachers = new List<Teacher>();
            subjects = new List<Subject>();
        }

        public void AddTeacher(Teacher teacher)
        {
            teachers.Add(teacher);
        }

        public void RemoveTeacher(Teacher teacher)
        {
            foreach (Subject subject in subjects)
            {
                RemoveConnection(teacher, subject);
            }
            teachers.Remove(teacher);
        }

        public List<Teacher> GetAllTeachers()
        {
            return teachers;
        }

        public void AddSubject(Subject subject)
        {
            subjects.Add(subject);
        }

        public void RemoveSubject(Subject subject)
        {
            foreach (Teacher teacher in teachers)
            {
                RemoveConnection(teacher, subject);
            }
            subjects.Remove(subject);
        }

        public List<Subject> GetAllSubjects()
        {
            return subjects;
        }

        public void AssignTeacherToSubject(Teacher teacher, Subject subject)
        {
            if (teacher == null||subject== null)
            {
                throw new ArgumentNullException();
            }
            if (!teacher.SubjectNames.Contains(subject.Name))
            {
                teacher.SubjectNames.Add(subject.Name);
            }

            if (!subject.TeacherNames.Contains(teacher.Name))
            {
                subject.TeacherNames.Add(teacher.Name);
            }
        }

        public void RemoveConnection(Teacher teacher, Subject subject)
        {
            if (subject.TeacherNames.Contains(teacher.Name))
            {
                subject.TeacherNames.Remove(teacher.Name);
            }

            if (teacher.SubjectNames.Contains(subject.Name))
            {
                teacher.SubjectNames.Remove(subject.Name);
            }
        }
        public void SortTeachersByName()
        {
            teachers = teachers.OrderBy(t => t.Name).ToList();
        }

        public void SortTeachersBySubjectCount()
        {
            teachers = teachers.OrderByDescending(t => t.SubjectNames.Count).ToList();
        }
    }
}
