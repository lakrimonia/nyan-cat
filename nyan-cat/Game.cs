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
            combo = 1;
            IsOver = false;
        }

        public Game(int catLeftTopCornerX, int catLeftTopCornerY, Map map)
        {
            NyanCat = new NyanCat(new Point(catLeftTopCornerX, catLeftTopCornerY));
            Field = map.Field;
            GameObjects = map.GameObjects;
            Score = 0;
            combo = 1;
            IsOver = false;
        }

        public void Update()
        {
            if (NyanCat.CurrentPowerUp?.Kind == PowerUpKind.FloristNyan && NyanCat.State == CatState.Run)
                combo += 1;
            if (NyanCat.CurrentPowerUp?.Kind == PowerUpKind.LoveNyan)
            {
                var enemyOrBomb = FindNearestEnemyOrBomb();
                if (enemyOrBomb != null)
                {
                    enemyOrBomb.Kill();
                    Score += 1000;
                }
            }
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
            {
                if (NyanCat.State != CatState.Run)
                    combo += 2;
                NyanCat.State = CatState.Run;
            }
            var metObject = FindIntersectedObject();
            switch (metObject)
            {
                case Milk _:
                    UseMilk(metObject);
                    break;
                case Food _:
                    UseFood(metObject);
                    break;
                case Bomb _:
                    UseBomb();
                    break;
                case PowerUp _:
                    UsePowerUp(metObject);
                    break;
                case Gem _:
                    UseGem(metObject);
                    break;
                case IEnemy _:
                    UseEnemy(metObject);
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
                .Where(gObj => !(gObj is Platform))
                .Where(gObj => gObj.LeftTopCorner.X <= endX
                && gObj.LeftTopCorner.Y <= endY
                && gObj.LeftTopCorner.X + gObj.Width >= beginX
                && gObj.LeftTopCorner.Y + gObj.Height >= beginY)
                .FirstOrDefault();
        }

        private bool IsCatOnPlatform()
        {
            var startX = NyanCat.LeftTopCorner.X;
            var startY = NyanCat.LeftTopCorner.Y;
            var endX = NyanCat.LeftTopCorner.X + NyanCat.Height;
            var platformUnderCat = GameObjects
                .Where(gObj => gObj is Platform)
                .Where(gObj => gObj.LeftTopCorner.Y == startY &&
                    gObj.LeftTopCorner.X <= endX &&
                    gObj.LeftTopCorner.X + gObj.Width >= startX)
                .FirstOrDefault();
            return !(platformUnderCat is null);
        }

        private void UseMilk(IGameObject metObject)
        {
            combo += metObject is Cow ? 25 * MilkGlassesCombo : 1 * MilkGlassesCombo;
            //combo += (metObject as Milk).Combo * MilkGlassesCombo;
            metObject.Kill();
        }

        private void UseFood(IGameObject metObject)
        {
            if (NyanCat.CurrentPowerUp.Kind == PowerUpKind.MilkGlasses)
                combo += MilkGlassesCombo;
            else
                Score += Food.Points * Combo;
            metObject.Kill();
        }

        private void UseBomb()
        {
            if (!IsInvulnerable())
                IsOver = true;
        }

        private void UsePowerUp(IGameObject metObject)
        {
            var powerUp = metObject as PowerUp;
            NyanCat.CurrentPowerUp = new PowerUp(powerUp.LeftTopCorner, powerUp.Kind);
            metObject.Kill();
        }

        private void UseGem(IGameObject metObject)
        {
            Score += 10000;
            var gem = metObject as Gem;
            NyanCat.CurrentGem = new Gem(gem.LeftTopCorner, gem.Kind);
            metObject.Kill();
        }

        private void UseEnemy(IGameObject metObject)
        {
            if (!IsInvulnerable() &&
                NyanCat.CurrentPowerUp?.Kind != PowerUpKind.DoggieNyan)
            {
                Score -= 100; // TODO: позже придумаю нормальную формулу
                if (NyanCat.CurrentGem.Kind != GemKind.MilkLongLife)
                    combo = 1;
            }
        }

        private bool IsInvulnerable()
        {
            return NyanCat.CurrentGem?.Kind == GemKind.Invulnerable ||
                   NyanCat.CurrentPowerUp?.Kind == PowerUpKind.BigNyan;
        }

        private IGameObject FindNearestEnemyOrBomb()
        {
            return GameObjects
                .Where(e => e is IEnemy || e is Bomb)
                .OrderBy(e => GetDistance(NyanCat.LeftTopCorner, e.LeftTopCorner))
                .FirstOrDefault();
        }

        private double GetDistance(Point p1, Point p2)
        {
            return Math.Sqrt((p2.X - p1.X) * (p2.X - p1.X) + (p2.Y - p1.Y) * (p2.Y - p1.Y));
        }
    }
}
