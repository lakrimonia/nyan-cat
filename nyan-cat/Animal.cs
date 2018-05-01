using System;
using System.Drawing;
using System.Numerics;

namespace nyan_cat
{
    public class Animal : IEnemy
    {
        public Tuple<int, int> BeginEnd { get; private set; }
        public Vector2 Velocity { get; private set; }
        public Point LeftTopCorner { get; private set; }
        public int Height { get; }
        public int Width { get; }
        public bool IsAlive { get; private set; }
        public bool IsMet { get; set; }

        public Animal(Platform platform)
        {
            if (platform.Width <= 50 || platform.LeftTopCorner.Y < 50)
                throw new ArgumentException();
            var x1 = platform.LeftTopCorner.X;
            var x2 = platform.LeftTopCorner.X + platform.Width;
            BeginEnd = Tuple.Create(x1, x2);
            Velocity = new Vector2(-1, 0);
            Height = 50;
            Width = 80;
            LeftTopCorner = GetLeftTopCorner(platform);
            IsAlive = true;
        }

        public void Move()
        {
            if (BeginEnd.Item2 - BeginEnd.Item1 <= Width)
            {
                IsAlive = false;
                return;
            }
            if (LeftTopCorner.X == BeginEnd.Item1)
                Velocity = new Vector2(0, 0);
            if (LeftTopCorner.X + Width == BeginEnd.Item2)
                Velocity = new Vector2(-2, 0);
            var dx = (int)Velocity.X;
            var dy = (int)Velocity.Y;

            LeftTopCorner = new Point(LeftTopCorner.X + dx,
                LeftTopCorner.Y + dy);
            BeginEnd = BeginEnd.Item1 == 0 ?
                Tuple.Create(0, BeginEnd.Item2 - 1) :
                Tuple.Create(BeginEnd.Item1 - 1, BeginEnd.Item2 - 1);
        }

        private Point GetLeftTopCorner(Platform platform)
        {
            return new Point(platform.LeftTopCorner.X,
                platform.LeftTopCorner.Y - Height);
        }

        public void Kill() => IsAlive = false;

        public void Accelerate(Vector2 acceleration)
        {
            Velocity = new Vector2(Velocity.X + acceleration.X,
                Velocity.Y + acceleration.Y);
        }

        public void Use(Game game)
        {
            IsMet = true;
            if (!game.NyanCat.ProtectedFromEnemies)
            {
                game.Score -= 100;
                if (game.NyanCat.CurrentGem.Kind != GemKind.MilkLongLife)
                    game.Combo = 1 * game.AddCombo;
            }
        }

        public override string ToString()
        {
            return $"Animal ({LeftTopCorner.X}, {LeftTopCorner.Y})";
        }
    }
}
