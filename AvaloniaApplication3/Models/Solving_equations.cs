using System.Collections.Generic;


namespace AvaloniaApplication3.Models
{
    internal class FractionEquasionSolver : ISolver //клас для считания дробей
    { 
        public Fraction Solve(List<object> OPS)
        {
        Stack<Fraction> fractions = new Stack<Fraction>();
        Fraction fraction_prom1 = new Fraction(1, 1);         //просто промежуточная дробь,чтобы в каждом case не создавать новую
        Fraction fraction_prom2 = new Fraction(1, 1);

        foreach (object symbol in OPS)
        {
            switch (symbol)
            {
                case "+":
                    fraction_prom1 = fractions.Pop();
                    fraction_prom2 = fractions.Pop();
                    fractions.Push(fraction_prom1 + fraction_prom2);
                    break;

                case "-":
                    fraction_prom1 = fractions.Pop();
                    fraction_prom2 = fractions.Pop();
                    fractions.Push(fraction_prom2 - fraction_prom1);
                    break;

                case "*":
                    fraction_prom1 = fractions.Pop();
                    fraction_prom2 = fractions.Pop();
                    fractions.Push(fraction_prom2 * fraction_prom1);
                    break;

                case "/":
                    fraction_prom1 = fractions.Pop();
                    fraction_prom2 = fractions.Pop();
                    fractions.Push(fraction_prom2 / fraction_prom1);
                    break;

                default:  //если не оператор, то это либо число, либо дробь

                    if (!(symbol is Fraction))   //если число, то привращаем в дробь
                    {
                        fraction_prom1 = new Fraction((long)symbol, 1);
                        fractions.Push(fraction_prom1);
                    }
                    else
                    {
                        Fraction f = (Fraction)symbol;
                        fractions.Push(new Fraction(f.Numerator, f.Denominator));
                    }
                    break;
            }
        }

        return fractions.Pop();

        }
    }
}
