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
        public Point Center { get; }
        public int Height { get; }
        public int Width { get; }
        public bool IsAlive { get; }

        public Bomb(Point center)
        {
            IsAlive = true;
            Width = 50;
            Height = 25;
            Center = center;
            Velocity = new Vector2(-1, 0);
        }

        public void Move()
        {
            throw new NotImplementedException();
        }
    }
}
