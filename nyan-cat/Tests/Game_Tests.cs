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
    public class Game_Tests
    {
        [Test]
        public void Intersection()
        {
            var food = new Food(new Point(60, 9));
            var map = MapCreator.CreateMap(300, 300, food);
            var game = new Game(10, 10, map);
            Assert.AreEqual(food, game.FindIntersectedObject());
        }

        [Test]
        public void NotIntersection()
        {
            var food = new Food(new Point(61, 9));
            var map = MapCreator.CreateMap(300, 300, food);
            var game = new Game(10, 10, map);
            Assert.AreNotEqual(food, game.FindIntersectedObject());
        }

        [Test]
        public void IntersectionAfterMove()
        {
            var food = new Food(new Point(61, 9));
            var map = MapCreator.CreateMap(300, 300, food);
            var game = new Game(10, 10, map);
            Assert.AreNotEqual(food, game.FindIntersectedObject());
            food.Move();
            Assert.AreEqual(food, game.FindIntersectedObject());
        }

        [Test]
        public void MilkGlasses()
        {
            var food = new Food(new Point(250, 150));
            var map = MapCreator.CreateMap(400, 400);
            var game = new Game(100, 120, map)
            {
                NyanCat = {CurrentPowerUp = new PowerUp(new Point(0, 0), PowerUpKind.MilkGlasses)}
            };
            food.Move();
            food.Move();
            food.Move();
        }
    }
}
