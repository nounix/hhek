using System;
using System.IO;

namespace fraction
{
    public class Fraction
    {
        private static long idCounter;
        private readonly long serialID;
        
        private int denominator;
        private int counter;
        
        // Constructor
        public Fraction(int denominator, int counter)
        {
            serialID = idCounter;
            this.denominator = denominator;
            this.counter = counter;
            idCounter++;
        }

        // Define + op
        public static Fraction operator +(Fraction b1, Fraction b2)
        {
            if (b1.counter == b2.counter)
                return new Fraction(b1.denominator + b2.denominator, b1.counter);

            ToSameCounter(ref b1, ref b2);

            return new Fraction(b1.denominator + b2.denominator, b1.counter);
        }

        // Define + op
        public static Fraction operator +(int num, Fraction b2)
        {
            return new Fraction(num, 1) + b2;
        }

        // Define - op
        public static Fraction operator -(Fraction b1, Fraction b2)
        {
            if (b1.counter == b2.counter)
                return new Fraction(b1.denominator - b2.denominator, b1.counter);

            ToSameCounter(ref b1, ref b2);

            return new Fraction(b1.denominator - b2.denominator, b1.counter);
        }

        // Define * op
        public static Fraction operator *(Fraction b1, Fraction b2)
        {
            return new Fraction(b1.denominator * b2.denominator, b1.counter * b2.counter);
        }

        // Define / op 
        public static Fraction operator /(Fraction b1, Fraction b2)
        {
            return b1 * Swap(b2);
        }

        // Override ToString
        public override string ToString()
        {
            Shorten();
            return denominator + "/" + counter;
        }

        private static void ToSameCounter(ref Fraction b1, ref Fraction b2)
        {
            if (b1.counter == b2.counter) return;

            var b1Counter = b1.counter;
            var b2Counter = b2.counter;
            b1.denominator *= b2Counter;
            b2.denominator *= b1Counter;
            b1.counter *= b2Counter;
            b2.counter *= b1Counter;
        }

        private static Fraction Swap(Fraction b)
        {
            return new Fraction(b.counter, b.denominator);
        }

        private void Shorten()
        {
            var gcd = Util.Gcd(denominator, counter);
            denominator /= gcd;
            counter /= gcd;
        }

        public string Serialize()
        {
            return "id=" + serialID + ";d=" + denominator + ";c=" + counter + ";";
        }

        public static Fraction Deserialize(string filename, long id)
        {
            var d = Util.ReadKeyFromFile(filename, id, 'd');
            var c = Util.ReadKeyFromFile(filename, id, 'c');
            return new Fraction(int.Parse(d), int.Parse(c));
        }
    }
}