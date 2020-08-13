using System;
using System.Collections.Generic;
using System.Text;

namespace BlueStone.Sorters
{
    public class SortAscending : ISort
    {
        public List<int> Sort(List<int> numberList)
        {
            numberList.Sort();

            return numberList;
        }
    }
}
