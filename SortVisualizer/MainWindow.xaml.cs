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
        public VisualHost drawHost = new();
        private readonly SortHost Host;
        private int arraySize = 100;
        public bool isSorted = true;
        public MainWindow()
        {
            InitializeComponent();
            Host = new SortHost(100);
            Host.OnSortDone += Host_OnSortDone;
            Host.OnRedraw += Host_OnRedraw;
            SortListInitialize();
            ArraySizeTextBox.Text = arraySize.ToString();
            ToDrawArea.Children.Add(drawHost);
            Host_OnRedraw(Host.GetArray());
        }

        private void SortListInitialize()
        {
            foreach (string Name in Host.GetAlgorithmNames())
            {
                SortListBox.Items.Add(Name);
            }
            SortListBox.SelectedIndex = 0;
        }

        private void Host_OnRedraw(int[] array)
        {
            double lineWithSpaceSize = ToDrawArea.ActualWidth / array.Length;
            DrawingVisual drawingVisual = new DrawingVisual();
            DrawingContext drawingContext = drawingVisual.RenderOpen();
            for (int i = 0; i < array.Length; i++)
            {
                drawingContext.DrawLine(new Pen(Brushes.White, lineWithSpaceSize / 2), new Point(i * lineWithSpaceSize, ToDrawArea.ActualHeight),
                    new Point(i * lineWithSpaceSize, ToDrawArea.ActualHeight - array[i] * ToDrawArea.ActualHeight / array.Length));
            }
            drawingContext.Close();
            drawHost.ChangeVisual(drawingVisual);
            SwapsLabel.Content = Host.TotalSwaps;
        }

        private void Host_OnSortDone()
        {
            SortButton.IsEnabled = true;
            UnsortButton.IsEnabled = true;
            SortListBox.IsEnabled = true;
            ChangeArraySize.IsEnabled = true;
            isSorted = true;
        }

        private async void SortButton_Click(object sender, RoutedEventArgs e)
        {
            if (!isSorted)
            {
                ChangeArraySize.IsEnabled = false;
                SortButton.IsEnabled = false;
                UnsortButton.IsEnabled = false;
                SortListBox.IsEnabled = false;
                Host.StartSort();
            }
        }

        private void UnsortButton_Click(object sender, RoutedEventArgs e)
        {
            Host.Unsort();
            isSorted = false;
        }

        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            Host_OnRedraw(Host.GetArray());
        }

        private void SpeedDownButton_Click(object sender, RoutedEventArgs e)
        {
            switch (Host.SortSpeed)
            {
                case 10:
                    return;
                case 25:
                    Host.SortSpeed = 10;
                    SpeedLabel.Content = "0.10";
                    break;
                case 50:
                    Host.SortSpeed = 25;
                    SpeedLabel.Content = "0.25";
                    break;
                case 100:
                    Host.SortSpeed = 50;
                    SpeedLabel.Content = "0.50";
                    break;
                case 200:
                    Host.SortSpeed = 100;
                    SpeedLabel.Content = "1.00";
                    break;
                case 500:
                    Host.SortSpeed = 200;
                    SpeedLabel.Content = "2.00";
                    break;
                case 1000:
                    Host.SortSpeed = 500;
                    SpeedLabel.Content = "5.00";
                    break;
            }
        }

        private void SpeedUpButton_Click(object sender, RoutedEventArgs e)
        {
            switch (Host.SortSpeed)
            {
                case 10:
                    Host.SortSpeed = 25;
                    SpeedLabel.Content = "0.25";
                    break;
                case 25:
                    Host.SortSpeed = 50;
                    SpeedLabel.Content = "0.50";
                    break;
                case 50:
                    Host.SortSpeed = 100;
                    SpeedLabel.Content = "1.00";
                    break;
                case 100:
                    Host.SortSpeed = 200;
                    SpeedLabel.Content = "2.00";
                    break;
                case 200:
                    Host.SortSpeed = 500;
                    SpeedLabel.Content = "5.00";
                    break;
                case 500:
                    Host.SortSpeed = 1000;
                    SpeedLabel.Content = "10";
                    break;
                case 1000:
                    return;
            }
        }

        private void SortListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Host.ChangeSortAlgoritm(SortListBox.SelectedIndex);
        }

        private void ChangeArraySize_Click(object sender, RoutedEventArgs e)
        {
            string input = new string(ArraySizeTextBox.Text.Where(char.IsDigit).ToArray());
            if(input == "")
            {
                MessageBox.Show("Print number into textbox!", "Error");
                ArraySizeTextBox.Text = arraySize.ToString();
                return;
            }
            if(Convert.ToInt32(input)>= 1000)
            {
                MessageBox.Show("Sorting large arrays can take a long time!", "Worning!");
            }
            ArraySizeTextBox.Text = input;
            arraySize = Convert.ToInt32(input);
            Host.CreateArray(arraySize);
        }
    }
}
