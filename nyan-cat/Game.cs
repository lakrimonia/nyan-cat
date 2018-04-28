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
            if (NyanCat.LeftTopCorner.Y <= 0)
            {
                IsOver = true;
                return;
            }
            if (IsCatOnPlatform())
                NyanCat.State = CatState.Run;
            var metObject = FindIntersectedObject();
            switch (metObject)
            {
                case Milk _:
                    combo += (metObject as Milk).Combo;
                    break;
                case Food _:
                    Score += Food.Points * Combo;
                    break;
                case Bomb _:
                    if (NyanCat.CurrentGem.Kind != GemKind.Invulnerable)
                        IsOver = true;
                    break;
                case PowerUp _:
                    NyanCat.CurrentPowerUp = metObject as PowerUp;
                    break;
                case Gem _:
                    Score += 10000;
                    NyanCat.CurrentGem = metObject as Gem;
                    break;
            }
            Score += 1 * Combo;
        }

        private IGameObject FindIntersectedObject()
        {
            // Ищет объект, с которым пересеклась кошка.
            //Если таких нет, возвращает null
            throw new NotImplementedException();
        }

        private bool IsCatOnPlatform()
        {
            throw new NotImplementedException();
        }
    }
}
