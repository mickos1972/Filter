using System.Collections.Generic;
using Microsoft.Extensions.Configuration;

namespace BlueStone
{
    public class FilterNumbers : IFilterNumbers
    {
        private readonly IFilter _myFilter;

        public FilterNumbers(Program.FilterServiceResolver filter, IConfiguration config)
        {
            //Get the name of the filter from config
            _myFilter = filter(config["filterName"]);
        }

        public List<int> applyFilter(List<int> source)
        {
            //Count down through the list as the size of the list is reduced
            //as we remove items.

            for (var i = source.Count - 1; i >= 0; i--)
            {
                if (_myFilter.Filter(source[i]))
                    source.RemoveAt(i);
            }

            return source;
        }
    }
}
