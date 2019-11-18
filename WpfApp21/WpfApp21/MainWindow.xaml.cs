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

namespace WpfApp21
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            //ChangeColorPeriodically();
        }

        int heightSize = 90;
        int widthSize = 90;
        Rectangle[,] cell;

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            for (int j = 0; j < heightSize; j++)
            {
                myGrid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(11) });
            }

            for (int j = 0; j < widthSize; j++)
            {
                myGrid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(11) });
            }

            cell = new Rectangle[heightSize, widthSize];
            for (int i = 0; i < heightSize; i++)
            {
                for (int j = 0; j < widthSize; j++)
                {
                    cell[i, j] = new Rectangle();
                    Grid.SetColumn(cell[i, j], j);
                    Grid.SetRow(cell[i, j], i);
                    //rField[i, j].MouseDown += this.rctnField_MouseDown;
                    cell[i, j].MouseDown += this.cell_MouseDown;
                    
                    cell[i, j].Stroke = Brushes.Black;
                    cell[i, j].StrokeThickness = 0.09;
                    cell[i, j].Height = 10;
                    cell[i, j].Width = 10;
                    cell[i, j].Fill = Brushes.White;
                    myGrid.Children.Add(cell[i, j]);
                }
            }
        }

        private void cell_MouseDown(object sender, MouseButtonEventArgs e)
        {
            var item = sender as Rectangle;
            item.Fill = Brushes.DarkGreen;
        }


        //async Task ChangeColorPeriodically()
        //{
        //    while (true)
        //    {
        //        await Task.Delay(1000); // <--
        //        if (cell[1, 1].Fill == Brushes.Black)
        //            cell[1, 1].Fill = Brushes.White;
        //        else
        //            cell[1, 1].Fill = Brushes.Black;
        //    }
        //}

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Random r = new Random();

            for (int i = 0; i < heightSize; i++)
            {
                for (int j = 0; j < widthSize; j++)
                {
                    if (r.Next(2) == 1)
                        cell[i, j].Fill = Brushes.DarkGreen;
                    else
                        cell[i, j].Fill = Brushes.White;
                }
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            neighbors();
        }

        bool startStop = false;
        async void neighbors()
        {            
            int sumNeighbors;
            int[,] nextGen = new int[heightSize, widthSize];

            if (startStop == false)
                startStop = true;
            else
                startStop = false;

            while (startStop)
            {
                for (int i = 0; i < heightSize; i++)
                {
                    for (int j = 0; j < widthSize; j++)
                    {
                        //await Task.Delay(2000);
                        sumNeighbors = 0;
                        

                        if ((i - 1 >= 0) && (j - 1 >= 0))
                        {
                            if (cell[i - 1, j - 1].Fill == Brushes.DarkGreen)
                                sumNeighbors++;
                        }
                            
                        if (i - 1 >= 0)
                        {
                            if (cell[i - 1, j].Fill == Brushes.DarkGreen)
                                sumNeighbors ++;
                        }
                            

                        if (i - 1 >= 0 && j + 1 < widthSize)
                        {
                            if (cell[i - 1, j + 1].Fill == Brushes.DarkGreen)
                                sumNeighbors ++ ;
                        }
                            

                        if (j - 1 >= 0)
                        {
                            if (cell[i, j - 1].Fill == Brushes.DarkGreen)
                                sumNeighbors ++ ;
                        }
                            

                        if (j + 1 < widthSize)
                        {
                            if (cell[i, j + 1].Fill == Brushes.DarkGreen)
                                sumNeighbors ++ ;
                        }
                            

                        if ((i + 1 < heightSize) && (j - 1 >= 0))
                        {
                            if (cell[i + 1, j - 1].Fill == Brushes.DarkGreen)
                                sumNeighbors ++ ;
                        }
                            

                        if (i + 1 < heightSize)
                        {
                            if (cell[i + 1, j].Fill == Brushes.DarkGreen)
                                sumNeighbors ++ ;
                        }
                            

                        if ((i + 1 < heightSize) && (j + 1 < widthSize))
                        {
                            if (cell[i + 1, j + 1].Fill == Brushes.DarkGreen)
                                sumNeighbors ++ ;
                        }

                        //MessageBox.Show("Кол-во соседей = " + sumNeighbors);
                        

                        if (cell[i, j].Fill == Brushes.White && sumNeighbors == 3)
                        {
                            //MessageBox.Show("Рождение новой клетки: " + i + " " + j);
                            nextGen[i, j] = 1;
                        }
                            

                        if (cell[i, j].Fill == Brushes.DarkGreen)
                        {
                            if (sumNeighbors == 2 || sumNeighbors == 3)
                                nextGen[i, j] = 1;
                            else nextGen[i, j] = 0;
                        }                      
                    }
                }




                for (int i = 0; i < heightSize; i++)
                {
                    for (int j = 0; j < widthSize; j++)
                    {
                        if (nextGen[i, j] == 0)
                            cell[i, j].Fill = Brushes.White;
                        else
                        {
                            //MessageBox.Show("Сработало:\ncell[i, j].Fill = Brushes.DarkGreen;");
                            //MessageBox.Show("Клетка с индексом: " + i +
                             //   ", " + j + " = зеленая");
                            cell[i, j].Fill = Brushes.DarkGreen;
                        }                            
                    }
                }

                await Task.Delay(500);
            }
        }

        private void Clear_Click(object sender, RoutedEventArgs e)
        {
            for (int i = 0; i < heightSize; i++)
            {
                for (int j = 0; j < widthSize; j++)
                {
                    cell[i, j].Fill = Brushes.White;
                }
            }
        }
    }
}
