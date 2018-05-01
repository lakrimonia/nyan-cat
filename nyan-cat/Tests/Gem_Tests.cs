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
    public class Gem_Tests
    {
        //[Test]
        //public void IncorrectCreation()
        //{
        //    Assert.Throws<ArgumentException>(() => { CreateDoubleGem(-1, 0); });
        //}

        [Test]
        public void CorrectCreation()
        {
            var gem = CreateDoubleGem(3, 3);
        }

        [Test]
        public void Move()
        {
            var gem = CreateDoubleGem(10, 10);
            gem.Move();
            Assert.AreEqual(new Point(9, 10), gem.LeftTopCorner);
        }

        [Test]
        public void OverEdge()
        {
            var gem = CreateDoubleGem(3, 3);
            Move(gem, 4);
            Assert.AreEqual(false, gem.IsAlive, gem.ToString());
        }

        public Gem CreateDoubleGem(int x, int y)
        {
            return new Gem(new Point(x, y), GemKind.DoubleCombo);
        }

        public void Move(Gem food, int count)
        {
            for (var i = 0; i < count; i++)
                food.Move();
        }

        #region DoubleCombo

        [Test]
        public void DoubleComboStartComboIsTwo()
        {
            var game = new Game(0, 0, MapCreator.CreateMap(100, 100));
            game.NyanCat.CurrentGem = new Gem(new Point(0, 0), GemKind.DoubleCombo);
            game.NyanCat.CurrentGem.Activate(game);
            game.Update();
            Assert.AreEqual(2, game.Combo);
        }

        [Test]
        public void AfterDoubleComboStartComboIsOne()
        {
            var game = new Game(0, 0, MapCreator.CreateMap(100, 100));
            game.NyanCat.CurrentGem = new Gem(new Point(0, 0), GemKind.DoubleCombo);
            game.NyanCat.CurrentGem.Activate(game);
            game.Update();
            Assert.AreEqual(2, game.Combo);
            game.NyanCat.CurrentGem.Deactivate(game);
            game.NyanCat.CurrentGem = null;
            game.Update();
            Assert.AreEqual(1, game.Combo);
        }

        [Test]
        public void DoubleComboMilkGivesTwo()
        {
            var platform = new Platform(new Point(100, 300), 300);
            var milk = new Milk(new Point(181, 250));
            var map = MapCreator.CreateMap(500, 500, platform, milk);
            var game = new Game(100, 250, map);
            game.NyanCat.CurrentGem = new Gem(new Point(0, 0), GemKind.DoubleCombo);
            game.NyanCat.CurrentGem.Activate(game);
            game.Update();
            Assert.AreEqual(4, game.Combo);
        }

        [Test]
        public void DoubleComboCowGivesFifty()
        {
            var platform = new Platform(new Point(100, 300), 300);
            var milk = new Cow(new Point(181, 250));
            var map = MapCreator.CreateMap(500, 500, platform, milk);
            var game = new Game(100, 250, map);
            game.NyanCat.CurrentGem = new Gem(new Point(0, 0), GemKind.DoubleCombo);
            game.NyanCat.CurrentGem.Activate(game);
            game.Update();
            Assert.AreEqual(52, game.Combo);
        }

        #endregion
    }
}
