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
        public void CorrectCreation()
        {
            var cow = new Cow(new Point(3, 3));
        }

        [Test]
        public void Move()
        {
            var cow = new Cow(new Point(10, 10));
            cow.Move();
            var result = new Point(10 + (int)UsualGameObjectProperties.Velocity.X, 10);
            Assert.AreEqual(result, cow.LeftTopCorner);
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
