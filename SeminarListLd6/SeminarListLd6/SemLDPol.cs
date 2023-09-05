using System.Collections.Generic;
using System;
using System.Linq;

namespace SeminarListLd6
{
    class SemLDPol
    { 
        public static List<string> GetPolinromes(string input)
        {
            const int p = 31;
            Int64 hash = 0;
            Int64 p_pow = 1;
            for (var i = 0; i < input.Length; ++i)
            {
                hash += (input[i] - 'a' + 1) * p_pow;
                p_pow *= p;
            }
        }
    }
}
