namespace SortVisualizer
{
    public interface ISortAlgorithm
    {
        public string Title { get; }
        public void Sort(object ArrayObj);// ArrayObj is object for Thread capability
    }
}
