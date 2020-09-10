using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;
using GradesManager.util;

namespace GradesManager.model
{
    public abstract class User
    {
        protected User(string type, string name, string password)
        {
            Type = type;
            Name = name;
            Password = password;
            Courses = new List<Course>();
        }

        protected User(string type, string name, string password, List<Course> courses)
        {
            Type = type;
            Name = name;
            Password = password;
            Courses = courses;
        }

        public string Type { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public List<Course> Courses { get; set; }


        public string Serialize()
        {
            var courses = new StringBuilder();
            Courses?.ForEach(course => courses.Append(course.Serialize()).Append(","));
            return "User-Type=" + Type + ";User-Name=" + Name + ";User-Password=" + Password + ";User-Courses=[" +
                   courses + "];";
        }

        public static User Deserialize(string line)
        {
            var type = Util.ReadKeyFromLine(line, "User-Type");
            var name = Util.ReadKeyFromLine(line, "User-Name");
            var password = Util.ReadKeyFromLine(line, "User-Password");
            var courses = Course.Deserialize(Util.ReadObjectListFromLine(line, "User-Courses"));
            if (type.Equals(Student.TYPE)) return new Student(name, password, courses);
            if (type.Equals(Teacher.TYPE)) return new Teacher(name, password, courses);

            throw new SerializationException();
        }

        public new string ToString()
        {
            var obj = new StringBuilder();

            obj.Append("Type: ").Append(Type).Append("\n");
            obj.Append("Name: ").Append(Name).Append("\n");
            obj.Append("Password: ").Append(Password).Append("\n");

            if (Courses == null) return obj.ToString();

            obj.Append("Courses:\n");
            foreach (var course in Courses)
            {
                obj.Append("\tName: ").Append(course.Name).Append("\n");
                obj.Append("\tSubjects:\n");
                foreach (var subject in course.Subjects)
                {
                    obj.Append("\t\tName: ").Append(subject.Name).Append("\n");
                    if (subject.Grades.Count > 0)
                    {
                        obj.Append("\t\tGrades:\n");
                        foreach (var grade in subject.Grades)
                        {
                            obj.Append("\tValue: ").Append(grade.Value);
                            obj.Append("\tType: ").Append(grade.Type);
                            obj.Append("\tDate: ").Append(grade.Date).Append("\n");
                        }

                        obj.Append("\n");
                    }

                    if (subject.Students.Count > 0)
                    {
                        obj.Append("\t\tStudents: ");
                        foreach (var student in subject.Students) obj.Append(student).Append(", ");
                        obj.Append("\n");
                    }

                    obj.Append("\n");
                }
            }

            return obj.ToString();
        }
    }
}