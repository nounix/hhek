using System;
using System.Collections.Generic;
using GradesManager.model;
using GradesManager.util;

namespace GradesManager.view
{
    public class Course
    {
        private readonly Context _ctx;
        private readonly Terminal _terminal;
        private readonly controller.Course _controller;

        public Course(Context ctx)
        {
            _ctx = ctx;
            _terminal = ctx.Terminal;
            _controller = new controller.Course(ctx);
        }

        public void Subscribe()
        {
            _ctx.CourseEvent += Render;
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

            options.Add(new Tuple<string, Action<string>>("Add", AddCourse));

            _ctx.User.Courses.ForEach(course =>
                options.Add(new Tuple<string, Action<string>>(course.Name, _controller.SelectCourse)));

            _terminal.PrintCenter("Course:");

            _terminal.SelectOption(options, _controller.Back);
        }

        private void AddCourse(string sel)
        {
            _terminal.Clear();

            _terminal.PrintCenter("Enter name:");

            _controller.AddCourse(_terminal.ReadCenter());
        }
    }
}