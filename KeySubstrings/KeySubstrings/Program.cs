using System;
using System.Collections.Generic;
using System.Linq;

namespace KeySubstrings
{
    class Program
    {
        static void Main(string[] args)
        {
            var stringList = new List<string>();
            var substringHashList = new Dictionary<string, HashSet<string>>();
            foreach (var item in stringList)
            {
                substringHashList.Add(item, SubstringSearcher(item));
            }

            var originalStrings = substringHashList
                .Values
                .SelectMany(x => x)
                .ToHashSet<string>();
            originalStrings = originalStrings
                .GroupBy(x => x)
                .Where(x => x.Count() == 1)
                .Select(x => x.Key)
                .ToHashSet<string>();

            foreach (var item in stringList)
            {
                WriteMinOriginalString(originalStrings, substringHashList, item);
            }
        }

        private static void WriteMinOriginalString(HashSet<string> originalStrings, Dictionary<string, HashSet<string>> substringHashList, string item)
        {
            var sortedStringHash = substringHashList[item].OrderBy(x => x);
            foreach (var substring in sortedStringHash)
            {
                if (originalStrings.Contains(substring))
                {
                    Console.WriteLine(substring);
                    return;
                }
            }
            Console.WriteLine();
        }

        private static HashSet<string> SubstringSearcher(string keyString)
        {
            var hash = new HashSet<string>();
            SearchVariationsString(hash, keyString, 0, keyString.Length);
            return hash;
        }

        private static void SearchVariationsString(HashSet<string> hash, string keyString, int v, int length)
        {
            if (length == 0)
                return;
            var subString = keyString.Substring(v, length);
            if (!hash.Contains(subString))
                hash.Add(subString);
            SearchVariationsString(hash, keyString, v + 1, length - 1);
            SearchVariationsString(hash, keyString, v, length - 1);
        }
    }
}
