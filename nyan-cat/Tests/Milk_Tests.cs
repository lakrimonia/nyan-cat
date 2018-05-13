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
            var result = new Point(10 + (int)UsualGameObjectProperties.Velocity.X, 10);
            Assert.AreEqual(result, platform.LeftTopCorner);
        }

        [Test]
        public void OverEdge()
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
