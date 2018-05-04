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
    public class Animal_Tests
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
            var platform = CreatePlatform(100, 100, 60);
            var animal = new TacNyan(platform);
        }

        [Test]
        public void LeftTopCorner()
        {
            var platform = CreatePlatform(100, 100, 60);
            var animal = new TacNyan(platform);
            Assert.AreEqual(new Point(100, 50),
                animal.LeftTopCorner, animal.ToString());
        }

        [Test]
        public void MoveIfOnTheLeftEdge()
        {
            var platform = CreatePlatform(100, 100, 52);
            var animal = new TacNyan(platform);
            var result = animal.LeftTopCorner;
            animal.Move();
            Assert.AreEqual(result, animal.LeftTopCorner);
        }

        [Test]
        public void MoveIfOnTheRightEdge()
        {
            var platform = CreatePlatform(100, 100, 52);
            var animal = new TacNyan(platform);
            var result = animal.LeftTopCorner.X - 2;
            Move(animal, platform, 3);
            Assert.AreEqual(result, animal.LeftTopCorner.X);
        }

        [Test]
        public void OverEdge()
        {
            var platform = CreatePlatform(0, 100, 52);
            var animal = new TacNyan(platform);
            var result = animal.LeftTopCorner.X - 2;
            Move(animal, platform, 3);
            Assert.AreEqual(false, animal.IsAlive);
        }

        public void Move(IGameObject animal, IGameObject platform, int count)
        {
            for (var i = 0; i < count; i++)
            {
                animal.Move();
                platform.Move();
            }
        }

        public Platform CreatePlatform(int x, int y, int width)
        {
            return new Platform(new Point(x, y), width);
        }
    }
}
