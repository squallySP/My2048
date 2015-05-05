using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    /// Interaction logic for TileControl.xaml
    /// </summary>
    public partial class TileControl : UserControl, INotifyPropertyChanged
    {
        public static int moveTurns = 10;
        private int _value;
        public int Value
        {
            get { return _value; }
            set
            { 
                _value = value;
                Notify("Value");
            }
        }
        public Cell CurrentCell { get; set; }
        public Cell TargetCell { get; set; }
        public Position CurrentPosition { get; private set; }
        private Position DeltaPosition { get; set; }

        public TileControl(Cell cell)
        {
            Value = 2;
            CurrentCell = cell;
            TargetCell = null;
            CurrentPosition = new Position(CurrentCell.X * 80, CurrentCell.Y * 80);
            DeltaPosition = new Position(0, 0);

            this.Margin = new Thickness(CurrentPosition.X, CurrentPosition.Y, 0, 0);


            InitializeComponent();

            grid.DataContext = this;
        }

        // for INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        public void Notify(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }

}
