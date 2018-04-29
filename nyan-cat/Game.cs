using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework.Interfaces;

namespace nyan_cat
{
    public class Game
    {
        public NyanCat NyanCat { get; private set; }
        public int Score { get; private set; }

        private int combo;
        //public int Combo => combo;
        public int Combo => NyanCat.CurrentGem?.Kind == GemKind.DoubleCombo
            ? combo * 2 : combo;

        public bool IsOver { get; private set; }
        public IGameObject[,] Field { get; }
        public List<IGameObject> GameObjects { get; private set; }

        public int MilkGlassesCombo => NyanCat.CurrentPowerUp.Kind == PowerUpKind.MilkGlasses ? 2 : 1;

        public Game(int catLeftTopCornerX, int catLeftTopCornerY)
        {
            NyanCat = new NyanCat(new Point(catLeftTopCornerX, catLeftTopCornerY));
            var map = MapCreator.CreateRandomMap();
            Field = map.Field;
            GameObjects = map.GameObjects;
            Score = 0;
            IsOver = false;
        }

        public Game(int catLeftTopCornerX, int catLeftTopCornerY, Map map)
        {
            NyanCat = new NyanCat(new Point(catLeftTopCornerX, catLeftTopCornerY));
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
                    combo += (metObject as Milk).Combo * MilkGlassesCombo;
                    break;
                case Food _:
                    if (NyanCat.CurrentPowerUp.Kind == PowerUpKind.MilkGlasses)
                        combo += MilkGlassesCombo;
                    Score += Food.Points * Combo;
                    break;
                case Bomb _:
                    if (IsInvulnerable())
                        IsOver = true;
                    break;
                case PowerUp _:
                    NyanCat.CurrentPowerUp = metObject as PowerUp;
                    break;
                case Gem _:
                    Score += 10000;
                    NyanCat.CurrentGem = metObject as Gem;
                    break;
                case IEnemy _:
                    if (IsInvulnerable())
                    {
                        Score -= 100; // TODO: позже придумаю нормальную формулу
                        if (NyanCat.CurrentGem.Kind != GemKind.MilkLongLife)
                            combo = 1;
                    }
                    break;
            }
            Score += 1 * Combo;
        }

        public IGameObject FindIntersectedObject()
        {
            var beginX = NyanCat.LeftTopCorner.X;
            var endX = NyanCat.LeftTopCorner.X + NyanCat.Width;
            var beginY = NyanCat.LeftTopCorner.Y;
            var endY = NyanCat.LeftTopCorner.Y + NyanCat.Height;

            return GameObjects
                .Where(gObj => gObj.LeftTopCorner.X <= endX
                && gObj.LeftTopCorner.Y <= endY
                && gObj.LeftTopCorner.X + gObj.Width >= beginX
                && gObj.LeftTopCorner.Y + gObj.Height >= beginY)
                .FirstOrDefault();
        }

        private bool IsCatOnPlatform()
        {
            throw new NotImplementedException();
        }

        private bool IsInvulnerable()
        {
            return NyanCat.CurrentGem.Kind != GemKind.Invulnerable ||
                   NyanCat.CurrentPowerUp.Kind != PowerUpKind.BigNyan;
        }
    }
}
