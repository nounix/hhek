using GradesManager.db;
using GradesManager.model;

namespace GradesManager.controller
{
    public class Subject
    {
        private readonly Context _ctx;

        public Subject(Context ctx)
        {
            _ctx = ctx;
        }

        public void AddSubject(string name)
        {
            _ctx.User.Courses[_ctx.Course].Subjects.Add(new model.Subject(name));
            DataBase.UpdateUser(_ctx.User);
            _ctx.CurrentView(Context.Views.Subject);
        }

        public void SelectSubject(string sel)
        {
            _ctx.Subject = _ctx.User.Courses[_ctx.Course].Subjects.FindIndex(subject => subject.Name.Equals(sel));

            if (_ctx.User.Type.Equals(model.Student.TYPE))
            {
                new view.Grade(_ctx).Subscribe();
                _ctx.CurrentView(Context.Views.Grade);
            }
            else
            {
                new view.SelStudent(_ctx).Subscribe();
                _ctx.CurrentView(Context.Views.Student);
            }
        }

        public void Back()
        {
            _ctx.Course = -1;
            _ctx.CurrentView(Context.Views.Course);
        }
    }
}