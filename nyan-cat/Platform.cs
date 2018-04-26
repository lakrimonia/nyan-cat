using System.Drawing;
using System.Numerics;

namespace nyan_cat
{
    public class Platform : IGameObject
    {
        public int Height = 10;
        public Vector2 Velocity { get; }
        public Point Center { get; private set; }
        public int Length { get; }
        public Point RightTopCorner { get; private set; }

        public Platform(Point center, int length)
        {
            Center = center;
            Length = length;
            RightTopCorner = GetRightTopCorner(Center);
            Velocity = new Vector2(-1, 0);
        }

        public void Move()
        {
            Center = new Point(Center.X - (int)Velocity.X,
                Center.Y - (int)Velocity.Y);
            RightTopCorner = GetRightTopCorner(Center);
        }

        private Point GetRightTopCorner(Point center)
        {
            return new Point(center.X - Length / 2,
                center.Y - Height / 2);
        }
    }
}
