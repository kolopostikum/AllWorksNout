using System.Collections.Generic;
using System.Linq;
using System;


namespace SeminarListLd6
{
    class SemLd
    { 
        public static string getMaxSubstring(string input1, string input2)
        {
            var length = input1.Length;
            var arrChar = new int[length, length];
            int maxValue = 0;
            int maxI = 0;

            for (int i = 0; i < length; i++)
            {
                for (int j = 0; j < length; j++)
                {
                    if ((i == 0) || (j == 0))
                    {
                        if (input1[i] == input2[j])
                            arrChar[i, j] = 1;
                        if (arrChar[i, j] > maxValue)
                        {
                            maxValue = arrChar[i, j];
                            maxI = i;
                        }
                    }
                    else if (input1[i] == input2[j])
                    {
                        arrChar[i, j] = arrChar[i - 1, j - 1] + 1;
                        if (arrChar[i, j] > maxValue)
                        {
                            maxValue = arrChar[i, j];
                            maxI = i;
                        }
                    }
                }
            }
            if (maxValue > 0)
                return input1.Substring(maxI + 1 - maxValue, maxValue);
            return null;
        }

    }
}
