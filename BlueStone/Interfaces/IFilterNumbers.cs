using System.Collections.Generic;

namespace BlueStone
{
    public interface IFilterNumbers
    {
        List<int> applyFilter(List<int> source);
    }
}