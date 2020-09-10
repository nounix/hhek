using System;

namespace fraction
{
    internal static class Program
    {
        public static void Main()
        {
            const string filename = "fraction.bak";

            var br1 = new Fraction(1,4);
            var br2 = new Fraction(2, 5);
            var br3 = br1 * br2;

            Console.WriteLine("br1: " + br1);
            Console.WriteLine("br2: " + br2);
            Console.WriteLine("br3: " + br3);
               
            // Serialize
            Util.WriteToFile(filename, br1.Serialize());
            Util.WriteToFile(filename, br2.Serialize());
            
            // Deserialize
            var br4 = Fraction.Deserialize(filename, 0);
            var br5 = Fraction.Deserialize(filename, 1);
            
            Console.WriteLine("br4: " + br4);
            Console.WriteLine("br5: " + br5);
        }
    }
}