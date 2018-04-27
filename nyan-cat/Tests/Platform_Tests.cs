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
        public void ZeroWidth()
        {
            Assert.Throws<ArgumentNullException>(() => { CreatePlatform(1, 1, 0); });
        }
        [Test]
        public void EvenWidth()
        {
            Assert.Throws<ArgumentNullException>(() => { CreatePlatform(1, 1, 10); });
        }
        [Test]
        public void CenterOutField()
        {
            Assert.Throws<ArgumentNullException>(() => { CreatePlatform(-1, 1, 11); });
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
