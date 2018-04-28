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
        public bool IsAlive { get; }
        public int Height { get; }
        public int Width { get; }
        public Point LeftTopCorner { get; private set; }
        public Vector2 Velocity { get; private set; }
        public CatState State;
        public Gem CurrentGem { get; }
        public PowerUp CurrentPowerUp { get; }

        public NyanCat(Point leftTopCorner)
        {
            IsAlive = true;
            Height = 80;
            Width = 50;
            LeftTopCorner = leftTopCorner;
            Velocity = new Vector2(0, 0);
        }

        public void Jump()
        {
            Velocity = new Vector2(0, 10);
            State = CatState.Jump;
        }

        public void Move()
        {
            throw new NotImplementedException();
            //if (State == CatState.Fall)
            //    Velocity = new Vector2(0, -10);
            //if (State == CatState.Run)
            //    Velocity = new Vector2(0, 0);
            //if (LeftTopCorner.Y + (int)Velocity.Y <= 0)
            //{
            //    LeftTopCorner = new Point(LeftTopCorner.X + (int)Velocity.X, 0);
            //    State = CatState.Fall;
            //}
            //LeftTopCorner = new Point(LeftTopCorner.X + (int)Velocity.X,
            //    LeftTopCorner.Y + (int)Velocity.Y);
        }
    }
}
