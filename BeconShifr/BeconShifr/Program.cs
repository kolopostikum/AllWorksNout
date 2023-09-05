using System;
using System.Collections.Generic;
using System.Linq;

namespace BeconShifr
{
    class Program
    {
        static void Main(string[] args)
        {
            var input = Console.ReadLine();
            var variables = new HashSet<string>();
            SearchCountBecon(input, variables, 0, input.Length);
            var result = variables.Count();
            Console.WriteLine(result);
        }

        private static void SearchCountBecon(string input, HashSet<string> variables, int indexFirst, int indexLast)
        {
            if (indexLast <= 0)
                return;
            var substring = input.Substring(indexFirst, indexLast);
            if (!variables.Contains(substring))
                variables.Add(substring);
            SearchCountBecon(input, variables, indexFirst + 1, indexLast - 1);
            SearchCountBecon(input, variables, indexFirst, indexLast - 1);
        }
    }
}
