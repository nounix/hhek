using System;
using System.Collections.Generic;
using GradesManager.model;
using GradesManager.util;

namespace GradesManager.view
{
    public class Login
    {
        private readonly Context _ctx;
        private readonly Terminal _terminal;
        private readonly controller.Login _controller;

        public Login(Context ctx)
        {
            _ctx = ctx;
            _terminal = ctx.Terminal;
            _controller = new controller.Login(ctx);
        }

        public void Subscribe()
        {
            _ctx.LoginEvent += Render;
        }

        private void Setup()
        {
            _terminal.Clear();

            Appointment.PrintAppointments(_terminal);
        }

        private void Render()
        {
            Setup();

            var options = new List<Tuple<string, Action<string>>>
            {
                new Tuple<string, Action<string>>("Login", DoLogin),
                new Tuple<string, Action<string>>("Register", Register)
            };

            _terminal.SelectOption(options, null);
        }

        private void DoLogin(string sel)
        {
            _terminal.Clear();

            _terminal.PrintCenter("Enter name:");
            var name = _terminal.ReadCenter();
            _terminal.PrintCenter("Enter password");
            var password = _terminal.ReadCenter();

            if (_controller.TryLogin(name, password))
                _controller.DoLogin(name, password);
            else
                _terminal.PrintCenter("Wrong Credentials!");
        }

        private void Register(string sel)
        {
            _terminal.Clear();

            _terminal.PrintCenter("Select user type:");

            var options = new List<string>
            {
                "Student",
                "Teacher"
            };

            var type = _terminal.SelectOption(options, null);

            _terminal.Clear();

            _terminal.PrintCenter("Enter name:");
            var name = _terminal.ReadCenter();
            _terminal.PrintCenter("Enter password");
            var password = _terminal.ReadCenter();

            var registered = _controller.Register(type, name, password);
            _terminal.PrintCenter(registered ? "Registered successful! You can now login." : "Registration failed!");
        }
    }
}