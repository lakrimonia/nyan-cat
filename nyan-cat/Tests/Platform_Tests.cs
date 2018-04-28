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
        public void OverEdge1()
        {
            var platform = CreatePlatform(2, 2, 2);
            Move(platform, 3);
            Assert.AreEqual(1, platform.Width, platform.ToString());
        }

        [Test]
        public void OverEdge2()
        {
            var platform = CreatePlatform(2, 2, 2);
            Move(platform, 4);
            Assert.AreEqual(false, platform.IsAlive, platform.ToString());
        }

        public Platform CreatePlatform(int x, int y, int width)
        {
            return new Platform(new Point(x, y), width);
        }

        public void Move(Platform platform, int count)
        {
            for (var i = 0; i < count; i++)
                platform.Move();
        }
    }
}
