using System.Collections.Generic;

namespace GradesManager.model
{
    public class Student : User
    {
        public const string TYPE = "Student";

        public Student(string name, string password) : base(TYPE, name, password)
        {
        }

        public Student(string name, string password, List<Course> courses) : base(TYPE, name, password, courses)
        {
        }
    }
}