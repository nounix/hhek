using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace battle_ship.dependencies.framework.ui
{
    public class Terminal
    {
        private const ConsoleColor ConsoleColorDefault = ConsoleColor.Gray;

        public Terminal()
        {
            Left = Console.WindowWidth / 3;
            Top = Console.WindowHeight / 8;

            ConsoleColor = ConsoleColorDefault;
            Console.CursorVisible = false;
            Console.Clear();
        }

        private int Left { get; set; }
        private int Top { get; set; }
        private ConsoleColor ConsoleColor { get; set; }

        ~Terminal()
        {
            Console.ForegroundColor = ConsoleColorDefault;
            Console.CursorVisible = true;
            Console.Clear();
        }


        public void Clear()
        {
            Left = Console.WindowWidth / 3;
            Top = Console.WindowHeight / 8;

            Console.ForegroundColor = ConsoleColorDefault;
            Console.Clear();
        }

        public void PrintCenter(string str)
        {
            Console.ForegroundColor = ConsoleColor;
            Console.SetCursorPosition(Left, Top++);
            Console.Write(str);
        }

        public string ReadCenter()
        {
            Console.SetCursorPosition(Left, Top++);
            return Console.ReadLine();
        }

        public void PrintSeparator()
        {
            Console.SetCursorPosition(0, ++Top);

            for (var i = 0; i < Console.WindowWidth; i++) Console.Write("#");

            Top += 2;
        }

        public void PrintLoading(string msg)
        {
            Console.SetCursorPosition(Left, Top);

            Console.Write(msg);

            Console.SetCursorPosition(Left, Top + 1);

            for (var i = 0; i < msg.Length; i++)
            {
                Console.Write(".");
                Thread.Sleep(100);
            }

            // clear message
            Console.SetCursorPosition(Left, Top);
            for (var i = 0; i < msg.Length; i++) Console.Write(" ");

            // clear loading bar
            Console.SetCursorPosition(Left, Top + 1);
            for (var i = 0; i < msg.Length; i++) Console.Write(" ");
        }

        private Action<int> PrintOptions(IReadOnlyList<string> options)
        {
            return delegate(int selected)
            {
                var start = Top;

                foreach (var option in options)
                {
                    ConsoleColor = option.Equals(options[selected]) ? ConsoleColor.Green : ConsoleColorDefault;
                    PrintCenter(option);
                }

                // revert back to default
                ConsoleColor = ConsoleColorDefault;
                Top = start;
            };
        }

        public string SelectOption(List<string> options, Action back)
        {
            var selected = 0;
            var isDone = false;
            var printOptions = PrintOptions(options);

            printOptions(selected);

            while (!isDone)
            {
                while (!Console.KeyAvailable)
                {
                }

                switch (Console.ReadKey(true).Key)
                {
                    case ConsoleKey.UpArrow:
                    {
                        if (selected > 0) printOptions(--selected);
                        break;
                    }

                    case ConsoleKey.DownArrow:
                    {
                        if (selected < options.Count - 1) printOptions(++selected);
                        break;
                    }

                    case ConsoleKey.RightArrow:
                    {
                        return options[selected];
                    }

                    case ConsoleKey.LeftArrow:
                    {
                        isDone = true;
                        back?.Invoke();
                        break;
                    }
                }
            }

            return "";
        }

        public void SelectOption(List<Tuple<string, Action>> options, Action back)
        {
            var selected = 0;
            var isDone = false;
            var printOptions = PrintOptions(options.Select(option => option.Item1).ToList());

            printOptions(selected);

            while (!isDone)
            {
                while (!Console.KeyAvailable)
                {
                }

                switch (Console.ReadKey(true).Key)
                {
                    case ConsoleKey.UpArrow:
                    {
                        if (selected > 0) printOptions(--selected);
                        break;
                    }

                    case ConsoleKey.DownArrow:
                    {
                        if (selected < options.Count - 1) printOptions(++selected);
                        break;
                    }

                    case ConsoleKey.RightArrow:
                    {
                        isDone = true;
                        options[selected].Item2?.Invoke();
                        break;
                    }

                    case ConsoleKey.LeftArrow:
                    {
                        isDone = true;
                        back?.Invoke();
                        break;
                    }
                }
            }
        }

        public void SelectOption(List<Tuple<string, Action<string>>> options, Action back)
        {
            var selected = 0;
            var isDone = false;
            var printOptions = PrintOptions(options.Select(option => option.Item1).ToList());

            printOptions(selected);

            while (!isDone)
            {
                while (!Console.KeyAvailable)
                {
                }

                switch (Console.ReadKey(true).Key)
                {
                    case ConsoleKey.UpArrow:
                    {
                        if (selected > 0) printOptions(--selected);
                        break;
                    }

                    case ConsoleKey.DownArrow:
                    {
                        if (selected < options.Count - 1) printOptions(++selected);
                        break;
                    }

                    case ConsoleKey.RightArrow:
                    {
                        isDone = true;
                        options[selected].Item2?.Invoke(options[selected].Item1);
                        break;
                    }

                    case ConsoleKey.LeftArrow:
                    {
                        isDone = true;
                        back?.Invoke();
                        break;
                    }
                }
            }
        }
    }
}