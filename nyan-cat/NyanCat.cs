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
        public Point Center { get; private set; }
        public Vector2 Velocity { get; private set; }
        public CatState State;
        public IGem CurrentGem { get; }
        public IPowerUp CurrentPowerUp { get; }

        public NyanCat(Point center)
        {
            IsAlive = true;
            Height = 80;
            Width = 50;
            Center = center;
            Velocity = new Vector2(0, 0);
        }

        public void Jump()
        {
            Velocity = new Vector2(0, 10);
            State = CatState.Jump;
        }

        public void Move()
        {
            if (State == CatState.Fall)
                Velocity = new Vector2(0, -10);
            Center = new Point(Center.X - (int)Velocity.X,
                Center.Y - (int)Velocity.Y);
        }
    }
}
