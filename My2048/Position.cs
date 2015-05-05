using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace My2048
{
    public class Position
    {
        public double X { get; set; }
        public double Y { get; set; }

        public Position(double x, double y)
        {
            X = x;
            Y = y;
        }

        public static Position operator +(Position oldP, Position deltaP)
        {
            return new Position(oldP.X + deltaP.X, oldP.Y + deltaP.Y);
        }
    }
}
