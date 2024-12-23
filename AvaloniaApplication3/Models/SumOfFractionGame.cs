using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.Json;
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
            Fraction Ansver_fraction;
            public GameGenerator(IEquasionGenerator gen, ISolver solv, IOPZ opz) 
            {
                _IGenerator = gen;
                _ISolver = solv;
                _IOPZ = opz;
            }

            public Wrapper_fractions GetExpression()
            {
                Wrapper_fractions result = _IGenerator.GenerateEquasion();
                Ansver_fraction = _ISolver.Solve(_IOPZ.CalculateOPZ(result.term), result.fraction_substitutions);
                result.fraction_substitutions.Add("O1", Ansver_fraction);
                result.term.Add("=");
                result.term.Add("O1");
                return result;
            }
            public bool CheckEquasion(string Answer)
            {
                if (Answer.Length == 0)
                {
                    return false;
                }
                string[] first = Answer.Split('/');
                if (first.Length > 1 && first[0].Length > 0 && first[1].Length > 0)
                {
                    try
                    {
                        Fraction Answ = new Fraction(long.Parse(first[0]), long.Parse(first[1]));
                        if ((Answ - Ansver_fraction).Numerator == 0)
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                    catch (Exception e)
                    {
                        string day = DateTime.Now.DayOfYear.ToString();
                        string tiks = DateTime.Now.Ticks.ToString();
                        if (!Directory.Exists("ErrorFolder"))
                        {
                            Directory.CreateDirectory("ErrorFolder");
                        }
                        using (StreamWriter fs = new StreamWriter($"ErrorFolder/errorlog {day + " " + tiks}.txt", false))
                        {
                            fs.Write(e.Message);
                        }
                        using (FileStream fs = new FileStream($"ErrorFolder/errorlog {day + " " + tiks}.json", FileMode.OpenOrCreate))
                        {
                            List<string> list = new List<string>()
                            {
                                e.Message,
                                e.StackTrace,
                                e.InnerException?.ToString(),
                                e.Source,
                                e.TargetSite?.ToString(),
                                e.HResult.ToString()
                            };
                            JsonSerializer.Serialize(fs, list);
                        }
                    }
                    return false;
                }
                else
                {
                    return false;
                }
            }
        }

        public static Wrapper_fractions Generator_Equasion_Sum_Multiplication(int Lower, int Top, string sign, int MaxNumer, int MaxDenom) //метод позволяющий по введённым параметрам сгенерировать дроби для равенства(для + и умножения)
        {
            Random rnd = new Random();
            int index_letter = 0;
            List<string> term = new List<string>();   //финальное выражение
            Dictionary<string, Fraction> fraction_substitutions = new Dictionary<string, Fraction>();  // словарь замен дроби на букву

            int number_of_fractions = rnd.Next(Lower, Top + 1); //(число дробей); добавил +1, чтобы логичней были промежутки, так промежуток от 2 до 2, будет в рандомк от 2 до 3, три не включая и так будет точно 2
            
            for (int i = 0; i < number_of_fractions; i++)   
            {
                int numerator = rnd.Next(1, MaxNumer);
                int denominator = rnd.Next(numerator, numerator * MaxDenom);
                string replacement_letter = "F" + index_letter;                   //переменная для замены

                fraction_substitutions.Add(replacement_letter, new Fraction(numerator, denominator));
                term.Add(replacement_letter);
                term.Add(sign);
                index_letter++;
            }

            term.RemoveAt(term.Count - 1);
            return new Wrapper_fractions(term, fraction_substitutions);
        }

        public static Wrapper_fractions Generator_Equasion_Subtraction(int Lower, int Top, int MaxNumer, int MaxDenom) //метод позволяющий по введённым параметрам сгенерировать дроби для равенства c разностью
        {
            Random rnd = new Random();
            Fraction Fraction_calculated; //дробь, для создания новой дроби,в этой дроби лежит текущий ответ (разность дробей)
            Fraction Fraction_prom;       //дробь для подсчёта новой Fraction_calculated
            int index_letter = 1;
            List<string> term = new List<string>();   //финальное выражение
            Dictionary<string, Fraction> fraction_substitutions = new Dictionary<string, Fraction>();  // словарь замен дроби на букву
            
            int numerator = rnd.Next(1, MaxNumer);
            int denominator = rnd.Next(1+numerator, numerator + MaxDenom);
            Fraction_calculated = new Fraction(numerator, denominator);

            string replacement_letter = "F" + 0;                   //переменная для замены
            fraction_substitutions.Add(replacement_letter, new Fraction(numerator, denominator));
            term.Add(replacement_letter);
            term.Add("-");

            int number_of_fractions = rnd.Next(Lower, Top + 1); //добавил +1, чтобы логичней были промежутки

            for (int i = 1; i < number_of_fractions; i++) //начинаем с одного, так как одну дробь мы уже создали
            {
                numerator = rnd.Next(1, (int)Fraction_calculated.Numerator);  
                denominator = rnd.Next(((int)Fraction_calculated.Denominator) + 1, ((int)Fraction_calculated.Denominator) + MaxDenom); 
                Fraction_prom = new Fraction(numerator, denominator);

                Fraction_calculated = Fraction_calculated - Fraction_prom; //новая дробь с учётом добавленной 

                replacement_letter = "F" + index_letter;                                              //переменная для замены
                fraction_substitutions.Add(replacement_letter, Fraction_prom);
                term.Add(replacement_letter);
                term.Add("-");
                index_letter++;
            }
            term.RemoveAt(term.Count - 1);
            return new Wrapper_fractions(term, fraction_substitutions);
        }

        public static Wrapper_fractions Generator_Equasion_Mixed(int Lower, int Top, int MaxNumer, int MaxDenom) //метод позволяющий по введённым параметрам сгенерировать дроби для смешаного равенства(с минусом и плюсом)
        {
            throw new NotImplementedException();
        }

        public class EasyDifficultyEquasionSum: IEquasionGenerator // простая сложность для сложения
        {
            public Wrapper_fractions GenerateEquasion()
            {
               
                return Generator_Equasion_Sum_Multiplication(2,2,"+",MaxEasyNumerator, MultiplayerCoefficientForDenominatorRange);
            }
        }


        public class EasyDifficultyEquasionSubtraction : IEquasionGenerator // простая сложность для вычитания
        {
            public Wrapper_fractions GenerateEquasion()
            {

                return Generator_Equasion_Subtraction(2,2, MaxEasyNumerator, PlusSubtractionCoefficientForDenominatorRange);
            }
        }

        public class EasyDifficultyEquasionMultiply : IEquasionGenerator // простая сложность для умножения
        {
            public Wrapper_fractions GenerateEquasion()
            {

                return Generator_Equasion_Sum_Multiplication(2, 2, "*", MaxEasyNumerator, MultiplayerCoefficientForDenominatorRange); ;
            }
        }
     

        public class MediumDifficultyEquasionSum: IEquasionGenerator // средняя сложность для сложения
        {
            public Wrapper_fractions GenerateEquasion()
            {

                return Generator_Equasion_Sum_Multiplication(2, 3, "+", MaxMediumNumerator, MultiplayerCoefficientForDenominatorRange);
            }

        }

        public class MediumDifficultyEquasionSubtraction : IEquasionGenerator // средняя сложность для вычитания
        {
            public Wrapper_fractions GenerateEquasion()
            {


                return Generator_Equasion_Subtraction(2, 3, MaxMediumNumerator, PlusSubtractionCoefficientForDenominatorRange);
            }
        }

        public class MediumDifficultyEquasionMultiply : IEquasionGenerator // средняя сложность для умножения
        {
            public Wrapper_fractions GenerateEquasion()
            {


                return Generator_Equasion_Sum_Multiplication(2, 3, "*", MaxMediumNumerator, MultiplayerCoefficientForDenominatorRange);
            }
        }

        public class HighDifficultyEquasionSum : IEquasionGenerator // сложная сложность для сложения
        {
            public Wrapper_fractions GenerateEquasion()
            {

                return Generator_Equasion_Sum_Multiplication(2, 4, "+", MaxHardNumerator, MultiplayerCoefficientForDenominatorRange);
            }
        }

        public class HighDifficultyEquasionSubtraction : IEquasionGenerator // средняя сложность для умножения
        {
            public Wrapper_fractions GenerateEquasion()
            {

                return Generator_Equasion_Subtraction(2, 4, MaxHardNumerator, PlusSubtractionCoefficientForDenominatorRange);
            }
        }

        public class HighDifficultyEquasionMultiply : IEquasionGenerator // сложная сложность для сложения
        {
            public Wrapper_fractions GenerateEquasion()
            {
                return Generator_Equasion_Sum_Multiplication(2, 4, "*", MaxHardNumerator, MultiplayerCoefficientForDenominatorRange);
            }
        }
        class GameCurrentData
        {

        }

    }
}
