using System;
using System.Collections.Generic;

namespace BlueStone
{
    public static class Utilities
    {
        public static readonly List<int> IntegerList = new List<int>();

        public static List<int> SplitString(string input)
        {
            var strings = input.Split(',');

            foreach (var c in strings)
            {
                IntegerList.Add(int.Parse(c));
            }

            return IntegerList;
        }
    }
}
