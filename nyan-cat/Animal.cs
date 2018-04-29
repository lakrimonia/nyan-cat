using System;
using System.Drawing;
using System.Numerics;

namespace nyan_cat
{
    public class Animal : IEnemy
    {
        public Tuple<int, int> BeginEnd { get; }
        public Vector2 Velocity { get; }
        public Point LeftTopCorner { get; }
        public int Height { get; }
        public int Width { get; }
        public bool IsAlive { get; }

        public Animal(Point leftTopCorner, Platform platform)
        {
            var x1 = platform.LeftTopCorner.X;
            var x2 = platform.LeftTopCorner.X + platform.Width;
            BeginEnd = Tuple.Create(x1, x2);
            // TODO: Velocity =
            Height = 50;
            Width = 50;
            IsAlive = true;
        }

        public void Move()
        {
            throw new NotImplementedException();
        }
    }
}
