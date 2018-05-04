using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace nyan_cat
{
    public class Dog : IGameObject
    {
        public Vector2 Velocity { get; }
        public Point LeftTopCorner { get; }
        public int Height { get; }
        public int Width { get; }
        public bool IsAlive { get; }

        public void Move()
        {
            throw new NotImplementedException();
        }

        public void Use(Game game)
        {
            throw new NotImplementedException();
        }

        public void Kill()
        {
            throw new NotImplementedException();
        }

        public void Accelerate(Vector2 acceleration)
        {
            throw new NotImplementedException();
        }
    }
}
