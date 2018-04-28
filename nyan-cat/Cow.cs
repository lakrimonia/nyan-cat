using System.Drawing;

namespace nyan_cat
{
    public class Cow : Milk
    {
        public new int Combo = 25;
        public Cow(Point leftTopCorner) : base(leftTopCorner) { }
        public override string ToString()
        {
            return $"Cow ({LeftTopCorner.X}, {LeftTopCorner.Y})";
        }
    }
}
