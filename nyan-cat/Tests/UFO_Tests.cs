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
        public void IncorrectCreation()
        {
            Assert.Throws<ArgumentException>(() => { new UFO(new Point(-1, 0)); });
        }

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
            Assert.AreEqual(new Point(9, 10), ufo.LeftTopCorner);
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
