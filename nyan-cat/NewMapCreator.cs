using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace nyan_cat
{
    public static class NewMapCreator
    {
        private const int GameWidth = 1000;
        private const int GameHeight = 700;
        private const int PlatformHeight = 26;
        private const int ObjUsualSize = 50;
        private const int BombHeight = 25;

        private static int addX;
        private static bool withoutEnemiesAndBombs;

        public static List<IGameObject> CreateRandomMap(bool isFuture = false, bool enemiesAndBombs = false)
        {
            withoutEnemiesAndBombs = !enemiesAndBombs;
            addX = isFuture ? GameWidth : 0;
            var map = new List<IGameObject>();
            for (var x = 0 + addX; x < GameWidth - 100 + addX; x += 250)
            {
                PlacePlatformsAndBombs(map, x);
                PlaceFoodAndMilk(map, x);
            }
            return map;
        }

        private static void PlaceFoodAndMilk(List<IGameObject> map, int x)
        {
            var rnd = new Random();
            for (var y = 50; y < GameHeight - 125; y += 125)
            {
                if (!IsThereChance(50))
                    continue;
                var count = rnd.Next(1, 4 + 1);
                for (var i = 0; i < count; i++)
                {
                    var foodOrMilk = rnd.Next(0, 1 + 1);
                    IGameObject item;
                    var leftTopCorner = new Point(x + 50 * i, y);
                    if (foodOrMilk == 0)
                    {
                        item = new Food(leftTopCorner);
                    }
                    else
                    {
                        var isCow = IsThereChance(10);
                        item = isCow 
                            ? new Cow(leftTopCorner) 
                            : new Milk(leftTopCorner);
                    }
                    PlaceGameObject(map, item);
                }
            }
        }

        private static void PlacePlatformsAndBombs(List<IGameObject> map, int x)
        {
            foreach (var p in GeneratePlatforms(map, x))
            {
                var platform = new Platform(new Point(x, p.Item1), p.Item2);
                PlaceGameObject(map, platform);
                if (withoutEnemiesAndBombs)
                    continue;
                var bomb = GenerateBomb(platform);
                if (bomb != null)
                    PlaceGameObject(map, bomb);
            }
        }

        private static IEnumerable<Tuple<int, int>> GeneratePlatforms(List<IGameObject> map, int x)
        {
            var rnd = new Random();
            var count = rnd.Next(3, 6 + 1);
            var ys = new List<int>();
            for (var i = 125; i < GameHeight; i += 125)
                ys.Add(i);
            ys = ys.OrderBy(e => rnd.Next(ys.Count)).ToList();
            foreach (var y in ys)
            {
                if (map.Any(p => p.LeftTopCorner.Y == y
                                 && p.LeftTopCorner.X + p.Width >= x))
                    continue;
                var width = rnd.Next(1, 4 + 1) * 100;
                while (x + width >= GameWidth + addX)
                    width -= 100;
                yield return Tuple.Create(y, width);
                count--;
                if (count == 0)
                    yield break;
            }
        }

        private static Bomb GenerateBomb(Platform platform)
        {
            if (!IsThereChance(20))
                return null;
            var rnd = new Random();
            var x = rnd.Next(platform.LeftTopCorner.X,
                platform.LeftTopCorner.X + platform.Width - ObjUsualSize);
            var y = platform.LeftTopCorner.Y - BombHeight;
            var bomb = new Bomb(new Point(x, y));
            return bomb;
        }

        private static bool IsThereChance(int percents)
        {
            var answer = new List<bool>();
            for (var i = 0; i < percents; i++)
                answer.Add(true);
            for (var i = 0; i < 100 - percents; i++)
                answer.Add(false);
            var rnd = new Random();
            var a = answer.OrderBy(e => rnd.Next(100));
            return a.FirstOrDefault();
        }

        private static void PlaceGameObject(List<IGameObject> map, IGameObject gameObject)
        {
            map.Add(gameObject);
        }
    }
}
