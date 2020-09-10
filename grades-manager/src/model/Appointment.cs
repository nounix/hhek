using GradesManager.db;
using GradesManager.util;

namespace GradesManager.model
{
    public class Appointment
    {
        private Appointment(string type, string date, string info)
        {
            Type = type;
            Date = date;
            Info = info;
        }

        private string Type { get; set; }
        private string Date { get; set; }
        private string Info { get; set; }

        public static Appointment Deserialize(string line)
        {
            var type = Util.ReadKeyFromLine(line, "Appointment-Type");
            var date = Util.ReadKeyFromLine(line, "Appointment-Date");
            var info = Util.ReadKeyFromLine(line, "Appointment-Info");

            return new Appointment(type, date, info);
        }

        public static void PrintAppointments(Terminal terminal)
        {
            foreach (var appointment in DataBase.GetAppointments()) terminal.PrintCenter(appointment.ToString());

            terminal.PrintSeparator();
        }

        private new string ToString()
        {
            return "Type: " + Type + " | Date: " + Date + " | Info: " + Info;
        }
    }
}