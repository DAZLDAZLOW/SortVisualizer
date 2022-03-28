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
        //-------------------ISortAlgoritm Implementation---------------
        public string Title { get { return "Bubble Sort"; } } 
        WaitDelegate ISortAlgorithm.Wait { get => wait; set => wait += value; } 
        WaitDelegate wait; 
        public event Action OnRedraw; 
        public event Action OnSortDone;
        //--------------------------------------------------------------
        public async Task Sort(int[] Array)
        {
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
                        OnRedraw();//ISortAlgoritm Implementation
                        await wait(); //ISortAlgoritm Implementation
                    }
                }
            }
            OnSortDone(); //ISortAlgoritm Implementation
        }

    }
}
