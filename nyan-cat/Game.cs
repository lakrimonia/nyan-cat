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
        public NyanCat NyanCat { get; private set; }
        public int Score { get; private set; }

        private int combo;
        public int Combo => NyanCat.CurrentGem.Kind == GemKind.DoubleCombo 
            ? combo * 2 : combo;

        public bool IsOver { get; private set; }
        public IGameObject[,] Field { get; }
        public List<IGameObject> GameObjects { get; private set; }

        public Game()
        {
            NyanCat = new NyanCat(new Point(0, 0));
            var map = MapCreator.CreateRandomMap();
            Field = map.Field;
            GameObjects = map.GameObjects;
            Score = 0;
            IsOver = false;
        }

        public void Update()
        {
            foreach (var gameObject in GameObjects)
            {
                gameObject.Move();
            }
            NyanCat.Move();
            if (NyanCat.Center.Y <= 0)
            {
                IsOver = true;
                return;
            }
            if (CatOnPlatform())
                NyanCat.State = CatState.Run;
            var metObject = FindIntersectedObject(); 
            if (!(metObject is null))
                throw new Exception(); // Активировать объект
            Score += 1 * Combo;
        }

        private IGameObject FindIntersectedObject()
        {
            // Ищет объект, с котормы пересеклась кошка.
            //Если таких нет, возвращает null
            throw new Exception();
        }

        private bool CatOnPlatform()
        {
            throw new Exception();
        }
    }
}
