using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace fraction
{
    public static class Util
    {
        public static int Gcd(int a, int b)
        {
            return b == 0 ? a : Gcd(b, a % b);
        }

        public static void WriteToFile(string filename, string str)
        {
            var sw = new StreamWriter(filename, true);
            sw.WriteLine(str);
            sw.Close();
        }

        public static string ReadKeyFromFile(string filename, long id, char k)
        {
            if (new FileInfo(filename).Length > 100) throw new FileLoadException("File to big!");
            
            var r1 = new Regex(@"^\s*id=").ToString();
            var r2 = new Regex(@";.*").ToString();
            var r3 = new Regex(@"=(\d*);").ToString();

            foreach (var line in File.ReadLines(filename, Encoding.UTF8))
            {
                foreach (Match match in Regex.Matches(line, r1 + id + r2 + k + r3, RegexOptions.IgnoreCase))
                {
                    return match.Groups[1].Value;
                }
            }
            
            throw new KeyNotFoundException();
        }
    }
}