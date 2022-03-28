using System;
using System.Threading.Tasks;

namespace SortVisualizer
{
    public interface ISortAlgorithm
    {
        string Title { get; }
        event Action OnRedraw;
        event Action OnSortDone;
        WaitDelegate Wait { get; set; }  
        Task Sort(int[] Array);
    }
    public delegate Task WaitDelegate();
}
