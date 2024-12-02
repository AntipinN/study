using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace AvaloniaApplication3.Models
{
    

    internal static class SumOfFractionGame
    {
        private const int MaxEasyNumerator = 15;
        private const int MaxMediumNumerator = 30;
        private const int MaxHardNumerator = 50;
        private const int MultiplayerCoefficientForDenominatorRange = 2;
        private const int PlusSubtractionCoefficientForDenominatorRange = 10;


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

        public static List<object> Generator_Equasion_Sum_Multiplication(int Lower, int Top, string sign, int MaxNumer, int MaxDenom) //метод позволяющий по введённым параметрам сгенерировать дроби для равенства(для + и умножения)
        {
            Random rnd = new Random();

            List<object> equasion = new List<object>();
            int number_of_fractions = rnd.Next(Lower, Top + 1);
            for (int i = 0; i < number_of_fractions; i++)   //добавил +1, чтобы логичней были промежутки, так промежуток от 2 до 2, будет в рандомк от 2 до 3, три не включая и так будет точно 2
            {
                int numerator = rnd.Next(1, MaxNumer);
                int denominator = rnd.Next(numerator, numerator * MaxDenom);
                equasion.Add(new Fraction(numerator, denominator));
                equasion.Add(sign);
            }
            equasion.RemoveAt(equasion.Count - 1);
            return equasion;
        }

        public static List<object> Generator_Equasion_Subtraction(int Lower, int Top, int MaxNumer, int MaxDenom) //метод позволяющий по введённым параметрам сгенерировать дроби для равенства c разностью
        {
            Random rnd = new Random();
            Fraction Fraction_calculated; //дробь, для создания новой дроби,в этой дроби лежит текущий ответ (разность дробей)
            Fraction Fraction_prom;       //дробь для подсчёта новой Fraction_calculated

            List<object> equasion = new List<object>();
            
            int numerator = rnd.Next(1, MaxNumer);
            int denominator = rnd.Next(1+numerator, numerator + MaxDenom);
            Fraction_calculated = new Fraction(numerator, denominator);
            equasion.Add(Fraction_calculated);
            equasion.Add("-");

            int number_of_fractions = rnd.Next(Lower, Top + 1); //добавил +1, чтобы логичней были промежутки

            for (int i = 1; i < number_of_fractions; i++) //начинаем с одного, так как одну дробь мы уже создали
            {
                numerator = rnd.Next(1, (int)Fraction_calculated.Numerator);  
                denominator = rnd.Next(((int)Fraction_calculated.Denominator) + 1, ((int)Fraction_calculated.Denominator) + MaxDenom); 
                Fraction_prom = new Fraction(numerator, denominator);

                Fraction_calculated = new Fraction((Fraction_calculated - Fraction_prom).Numerator, (Fraction_calculated - Fraction_prom).Denominator); //новая дробь с учётом добавленной 

                equasion.Add(Fraction_prom);
                equasion.Add("-");
            }
            equasion.RemoveAt(equasion.Count - 1);
            return equasion;
        }

        public class EasyDifficultyEquasionSum: IEquasionGenerator // простая сложность для сложения
        {
            public List<object> GenerateEquasion()
            {
               
                return Generator_Equasion_Sum_Multiplication(2,2,"+",MaxEasyNumerator, MultiplayerCoefficientForDenominatorRange);
            }
        }


        public class EasyDifficultyEquasionSubtraction : IEquasionGenerator // простая сложность для вычитания
        {
            public List<object> GenerateEquasion()
            {

                //Random rnd = new Random();

                //List<object> equasion = new List<object>();

                //int numerator = rnd.Next(1, MaxEasyNumerator);
                //int denominator = rnd.Next(1 + numerator, numerator + PlusSubtractionCoefficientForDenominatorRange);
                //equasion.Add(new Fraction(numerator, denominator));
                //equasion.Add("-");

                //numerator = rnd.Next(1, numerator);
                //denominator = rnd.Next(denominator +1, denominator + PlusSubtractionCoefficientForDenominatorRange);
                //equasion.Add(new Fraction(numerator, denominator));

                return Generator_Equasion_Subtraction(2,2, MaxEasyNumerator, PlusSubtractionCoefficientForDenominatorRange);
            }
        }

        public class EasyDifficultyEquasionMultiply : IEquasionGenerator // простая сложность для умножения
        {
            public List<object> GenerateEquasion()
            {

                //Random rnd = new Random();

                //List<object> equasion = new List<object>();
                //for (int i = 0; i < 3; i++)
                //{
                //    int numerator = rnd.Next(1, MaxEasyNumerator);
                //    int denominator = rnd.Next(1 + numerator, 1 + numerator * MultiplayerCoefficientForDenominatorRange);
                //    equasion.Add(new Fraction(numerator, denominator));
                //    equasion.Add("*");
                //}
                //equasion.RemoveAt(equasion.Count - 1);

                return Generator_Equasion_Sum_Multiplication(2, 2, "*", MaxEasyNumerator, MultiplayerCoefficientForDenominatorRange); ;
            }
        }
     

        public class MediumDifficultyEquasionSum: IEquasionGenerator // средняя сложность для сложения
        {
            public List<object> GenerateEquasion()
            {
                //Random rnd = new Random();

                //List<object> equasion = new List<object>();
                //for (int i = 0; i < rnd.Next(1, 3); i++)
                //{
                //    int numerator = rnd.Next(0, MaxMediumNumerator);
                //    int denominator = rnd.Next(numerator, numerator * MultiplayerCoefficientForDenominatorRange);
                //    equasion.Add(new Fraction(numerator, denominator));
                //    equasion.Add("+");
                //}
                //equasion.RemoveAt(equasion.Count - 1);

                return Generator_Equasion_Sum_Multiplication(2, 3, "+", MaxMediumNumerator, MultiplayerCoefficientForDenominatorRange);
            }

        }

        public class MediumDifficultyEquasionSubtraction : IEquasionGenerator // средняя сложность для вычитания
        {
            public List<object> GenerateEquasion()
            {

                //Random rnd = new Random();

                //List<object> equasion = new List<object>();

                //int numerator = rnd.Next(1, MaxEasyNumerator);
                //int denominator = rnd.Next(1 + numerator, numerator + PlusSubtractionCoefficientForDenominatorRange);
                //equasion.Add(new Fraction(numerator, denominator));
                //equasion.Add("-");

                //numerator = rnd.Next(1, numerator);
                //denominator = rnd.Next(denominator + 1, denominator + PlusSubtractionCoefficientForDenominatorRange);
                //equasion.Add(new Fraction(numerator, denominator));

                return Generator_Equasion_Subtraction(2, 3, MaxMediumNumerator, PlusSubtractionCoefficientForDenominatorRange);
            }
        }

        public class MediumDifficultyEquasionMultiply : IEquasionGenerator // средняя сложность для умножения
        {
            public List<object> GenerateEquasion()
            {

                //Random rnd = new Random();

                //List<object> equasion = new List<object>();
                //for (int i = 0; i < 3; i++)
                //{
                //    int numerator = rnd.Next(1, MaxEasyNumerator);
                //    int denominator = rnd.Next(1 + numerator, 1 + numerator * MultiplayerCoefficientForDenominatorRange);
                //    equasion.Add(new Fraction(numerator, denominator));
                //    equasion.Add("*");
                //}
                //equasion.RemoveAt(equasion.Count - 1);

                return Generator_Equasion_Sum_Multiplication(2, 3, "*", MaxMediumNumerator, MultiplayerCoefficientForDenominatorRange);
            }
        }

        public class HighDifficultyEquasionSum : IEquasionGenerator // сложная сложность для сложения
        {
            public List<object> GenerateEquasion()
            {
                //Random rnd = new Random();

                //List<object> equasion = new List<object>();
                //for (int i = 0; i < rnd.Next(2, 4); i++)
                //{
                //    int numerator = rnd.Next(0, MaxHardNumerator);
                //    int denominator = rnd.Next(numerator, numerator * MultiplayerCoefficientForDenominatorRange);
                //    equasion.Add(new Fraction(numerator, denominator));
                //}

                return Generator_Equasion_Sum_Multiplication(2, 4, "+", MaxHardNumerator, MultiplayerCoefficientForDenominatorRange);
            }
        }

        public class HighDifficultyEquasionSubtraction : IEquasionGenerator // средняя сложность для умножения
        {
            public List<object> GenerateEquasion()
            {

                //Random rnd = new Random();

                //List<object> equasion = new List<object>();
                //for (int i = 0; i < 3; i++)
                //{
                //    int numerator = rnd.Next(1, MaxEasyNumerator);
                //    int denominator = rnd.Next(1 + numerator, 1 + numerator * MultiplayerCoefficientForDenominatorRange);
                //    equasion.Add(new Fraction(numerator, denominator));
                //    equasion.Add("*");
                //}
                //equasion.RemoveAt(equasion.Count - 1);

                return Generator_Equasion_Subtraction(2, 4, MaxHardNumerator, PlusSubtractionCoefficientForDenominatorRange);
            }
        }

        public class HighDifficultyEquasionMultiply : IEquasionGenerator // сложная сложность для сложения
        {
            public List<object> GenerateEquasion()
            {
                //Random rnd = new Random();

                //List<object> equasion = new List<object>();
                //for (int i = 0; i < rnd.Next(2, 4); i++)
                //{
                //    int numerator = rnd.Next(0, MaxHardNumerator);
                //    int denominator = rnd.Next(numerator, numerator * MultiplayerCoefficientForDenominatorRange);
                //    equasion.Add(new Fraction(numerator, denominator));
                //}

                return Generator_Equasion_Sum_Multiplication(2, 4, "*", MaxHardNumerator, MultiplayerCoefficientForDenominatorRange);
            }
        }
        class GameCurrentData
        {

        }

    }
}
