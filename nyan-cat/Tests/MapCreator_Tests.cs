using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace nyan_cat.Tests
{
    [TestFixture]
    public class MapCreator_Tests
    {
        [Test]
        public void DistYBetweenPlatfsGreater50()
        {
            var map = MapCreator.CreateRandomMap();
            for (var x = 0; x < MapCreator.GameWidth; x += 250)
            {
                var a = map.GameObjects
                    .Where(e => e is Platform)
                    .Where(e => e.LeftTopCorner.X == x)
                    .Select(e => e.LeftTopCorner.Y)
                    .OrderBy(e => e);
                var b = a
                    .Zip(a.Skip(1), (p1, p2) => Math.Abs(p1 + 26 - p2))
                    .Where(e => e < 50)
                    .ToList();
                Assert.AreEqual(0, b.Count);
            }
        }

        [Test]
        public void RareThings()
        {
            for (var i = 0; i < 50; i++)
            {
                var map = MapCreator.CreateRandomMap();
                CowsMustBeRare(map);
                GemsMustBeRare(map);
                PowerUpsMustBeRare(map);
            }
        }
        
        public void CowsMustBeRare(Map map)
        {
            var cowsCount = map.GameObjects.Count(e => e is Cow);
            Assert.LessOrEqual(cowsCount, 2, "Cows must be rare!");
        }
        
        public void GemsMustBeRare(Map map)
        {
            var gemsCount = map.GameObjects.Count(e => e is Gem);
            Assert.LessOrEqual(gemsCount, 2, "Gems must be rare!");
        }
        
        public void PowerUpsMustBeRare(Map map)
        {
            var powerUpsCount = map.GameObjects.Count(e => e is PowerUp);
            Assert.LessOrEqual(powerUpsCount, 3, "Power ups must be rare!");
        }
    }
}
