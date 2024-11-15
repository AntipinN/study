using System;
using System.Collections.Generic;

namespace AvaloniaApplication3.Models
{
    

    internal static class SumOfFractionGame
    {
        private const int MaxEasyNumerator = 15;
        private const int MaxMediumNumerator = 30;
        private const int MaxHardNumerator = 50;
        private const int MultiplayerCoefficientForDenominatorRange = 2;

        public class GameGenerator
        {
            IEquasionGenerator _IGenerator;
            ISolver _ISolver;
            IOPZ _IOPZ;
            public GameGenerator(IEquasionGenerator gen, ISolver solv, IOPZ opz) 
            {
                _IGenerator = gen;
                _ISolver = solv;
                _IOPZ = opz;
            }

            public List<object> GetExpression()
            {
                List<object> result = _IGenerator.GenerateEquasion();
                
                result.Add(_ISolver.Solve(_IOPZ.CalculateOPZ(result)));
                result.Insert(result.Count - 1, "=");
                return result;
            }
            
        }
        
        public class EasyDifficultyEquasion: IEquasionGenerator
        {
            public List<object> GenerateEquasion()
            {
                
                Random rnd = new Random();

                List<object> equasion = new List<object>();
                for (int i = 0; i < 4; i++)
                {
                    int numerator = rnd.Next(0, MaxEasyNumerator);
                    int denominator = rnd.Next(1, 1 + numerator * MultiplayerCoefficientForDenominatorRange);
                    equasion.Add(new Fraction(numerator, denominator));
                    equasion.Add("+");
                }
                equasion.RemoveAt(equasion.Count - 1);
                return equasion;
            }
        }
        public class MediumDifficultyEquasion: IEquasionGenerator
        {
            public List<object> GenerateEquasion()
            {
                Random rnd = new Random();

                List<object> equasion = new List<object>();
                for (int i = 0; i < rnd.Next(2, 4); i++)
                {
                    int numerator = rnd.Next(0, MaxMediumNumerator);
                    int denominator = rnd.Next(numerator, numerator * MultiplayerCoefficientForDenominatorRange);
                    equasion.Add(new Fraction(numerator, denominator));
                }

                return equasion;
            }

        }
        public class HighDifficultyEquasion : IEquasionGenerator
        {
            public List<object> GenerateEquasion()
            {
                Random rnd = new Random();

                List<object> equasion = new List<object>();
                for (int i = 0; i < rnd.Next(2, 4); i++)
                {
                    int numerator = rnd.Next(0, MaxHardNumerator);
                    int denominator = rnd.Next(numerator, numerator * MultiplayerCoefficientForDenominatorRange);
                    equasion.Add(new Fraction(numerator, denominator));
                }

                return equasion;
            }
        }
        class GameCurrentData
        {

        }

    }
}
