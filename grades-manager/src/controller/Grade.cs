using GradesManager.db;
using GradesManager.model;

namespace GradesManager.controller
{
    public class Grade
    {
        private readonly Context _ctx;

        public Grade(Context ctx)
        {
            _ctx = ctx;
        }

        public void AddGrade(int value, string type, string date)
        {
            _ctx.User.Courses[_ctx.Course].Subjects[_ctx.Subject].Grades.Add(new model.Grade(value, type, date));
            DataBase.UpdateUser(_ctx.User);
            _ctx.CurrentView(Context.Views.Grade);
        }

        public void AddGradeTeacher(int value, string type, string date)
        {
            // TODO
            DataBase.UpdateUser(_ctx.User);
            _ctx.CurrentView(Context.Views.Grade);
        }

        public void SetGrade(string sel, int value, string type, string date)
        {
            _ctx.Grade = _ctx.User.Courses[_ctx.Course].Subjects[_ctx.Subject].Grades
                .FindIndex(g => g.ToString().Equals(sel));

            // triggers event in grade, context is subscribed to
            _ctx.User.Courses[_ctx.Course].Subjects[_ctx.Subject].Grades[_ctx.Grade].SetGrade(value, type, date);
        }

        public void SetGradeTeacher(string sel, int value, string type, string date)
        {
            var course = _ctx.User.Courses[_ctx.Course];
            var subject = course.Subjects[_ctx.Subject];
            var student = subject.Students[_ctx.Student];

            _ctx.Grade = DataBase.GetGrades(_ctx.User.Name, _ctx.User.Password, course.Name, subject.Name, student)
                .FindIndex(g => g.ToString().Equals(sel));

            DataBase.SetGrade(_ctx.User.Name, _ctx.User.Password, course.Name, subject.Name, student, _ctx.Grade, value,
                type, date);
        }

        public void Back()
        {
            if (_ctx.User.Type.Equals(model.Student.TYPE))
            {
                _ctx.Subject = -1;
                _ctx.CurrentView(Context.Views.Subject);
            }
            else
            {
                _ctx.Student = -1;
                _ctx.CurrentView(Context.Views.Student);
            }
        }
    }
}