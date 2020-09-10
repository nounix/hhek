using System;
using System.Collections.Generic;
using GradesManager.db;
using GradesManager.model;
using GradesManager.util;

namespace GradesManager.view
{
    public class Grade
    {
        private readonly Context _ctx;
        private readonly Terminal _terminal;
        private readonly controller.Grade _controller;

        public Grade(Context ctx)
        {
            _ctx = ctx;
            _terminal = ctx.Terminal;
            _controller = new controller.Grade(_ctx);
        }

        public void Subscribe()
        {
            _ctx.GradeEvent += Render;
        }

        private void Setup()
        {
            _terminal.Clear();
            Appointment.PrintAppointments(_terminal);
            _ctx.PrintContext(_terminal);
        }

        private void Render()
        {
            Setup();

            var options = new List<Tuple<string, Action<string>>>();

            options.Add(new Tuple<string, Action<string>>("Add", AddGrade));

            List<model.Grade> grades;

            if (_ctx.User.Type.Equals(model.Student.TYPE))
            {
                grades = _ctx.User.Courses[_ctx.Course].Subjects[_ctx.Subject].Grades;
            }
            else
            {
                var course = _ctx.User.Courses[_ctx.Course];
                var subject = course.Subjects[_ctx.Subject];
                var student = subject.Students[_ctx.Student];
                grades = DataBase.GetGrades(_ctx.User.Name, _ctx.User.Password, course.Name, subject.Name, student);
            }

            grades?.ForEach(grade =>
                options.Add(new Tuple<string, Action<string>>(grade.ToString(), SetGrade)));

            _terminal.PrintCenter("Select grade:");

            _terminal.SelectOption(options, _controller.Back);
        }

        private void AddGrade(string sel)
        {
            _terminal.Clear();

            _terminal.PrintCenter("Enter value:");
            var value = int.Parse(_terminal.ReadCenter());
            _terminal.PrintCenter("Enter type:");
            var type = _terminal.ReadCenter();
            _terminal.PrintCenter("Enter date:");
            var date = _terminal.ReadCenter();

            if (_ctx.User.Type.Equals(model.Student.TYPE))
                _controller.AddGrade(value, type, date);
            else
                _controller.AddGradeTeacher(value, type, date);
        }

        private void SetGrade(string sel)
        {
            _terminal.Clear();

            _terminal.PrintCenter("Enter value:");
            var value = int.Parse(_terminal.ReadCenter());
            _terminal.PrintCenter("Enter type:");
            var type = _terminal.ReadCenter();
            _terminal.PrintCenter("Enter date:");
            var date = _terminal.ReadCenter();

            if (_ctx.User.Type.Equals(model.Student.TYPE))
                _controller.SetGrade(sel, value, type, date);
            else
                _controller.SetGradeTeacher(sel, value, type, date);
        }
    }
}