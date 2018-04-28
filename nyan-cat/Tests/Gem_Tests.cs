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
        [Test]
        public void IncorrectCreation()
        {
            Assert.Throws<ArgumentException>(() => { CreateDoubleGem(-1, 0); });
        }

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

        public Gem CreateDoubleGem(int x , int y)
        {
            return new Gem(new Point(x, y), GemKind.DoubleCombo);
        }

        public void Move(Gem food, int count)
        {
            for (var i = 0; i < count; i++)
                food.Move();
        }
    }
}
