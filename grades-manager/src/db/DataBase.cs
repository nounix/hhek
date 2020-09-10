using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using GradesManager.model;
using GradesManager.util;

namespace GradesManager.db
{
    public static class DataBase
    {
        private const string UserPath = "user.db";
        private const string AppointmentPath = "appointment.db";
        
        private static readonly List<User> Users = new List<User>();
        private static readonly List<Appointment> Appointments = new List<Appointment>();

        private static bool _usersChanged;

        public static List<User> GetUsers()
        {
            return Users;
        }

        public static List<Appointment> GetAppointments()
        {
            return Appointments;
        }

        public static void LoadUsers()
        {
            try
            {
                foreach (var line in File.ReadLines(UserPath, Encoding.UTF8))
                    Users.Add(User.Deserialize(line));
            }
            catch (FileNotFoundException e)
            {
                Debug.WriteLine(e);
                Console.WriteLine("INFO: No database file found!\n");
            }
            catch (SerializationException e)
            {
                Debug.WriteLine(e);
            }
        }

        public static void LoadAppointments()
        {
            try
            {
                foreach (var line in File.ReadLines(AppointmentPath, Encoding.UTF8))
                    Appointments.Add(Appointment.Deserialize(line));
            }
            catch (FileNotFoundException e)
            {
                Debug.WriteLine(e);
                Console.WriteLine("INFO: No database file found!\n");
            }
        }

        public static void SaveUsers()
        {
            if (!_usersChanged) return;

            _usersChanged = false;

            var users = new StringBuilder();
            
            Users.ForEach(user => users.Append(user.Serialize()).AppendLine());
            
            Util.WriteToFile(UserPath, users.ToString());
        }

        public static void InsertUser(User user)
        {
            if (UserExists(user.Name)) return;
            Users.Add(user);
            _usersChanged = true;
        }

        public static void UpdateUser(User user)
        {
            var i = Users.FindIndex(u => u.Name.Equals(user.Name));
            if (i > -1) Users[i] = user;
            _usersChanged = true;
        }

        public static User GetUserByNameAndPassword(string name, string password)
        {
            return Users.Find(u => u.Name.Equals(name) && u.Password.Equals(password));
        }

        public static List<Grade> GetGrades(string name, string password, string courseSearch, string subjectSearch,
            string student)
        {
            if (!IsTeacherOf(name, password, courseSearch, subjectSearch, student)) return null;

            return Users.Find(u => u.Name.Equals(student)).Courses.Find(c => c.Name.Equals(courseSearch))
                .Subjects.Find(sub => sub.Name.Equals(subjectSearch)).Grades;
        }

        public static void SetGrade(string name, string password, string courseSearch, string subjectSearch,
            string student, int grade, int value, string type, string date)
        {
            if (!IsTeacherOf(name, password, courseSearch, subjectSearch, student)) return;

            Users.Find(u => u.Name.Equals(student)).Courses.Find(c => c.Name.Equals(courseSearch))
                .Subjects.Find(sub => sub.Name.Equals(subjectSearch)).Grades[grade].SetGrade(value, type, date);

            _usersChanged = true;
        }

        private static bool IsTeacherOf(string name, string password, string courseSearch, string subjectSearch,
            string student)
        {
            var teacher = GetUserByNameAndPassword(name, password);
            if (!teacher.Type.Equals(Teacher.TYPE)) return false;

            return teacher.Courses.Any(c => c.Name.Equals(courseSearch) && c.Subjects
                                                .Any(s => s.Name.Equals(subjectSearch) && s.Students
                                                              .Any(st => st.Equals(student)))) &&
                   Users.Any(u => u.Name.Equals(student) && u.Courses.Any(c => c.Name.Equals(courseSearch) && c.Subjects
                                                                                   .Any(sub => sub.Name.Equals(
                                                                                       subjectSearch))));
        }

        public static bool UserExists(string name)
        {
            foreach (var usr in Users)
                if (usr.Name.Equals(name))
                    return true;

            return false;
        }

        public static bool UserExists(string name, string password)
        {
            foreach (var usr in Users)
                if (usr.Name.Equals(name) && usr.Password.Equals(password))
                    return true;

            return false;
        }
    }
}