using System;
using System.Collections.Generic;
using System.Linq;

namespace SeminarListDictionary
{
    class Combinater
    {
        public static IEnumerable<string> CombiningStrings
            (IEnumerable<string> firstStrings, IEnumerable<string> secondStrings)
        {
            var keys = firstStrings.ToHashSet();
            
            foreach (var item in firstStrings)
                yield return item;
            
            foreach (var item in secondStrings)
            {
                if (!keys.Contains(item))
                    yield return item;
            }
        }

    }
}
