using GradesManager.db;
using GradesManager.model;

namespace GradesManager.controller
{
    public class Login
    {
        private readonly Context _ctx;

        public Login(Context ctx)
        {
            _ctx = ctx;
        }

        public bool TryLogin(string name, string password)
        {
            return DataBase.UserExists(name, password);
        }

        public void DoLogin(string name, string password)
        {
            _ctx.User = DataBase.GetUserByNameAndPassword(name, password);
            // Subscribe for grade changes
            _ctx.Subscribe();

            if (_ctx.User.Type.Equals(model.Student.TYPE))
                new view.Student(_ctx).Subscribe();
            else
                new view.Teacher(_ctx).Subscribe();

            _ctx.CurrentView(Context.Views.User);
        }

        public bool Register(string type, string name, string password)
        {
            if (!type.Equals(model.Student.TYPE) && !type.Equals(model.Teacher.TYPE) || DataBase.UserExists(name))
                return false;

            if (type.Equals(model.Student.TYPE))
                DataBase.InsertUser(new model.Student(name, password));
            else if (type.Equals(model.Teacher.TYPE))
                DataBase.InsertUser(new model.Teacher(name, password));

            return true;
        }
    }
}