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
        [TestCase(1, 1, 10)]
        [TestCase(-1, 1, 11)]
        public void IncorrectCreation(int xCenter, int yCenter, int width)
        {
            Assert.Throws<ArgumentNullException>(() => { CreatePlatform(xCenter, yCenter, width); });
        }

        [Test]
        public void CorrectCreation()
        {
            var platform = CreatePlatform(50, 50, 11);
        }

        public Platform CreatePlatform(int x, int y, int width)
        {
            return new Platform(new Point(x, y), width);
        }
    }
}
