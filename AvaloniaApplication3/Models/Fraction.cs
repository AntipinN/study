using System;

namespace AvaloniaApplication3.Models
{
    public class Fraction
    {
        private long denominator;
        public long Numerator { get; set; }
        public long Denominator 
        {
            get { return denominator; }
            set { if (value == 0) { throw new DivideByZeroException("Class Fraction was given zero in denominator!"); }
                   else denominator = value;
                }
        }
        public Fraction(long numerator, long denominator)
        {
            Numerator = numerator;
            Denominator = denominator;
        }
        public Fraction(long numerator)
        {
            Numerator = numerator;
            Denominator = 1;
        }
        /// To do
        /// Реализовать приведение числа к дроби
        /// 
        /// 
        public static Fraction operator * (Fraction a, Fraction b)
        {
            return !new Fraction(a.Numerator * b.Numerator, a.Denominator * b.Denominator);
        }
        public static Fraction operator * (Fraction a, long b)
        {
            return !new Fraction(a.Numerator * b, a.Denominator);
        }
        public static Fraction operator * (long a, Fraction b)
        {
            return !new Fraction(a * b.Numerator, b.Denominator);
        }
        public static Fraction operator + (Fraction a, Fraction b)
        {
            Fraction c = new Fraction(a.Numerator * b.Denominator + b.Numerator * a.Denominator , b.Denominator * a.Denominator);
            return !c;
        }
        public static Fraction operator + (Fraction a, long b)
        {
            return !new Fraction(a.Numerator + b * a.Denominator, a.Denominator);
        }
        public static Fraction operator + (long a, Fraction b)
        {
            return !new Fraction(a * b.Denominator + b.Numerator, b.Denominator);
        }
        public static Fraction operator - (Fraction a, Fraction b)
        {
            return !new Fraction(a.Numerator * b.Denominator - (a.Denominator * b.Numerator), a.Denominator * b.Denominator);
        }
        public static Fraction operator - (Fraction a, long b)
        {
            return !new Fraction(a.Numerator - b*a.Denominator, a.Denominator);
        }
        public static Fraction operator - (long a, Fraction b)
        {
            return !new Fraction(a*b.Denominator - b.Numerator, b.Denominator);
        }
        public static Fraction operator / (Fraction a, Fraction b)
        {
            return !new Fraction(a.Numerator * b.Denominator, a.Denominator * b.Numerator);
        }
        public static Fraction operator / (Fraction a, long b)
        {
            if (b == 0)
            {
                throw new DivideByZeroException("Argument 'long' is zero");
            }
            else if (b < 0)
            {
                return !new Fraction(-a.Numerator, a.Denominator * (-b));
            }
            else
            {
                return !new Fraction(a.Numerator, a.Denominator * b);
            }
        }
        public static Fraction operator / (long a, Fraction b)
        {
            return !new Fraction(a * b.Denominator, b.Numerator);
        }

        public static Fraction operator ! (Fraction a)
        {
            long gcd = GCD(Math.Abs(a.Numerator), Math.Abs(a.Denominator));
            if (a.Denominator < 0)
            {
                a.Numerator = a.Numerator * (-1);
                a.Denominator = Math.Abs(a.Denominator);
            }
            return new Fraction(a.Numerator / gcd, a.Denominator / gcd);
        }
        public static bool operator ==(Fraction a, Fraction b)
        {
            if ((a - b).Numerator == 0)
            {
                return true;
            }
            return false;

        }
        public static bool operator !=(Fraction a, Fraction b)
        {
            if ((a - b).Numerator != 0)
            {
                return true;
            }
            return false;

        }

        public bool Equals(Fraction a, Fraction b)                             //переопределяем сравнение для данного класса
        {
            if ((a - b).Numerator == 0)
            {
                return true;
            }
            return false;
        }

        static long GCD(long a, long b)
        {
            long remainer = -1;
            while (remainer != 0)
            {
                remainer = a % b;
                a = b;
                b = remainer;
            }
            return a;
        }
        public long GcD(long a, long b)
        {
            return GCD(a, b);
        }
        public override string ToString()
        {
            return $"{Numerator.ToString()}\n" + '/' + $"\n{Denominator.ToString()}";
        }
    }
}
