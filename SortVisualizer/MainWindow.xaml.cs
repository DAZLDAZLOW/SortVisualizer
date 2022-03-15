using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Diagnostics;
using System.Threading;
using SortVisualizer.Algorithms;

namespace SortVisualizer
{
    public partial class MainWindow : Window
    {
        public static MainWindow SingleTon;
        public VisualHost DrawHost = new();
        public int ArraySize = 100;
        public int SortSpeed = 100;
        public int[] NumbersForSort;
        public bool IsSorted;
        public long TotalSwaped = 0;
        private int SortMethod;
        private List<ISortAlgorithm> SortAlgorithms = new();
        public MainWindow()
        {
            InitializeComponent();
            SortListInitialize();
            SingleTon = this;
            NumbersForSort = new int[ArraySize];
            for (int i = 0; i < NumbersForSort.Length; i++)
            {
                NumbersForSort[i] = i + 1;
            }
            IsSorted = true;
            ArraySizeTextBox.Text = ArraySize.ToString(); 
            ToDrawArea.Children.Add(DrawHost);
            DrawArray();
        }

        private void SortListInitialize()
        {
            //---Insert-your-Algorithms-here:---
            SortAlgorithms.Add(new BubbleSort());
            SortAlgorithms.Add(new QuickSort());
            SortAlgorithms.Add(new MergeSort());


            //---------------------------------
            SortAlgorithms = SortAlgorithms.OrderBy(a => a.Title).ToList();
            foreach(ISortAlgorithm algorithm in SortAlgorithms)
            {
                SortListBox.Items.Add(algorithm.Title);
            }
            SortMethod = 0;
            SortListBox.SelectedIndex = 0;
        }

        public void Swap(int IndexA, int IndexB)
        {
            SwapsLabel.Content = ++TotalSwaped;
            DrawArray();
        }

        public void DrawArray()
        {
            double LineWithSpaceSize = ToDrawArea.ActualWidth / NumbersForSort.Length;
            DrawingVisual drawingVisual = new DrawingVisual();
            DrawingContext drawingContext = drawingVisual.RenderOpen();
            for (int i = 0; i < NumbersForSort.Length; i++)
            {
                drawingContext.DrawLine(new Pen(Brushes.White, LineWithSpaceSize / 2), new Point(i * LineWithSpaceSize, ToDrawArea.ActualHeight),
                    new Point(i * LineWithSpaceSize, ToDrawArea.ActualHeight - NumbersForSort[i] * ToDrawArea.ActualHeight / NumbersForSort.Length));
            }
            drawingContext.Close();
            DrawHost.ChangeVisual(drawingVisual);
        }

        public void SortIsDone()
        {
            SortButton.IsEnabled = true;
            UnsortButton.IsEnabled = true;
            SortListBox.IsEnabled = true;
            ChangeArraySize.IsEnabled = true;
            IsSorted = true;
        }

        private async void SortButton_Click(object sender, RoutedEventArgs e)
        {
            TotalSwaped = 0;
            if (!IsSorted)
            {
                ChangeArraySize.IsEnabled = false;
                SortButton.IsEnabled = false;
                UnsortButton.IsEnabled = false;
                SortListBox.IsEnabled = false;
                Thread thread = new Thread(new ParameterizedThreadStart(SortAlgorithms[SortMethod].Sort));
                thread.Start(NumbersForSort);
            }
            
        }

        private void UnsortButton_Click(object sender, RoutedEventArgs e)
        {
            Random rnd = new Random();
            for (int i = NumbersForSort.Length - 1; i >= 1; i--)
            {
                int j = rnd.Next(i + 1);

                int tmp = NumbersForSort[j];
                NumbersForSort[j] = NumbersForSort[i];
                NumbersForSort[i] = tmp;
            }
            DrawArray();
            IsSorted = false;
        }

        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            DrawArray();
        }

        private void SpeedDownButton_Click(object sender, RoutedEventArgs e)
        {
            switch (SortSpeed)
            {
                case 10:
                    return;
                    break;
                case 25:
                    SortSpeed = 10;
                    SpeedLabel.Content = "0.10";
                    break;
                case 50:
                    SortSpeed = 25;
                    SpeedLabel.Content = "0.25";
                    break;
                case 100:
                    SortSpeed = 50;
                    SpeedLabel.Content = "0.50";
                    break;
                case 200:
                    SortSpeed = 100;
                    SpeedLabel.Content = "1.00";
                    break;
                case 500:
                    SortSpeed = 200;
                    SpeedLabel.Content = "2.00";
                    break;
                case 1000:
                    SortSpeed = 500;
                    SpeedLabel.Content = "5.00";
                    break;
            }
        }

        private void SpeedUpButton_Click(object sender, RoutedEventArgs e)
        {
            switch (SortSpeed)
            {
                case 10:
                    SortSpeed = 25;
                    SpeedLabel.Content = "0.25";
                    break;
                case 25:
                    SortSpeed = 50;
                    SpeedLabel.Content = "0.50";
                    break;
                case 50:
                    SortSpeed = 100;
                    SpeedLabel.Content = "1.00";
                    break;
                case 100:
                    SortSpeed = 200;
                    SpeedLabel.Content = "2.00";
                    break;
                case 200:
                    SortSpeed = 500;
                    SpeedLabel.Content = "5.00";
                    break;
                case 500:
                    SortSpeed = 1000;
                    SpeedLabel.Content = "10";
                    break;
                case 1000:
                    return;
            }
        }

        private void SortListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SortMethod = SortListBox.SelectedIndex;
        }

        private void ChangeArraySize_Click(object sender, RoutedEventArgs e)
        {
            string input = new string(ArraySizeTextBox.Text.Where(char.IsDigit).ToArray());
            if(input == "")
            {
                MessageBox.Show("Print number into textbox!", "Error");
                ArraySizeTextBox.Text = ArraySize.ToString();
                return;
            }
            if(Convert.ToInt32(input)>= 1000)
            {
                MessageBox.Show("Sorting large arrays can take a long time!", "Worning!");
            }
            ArraySizeTextBox.Text = input;
            ArraySize = Convert.ToInt32(input);
            NumbersForSort = new int[ArraySize];
            for (int i = 0; i < NumbersForSort.Length; i++)
            {
                NumbersForSort[i] = i + 1;
            }
            DrawArray();
        }
    }
}
