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
    public class UFO_Tests
    {
        [Test]
        public void CorrectCreation()
        {
            var ufo = new UFO(new Point(3, 3));
        }

        [Test]
        public void Move()
        {
            var ufo = new UFO(new Point(10, 10));
            ufo.Move();
            var result = new Point(10 + (int)UsualGameObjectProperties.Velocity.X, 10);
            Assert.AreEqual(result, ufo.LeftTopCorner);
        }

        [Test]
        public void OverEdge()
        {
            var ufo = new UFO(new Point(3, 3));
            Move(ufo, 4);
            Assert.AreEqual(false, ufo.IsAlive, ufo.ToString());
        }

        public void Move(UFO ufo, int count)
        {
            for (var i = 0; i < count; i++)
                ufo.Move();
        }
    }
}
