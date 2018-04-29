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
        [Test]
        public void IncorrectCreation()
        {
            Assert.Throws<ArgumentException>(() => { CreatePowerUp(-1, 0); });
        }

        [Test]
        public void CorrectCreation()
        {
            var powerUp= CreatePowerUp(3, 3);
        }

        [Test]
        public void Move()
        {
            var powerUp = CreatePowerUp(10, 10);
            powerUp.Move();
            Assert.AreEqual(new Point(9, 10), powerUp.LeftTopCorner);
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


    }
}
