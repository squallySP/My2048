using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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

namespace My2048
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public TileControl myTile = new TileControl(0,0);
        public TileControl myTile2 = new TileControl(2,2);
        private GameBoard myGameBoard;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void ButtonNewGame_Click(object sender, RoutedEventArgs e)
        {
            myGameBoard = new GameBoard();

            myGameBoard.AddTileToCells();
            Thread.Sleep(20); 
            myGameBoard.AddTileToCells();

            Refresh();


        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Up)
                MessageBox.Show("UP");
            if (e.Key == Key.Down)
                MessageBox.Show("DOWN");
            if (e.Key == Key.Left)
                MessageBox.Show("LEFT");
            if (e.Key == Key.Right)
            {
                //MessageBox.Show(myTile.ToString());
                //myTile.CurrentPosition
                myTile.Move();
            }
        }

        private void Refresh()
        {
            TileConvas.Children.Clear();

            foreach (TileControl tile in myGameBoard.gameCells)
            {
                if (tile != null)
                    TileConvas.Children.Add(tile);
            }
        }
    }
}
