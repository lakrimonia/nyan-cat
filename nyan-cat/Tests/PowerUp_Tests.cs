using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace nyan_cat.Tests
{
    [TestFixture]
    public class PowerUp_Tests
    {
        //[Test]
        //public void IncorrectCreation()
        //{
        //    Assert.Throws<ArgumentException>(() => { CreatePowerUp(-1, 0); });
        //}

        [Test]
        public void CorrectCreation()
        {
            var powerUp = CreatePowerUp(3, 3);
        }

        [Test]
        public void Move()
        {
            var powerUp = CreatePowerUp(10, 10);
            powerUp.Move();
            Assert.AreEqual(new Point(0, 10), powerUp.LeftTopCorner);
        }

        [Test]
        public void OverEdge()
        {
            var powerUp = CreatePowerUp(3, 3);
            Move(powerUp, 4);
            Assert.AreEqual(false, powerUp.IsAlive, powerUp.ToString());
        }

        public PowerUp CreatePowerUp(int x, int y)
        {
            return new PowerUp(new Point(x, y), PowerUpKind.TurboNyan);
        }

        public void Move(PowerUp powerUp, int count)
        {
            for (var i = 0; i < count; i++)
                powerUp.Move();
        }

        #region MilkGlasses
        [Test]
        public void MilkGlasses()
        {
            MilkGlasses(new Food(new Point(181, 150)), 3);
            MilkGlasses(new Milk(new Point(181, 150)), 3);
            MilkGlasses(new Cow(new Point(181, 150)), 51);
        }

        public void MilkGlasses(IGameObject gameObject, int expCombo)
        {
            var map = MapCreator.CreateMap(400, 400, new Platform(new Point(100, 200), 200), gameObject);
            var game = new Game(100, 150, map)
            {
                NyanCat = { CurrentPowerUp = new PowerUp(new Point(0, 0), PowerUpKind.MilkGlasses) }
            };
            game.Update();
            Assert.AreEqual(game.Combo, expCombo);
            Assert.AreEqual(game.Score, expCombo);
        }
        #endregion

        #region BigNyan

        [Test]
        public void BigNyanInvulnerable()
        {
            var enemy = new UFO(new Point(300, 250));
            var bomb = new Bomb(new Point(300, 250));
            var platform = new Platform(new Point(100, 300), 250);
            BigNyanProtectedFromBombs(platform, bomb);
            BigNyanProtectedFromEnemies(platform, enemy);
        }

        public void BigNyanProtectedFromEnemies(Platform platform, UFO enemy)
        {
            var cow = new Cow(new Point(250, 250));
            var map = MapCreator.CreateMap(500, 500, cow, enemy, platform);
            var game = new Game(219, 250, map);
            game.NyanCat.CurrentPowerUp = new PowerUp(new Point(0, 0), PowerUpKind.BigNyan);
            game.NyanCat.CurrentPowerUp.Activate(game);
            game.Update();
            var score = game.Score;
            var combo = game.Combo;
            game.Update();
            Assert.GreaterOrEqual(game.Combo, combo);
            Assert.GreaterOrEqual(game.Score, score);
        }
        
        public void BigNyanProtectedFromBombs(Platform platform, Bomb bomb)
        {
            var map = MapCreator.CreateMap(500, 500, bomb, platform);
            var game = new Game(219, 250, map);
            game.NyanCat.CurrentPowerUp = new PowerUp(new Point(0, 0), PowerUpKind.BigNyan);
            game.NyanCat.CurrentPowerUp.Activate(game);
            game.Update();
            Assert.AreEqual(false, game.IsOver);
        }

        [Test]
        public void BigNyanCollectsMoreFood()
        {
            var milk = new Milk(new Point(100, 190));
            var cow = new Cow(new Point(180, 190));
            var food = new Food(new Point(220, 250));
            var platform = new Platform(new Point(100, 300), 250);
            var map = MapCreator.CreateMap(500, 500, milk, cow, food, platform);
            var game = new Game(100, 250, map);
            game.NyanCat.CurrentPowerUp = new PowerUp(new Point(0, 0), PowerUpKind.BigNyan);
            game.NyanCat.CurrentPowerUp.Activate(game);
            game.Update();
            game.Update();
            game.Update();
            Assert.GreaterOrEqual(game.Score, 100);
        }

        #endregion

        #region Piano

        [Test]
        public void Piano()
        {
            var platform = new Platform(new Point(100, 200), 200);
            var map = MapCreator.CreateMap(400, 400, platform);
            var game = new Game(100, 140, map);
            game.NyanCat.CurrentPowerUp = new PowerUp(new Point(0, 0), PowerUpKind.Piano);
            game.Update();
            game.Update();
            Assert.AreEqual(3, game.Combo);
        }

        #endregion

        #region LoveNyan

        [Test]
        public void LoveNyan()
        {
            var platform = new Platform(new Point(50, 500), 300);
            var animal = new TacNyan(platform);
            var ufo = new UFO(new Point(50, 550));
            var bomb = new Bomb(new Point(50, 600));
            var map = MapCreator.CreateMap(700, 700, platform, animal, ufo, bomb);
            var game = new Game(50, 50, map);
            game.NyanCat.CurrentPowerUp = new PowerUp(new Point(0, 0), PowerUpKind.LoveNyan);
            game.Update();
            game.Update();
            game.Update();
            Assert.AreEqual(1, game.GameObjects.Count);
            Assert.GreaterOrEqual(game.Score, 3000);
        }

        #endregion

        #region FloristNyan

        [Test]
        public void FloristNyan()
        {
            var platform = new Platform(new Point(100, 300), 300);
            var map = MapCreator.CreateMap(450, 450, platform);
            var game = new Game(100, 250, map);
            game.NyanCat.CurrentPowerUp = new PowerUp(new Point(0, 0), PowerUpKind.FloristNyan);
            game.Update();
            game.Update();
            game.Update();
            Assert.AreEqual(4, game.Combo);
        }

        #endregion

        #region DoggieNyan

        [Test]
        public void DoggieNyanProtectedFromEnemies()
        {
            var platform = new Platform(new Point(100, 300), 300);
            var ufo = new UFO(new Point(181, 250));
            var milk = new Milk(new Point(170, 250));
            var map = MapCreator.CreateMap(500, 500, platform, ufo, milk);
            var game = new Game(100, 250, map);
            game.NyanCat.CurrentPowerUp = new PowerUp(new Point(0, 0), PowerUpKind.DoggieNyan);
            game.Update();
            game.Update();
            game.Update();
            Assert.AreEqual(2, game.Combo);
        }

        [Test]
        public void DoggieNyanNotProtectedFromBombs()
        {
            var platform = new Platform(new Point(100, 300), 300);
            var bomb = new Bomb(new Point(190, 250));
            var map = MapCreator.CreateMap(500, 500, platform, bomb);
            var game = new Game(100, 250, map);
            game.NyanCat.CurrentPowerUp = new PowerUp(new Point(0, 0), PowerUpKind.DoggieNyan);
            game.NyanCat.CurrentPowerUp.Activate(game);
            game.Update();
            game.Update();
            Assert.AreEqual(true, game.IsOver);
        }

        #endregion
    }
}
