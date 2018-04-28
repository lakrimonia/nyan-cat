using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace nyan_cat
{
    public enum GemKind
    {
        DoubleCombo,
        Invulnerable,
        MilkLongLife
    }

    public class Gem : IGameObject
    {
        public GemKind Kind { get; }
        public Vector2 Velocity { get; }
        public Point LeftTopCorner { get; }
        public int Height { get; }
        public int Width { get; }
        public bool IsAlive { get; }

        public Gem(Point leftTopCorner, GemKind kind)
        {
            LeftTopCorner = leftTopCorner;
            Kind = kind;
            Velocity = new Vector2(-1, 0);
            Height = 50;
            Width = 50;
            IsAlive = true;
        }

        public void Move()
        {
            throw new NotImplementedException();
        }

        public override string ToString()
        {
            return $"Gem ({LeftTopCorner.X}, {LeftTopCorner.Y})";
        }
    }
}
