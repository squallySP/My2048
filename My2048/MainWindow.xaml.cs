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
    /// 

    public partial class MainWindow : Window
    {
        public static int steps = 5;

        private int bestScore = 0;
        private GameBoard myGameBoard = new GameBoard();
        public MainWindow()
        {
            InitializeComponent();
            NewGame();
            grid.DataContext = this.myGameBoard;            
        }

        private void ButtonNewGame_Click(object sender, RoutedEventArgs e)
        {
            bestScore = myGameBoard.BestScore;
            NewGame();
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (myGameBoard.IsBusy)
                return;

            bool canMove = false;
            myGameBoard.IsBusy = true;

            if (e.Key == Key.Up)
                canMove = myGameBoard.TryToMoveUp();
            if (e.Key == Key.Down)
                canMove = myGameBoard.TryToMoveDown();
            if (e.Key == Key.Left)
                canMove = myGameBoard.TryToMoveLeft();
            if (e.Key == Key.Right)
                canMove = myGameBoard.TryToMoveRight();

            if (canMove)
            {
                for (int i = 0; i < steps; i++)
                {
                    myGameBoard.MoveOneStep();
                    Refresh();
                    Thread.Sleep(50);
                }
                myGameBoard.Move();
                myGameBoard.RecycleTiles();
                myGameBoard.Upgrade();
                myGameBoard.AddTileToCells();
                if(myGameBoard.IsGameOver())
                {
                    MessageBox.Show("GameOver!");
                }
                Refresh();
            }

            myGameBoard.IsBusy = false;
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

        private void NewGame()
        {
            myGameBoard.ReNew();

            myGameBoard.AddTileToCells();
            Thread.Sleep(20);
            myGameBoard.AddTileToCells();

            Refresh();
        }

        private void MoveTiles()
        {
            for (int i = 0; i < steps; i++)
            {
                Thread.Sleep(50);
                myGameBoard.MoveOneStep();
                Refresh();
            }
            myGameBoard.Move();
        }
                
    }
}
