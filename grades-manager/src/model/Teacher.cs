using System.Collections.Generic;
using GradesManager.db;

namespace GradesManager.model
{
    public class Teacher : User
    {
        public const string TYPE = "Teacher";

        public Teacher(string name, string password) : base(TYPE, name, password)
        {
        }

        public Teacher(string name, string password, List<Course> courses) : base(TYPE, name, password, courses)
        {
        }

        public List<Grade> GetGrades(string course, string subject, string student)
        {
            return DataBase.GetGrades(Name, Password, course, subject, student);
        }
    }
}