using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace nyan_cat
{
    public interface IGameObject
    {
        Vector2 Velocity { get; }
        Point LeftTopCorner { get; }
        int Height { get; }
        int Width { get; }
        bool IsAlive { get; }

        void Move();
        void Kill();
        void Accelerate(Vector2 acceleration);
    }
}
