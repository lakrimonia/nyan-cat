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
    public class Milk_Tests
    {
        [Test]
        public void IncorrectCreation()
        {
            Assert.Throws<ArgumentException>(() => { new Milk(new Point(-1, 0)); });
        }

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
            Assert.AreEqual(new Point(9, 10), milk.LeftTopCorner);
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
