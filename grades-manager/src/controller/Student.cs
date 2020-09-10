using GradesManager.model;

namespace GradesManager.controller
{
    public class Student
    {
        private readonly Context _ctx;
        private readonly view.Student _student;

        public Student(Context ctx, view.Student student)
        {
            _ctx = ctx;
            _student = student;
        }

        public void ChangeGrade(string sel)
        {
            new view.Course(_ctx).Subscribe();
            _ctx.CurrentView(Context.Views.Course);
        }

        public void Back()
        {
            _student.Unsubscribe();
            _ctx.CurrentView(Context.Views.Login);
        }
    }
}