using System;
using System.Collections.Generic;
using GradesManager.model;
using GradesManager.util;

namespace GradesManager.view
{
    public class Student
    {
        private readonly Context _ctx;
        private readonly Terminal _terminal;
        private readonly controller.Student _controller;

        public Student(Context ctx)
        {
            _ctx = ctx;
            _terminal = ctx.Terminal;
            _controller = new controller.Student(ctx, this);
        }

        public void Subscribe()
        {
            _ctx.UserEvent += Render;
        }
        
        public void Unsubscribe()
        {
            _ctx.UserEvent -= Render;
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

            var options = new List<Tuple<string, Action<string>>>
            {
                new Tuple<string, Action<string>>("Change Grade", _controller.ChangeGrade)
            };

            _terminal.PrintCenter("Select option:");

            _terminal.SelectOption(options, _controller.Back);
        }
    }
}