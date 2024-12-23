using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;


namespace AvaloniaApplication3.Models
{
    public class FractionEquasionSolver : ISolver //клас для считания дробей
    { 
        public Fraction Solve(List<string> OPS, Dictionary<string, Fraction> fraction_substitutions)
        {
        Stack<Fraction> fractions = new Stack<Fraction>();
        Fraction fraction_prom1 = new Fraction(1, 1);         //просто промежуточная дробь,чтобы в каждом case не создавать новую
        Fraction fraction_prom2 = new Fraction(1, 1);

        foreach (string symbol in OPS)
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

                    if (char.IsDigit(symbol[0]))   //если число, то привращаем в дробь (проверяем по средством того,юуква это или нет,так как если это буква то это заменённая дробь)
                    {
                        fraction_prom1 = new Fraction(long.Parse(symbol), 1);
                        fractions.Push(fraction_prom1);
                    }
                    else
                    {
                        Fraction f = fraction_substitutions[symbol];
                        fractions.Push(new Fraction(f.Numerator, f.Denominator));
                    }
                    break;
            }
        }

        return fractions.Pop();

        }
    }
}
