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
            Assert.AreEqual(new Point(10, 36), cat.LeftTopCorner, cat.ToString());
            Assert.AreEqual(CatState.Jump, cat.State, cat.ToString());
            cat.Move();
            Assert.AreEqual(new Point(10, 36 - 13), cat.LeftTopCorner, cat.ToString()); 
        }

        [Test]
        public void FallAfterJump()
        {
            var cat = new NyanCat(new Point(10, 50));
            cat.Jump();
            Move(cat, 15 + 1);
            Assert.AreEqual(CatState.Fall, cat.State, cat.ToString());
        }

        [Test]
        public void DoubleJump()
        {
            var cat = new NyanCat(new Point(10, 300));
            cat.State = CatState.Run;
            cat.Jump();
            Move(cat, 15);
            cat.Jump();
            Move(cat, 1);
            Assert.AreEqual(CatState.Jump, cat.State, cat.ToString());
            Move(cat, 15);
            Assert.AreEqual(CatState.Fall, cat.State, cat.ToString());
        }

        [Test]
        public void NotTripleJump()
        {
            var cat = new NyanCat(new Point(10, 300));
            cat.Jump();
            Move(cat, 15);
            cat.Jump();
            Assert.AreEqual(CatState.Jump, cat.State, cat.ToString());
            Move(cat, 16);
            Assert.AreEqual(CatState.Fall, cat.State, cat.ToString());
            cat.Jump();
            Assert.AreEqual(CatState.Fall, cat.State, cat.ToString());
        }

        [Test]
        public void ReloadJumpsAfterRun()
        {
            var cat = new NyanCat(new Point(10, 50));
            cat.Jump();
            cat.Jump();
            Move(cat, 15);
            cat.Jump();
            Assert.AreEqual(CatState.Fall, cat.State, cat.ToString());
            cat.State = CatState.Run;
            cat.Jump();
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

        public void Move (NyanCat cat, int count)
        {
            for (var i = 0; i < count; i++)
                cat.Move();
        }
    }
}
