using SortVisualizer.Algorithms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SortVisualizer
{
    public class SortHost
    {
        private readonly Random HolyRandom = new Random();
        private readonly List<ISortAlgorithm> sortAlgorithms = new();
        private ISortAlgorithm currentSortAlgorithm;
        private int[] array;
        public int SortSpeed { get; set; } //Delay for Sort = 1000/SortSpeed (Milliseconds) 
        public int TotalSwaps { private set; get; }
        public event Action<int[]> OnRedraw;
        public event Action OnSortDone;
       
        public SortHost(int ArraySize)
        {
            //---Insert-your-Algorithms-here:---
            sortAlgorithms.Add(new BubbleSort());
            sortAlgorithms.Add(new QuickSort());
            sortAlgorithms.Add(new MergeSort());


            //---------------------------------
            foreach (ISortAlgorithm algorithm in sortAlgorithms)
            {
                algorithm.Wait = Wait;
                algorithm.OnRedraw += Algorithm_OnRedraw;
                algorithm.OnSortDone += Algorithm_OnSortDone;
            }
            sortAlgorithms = sortAlgorithms.OrderBy(a => a.Title).ToList();
            currentSortAlgorithm = sortAlgorithms[0];
            CreateArray(ArraySize);
            SortSpeed = 100;
            TotalSwaps = 0;
        }

        public async void StartSort()
        {
            TotalSwaps = 0;
            var Sorting = currentSortAlgorithm.Sort(array);


            await Sorting;
        }

        public void Unsort()
        {
            for (int i = array.Length - 1; i >= 1; i--)
            {
                int j = HolyRandom.Next(i + 1);

                int tmp = array[j];
                array[j] = array[i];
                array[i] = tmp;
            }
            OnRedraw(array);
        }

        public void CreateArray(int Size)
        {
            array = new int[Size];
            for (int i = 0; i < array.Length; i++)
            {
                array[i] = i + 1;
            }
            if (OnRedraw != null)
                OnRedraw(array);
        }

        public async Task Wait()
        {
            if (SortSpeed < 1000)
                await Task.Delay(1000 / SortSpeed);
        }

        private void Algorithm_OnRedraw()
        {
            TotalSwaps++;
            OnRedraw(array);
        }

        private void Algorithm_OnSortDone() => OnSortDone();

        public int GetArraySize() => array.Length;

        public int[] GetArray() => array;

        public string[] GetAlgorithmNames() => sortAlgorithms.Select(s => s.Title).ToArray();

        public void ChangeSortAlgoritm(int Index) => currentSortAlgorithm = sortAlgorithms[Index];

    }
}
