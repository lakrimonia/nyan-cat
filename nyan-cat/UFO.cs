using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace nyan_cat
{
    public class UFO : IEnemy
    {
        public Vector2 Velocity { get; }
        public Point LeftTopCorner { get; }
        public int Height { get; }
        public int Width { get; }
        public bool IsAlive { get; }

        public UFO(Point leftTopCorner)
        {
            Velocity = new Vector2(-1, 0);
            LeftTopCorner = leftTopCorner;
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
