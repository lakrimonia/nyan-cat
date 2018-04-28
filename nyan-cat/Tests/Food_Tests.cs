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
    public class Food_Tests
    {
        [Test]
        public void IncorrectCreation()
        {
            Assert.Throws<ArgumentException>(() => { new Food(new Point(-1, 0)); });
        }

        [Test]
        public void CorrectCreation()
        {
            var food = new Food(new Point(3, 3));
        }

        [Test]
        public void Move()
        {
            var food = new Food(new Point(10, 10));
            food.Move();
            Assert.AreEqual(new Point(9, 10), food.LeftTopCorner);
        }

        [Test]
        public void OverEdge()
        {
            var food = new Food(new Point(3, 3));
            Move(food, 4);
            Assert.AreEqual(false, food.IsAlive, food.ToString());
        }

        public void Move(Food food, int count)
        {
            for (var i = 0; i < count; i++)
                food.Move();
        }
    }
}
