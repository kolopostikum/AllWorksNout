using System.Collections.Generic;
using System;
using System.Text.RegularExpressions;
using System.Text;
using MathNet.Symbolics;
using Expr = MathNet.Symbolics.SymbolicExpression;


namespace Seminar
{

   public class RecursivYield
    {
        private static List<char> symbols = new List<char> { '0', '1', '2', 'x', '+', '*', '-' };
        private static List<char> operations = new List<char> { '+', '*', '-' };
        private static List<char> variables = new List<char> { '0', '1', '2', 'x' };
        public readonly LinkedList<String> experssions;

        public RecursivYield(int number)
        {
            experssions = new LinkedList<String>();
            var experssionsWihDoubl = new LinkedList<String>();
            var expressionsLoop = new LinkedList<List<char>>();

            Action<List<char>> WriteRes = x =>
            {
                foreach (var ch in x)
                {
                    Console.Write(ch);
                }
                Console.WriteLine(); ;
            };   

            foreach (var item in MakeSubsets(new List<char>(), number + 1, number + 1))
            {
                var res = new List<char>(item);
                for (int i = 0; i < number; i++)
                {
                    expressionsLoop.AddLast(res);
                    res.Add(')');
                }
            }
            var counter = 0;
            foreach (var item in expressionsLoop)
            {
                var res = "";
                if (CheckLoop(item))
                {
                    foreach (var ch in item)
                        res += ch;
                    counter++;
                    experssionsWihDoubl.AddLast(res);
                }
            }

            experssions = DeleteDuble(experssionsWihDoubl);
            foreach (var item in experssions)
            {
                Console.WriteLine(item);
            }
            Console.WriteLine(experssions.Count);
        }

        private LinkedList<string> DeleteDuble(LinkedList<string> expressionsWihDoubl)
        {
            var uniqueExpressions = new LinkedList<string>();
            List<Expression[]> keyValuePolinom = new List<Expression[]>();

            foreach (var expression in expressionsWihDoubl)
            {
                if (!CheckForDuplicate(expression, keyValuePolinom))
                {
                    uniqueExpressions.AddLast(expression);
                }
            }

            return uniqueExpressions;
        }
        private bool CheckForDuplicate(string expression, List<Expression[]> keyValuePolinom)
        {
            Expression[] coefficients = GetCoefficients(expression);

            foreach (Expression[] polinomCoefficients in keyValuePolinom)
            {
                if (AreCoefficientsEqual(coefficients, polinomCoefficients))
                {
                    // Полином с такими коэффициентами уже существует
                    return true;
                }
            }

            // Добавляем коэффициенты полинома в список
            keyValuePolinom.Add(coefficients);

            // Полином с такими коэффициентами отсутствует
            return false;

        }
        private bool AreCoefficientsEqual(Expression[] coefficients1, Expression[] coefficients2)
        {
            if (coefficients1.Length != coefficients2.Length) return false;
            for (int i = 0; i < coefficients1.Length; i++)
            {
                if (!coefficients1[i].Equals(coefficients2[i]))
                    return false;
            }
            return true;
        }

        private Expression[] GetCoefficients(string expression)
        {
            var x = MathNet.Symbolics.Expression.Symbol("x");
            var exp = Infix.ParseOrThrow(expression);
            var res = Polynomial.Coefficients(x, exp);
            return res;
        }
        private bool CheckLoop(List<char> item)
        {
            var stack = new Stack<char>();
            foreach (var e in item)
            {
                if (e == '(')
                    stack.Push(e);
                if (e == ')')
                {
                    if (stack.Count == 0) return false;
                    if (stack.Pop() != '(') return false;
                }
            }
            return stack.Count == 0;
        }

        static IEnumerable<List<char>> MakeSubsets(List<char> subset, int numberAct, int number)
        {
            if (numberAct == 0)
            {
                yield return subset;
                yield break;
            }

            if (subset.Count == 0)
            {
                for (int j = 0; j < numberAct; j++)
                {
                    for (int i = 0; i < variables.Count; i++)
                    {
                        var pupa = new List<char>(subset);
                        pupa.Add(variables[i]);
                        foreach (var e in MakeSubsets(pupa, numberAct - 1, number))
                            yield return e;
                    }
                    subset.Add('(');
                }
            }
            else if (operations.Contains(subset[^1]))
            {
                for (int j = 0; j < numberAct; j++)
                {
                    for (int i = 0; i < variables.Count; i++)
                    {
                        var pupa = new List<char>(subset);
                        pupa.Add(variables[i]);
                        foreach (var e in MakeSubsets(pupa, numberAct - 1, number))
                            yield return e;
                    }
                    subset.Add('(');
                }
            }
            else
            {
                for (int j = 0; j < number - numberAct; j++)
                {
                    for (int i = 0; i < operations.Count; i++)
                    {
                        var pupa = new List<char>(subset);
                        pupa.Add(operations[i]);
                        foreach (var e in MakeSubsets(pupa, numberAct, number))
                            yield return e;
                    }
                    subset.Add(')');
                }
            }
        }
    }

}
