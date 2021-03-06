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
        public Vector2 Velocity { get; private set; }
        public Point LeftTopCorner { get; private set; }
        public int Height { get; }
        public int Width { get; }
        public bool IsAlive { get; private set; }

        public Food(Point leftTopCorner)
        {
            IsAlive = true;
            LeftTopCorner = leftTopCorner;
            Height = UsualGameObjectProperties.Height;
            Width = UsualGameObjectProperties.Width;
            Velocity = UsualGameObjectProperties.Velocity;
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
            if (game.NyanCat.CurrentPowerUp?.Kind == PowerUpKind.MilkGlasses)
                game.Combo += game.MilkGlassesCombo * game.AddCombo;
            else
                game.Score += Points * game.Combo;
            Kill();
        }

        public override string ToString()
        {
            return $"Food ({LeftTopCorner.X}, {LeftTopCorner.Y})";
        }
    }
}
