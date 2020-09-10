using System.Collections.Generic;
using System.Text;
using GradesManager.util;

namespace GradesManager.model
{
    public class Subject
    {
        public Subject(string name)
        {
            Name = name;
            Grades = new List<Grade>();
            Students = new List<string>();
        }

        public Subject(string name, List<Grade> grades, List<string> students)
        {
            Name = name;
            Grades = grades;
            Students = students;
        }

        public string Name { get; set; }
        public List<Grade> Grades { get; set; }
        public List<string> Students { get; set; }

        public string Serialize()
        {
            var grades = new StringBuilder();
            var students = new StringBuilder();

            Grades?.ForEach(grade => grades.Append(grade.Serialize()).Append(","));
            Students?.ForEach(student => students.Append(student).Append(","));

            return "Subject-Name=" + Name + ";Subject-Grades=[" + grades + "];Subject-Students=[" + students + "];";
        }

        public static List<Subject> Deserialize(List<string> subjects)
        {
            var subjectsObj = new List<Subject>();

            foreach (var subject in subjects)
            {
                var name = Util.ReadKeyFromLine(subject, "Subject-Name");
                var grades = Grade.Deserialize(Util.ReadObjectListFromLine(subject, "Subject-Grades"));
                var students = Util.ReadListFromLine(subject, "Subject-Students");
                subjectsObj.Add(new Subject(name, grades, students));
            }

            return subjectsObj;
        }
    }
}