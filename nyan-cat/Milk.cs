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
        public Vector2 Velocity { get; private set; }
        public Point LeftTopCorner { get; private set; }
        public int Height { get; }
        public int Width { get; }
        public bool IsAlive { get; private set; }

        public Milk(Point leftTopCorner)
        {
            Velocity = UsualGameObjectProperties.Velocity;
            LeftTopCorner = leftTopCorner;
            Height = UsualGameObjectProperties.Height;
            Width = UsualGameObjectProperties.Width;
            IsAlive = true;
        }

        public void Move()
        {
            var dx = (int)Velocity.X;
            var dy = (int)Velocity.Y;
            LeftTopCorner = new Point(LeftTopCorner.X + dx,
                LeftTopCorner.Y + dy);
            IsAlive = IsAlive && LeftTopCorner.X > 0;
        }

        public void Kill() => IsAlive = false;

        public void Accelerate(Vector2 acceleration)
        {
            Velocity = new Vector2(Velocity.X + acceleration.X,
                Velocity.Y + acceleration.Y);
        }

        public void Use(Game game)
        {
            game.Combo += this is Cow
                ? 25 * game.MilkGlassesCombo * game.AddCombo
                : 1 * game.MilkGlassesCombo * game.AddCombo;
            Kill();
        }

        public override string ToString()
        {
            return $"Milk ({LeftTopCorner.X}, {LeftTopCorner.Y})";
        }
    }
}
