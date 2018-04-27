using System.Drawing;
using System.Numerics;

namespace nyan_cat
{
    public class Platform : IGameObject
    {
        public bool IsAlive { get; }
        public int Height { get; }
        public Vector2 Velocity { get; }
        public Point Center { get; private set; }
        public int Width { get; }
        public Point RightTopCorner { get; private set; }

        public Platform(Point center, int width)
        {
            IsAlive = true;
            Height = 26;
            Center = center;
            Width = width;
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
            return new Point(center.X - Width / 2,
                center.Y - Height / 2);
        }
    }
}
