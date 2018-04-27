﻿using System;
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
        public Vector2 Velocity { get; }
        public Point Center { get; }
        public int Height { get; }
        public int Width { get; }
        public bool IsAlive { get; }

        public Food(Point center)
        {
            IsAlive = true;
            Center = center;
            Height = 50;
            Width = 50;
            Velocity = new Vector2(-1, 0);
        }

        public void Move()
        {
            throw new NotImplementedException();
        }
    }
}
