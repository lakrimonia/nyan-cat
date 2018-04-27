using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace nyan_cat
{
    public class Milk : IGameObject
    {
        public int Combo = 1;
        public Vector2 Velocity { get; }
        public Point Center { get; }
        public int Height { get; }
        public int Width { get; }
        public bool IsAlive { get; }

        public Milk(Point center)
        {
            Velocity = new Vector2(-1, 0);
            Center = center;
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
