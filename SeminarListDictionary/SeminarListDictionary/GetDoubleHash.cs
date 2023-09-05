using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SeminarListDictionary
{
    class GetDoubleHash
    {
        private static bool isSearchedFirst = false;
        private static bool isSearchedSecond = false;
        private static string firstString = "";
        private static string secondString = "";


        public static Tuple<string, string> GetDoubleHashMethod(Int32 key)
        {
            string variableString = "";
            SearchFirstString(key, variableString);
            return Tuple.Create(firstString, secondString);
        }

        private static void SearchFirstString(int key, string variableString)
        {
            if (isSearchedFirst && isSearchedSecond)
                return;
            if (!isSearchedFirst)
            {
                if (variableString.GetHashCode() == key)
                {
                    firstString = variableString;
                    isSearchedFirst = true;
                }
            }
            else
            {
                if (variableString.GetHashCode() == key)
                {
                    secondString = variableString;
                    isSearchedSecond = true;
                }
            }
            for (int i = char.MinValue; i < char.MaxValue; i++)
            {
                var temp = variableString;
                temp += i;
                SearchFirstString(key, temp);
            }
        }
    }
}
