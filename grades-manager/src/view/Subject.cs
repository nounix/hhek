using System;
using System.Collections.Generic;
using GradesManager.model;
using GradesManager.util;

namespace GradesManager.view
{
    public class Subject
    {
        private readonly Context _ctx;
        private readonly Terminal _terminal;
        private readonly controller.Subject _controller;

        public Subject(Context ctx)
        {
            _ctx = ctx;
            _terminal = ctx.Terminal;
            _controller = new controller.Subject(ctx);
        }

        public void Subscribe()
        {
            _ctx.SubjectEvent += Render;
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

            options.Add(new Tuple<string, Action<string>>("Add", AddSubject));

            _ctx.User.Courses[_ctx.Course].Subjects?.ForEach(subject =>
                options.Add(new Tuple<string, Action<string>>(subject.Name, _controller.SelectSubject)));

            _terminal.PrintCenter("Select subject:");

            _terminal.SelectOption(options, _controller.Back);
        }

        private void AddSubject(string sel)
        {
            _terminal.Clear();

            _terminal.PrintCenter("Enter name:");
            _controller.AddSubject(_terminal.ReadCenter());
        }
    }
}