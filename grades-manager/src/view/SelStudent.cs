using System;
using System.Collections.Generic;
using GradesManager.model;
using GradesManager.util;

namespace GradesManager.view
{
    public class SelStudent
    {
        private readonly Context _ctx;
        private readonly Terminal _terminal;
        private readonly controller.SelStudent _controller;

        public SelStudent(Context ctx)
        {
            _ctx = ctx;
            _terminal = ctx.Terminal;
            _controller = new controller.SelStudent(_ctx);
        }

        public void Subscribe()
        {
            _ctx.StudentEvent += Render;
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

            options.Add(new Tuple<string, Action<string>>("Add", AddStudent));

            _ctx.User.Courses[_ctx.Course].Subjects[_ctx.Subject].Students.ForEach(student =>
                options.Add(new Tuple<string, Action<string>>(student, _controller.SelectStudent)));

            _terminal.PrintCenter("Select student:");

            _terminal.SelectOption(options, _controller.Back);
        }

        private void AddStudent(string sel)
        {
            _terminal.Clear();

            _terminal.PrintCenter("Enter name:");
            _controller.AddStudent(_terminal.ReadCenter());
        }
    }
}