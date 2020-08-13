namespace BlueStone
{
    public class EvenNumbersFilter : IFilter
    {
        public bool Filter(int n)
        {
            return n % 2 == 0;
        }
    }
}