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
    public class NyanCat_Tests
    {
        [Test]
        public void IncorrectCreation()
        {
            Assert.Throws<ArgumentException>(() => { new NyanCat(new Point(-1, 0)); });
        }

        [Test]
        public void CorrectCreation()
        {
            var cat = new NyanCat(new Point(3, 3));
        }

        [Test]
        public void Run()
        {
            var cat = new NyanCat(new Point(10, 50));
            cat.Move();
            Assert.AreEqual(new Point(10, 50), cat.LeftTopCorner, cat.ToString());
            Assert.AreEqual(CatState.Run, cat.State, cat.ToString());
        }

        [Test]
        public void Jump()
        {
            var cat = new NyanCat(new Point(10, 50));
            cat.Jump();
            cat.Move();
            Assert.AreEqual(new Point(10, 40), cat.LeftTopCorner, cat.ToString());
            Assert.AreEqual(CatState.Jump, cat.State, cat.ToString());
        }

        [Test]
        public void FallIfTouchTheTop()
        {
            var cat = new NyanCat(new Point(10, 5));
            cat.Jump();
            cat.Move();
            Assert.AreEqual(new Point(10, 0), cat.LeftTopCorner, cat.ToString());
            Assert.AreEqual(CatState.Fall, cat.State, cat.ToString());
        }

        [Test]
        public void Death()
        {
            var cat = new NyanCat(new Point(10, 780));
            cat.State = CatState.Fall;
            cat.Move();
            Assert.AreEqual(false, cat.IsAlive, cat.ToString());
        }
    }
}
