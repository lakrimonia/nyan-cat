using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace nyan_cat
{
    public class Food : IGameObject
    {
        public const int Points = 42;
        public Vector2 Velocity { get; }
        public Point LeftTopCorner { get; private set; }
        public int Height { get; }
        public int Width { get; }
        public bool IsAlive { get; private set; }

        public Food(Point leftTopCorner)
        {
            IsAlive = true;
            LeftTopCorner = leftTopCorner;
            Height = 50;
            Width = 50;
            Velocity = new Vector2(-1, 0);
        }

        public void Move()
        {
            throw new NotImplementedException();
            //var dx = (int)Velocity.X;
            //var dy = (int)Velocity.Y;
            //IsAlive = LeftTopCorner.X - Width / 2 < 0;
            //if (!IsAlive)
            //    return;
            //LeftTopCorner = new Point(LeftTopCorner.X + dx,
            //    LeftTopCorner.Y + dy);
        }

        public override string ToString()
        {
            return $"Food ({LeftTopCorner.X}, {LeftTopCorner.Y})";
        }
    }
}
