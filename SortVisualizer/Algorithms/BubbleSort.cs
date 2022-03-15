using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Diagnostics;

namespace SortVisualizer.Algorithms
{
    internal class BubbleSort : ISortAlgorithm
    {
        public string Title { get { return "Bubble Sort"; } }

        public void Sort(object ArrayObj)
        {
            int[] Array = (int[])ArrayObj;
            int temp;
            for (int i = 0; i < Array.Length; i++)
            {
                for (int j = i + 1; j < Array.Length; j++)
                {
                    if (Array[i] > Array[j])
                    {
                        temp = Array[i];
                        Array[i] = Array[j];
                        Array[j] = temp;
                        MainWindow.SingleTon.Dispatcher.Invoke(() => { MainWindow.SingleTon.Swap(i, j); });
                        if ((MainWindow.SingleTon.SortSpeed) < 1000)
                            Thread.Sleep((int)(1000 / MainWindow.SingleTon.SortSpeed));
                    }
                }
            }
            MainWindow.SingleTon.Dispatcher.Invoke(() => { MainWindow.SingleTon.SortIsDone(); });
            
        }
    }
}
