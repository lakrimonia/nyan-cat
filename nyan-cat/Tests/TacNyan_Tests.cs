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
    public class TacNyan_Tests
    {
        [Test]
        public void IncorrectCreation1()
        {
            var platform = CreatePlatform(100, 100, 1);
            Assert.Throws<ArgumentException>(() => { new TacNyan(platform); });
        }

        [Test]
        public void IncorrectCreation2()
        {
            var platform = CreatePlatform(10, 10, 60);
            Assert.Throws<ArgumentException>(() => { new TacNyan(platform); });
        }

        [Test]
        public void CorrectCreation()
        {
            var platform = CreatePlatform(100, 100, 90);
            var tacNyan = new TacNyan(platform);
        }

        [Test]
        public void LeftTopCorner()
        {
            var platform = CreatePlatform(100, 100, 90);
            var tacNyan = new TacNyan(platform);
            Assert.AreEqual(new Point(100, 50),
                tacNyan.LeftTopCorner, tacNyan.ToString());
        }

        [Test]
        public void MoveIfOnTheLeftEdge()
        {
            var platform = CreatePlatform(100, 100, 90);
            var tacNyan = new TacNyan(platform);
            var result = tacNyan.LeftTopCorner;
            tacNyan.Move();
            Assert.AreEqual(result, tacNyan.LeftTopCorner);
        }

        public void Move(IGameObject tacNyan, IGameObject platform, int count)
        {
            for (var i = 0; i < count; i++)
            {
                tacNyan.Move();
                platform.Move();
            }
        }

        public Platform CreatePlatform(int x, int y, int width)
        {
            return new Platform(new Point(x, y), width);
        }
    }
}
