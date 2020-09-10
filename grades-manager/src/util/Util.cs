using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace GradesManager.util
{
    public static class Util
    {
        public static void WriteToFile(string filename, string str)
        {
            var sw = new StreamWriter(filename, false);
            sw.WriteLine(str);
            sw.Close();
        }

        public static string ReadKeyFromLine(string line, string key)
        {
            var reg = new Regex(@"=(.*?);").ToString();

            foreach (Match match in Regex.Matches(line, key + reg)) return match.Groups[1].Value;

//            throw new KeyNotFoundException();
            return "";
        }

        public static List<string> ReadListFromLine(string line, string key)
        {
            var list = new List<string>();

            var reg = new Regex(@"=\[(.*?)\]").ToString();

            foreach (Match match in Regex.Matches(line, key + reg))
            {
                var m = match.Groups[1].Value;
                if (m.Length > 0 && m.Last().Equals(',')) m = m.Substring(0, m.Length - 1);
                list.AddRange(m.Split(','));
                return list;
            }

//            throw new KeyNotFoundException();
            return new List<string>();
        }

        public static List<string> ReadObjectListFromLine(string line, string key)
        {
            var list = new List<string>();

            var reg = new Regex(@"=\[(.*)\]").ToString();

            foreach (Match match in Regex.Matches(line, key + reg))
            {
                var m = match.Groups[1].Value;
                var nestedKey = m.Split('=')[0];
                list.AddRange(m.Split(',' + nestedKey).Select(s =>
                {
                    if (s.Contains(nestedKey)) return s;
                    return nestedKey + s;
                }));
                return list;
            }

//            throw new KeyNotFoundException();
            return new List<string>();
        }
    }
}