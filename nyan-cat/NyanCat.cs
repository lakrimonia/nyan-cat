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
        private int jumpTime;
        private bool canJump;
        public int Height { get; internal set; }
        public int Width { get; internal set; }
        public Point LeftTopCorner { get; internal set; }
        public Vector2 Velocity { get; private set; }
        public CatState State { get; set; }
        public Gem CurrentGem { get; set; }
        public PowerUp CurrentPowerUp { get; set; }
        public bool ProtectedFromBombs { get; internal set; }
        public bool ProtectedFromEnemies { get; internal set; }

        public NyanCat(Point leftTopCorner)
        {
            IsAlive = true;
            Height = 50;
            Width = 80;
            LeftTopCorner = leftTopCorner;
            Velocity = new Vector2(0, 0);
            canJump = true;
        }

        public void Jump()
        {
            if (State == CatState.Fall)
                return;
            if (State == CatState.Run)
                canJump = true;
            else if (canJump)
                canJump = false;
            else
                return;
            jumpTime = 0;
            Velocity = new Vector2(0, -15);
            State = CatState.Jump;
        }

        public void LandOnPlatform(Platform platform)
        {
            State = CatState.Run;
            LeftTopCorner = new Point(LeftTopCorner.X,
                platform.LeftTopCorner.Y - Height);
        }

        public void Move()
        {
            if (State == CatState.Jump && jumpTime >= 15)
                State = CatState.Fall;
            else
            {
                jumpTime += 1;
                Accelerate(new Vector2(0, 1));
            }
            if (State == CatState.Fall)
            {
                Velocity = new Vector2(0, 10);
                State = CatState.Fall;
            }
                
            if (State == CatState.Run)
                Velocity = new Vector2(0, 0);
            if (LeftTopCorner.Y + (int)Velocity.Y < 0)
            {
                LeftTopCorner = new Point(LeftTopCorner.X + (int)Velocity.X, 0);
                State = CatState.Fall;
                return;
            }
            LeftTopCorner = new Point(LeftTopCorner.X + (int)Velocity.X,
                LeftTopCorner.Y + (int)Velocity.Y);

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
