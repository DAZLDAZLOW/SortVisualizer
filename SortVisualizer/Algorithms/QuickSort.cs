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
        public string Title { get { return "Quick Sort"; } }

        public void Sort(object ArrayObj)
        {
            int[] Array = (int[])ArrayObj;
            QuickSortA(Array, 0, Array.Length - 1);
            MainWindow.SingleTon.Dispatcher.Invoke(() => { MainWindow.SingleTon.SortIsDone(); });
        }

        private void SwapTwo(ref int x, ref int y)
        {
            var t = x;
            x = y;
            y = t;
        }

        private int Partition(int[] Array, int minIndex, int maxIndex)
        {
            var pivot = minIndex - 1;
            for (var i = minIndex; i < maxIndex; i++)
            {
                if (Array[i] < Array[maxIndex])
                {
                    pivot++;
                    MainWindow.SingleTon.Dispatcher.Invoke(() => { MainWindow.SingleTon.Swap(pivot, i); });
                    SwapTwo(ref Array[pivot], ref Array[i]);
                    if ((MainWindow.SingleTon.SortSpeed) < 1000)
                        Thread.Sleep((int)(1000 / MainWindow.SingleTon.SortSpeed));
                }
            }

            pivot++;
            MainWindow.SingleTon.Dispatcher.Invoke(() => { MainWindow.SingleTon.Swap(pivot, maxIndex); });
            if ((MainWindow.SingleTon.SortSpeed) < 1000)
                Thread.Sleep((int)(1000 / MainWindow.SingleTon.SortSpeed));
            SwapTwo(ref Array[pivot], ref Array[maxIndex]);
            return pivot;
        }

        private int[] QuickSortA(int[] Array, int minIndex, int maxIndex)
        {
            if (minIndex >= maxIndex)
            {
                return Array;
            }
            var pivotIndex = Partition(Array, minIndex, maxIndex);
            QuickSortA(Array, minIndex, pivotIndex - 1);
            QuickSortA(Array, pivotIndex + 1, maxIndex);
            return Array;
        }
    }
}
