using System;
using System.Drawing;
using System.Numerics;

namespace nyan_cat
{
    public class Cow : Milk
    {
        public  Cow(Point leftTopCorner) : base(leftTopCorner) { }

        public override string ToString()
        {
            return $"Cow ({LeftTopCorner.X}, {LeftTopCorner.Y})";
        }
    }
}
