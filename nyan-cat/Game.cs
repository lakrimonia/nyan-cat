using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace nyan_cat
{
    public class Game
    {
        public NyanCat NyanCat { get; }
        public int Score { get; }
        public int Combo { get; }
        public bool IsOver { get; }
        public IGameObject[,] Map { get; }
        public List<IGameObject> GameObjects { get; }

        public Game()
        {
            NyanCat = new NyanCat(new Point(0, 0));
            Map = MapCreator.CreateRandomMap();
        }
    }
}
