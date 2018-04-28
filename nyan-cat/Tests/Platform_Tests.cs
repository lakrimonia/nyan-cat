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
    public class Platform_Tests
    {

        [TestCase(1, 1, 0)]
        [TestCase(-1, 1, 10)]
        public void IncorrectCreation(int xCenter, int yCenter, int width)
        {
            Assert.Throws<ArgumentException>(() => { CreatePlatform(xCenter, yCenter, width); });
        }
        [Test]
        public void CorrectCreation()
        {
            var platform = CreatePlatform(10, 10, 10);
        }

        [Test]
        public void Move()
        {
            var platform = CreatePlatform(10, 10, 10);
            platform.Move();
            Assert.AreEqual(new Point(9, 10), platform.LeftTopCorner);
        }

        [Test]

        public void OverEdge()
        {
            var platform = CreatePlatform(0, 0, 1);
            platform.Move();
            Assert.AreEqual(false, platform.IsAlive);
        }
        public Platform CreatePlatform(int x, int y, int width)
        {
            return new Platform(new Point(x, y), width);
        }
    }
}
