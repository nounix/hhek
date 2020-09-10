using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace battle_ship.dependencies.util
{
    public static class Util
    {
        public static List<string> ReadLines(string path)
        {
            return File.ReadLines(path, Encoding.UTF8).ToList();
        }

        public static void WriteLine(string path, string str)
        {
            var sw = new StreamWriter(path, true);
            sw.WriteLine(str);
            sw.Close();
        }
    }
}