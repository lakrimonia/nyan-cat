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
        public Vector2 Velocity { get; private set; }
        public Point LeftTopCorner { get; private set; }
        public int Height { get; }
        public int Width { get; }
        public bool IsAlive { get; private set; }
        public bool IsMet { get; set; }
        public Platform Location { get; private set; }
        public int Energy { get; private set; }
        public DogPathfinder Pathfinder { get; private set; }

    public Dog(Platform platform)
        {
            if (platform.Width <= 50 || platform.LeftTopCorner.Y < 50)
                throw new ArgumentException();
            Velocity = new Vector2(-1, 0);
            Height = 50;
            Width = 80;
            LeftTopCorner = GetLeftTopCorner(platform);
            IsAlive = true;
            Energy = 0;
            Location = platform;
            Pathfinder = new DogPathfinder();
        }

        public void Move()
        {
            throw new Exception("Wrong call!");
        }

        public void Move(Game game)
        {
            
            Energy += 1;
            if (Energy >= 30)
            {
                var nextPlatform = Pathfinder.FindPath(game, Location)[0];
                Energy = 0;
                LeftTopCorner = GetLeftTopCorner(nextPlatform);
                return;
            }

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
                game.Score -= 100;
                if (game.NyanCat.CurrentGem.Kind != GemKind.MilkLongLife)
                    game.Combo = 1 * game.AddCombo;
            }
        }

        private Point GetLeftTopCorner(Platform platform)
        {
            return new Point(platform.LeftTopCorner.X + platform.Width / 2,
                platform.LeftTopCorner.Y - Height);
        }


        public override string ToString()
        {
            return $"Dog ({LeftTopCorner.X}, {LeftTopCorner.Y})";
        }
    }
}
