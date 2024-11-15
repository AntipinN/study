using System.Collections.Generic;


namespace AvaloniaApplication3.Models
{
    public class UniversalOPZFormer : IOPZ
    {
        public List<object> CalculateOPZ(List<object> data_from_string)
        {
            List<object> OPZ = new List<object>();
            Stack<object> operations = new Stack<object>();
            int Priority;
            for (int i = 0; i < data_from_string.Count; i++)
            {
                Priority = OpredelPrioritet(data_from_string[i]);

                if (Priority == -1) //если не операция
                {
                    OPZ.Add(data_from_string[i]);
                }
                else
                {
                    if (operations.Count == 0 || Priority == 0 || Priority > OpredelPrioritet(operations.Peek())) //если стек пуст или приоритет 0 или следующий приоретет больше текущего находящегося в стеке
                    {
                        operations.Push(data_from_string[i]);                     //заносим в стек операцию
                    }
                    else
                    {
                        while (operations.Count > 0 && Priority <= OpredelPrioritet(operations.Peek()))  //пока стек не пуст и следующий элемент меньше текущего в стеке
                        {
                            OPZ.Add(operations.Pop()); //вытаскиваем его в ОПЗ
                        }

                        if ((string)data_from_string[i] == ")") //если это закрывающая скобка, то на вершине стека у нас сидит окрываюшаясЯ скобка,которыю мы удаляем , в другом случае заносим в стек
                        {
                            operations.Pop();
                        }
                        else
                        {
                            operations.Push(data_from_string[i]);
                        }
                    }
                }
            }

            while (operations.Count > 0) //если стек не пустой то заносим всё что осталось в OPZ
            {
                OPZ.Add(operations.Pop());
            }
            return OPZ;

        }

        private int OpredelPrioritet(object symbol)
        {
            switch (symbol)
            {
                case "{":
                case "(":

                    return 0;

                case "}":
                case ")":
                case ":=":
                case ";":

                    return 1;

                case "||":
                case "or":
                    return 3;

                case "&&":
                case "and":
                    return 4;

                case "not":
                    return 5;

                case ">":
                case "<":
                case ">=":
                case "<=":
                case "==":
                case "!=":
                case "<>":
                    return 6;

                case "+":
                case "-":
                    return 7;

                case "*":
                case "/":
                case "%":
                case "~":
                case "div":
                case "mod":
                    return 8;

                default:    //если это набор символов(буквы и цифры)
                    return -1;
            }
        }
    }
}
