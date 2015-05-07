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
        public static int steps = 5;
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
        public Position DeltaPosition { get; set; }
        public bool toRecycle = false;
        public bool toUpgrade = false;
        public bool toShowup = false;
        public bool hasMoved = false;

        public TileControl()
        {
            Value = 2;
            InitializeComponent();
            grid.DataContext = this;
        }

        public TileControl(int i, int j)
        {
            Cell cell = new Cell(i, j);
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


        public void Move()
        {
            CurrentPosition.X = TargetCell.X * 80;
            CurrentPosition.Y = TargetCell.Y * 80;
            this.Margin = new Thickness(CurrentPosition.X, CurrentPosition.Y, 0, 0);
        }

        public void MoveOneStep()
        {
            CurrentPosition.X = CurrentPosition.X + DeltaPosition.X;
            CurrentPosition.Y = CurrentPosition.Y + DeltaPosition.Y;
            this.Margin = new Thickness(CurrentPosition.X, CurrentPosition.Y, 0, 0);
        }

        public void ShowUp() 
        {

        }

        public void Recycle() 
        {
            
            toRecycle = false;
            toUpgrade = false;
            toShowup = false;
            hasMoved = false;

            Value = 2;
            CurrentCell = null;
            TargetCell = null;
            CurrentPosition = null;
            DeltaPosition = null;
        }

        public void Upgrade()
        {
            toUpgrade = false;
            Value = Value * 2;
        }

        public void MovementOver()
        {
            toRecycle = false;
            toUpgrade = false;
            toShowup = false;
            hasMoved = false;

            CurrentCell = TargetCell;
            TargetCell = null;
            CurrentPosition = new Position(CurrentCell.X * 80, CurrentCell.Y * 80);
            DeltaPosition = new Position(0, 0);
            this.Margin = new Thickness(CurrentPosition.X, CurrentPosition.Y, 0, 0);
        }

        public void SetToCell(int i, int j)
        {
            Cell cell = new Cell(i, j);
            Value = 2;
            CurrentCell = cell;
            TargetCell = null;
            CurrentPosition = new Position(CurrentCell.X * 80, CurrentCell.Y * 80);
            DeltaPosition = new Position(0, 0);
            this.Margin = new Thickness(CurrentPosition.X, CurrentPosition.Y, 0, 0);
        }
    }

}
