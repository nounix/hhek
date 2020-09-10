using GradesManager.db;
using GradesManager.model;

namespace GradesManager.controller
{
    public class Course
    {
        private readonly Context _ctx;

        public Course(Context ctx)
        {
            _ctx = ctx;
        }

        public void AddCourse(string name)
        {
            _ctx.User.Courses.Add(new model.Course(name));
            DataBase.UpdateUser(_ctx.User);
            _ctx.CurrentView(Context.Views.Course);
        }

        public void SelectCourse(string sel)
        {
            _ctx.Course = _ctx.User.Courses.FindIndex(course => course.Name.Equals(sel));

            new view.Subject(_ctx).Subscribe();
            _ctx.CurrentView(Context.Views.Subject);
        }

        public void Back()
        {
            _ctx.CurrentView(Context.Views.User);
        }
    }
}