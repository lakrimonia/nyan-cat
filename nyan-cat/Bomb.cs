using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace nyan_cat
{
    public class Bomb : IGameObject
    {
        public Vector2 Velocity { get; }
        public Point LeftTopCorner { get; private set; }
        public int Height { get; }
        public int Width { get; }
        public bool IsAlive { get; private set; }

        public Bomb(Point leftTopCorner)
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

        public void Move()
        {
            var dx = (int)Velocity.X;
            var dy = (int)Velocity.Y;
            LeftTopCorner = new Point(LeftTopCorner.X + dx,
                LeftTopCorner.Y + dy);
            IsAlive = LeftTopCorner.X > 0;
        }

        public void Kill() => IsAlive = false;

        public override string ToString()
        {
            return $"Bomb ({LeftTopCorner.X}, {LeftTopCorner.Y})";
        }
    }
}
