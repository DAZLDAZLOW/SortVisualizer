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
        public string Title { get { return "Merge Sort"; } }
		private int[] temporaryArray;

		public void Sort(object ArrayObj)
        {
			int[] Array = (int[])ArrayObj;
			temporaryArray = new int[Array.Length];
			Mergesort(Array, 0, Array.Length - 1);
			MainWindow.SingleTon.Dispatcher.Invoke(() => { MainWindow.SingleTon.SortIsDone(); });
		}

		void Merge(int[] Array, int start, int middle, int end)
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
				MainWindow.SingleTon.Dispatcher.Invoke(() => { MainWindow.SingleTon.Swap(default, default); });//Заглушка
				if ((MainWindow.SingleTon.SortSpeed) < 1000)
					Thread.Sleep((int)(1000 / MainWindow.SingleTon.SortSpeed));
			}
        }

		void Mergesort(int[] Array, int start, int end)
		{
			if (start == end) return;
			var middle = (start + end) / 2;
			Mergesort(Array, start, middle);
			Mergesort(Array, middle + 1, end);
			Merge(Array, start, middle, end);
		}

	}
}
