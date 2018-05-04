using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework.Interfaces;

namespace nyan_cat
{
    public class Game
    {
        public NyanCat NyanCat { get; private set; }
        public int Score { get; internal set; }

        internal int AddCombo = 1;
        public int Combo { get; internal set; }

        public bool IsOver { get; internal set; }
        public IGameObject[,] Field { get; }
        public List<IGameObject> GameObjects { get; private set; }
        private List<IGameObject> futureGameObjects;
        private bool wasSpeedUp;

        private int fieldWidth;
        private int fieldHeight;

        public int MilkGlassesCombo =>
            NyanCat.CurrentPowerUp?.Kind == PowerUpKind.MilkGlasses
                ? 2
                : 1;
       internal bool ComboProtectedFromEnemies { get; set; }

        public Game(int catLeftTopCornerX, int catLeftTopCornerY)
        {
            NyanCat = new NyanCat(new Point(catLeftTopCornerX, catLeftTopCornerY));
            fieldWidth = MapCreator.GameWidth;
            fieldHeight = MapCreator.GameHeight;
            GameObjects = MapCreator.CreateRandomMap();
            futureGameObjects = MapCreator.CreateRandomMap(true);
            Score = 0;
            Combo = 1;
            IsOver = false;
        }

        public Game(int catLeftTopCornerX, int catLeftTopCornerY, List<IGameObject> map)
        {
            // TODO: fix it
            fieldWidth = MapCreator.GameWidth;
            fieldHeight = MapCreator.GameHeight;

            NyanCat = new NyanCat(new Point(catLeftTopCornerX, catLeftTopCornerY));
            GameObjects = map;
            futureGameObjects = new List<IGameObject>();
            Score = 0;
            Combo = 1;
            IsOver = false;
        }

        public void Update()
        {
            if (NyanCat.CurrentPowerUp?.Kind == PowerUpKind.FloristNyan
                && NyanCat.State == CatState.Run)
                Combo += AddCombo;
            if (NyanCat.CurrentPowerUp?.Kind == PowerUpKind.LoveNyan)
            {
                var enemyOrBomb = FindNearestEnemyOrBomb();
                if (enemyOrBomb != null)
                {
                    enemyOrBomb.Kill();
                    Score += 1000;
                }
            }
            MoveAllObjects();
            NyanCat.Move();
            if (NyanCat.LeftTopCorner.Y <= 0)
            {
                IsOver = true;
                return;
            }

            if (IsCatOnPlatform())
            {
                if (NyanCat.CurrentPowerUp?.Kind == PowerUpKind.Piano
                    && NyanCat.State != CatState.Run)
                    Combo += 2 * AddCombo;
                NyanCat.State = CatState.Run;
            }
            else if (NyanCat.State != CatState.Jump)
                NyanCat.State = CatState.Fall;
            HandleIntersection();
            Score += 1 * Combo;
            UpdateListOfObjects();
        }

        private void UpdateListOfObjects()
        {
            GameObjects = GameObjects.Where(gameObj => gameObj.IsAlive).ToList();
            var newObjects = futureGameObjects.Where(e => e.LeftTopCorner.X < fieldWidth).ToList();
            foreach (var newObject in newObjects)
            {
                GameObjects.Add(newObject);
                futureGameObjects.Remove(newObject);
            }
            if (futureGameObjects.Count == 0)
                futureGameObjects = MapCreator.CreateRandomMap(true, true);
        }

        public IGameObject FindIntersectedObject()
        {
            var beginX = NyanCat.LeftTopCorner.X;
            var endX = NyanCat.LeftTopCorner.X + NyanCat.Width;
            var beginY = NyanCat.LeftTopCorner.Y;
            var endY = NyanCat.LeftTopCorner.Y + NyanCat.Height;

            return GameObjects
                .Where(gObj => !(gObj is Platform))
                .Where(gObj => !(gObj is IEnemy) || !((IEnemy)gObj).IsMet)
                .FirstOrDefault(gObj => gObj.LeftTopCorner.X <= endX
                && gObj.LeftTopCorner.Y <= endY
                && gObj.LeftTopCorner.X + gObj.Width >= beginX
                && gObj.LeftTopCorner.Y + gObj.Height >= beginY);
        }

        private bool IsCatOnPlatform()
        {
            return GameObjects
                .Where(e => e is Platform)
                .Where(p => NyanCat.LeftTopCorner.Y + NyanCat.Height >= p.LeftTopCorner.Y)
                .Where(p => NyanCat.LeftTopCorner.Y + NyanCat.Height < p.LeftTopCorner.Y + p.Height)
                .Where(p => NyanCat.LeftTopCorner.X >= p.LeftTopCorner.X)
                .Any(p => NyanCat.LeftTopCorner.X <= p.LeftTopCorner.X + p.Width);
        }

        private void HandleIntersection() => FindIntersectedObject()?.Use(this);

        private void MoveAllObjects()
        {
            var acceleration = new Vector2(0, 0);
            if (NyanCat.CurrentPowerUp?.Kind == PowerUpKind.TurboNyan)
            {
                wasSpeedUp = true;
                acceleration = new Vector2(-5, 0);
            }
            else if (wasSpeedUp)
            {
                acceleration = new Vector2(5, 0);
                wasSpeedUp = false;
            }
            MoveAllObjects(GameObjects, acceleration);
            MoveAllObjects(futureGameObjects, acceleration);
        }

        private void MoveAllObjects(List<IGameObject> gameObjects, Vector2 acceleration)
        {
            foreach (var gameObject in gameObjects)
            {
                gameObject.Accelerate(acceleration);
                gameObject.Move();
            }
        }

        internal bool IsInvulnerable()
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