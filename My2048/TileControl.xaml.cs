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
    /// Interaction logic for TileControl.xaml
    /// </summary>
    public partial class TileControl : UserControl
    {
        public int PosX { get; set; }
        public int PosY { get; set; }
        public int CurrentPosX { get; set; }
        public int CurrentPosY { get; set; }
        public int TargetPosX { get; set; }
        public int TargetPosY { get; set; }

        public TileControl()
        {
            InitializeComponent();
        }
    }
}
