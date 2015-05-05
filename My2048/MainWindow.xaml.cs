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

namespace My2048
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void ButtonNewGame_Click(object sender, RoutedEventArgs e)
        {
            //MessageBox.Show("Hello");
            var myTile = new TileControl(new Cell(0,0));
            var myTile2 = new TileControl(new Cell(1, 1));
            TileConvas.Children.Add(myTile);
            TileConvas.Children.Add(myTile2);
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
                MessageBox.Show("RIGHT");
        }
    }
}
