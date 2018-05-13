using System;
using System.Drawing;
using System.Numerics;

namespace nyan_cat
{
    public class TacNyan : IEnemy
    {
        public Tuple<int, int> BeginEnd { get; private set; }
        public Vector2 Velocity { get; private set; }
        public Point LeftTopCorner { get; private set; }
        public int Height { get; }
        public int Width { get; }
        public bool IsAlive { get; private set; }
        public bool IsMet { get; set; }

        public TacNyan(Platform platform)
        {
            if (platform.Width <= 50 || platform.LeftTopCorner.Y < 50)
                throw new ArgumentException();
            var x1 = platform.LeftTopCorner.X;
            var x2 = platform.LeftTopCorner.X + platform.Width;
            BeginEnd = Tuple.Create(x1, x2);
            Velocity = new Vector2(-1, 0);
            Height =UsualGameObjectProperties.Height;
            Width = 80;
            LeftTopCorner = GetLeftTopCorner(platform);
            IsAlive = true;
        }

        public void Move()
        {
            var dx = (int)Velocity.X;
            var dy = (int)Velocity.Y;
            if (LeftTopCorner.X + (int)Velocity.X < BeginEnd.Item1)
                LeftTopCorner = new Point(BeginEnd.Item1,
                LeftTopCorner.Y + dy);
            else if (LeftTopCorner.X + (int)Velocity.X > BeginEnd.Item2)
                LeftTopCorner = new Point(BeginEnd.Item2,
                LeftTopCorner.Y + dy);
            else
                LeftTopCorner = new Point(LeftTopCorner.X + dx,
                    LeftTopCorner.Y + dy);
            if (LeftTopCorner.X == BeginEnd.Item1)
                Velocity = new Vector2(0, 0);
            if (LeftTopCorner.X + Width == BeginEnd.Item2)
                Velocity =
                    new Vector2(UsualGameObjectProperties.Velocity.X * 2, 0);
            BeginEnd = 
                Tuple.Create(BeginEnd.Item1 + (int)UsualGameObjectProperties.Velocity.X,
                BeginEnd.Item2 + (int)UsualGameObjectProperties.Velocity.X);
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
                game.NyanCat.CurrentPowerUp?.Deactivate(game);
                game.NyanCat.CurrentPowerUp = null;
                var subtractedScore = game.Score < 100 ? 0 : Math.Min(game.Score.ToString().Length * 10, 1000000);
                game.Score -= subtractedScore;
                if (!game.ComboProtectedFromEnemies)
                    game.Combo = 1 * game.AddCombo;
            }
        }

        public override string ToString()
        {
            return $"TacNyan ({LeftTopCorner.X}, {LeftTopCorner.Y})";
        }
    }
}
