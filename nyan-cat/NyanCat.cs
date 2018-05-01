using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace nyan_cat
{
    public enum CatState
    {
        Run,
        Jump,
        Fall
    }

    public class NyanCat : IGameObject
    {
        public bool IsAlive { get; private set; }

        public int Height => CurrentPowerUp?.Kind == PowerUpKind.BigNyan
            ? 50 * 2
            : 50;

        public int Width => CurrentPowerUp?.Kind == PowerUpKind.BigNyan
            ? 80 * 2
            : 80;

        private Point leftTopCorner;

        public Point LeftTopCorner => CurrentPowerUp?.Kind == PowerUpKind.BigNyan
            ? new Point(leftTopCorner.X, leftTopCorner.Y - 50)
            : leftTopCorner;
        public Vector2 Velocity { get; private set; }
        public CatState State { get; set; }
        public Gem CurrentGem { get; set; }
        public PowerUp CurrentPowerUp { get; set; }

        public NyanCat(Point leftTopCorner)
        {
            //if (leftTopCorner.X < 0 || leftTopCorner.Y < 0)
            //    throw new ArgumentException();
            IsAlive = true;
            //Height = 80;
            //Width = 50;
            this.leftTopCorner = leftTopCorner;
            Velocity = new Vector2(0, 0);
        }

        public void Jump()
        {
            Velocity = new Vector2(0, -10);
            State = CatState.Jump;
        }

        public void Move()
        {
            if (State == CatState.Fall)
                Velocity = new Vector2(0, 10);
            if (State == CatState.Run)
                Velocity = new Vector2(0, 0);
            if (LeftTopCorner.Y + (int)Velocity.Y < 0)
            {
                leftTopCorner = new Point(LeftTopCorner.X + (int)Velocity.X, 0);
                State = CatState.Fall;
                return;
            }
            leftTopCorner = new Point(leftTopCorner.X + (int)Velocity.X,
                leftTopCorner.Y + (int)Velocity.Y);

            if (LeftTopCorner.Y >= 788)
                IsAlive = false;
        }

        public void Kill() => IsAlive = false;

        public void Accelerate(Vector2 acceleration)
        {
            Velocity = new Vector2(Velocity.X + acceleration.X,
                Velocity.Y + acceleration.Y);
        }

        public void Use(Game game) { }

        public override string ToString()
        {
            return $"NyanCat({LeftTopCorner.X}, {LeftTopCorner.Y}), State =  {State}, Velocity = {Velocity}";
        }
    }
}
