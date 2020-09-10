using GradesManager.db;
using GradesManager.util;

namespace GradesManager.model
{
    public class Context
    {
        public delegate void EventHandler();

        public event EventHandler LoginEvent;
        public event EventHandler UserEvent;
        public event EventHandler CourseEvent;
        public event EventHandler SubjectEvent;
        public event EventHandler StudentEvent;
        public event EventHandler GradeEvent;

        public enum Views
        {
            Login,
            User,
            Course,
            Subject,
            Student,
            Grade
        }


        private int _currentView;

        public Context(Terminal terminal)
        {
            Course = -1;
            Subject = -1;
            Student = -1;
            Grade = -1;

            Terminal = terminal;
        }

        public Terminal Terminal { get; set; }

        public User User { get; set; }

        public int Course { get; set; }
        public int Subject { get; set; }
        public int Student { get; set; }
        public int Grade { get; set; }

        public int CurrentView()
        {
            return _currentView;
        }

        public void CurrentView(Views view)
        {
            _currentView = (int) view;

            switch (view)
            {
                case Views.Login:
                    LoginEvent?.Invoke();
                    break;
                case Views.User:
                    UserEvent?.Invoke();
                    break;
                case Views.Course:
                    CourseEvent?.Invoke();
                    break;
                case Views.Subject:
                    SubjectEvent?.Invoke();
                    break;
                case Views.Student:
                    StudentEvent?.Invoke();
                    break;
                case Views.Grade:
                    GradeEvent?.Invoke();
                    break;
            }
        }

        public void Subscribe()
        {
            if (User.Type.Equals(model.Student.TYPE))
            {
                User.Courses.ForEach(c =>
                    c.Subjects?.ForEach(s => s.Grades.ForEach(g => g.Event += () =>
                    {
                        Grade = -1;
                        DataBase.UpdateUser(User);
                        CurrentView(Views.Grade);
                    })));
            }
            else
            {
                User.Courses.ForEach(c => c.Subjects.ForEach(s => s.Students.ForEach(student =>
                    DataBase.GetGrades(User.Name, User.Password, c.Name, s.Name, student)
                        ?.ForEach(g => g.Event += () =>
                        {
                            Grade = -1;
                            DataBase.UpdateUser(User);
                            CurrentView(Views.Grade);
                        }))));
            }
        }

        public void PrintContext(Terminal terminal)
        {
            terminal.PrintCenter("User: " + User.Name);
            if (Course != -1) terminal.PrintCenter("Course: " + User.Courses[Course].Name);
            if (Subject != -1) terminal.PrintCenter("Subject: " + User.Courses[Course].Subjects[Subject].Name);
            if (Student != -1)
                terminal.PrintCenter("Student: " + User.Courses[Course].Subjects[Subject].Students[Student]);
            if (Grade != -1)
                terminal.PrintCenter("Grade: " + User.Courses[Course].Subjects[Subject].Grades[Grade].Date);

            terminal.PrintSeparator();
            // TODO: add jahresendnote
        }
    }
}