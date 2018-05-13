using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using System.Drawing;

namespace nyan_cat.Tests
{
    [TestFixture]
    public class Milk_Tests
    {

        [Test]
        public void CorrectCreation()
        {
            var milk = new Milk(new Point(3, 3));
        }

        [Test]
        public void Move()
        {
            var milk = new Milk(new Point(10, 10));
            milk.Move();
            var result = new Point(10 + (int)UsualGameObjectProperties.Velocity.X, 10);
            Assert.AreEqual(result, milk.LeftTopCorner);
        }

        [Test]
        public void OverEdge()
        {
            var milk = new Milk(new Point(3, 3));
            Move(milk, 4);
            Assert.AreEqual(false, milk.IsAlive, milk.ToString());
        }

        public void Move(Milk milk, int count)
        {
            for (var i = 0; i < count; i++)
                milk.Move();
        }
    }
}
