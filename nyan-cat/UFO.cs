using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace nyan_cat
{
    public class UFO : IEnemy
    {
        public Vector2 Velocity { get; private set; }
        public Point LeftTopCorner { get; private set; }
        public int Height { get; }
        public int Width { get; }
        public bool IsAlive { get; private set; }
        public bool IsMet { get; set; }

        public UFO(Point leftTopCorner)
        {
            Velocity = UsualGameObjectProperties.Velocity;
            LeftTopCorner = leftTopCorner;
            Height = UsualGameObjectProperties.Height;
            Width = 80;
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
            IsMet = true;
            if (!game.NyanCat.ProtectedFromEnemies)
            {
                game.NyanCat.CurrentPowerUp?.Deactivate(game);
                game.NyanCat.CurrentPowerUp = null;
                var subtractedScore = game.Score < 100 ? 0 : Math.Max(game.Score.ToString().Length * 10, 1000000);
                game.Score -= subtractedScore;
                if (game.NyanCat.CurrentGem?.Kind != GemKind.MilkLongLife)
                    game.Combo = 1 * game.AddCombo;
            }
        }
    }
}
