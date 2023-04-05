using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace lab4
{
    [DataContract]
    public class Subject
    {
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public List<String> TeacherNames { get; set; }
        public Subject(string name)
        {
            Name = name;
            TeacherNames = new List<String>();
        }
        public override string ToString()
        {
            return this.Name;
        }
    }
}
