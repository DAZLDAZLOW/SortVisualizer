using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SortVisualizer.Algorithms
{
    internal class MergeSort : ISortAlgorithm
    {
        //-------------------ISortAlgoritm Implementation---------------
        WaitDelegate ISortAlgorithm.Wait { get => wait; set => wait += value; } 
        public string Title { get { return "Merge Sort"; } } 
        WaitDelegate wait;
        public event Action OnRedraw; 
        public event Action OnSortDone;
        //--------------------------------------------------------------
        private int[] temporaryArray;

        public async Task Sort(int[] array)
        {
            temporaryArray = new int[array.Length];
            await Mergesort(array, 0, array.Length - 1);
            OnSortDone(); //ISortAlgoritm Implementation
        }

        async Task Merge(int[] Array, int start, int middle, int end)
        {
            var leftPtr = start;
            var rightPtr = middle + 1;
            var length = end - start + 1;
            for (int i = 0; i < length; i++)
            {
                if (rightPtr > end || (leftPtr <= middle && Array[leftPtr] < Array[rightPtr]))
                {
                    temporaryArray[i] = Array[leftPtr];
                    leftPtr++;
                }
                else
                {
                    temporaryArray[i] = Array[rightPtr];
                    rightPtr++;
                }
            }
            for (int i = 0; i < length; i++)
            {
                Array[i + start] = temporaryArray[i];
                OnRedraw(); //ISortAlgoritm Implementation
                await wait(); //ISortAlgoritm Implementation
            }
        }

        async Task Mergesort(int[] Array, int start, int end)
        {
            if (start == end) return;
            var middle = (start + end) / 2;
            await Mergesort(Array, start, middle);
            await Mergesort(Array, middle + 1, end);
            await Merge(Array, start, middle, end);
        }

    }
}
