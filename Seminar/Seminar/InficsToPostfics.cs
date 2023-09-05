using System.Collections.Generic;
using System;

namespace Seminar
{
    public class InficsToPostfics
    {
        private static List<char> Operators;
        private static string Postfics;

        public static int CounterPostfics(string str)
        {
            var operations = new Dictionary<char, Func<int, int, int>>();
            operations.Add('+', (y, x) => x + y);
            operations.Add('-', (y, x) => x - y);
            operations.Add('*', (y, x) => x * y);
            operations.Add('/', (y, x) => x / y);

            var stack = new Stack<int>();
            foreach (var e in str)
            {
                if (e <= '9' && e >= '0')
                    stack.Push(e - '0');
                else if (operations.ContainsKey(e))
                    stack.Push(operations[e](stack.Pop(), stack.Pop()));
                else
                    throw new ArgumentException();
            }
            return stack.Pop();

        }

        public static string Parser(string input)
        {
            Postfics = "";
            Operators = new List<char>();
            var operDict = new Dictionary<char, int>();
            operDict['('] = 0;
            operDict['+'] = 1;
            operDict['-'] = 1;
            operDict['*'] = 2;
            operDict['/'] = 2;

            ParserOperations(input, operDict);

            while (Operators.Count > 0)
            {
                Postfics += Operators[^1];
                Operators.RemoveAt(Operators.Count - 1);
            }

            return Postfics;
        }

        private static void ParserOperations(string input,
            Dictionary<char, int> operDict)
        {
            for (int i = 0; i < input.Length; i++)
            {
                if (char.IsDigit(input[i]))
                    Postfics += input[i];
                else 
                    ParserSymbols(input[i], operDict);
            }
        }

        private static void ParserSymbols(char symbol, Dictionary<char, int> operDict)
        {
            switch (symbol)
            {
                case '(':
                    Operators.Add('(');
                    break;
                case ')':
                    ParserBackQuote(operDict);
                    break;
                default:
                    ParserMathSymbols(symbol, operDict);
                    break;
            }
        }

        private static void ParserMathSymbols(char symbol, Dictionary<char, int> operDict)
        {
            if (Operators.Count == 0)
                Operators.Add(symbol);
            else if (operDict[symbol] > operDict[Operators[^1]])
                Operators.Add(symbol);
            else if (operDict[symbol] <= operDict[Operators[^1]])
            {
                while (
                    operDict[Operators[^1]] >= operDict[symbol]
                    )
                {
                    Postfics += Operators[^1];
                    Operators.RemoveAt(Operators.Count - 1);
                    if (Operators.Count == 0)
                        break;
                }
                Operators.Add(symbol);
            }
        }

        private static void ParserBackQuote(Dictionary<char, int> operDict)
        {
            while (Operators[^1] != '(')
            {
                Postfics += Operators[^1];
                Operators.RemoveAt(Operators.Count - 1);
                if (Operators.Count == 0)
                    break;
            }
            Operators.RemoveAt(Operators.Count - 1);
        }
    }
}
