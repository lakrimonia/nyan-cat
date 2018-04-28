using System;
using System.Drawing;
using System.Numerics;

namespace nyan_cat
{
    public class Cow : Milk
    {
        public new int Combo = 25;
        public new Vector2 Velocity { get; }
        public new Point LeftTopCorner { get; private set; }
        public new int Height { get; }
        public new int Width { get; }
        public new bool IsAlive { get; private set; }

        public  Cow(Point leftTopCorner) : base(leftTopCorner) 
        {
            if (leftTopCorner.X < 0 || leftTopCorner.Y < 0
                || leftTopCorner.X > 1000 || leftTopCorner.Y > 788)
                throw new ArgumentException();
            IsAlive = true;
            Width = 50;
            Height = 25;
            LeftTopCorner = leftTopCorner;
            Velocity = new Vector2(-1, 0);
        }

        new public void Move()
        {
            var dx = (int)Velocity.X;
            var dy = (int)Velocity.Y;
            LeftTopCorner = new Point(LeftTopCorner.X + dx,
                LeftTopCorner.Y + dy);
            IsAlive = LeftTopCorner.X > 0;
        }
        
                public override string ToString()
        {
            return $"Cow ({LeftTopCorner.X}, {LeftTopCorner.Y})";
        }
    }
}
