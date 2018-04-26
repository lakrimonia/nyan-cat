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
        public Point Center { get; }
        public Vector2 Velocity { get; }
        public CatState State;
        public IGem CurrentGem { get; }
        public IPowerUp CurrentPowerUp { get; }

        public NyanCat(Point center)
        {
            Center = center;
        }

        public void Jump()
        {

        }
    }
}
