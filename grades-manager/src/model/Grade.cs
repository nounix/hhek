using System.Collections.Generic;
using GradesManager.util;

namespace GradesManager.model
{
    public class Grade
    {
        public delegate void EventHandler();

        public event EventHandler Event;

        public Grade(int points, string type, string date)
        {
            Value = points;
            Type = type;
            Date = date;

            Event?.Invoke();
        }

        public int Value { get; set; }
        public string Type { get; set; }
        public string Date { get; set; }


        public void SetGrade(int value, string type, string date)
        {
            Value = value;
            Type = type;
            Date = date;

            Event?.Invoke();
        }

        public string Serialize()
        {
            return "Grade-Value=" + Value + ";Grade-Type=" + Type + ";Grade-Date=" + Date + ";";
        }

        public static List<Grade> Deserialize(List<string> grades)
        {
            var gradesObj = new List<Grade>();

            if (grades.Count < 1 || !grades[0].Contains("Grade-Value")) return gradesObj;

            foreach (var grade in grades)
            {
                var value = int.Parse(Util.ReadKeyFromLine(grade, "Grade-Value"));
                var type = Util.ReadKeyFromLine(grade, "Grade-Type");
                var date = Util.ReadKeyFromLine(grade, "Grade-Date");

                gradesObj.Add(new Grade(value, type, date));
            }

            return gradesObj;
        }

        public new string ToString()
        {
            return "Value = " + Value + ", Type = " + Type + ", Date = " + Date;
        }
    }
}