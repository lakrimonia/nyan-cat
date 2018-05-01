using System;
using System.Drawing;
using System.Numerics;

namespace nyan_cat
{
    public class Platform : IGameObject
    {
        public bool IsAlive { get; private set; }
        public int Height { get; }
        public Vector2 Velocity { get; private set; }
        public int Width { get; private set; }
        public Point LeftTopCorner { get; private set; }

        public Platform(Point leftTopCorner, int width)
        {
            //if (width <= 0 || width >= 1000 ||
            //    leftTopCorner.X < 0 || leftTopCorner.Y < 0)
            //    throw new ArgumentException();
            IsAlive = true;
            Height = 26;
            Width = width;
            LeftTopCorner = leftTopCorner;
            Velocity = new Vector2(-1, 0);
        }

        public void Move()
        {
            var dx = (int)Velocity.X;
            var dy = (int)Velocity.Y;
            IsAlive = LeftTopCorner.X + Width > Math.Abs(dx);
            if (LeftTopCorner.X + dx < 0)
            {
                Width += dx;
                dx = 0;
            }
            LeftTopCorner = new Point(LeftTopCorner.X + dx,
                LeftTopCorner.Y + dy);
        }

        public void Kill() => IsAlive = false;

        public void Accelerate(Vector2 acceleration)
        {
            Velocity = new Vector2(Velocity.X + acceleration.X,
                Velocity.Y + acceleration.Y);
        }

        public void Use(Game game) { }

        public override string ToString()
        {
            return $"Platform ({LeftTopCorner.X}, {LeftTopCorner.Y}, {Width})";
        }
    }
}
