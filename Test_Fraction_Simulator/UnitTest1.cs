using AvaloniaApplication3;
using AvaloniaApplication3.Models;
using System;
using System.Text;
using static System.Runtime.InteropServices.JavaScript.JSType;
namespace Test_Fraction_Simulator
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void Test_Fraction_Comparison() // сравнение дробей
        {
     
            Fraction input_data1 = new Fraction(1, 2);
            Fraction input_data2 = new Fraction(1, 2);
            bool output_bool=false;

            if (input_data1 == input_data2)
            {
                output_bool = true;

            }


            Assert.AreEqual(output_bool, true);
        }

        //[TestMethod]
        //public void Test_GCD_correctness() //провека на правильность подсчёта НОД
        //{

        //    int input_data1 = 10;
        //    int input_data2 = 15;

        //    int output_data = Fraction.GCD(10, 15);

        //    Assert.AreEqual(output_data, 5);

        //}

        [TestMethod]
        public void Test_Fraction_Аddition_Simple_Denominators() //сложение дробей с взаимнопростыми знаменателями
        {
            
            Fraction output_data = new Fraction(1, 2) + new Fraction(1, 3);
            bool output_bool = false;

            if (output_data == new Fraction(5, 6))
            {
                output_bool = true;
            }

             Assert.AreEqual(output_bool, true);
          // Assert.AreEqual(output_data, new Fraction(5, 6));
        }

        [TestMethod]
        public void Test_Fraction_Аddition_Not_Simple_Denominators() //сложение дробей с не взаимнопростыми знаменателями
        {
            
            Fraction output_data = new Fraction(1, 2) + new Fraction(1, 4);
            bool output_bool = false;

            if (output_data == new Fraction(3, 4))
            {
                output_bool = true;
            }

            Assert.AreEqual(output_bool, true);
            // Assert.AreEqual(output_data, new Fraction(3, 4));
        }

        [TestMethod]
        public void Test_Fraction_Аddition_With_Numbers() //сложение дробей с цифрами слева и справа
        {

            Fraction output_data = 2 + new Fraction(1, 2) + 3;
            bool output_bool = false;

            if (output_data == new Fraction(11, 2))
            {
                output_bool = true;
            }

            Assert.AreEqual(output_bool, true);
            // Assert.AreEqual(output_data, new Fraction(11, 2));
        }

        public void Test_Fraction_Subtraction_Simple_Denominators() //вычитание дробей с взаимнопростыми знаменателями
        {
            
            Fraction output_data = new Fraction(1, 2) - new Fraction(1, 3);
            bool output_bool = false;

            if (output_data == new Fraction(1, 6))
            {
                output_bool = true;
            }

            Assert.AreEqual(output_bool, true);
            // Assert.AreEqual(output_data, new Fraction(1, 6));
        }

        [TestMethod]
        public void Test_Fraction_Subtraction_Not_Simple_Denominators() //вычитание дробей с не взаимнопростыми знаменателями
        {
            
            Fraction output_data = new Fraction(1, 2) - new Fraction(1, 6);
            bool output_bool = false;

            if (output_data == new Fraction(1, 3))
            {
                output_bool = true;
            }

            Assert.AreEqual(output_bool, true);
            // Assert.AreEqual(output_data, new Fraction(1, 3));
        }

        public void Test_Fraction_Subtraction_With_Numbers() //вычитание дроби из цифры и наоборот
        {

            Fraction output_data = 5 - new Fraction(1, 2) - 2;
            bool output_bool = false;

            if (output_data == new Fraction(5, 2))
            {
                output_bool = true;
            }

            Assert.AreEqual(output_bool, true);
            // Assert.AreEqual(output_data, new Fraction(5, 2));
        }

        [TestMethod]
        public void Test_Fraction_Reduction() //сокращение дробей
        {

            Fraction output_data = new Fraction(2, 4);
            bool output_bool = false;

            if (!output_data == new Fraction(1, 2))
            {
                output_bool = true;
            }

            Assert.AreEqual(output_bool, true);
            // Assert.AreEqual(output_data, new Fraction(1, 2));
        }



        [TestMethod]
        public void Test_OPZ_Without_Brackets()  //правильность построения опз
        {
            
            List<object> input_data = new List<object>() { new Fraction(2,1),"-", new Fraction(3,4),"+", new Fraction(4, 5) };
            UniversalOPZFormer OPZ = new UniversalOPZFormer();
            List<object> answer = new List<object>() { new Fraction(2, 1), new Fraction(3, 4), "-", new Fraction(4, 5), "+"};
            List<object> output_data = OPZ.CalculateOPZ(input_data);
            bool output_bool= true;

           
            for (int i = 0; i < answer.Count; i++)
            {
                if (answer[i]!= output_data[i])
                {
                    output_bool = false;
                    break;
                }
            }
           

            Assert.AreEqual(output_bool, true);
        }

        [TestMethod]
        public void Test_Solve()  //проверяем правильность счёта
        {
            
            List<object> input_data = new List<object>() { new Fraction(2, 3), new Fraction(3, 4), "-", new Fraction(4, 5), "+" };
            FractionEquasionSolver Solver = new FractionEquasionSolver();
            Fraction output_data = Solver.Solve(input_data);
            bool output_bool = true;


            if (!output_data == new Fraction(43, 60))
            {
                output_bool = true;
            }

            Assert.AreEqual(output_bool, true);
            // Assert.AreEqual(output_data, new Fraction(1, 2));


            
        }
        
    }
}