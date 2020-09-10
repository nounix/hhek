using GradesManager.db;
using GradesManager.model;

namespace GradesManager.controller
{
    public class SelStudent
    {
        private readonly Context _ctx;

        public SelStudent(Context ctx)
        {
            _ctx = ctx;
        }

        public void AddStudent(string name)
        {
            _ctx.User.Courses[_ctx.Course].Subjects[_ctx.Subject].Students.Add(name);
            DataBase.UpdateUser(_ctx.User);
            _ctx.CurrentView(Context.Views.Student);
        }

        public void SelectStudent(string sel)
        {
            _ctx.Student = _ctx.User.Courses[_ctx.Course].Subjects[_ctx.Subject].Students.FindIndex(s => s.Equals(sel));

            new view.Grade(_ctx).Subscribe();
            _ctx.CurrentView(Context.Views.Grade);
        }

        public void Back()
        {
            _ctx.Subject = -1;
            _ctx.CurrentView(Context.Views.Subject);
        }
    }
}