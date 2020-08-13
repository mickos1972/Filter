using System.Collections.Generic;
using BlueStone.Sorters;
using Microsoft.Extensions.Configuration;

namespace BlueStone
{
    public class SortNumbers : ISortNumbers
    {
        private readonly ISort _mySort;

        public SortNumbers(Program.SortServiceResolver filter, IConfiguration config)
        {
            //Get the name of the sort from config
            _mySort = filter(config["sortName"]);
        }

        public List<int> applySort(List<int> numbers)
        {
            var result = _mySort.Sort(numbers);

            return result;
        }
    }
}