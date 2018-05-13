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
    public class Bomb_Tests
    {
        [Test]
        public void CorrectCreation()
        {
            var bomb = new Bomb(new Point(3, 3));
        }

        [Test]
        public void Move()
        {
            var bomb = new Bomb(new Point(10, 10));
            bomb.Move();
            var result = new Point(10 + (int)UsualGameObjectProperties.Velocity.X, 10);
            Assert.AreEqual(result, bomb.LeftTopCorner);
        }

        [Test]
        public void OverEdge()
        {
            var bomb = new Bomb(new Point(3, 3));
            Move(bomb, 4);
            Assert.AreEqual(false, bomb.IsAlive, bomb.ToString());
        }

        public void Move(Bomb bomb, int count)
        {
            for (var i = 0; i < count; i++)
                bomb.Move();
        }
    }
}
