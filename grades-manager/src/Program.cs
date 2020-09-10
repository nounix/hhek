using System;
using System.Collections.Generic;
using System.Linq;
using GradesManager.db;
using GradesManager.model;
using GradesManager.service;
using GradesManager.util;
using GradesManager.view;
using Course = GradesManager.model.Course;
using Grade = GradesManager.model.Grade;
using Student = GradesManager.model.Student;
using Subject = GradesManager.model.Subject;
using Teacher = GradesManager.model.Teacher;

namespace GradesManager
{
    internal class Program
    {
        private static void Test()
        {
            var grades = new[] {new Grade(1, "m", "12.06"), new Grade(3, "s", "14.03"), new Grade(5, "s", "18.01")}
                .ToList();
            var mathe = new Subject("mathe", grades, new List<string>());
            var deutsch = new Subject("deutsch", grades, new List<string>());
            var course = new Course("7B", new[] {mathe, deutsch}.ToList());
            var course2 = new Course("8B", new[] {deutsch, mathe}.ToList());
            var course3 = new Course("9B", new[] {deutsch, mathe}.ToList());
            var student = new Student("martin", "asd", new[] {course}.ToList());
            var student2 = new Student("rahmad", "qwe", new[] {course2}.ToList());
            var student3 = new Student("ali", "jklj", new[] {course3}.ToList());
            var deutschTeacher =
                new Subject("deutsch", new List<Grade>(), new[] {student.Name, student2.Name}.ToList());
            var matheTeacher = new Subject("mathe", new List<Grade>(), new[] {student.Name, student2.Name}.ToList());
            var courseTeacher = new Course("7B", new[] {deutschTeacher, matheTeacher}.ToList());
            var courseTeacher2 = new Course("8B", new[] {matheTeacher, deutschTeacher}.ToList());
            var teacher = new Teacher("muller", "hihi", new[] {courseTeacher, courseTeacher2}.ToList());

//            DataBase.SaveUser(student);
//            DataBase.SaveUser(student2);
//            DataBase.SaveUser(student3);
//            DataBase.SaveUser(teacher);

//            /*
//            Console.WriteLine(DataBase.GetUsers()[0].ToString());
//            Console.WriteLine(DataBase.GetUsers()[1].ToString());
//            Console.WriteLine(DataBase.GetUsers()[2].ToString());
//            Console.WriteLine(DataBase.GetUsers()[3].ToString());

            Console.WriteLine(student.Serialize());
            Console.WriteLine(student2.Serialize());
            Console.WriteLine(student3.Serialize());
            Console.WriteLine(teacher.Serialize());

//            foreach (var line in File.ReadLines("asd.txt", Encoding.UTF8))
//            {
//                Console.WriteLine("\n----------------");
//                Util.ReadListFromLine(line, "User-Courses").ForEach(Console.WriteLine);
//                Console.WriteLine("\n----------------123");
//                Util.ReadListFromLine(line, "User-Courses").ForEach(s => 
//                    Util.ReadListFromLine(s, "Course-Subjects").ForEach(Console.WriteLine));
//            }

//            Console.WriteLine("heee ---------------");

//            Console.WriteLine(teacher.GetGrades("7B", "deutsch", "martin")[0]);
//            Console.WriteLine(teacher.GetGrades("7B", "deutsch", "martin")[1]);
//            Console.WriteLine(teacher.GetGrades("7B", "deutsch", "martin")[2]);
//            Console.WriteLine(teacher.GetGrades("8B", "mathe", "rahmad")[0]);
//            Console.WriteLine(teacher.GetGrades("8B", "mathe", "rahmad")[1]);
//            Console.WriteLine(teacher.GetGrades("8B", "mathe", "rahmad")[2]);
//            Console.WriteLine(teacher.GetGrades("9B", "mathe", "ali")[0]);

//            Console.WriteLine(DataBase.IsTeacherOf("muller", "hihi", "9B", "mathe", "ali")); // is false
//            */
        }

        private static void Main(string[] args)
        {
//            Test();
            DataBase.LoadUsers();
            DataBase.LoadAppointments();

            Context ctx = new Context(new Terminal());

            new Login(ctx).Subscribe();

            SaveUser.Start();
            
            ctx.CurrentView(Context.Views.Login);
        }
    }
}