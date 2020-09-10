using System.Collections.Generic;
using System.Text;
using GradesManager.util;

namespace GradesManager.model
{
    public class Course
    {
        public Course(string name)
        {
            Name = name;
            Subjects = new List<Subject>();
        }

        public Course(string name, List<Subject> subjects) : this(name)
        {
            Subjects = subjects;
        }

        public string Name { get; set; }
        public List<Subject> Subjects { get; set; }

        public string Serialize()
        {
            var subjects = new StringBuilder();
            Subjects?.ForEach(subject => subjects.Append(subject.Serialize()).Append(","));
            return "Course-Name=" + Name + ";Course-Subjects=[" + subjects + "];";
        }

        public static List<Course> Deserialize(List<string> courses)
        {
            var coursesObj = new List<Course>();

            foreach (var course in courses)
            {
                var name = Util.ReadKeyFromLine(course, "Course-Name");
                var subjects = Subject.Deserialize(Util.ReadObjectListFromLine(course, "Course-Subjects"));
                coursesObj.Add(new Course(name, subjects));
            }

            return coursesObj;
        }
    }
}