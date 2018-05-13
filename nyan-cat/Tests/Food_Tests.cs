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
        public void CorrectCreation()
        {
            var food = new Food(new Point(3, 3));
        }

        [Test]
        public void Move()
        {
            var food = new Food(new Point(10, 10));
            food.Move();
            var result = new Point(10 + (int)UsualGameObjectProperties.Velocity.X, 10);
            Assert.AreEqual(result, food.LeftTopCorner);
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
