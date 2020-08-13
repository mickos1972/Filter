using System;
using System.Collections.Generic;

namespace BlueStone
{
    public class PrimeFilter : IFilter
    {
        public bool Filter(int n)
        {
            for (var i = 2; i < n; i++)
                if (n % i == 0)
                    return false;

            return true;
        }
    }
}