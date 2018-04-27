using System.Drawing;

namespace nyan_cat
{
    public class Cow : Milk
    {
        public new int Combo = 25;
        public Cow(Point center) : base(center) { }
    }
}
