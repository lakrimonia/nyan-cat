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
    public class Cow_Tests
    {
        [Test]
        public void IncorrectCreation()
        {
            Assert.Throws<ArgumentException>(() => { new Cow(new Point(-1, 0)); });
        }

        [Test]
        public void CorrectCreation()
        {
            var cow = new Cow(new Point(3, 3));
        }

        [Test]
        public void Move()
        {
            var cow = new Cow(new Point(10, 10));
            cow.Move();
            Assert.AreEqual(new Point(9, 10), cow.LeftTopCorner);
        }

        [Test]
        public void OverEdge()
        {
            var cow = new Cow(new Point(3, 3));
            Move(cow, 4);
            Assert.AreEqual(false, cow.IsAlive, cow.ToString());
        }

        public void Move(Cow cow, int count)
        {
            for (var i = 0; i < count; i++)
                cow.Move();
        }
    }
}
