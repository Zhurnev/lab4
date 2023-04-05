using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace lab4
{
    [DataContract]
    public class Teacher
    {
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public List<string> SubjectNames { get; set; }
        public string FormattedSubjectNames
        {
            get
            {
                if (SubjectNames.Count == 0)
                {
                    return "None";
                }
                else
                {
                    return string.Join(", ", SubjectNames);
                }
            }
        }

        public Teacher(string name)
        {
            Name = name;
            SubjectNames = new List<string>();
        }

        public override string ToString()
        {
            return this.Name;
        }
    }
}
