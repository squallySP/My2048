using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace My2048
{
    public class Cell
    {
        public int X { get; set; }
        public int Y { get; set; }

        public Cell(int x, int y)
        {
            X = x;
            Y = y;
        }

        public Cell Clone()
        {
            return new Cell(X, Y);
        }

        public bool IsSameAs(Cell cell)
        {
            return X == cell.X && Y == cell.Y;
        }
    }
}
