using GradesManager.model;

namespace GradesManager.controller
{
    public class Teacher
    {
        private readonly Context _ctx;
        private readonly view.Teacher _teacher;

        public Teacher(Context ctx, view.Teacher teacher)
        {
            _ctx = ctx;
            _teacher = teacher;
        }

        public void ChangeGrade(string sel)
        {
            new view.Course(_ctx).Subscribe();
            _ctx.CurrentView(Context.Views.Course);
        }

        public void Back()
        {
            _teacher.Unsubscribe();
            _ctx.CurrentView(Context.Views.Login);
        }
    }
}