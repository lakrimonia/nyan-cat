using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace nyan_cat
{
    public enum PowerUpKind
    {
        TurboNyan, MilkGlasses, BigNyan, LoveNyan, FloristNyan,
        DoggieNyan, Piano
    }

    public class PowerUp : IGameObject
    {
        public PowerUpKind Kind { get; }
        public Vector2 Velocity { get; private set; }
        public Point LeftTopCorner { get; private set; }
        public int Height { get; }
        public int Width { get; }
        public bool IsAlive { get; private set; }

        public PowerUp(Point leftTopCorner, PowerUpKind kind)
        {
            if (leftTopCorner.X < 0 || leftTopCorner.Y < 0
                || leftTopCorner.X > 1000 || leftTopCorner.Y > 788)
                throw new ArgumentException();
            Kind = kind;
            Velocity = new Vector2(-1, 0);
            LeftTopCorner = leftTopCorner;
            Height = 50;
            Width = 50;
            IsAlive = true;
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

        public void Accelerate(Vector2 acceleration)
        {
            Velocity = new Vector2(Velocity.X + acceleration.X,
                Velocity.Y + acceleration.Y);
        }

        public override string ToString()
        {
            return $"{Kind} ({LeftTopCorner.X}, {LeftTopCorner.Y})";
        }
    }
}
