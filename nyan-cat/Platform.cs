using System;
using System.Drawing;
using System.Numerics;

namespace nyan_cat
{
    public class Platform : IGameObject
    {
        public bool IsAlive { get; private set; }
        public int Height { get; }
        public Vector2 Velocity { get; }
        public Point Center { get; private set; }
        public int Width { get; private set; }
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
            var dx = (int)Velocity.X;
            var dy = (int)Velocity.Y;
            IsAlive = RightTopCorner.X + Width <= Math.Abs(dx);
            if (!IsAlive)
                return;
            if (RightTopCorner.X + dx < 0)
            {
                Width -= dx;
                dx = dx / 2 + 2;
            }
            Center = new Point(Center.X + dx,
                Center.Y + dy);
            RightTopCorner = GetRightTopCorner(Center);
        }

        private Point GetRightTopCorner(Point center)
        {
            return new Point(center.X - Width / 2,
                center.Y - Height / 2);
        }
    }
}
