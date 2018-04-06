using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BreakOutGame.Util
{
    public class GroupNameComparer : IComparer<string>
    {
        public int Compare(string x, string y)
        {
            String[] ar1 = x?.Split(" ");
            String[] ar2 = y?.Split(" ");
            int compareValue = 0;
            int counter = 0;
            int smallestLength = ar1.Length < ar2.Length ? ar1.Length : ar2.Length;
            while (compareValue == 0 && counter < smallestLength)
            {
                if (Int64.TryParse(ar1?[counter], out var number1) && Int64.TryParse(ar2[counter], out var number2))
                {
                    compareValue = number1.CompareTo(number2);
                }
                else
                {
                    compareValue = string.CompareOrdinal(ar1?[counter], ar2?[counter]);
                }
            
                counter++;
            }
            if (compareValue == 0 && ar1.Length != ar2.Length)
            {
                compareValue = ar1.Length < ar2.Length ? -1 : 1;
            }
            return compareValue;
        }
    }
}
