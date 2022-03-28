using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SortVisualizer.Algorithms
{
    internal class QuickSort : ISortAlgorithm
    {
        //-------------------ISortAlgoritm Implementation---------------
        public string Title { get { return "Quick Sort"; } } 
        WaitDelegate ISortAlgorithm.Wait { get => wait; set => wait += value; } 
        WaitDelegate wait; 
        public event Action OnRedraw;  
        public event Action OnSortDone;
        //--------------------------------------------------------------

        public async Task Sort(int[] Array)
        {
            await QuickSortA(Array, 0, Array.Length - 1);
            OnSortDone(); //ISortAlgoritm Implementation
        }

        private void SwapTwo(ref int x, ref int y)
        {
            var t = x;
            x = y;
            y = t;
        }

        private async Task<int> Partition(int[] Array, int minIndex, int maxIndex)
        {
            var pivot = minIndex - 1;
            for (var i = minIndex; i < maxIndex; i++)
            {
                if (Array[i] < Array[maxIndex])
                {
                    pivot++;
                    OnRedraw();
                    SwapTwo(ref Array[pivot], ref Array[i]);
                    await wait();
                }
            }

            pivot++;
            OnRedraw(); //ISortAlgoritm Implementation
            await wait(); //ISortAlgoritm Implementation
            SwapTwo(ref Array[pivot], ref Array[maxIndex]);
            return pivot;
        }

        private async Task<int[]> QuickSortA(int[] Array, int minIndex, int maxIndex)
        {
            if (minIndex >= maxIndex)
            {
                return Array;
            }
            var pivotIndex = await Partition(Array, minIndex, maxIndex);
            await QuickSortA(Array, minIndex, pivotIndex - 1);
            await QuickSortA(Array, pivotIndex + 1, maxIndex);
            return Array;
        }
    }
}
