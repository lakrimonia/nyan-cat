using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace nyan_cat
{
    public static class MapCreator
    {
        private const int GameWidth = 1000;
        private const int GameHeight = 788;
        private const int PlatformHeight = 26;
        private const int OtherObjectSize = 50;

        public static IGameObject[,] CreateRandomMap()
        {
            var map = new IGameObject[GameWidth, GameHeight];
            for (var x = 0; x <= GameWidth - 200; x += 250)
            {
                PlacePlatformsAndBombs(map, x);
                PlaceFoodAndMilk(map, x);
                PlacePowerUps();
            }
            return map;
        }

        private static void PlacePlatformsAndBombs(IGameObject[,] map, int x)
        {
            foreach (var p in GeneratePlatforms(map, x))
            {
                var platformCenter = new Point(x - p.Value / 2, p.Key - PlatformHeight / 2);
                var platform = new Platform(platformCenter, p.Value);
                var bomb = GenerateBomb(platform);
                PlaceGameObject(map, platform);
                if (bomb != null)
                    PlaceGameObject(map, bomb);
            }
        }

        private static void PlaceFoodAndMilk(IGameObject[,] map, int x)
        {
            for (var y = 0; y < GameHeight - 100; y += 50 + PlatformHeight)
            {
                var rnd = new Random();
                var foodCount = rnd.Next(0 - 1, 5);
                for (var i = 0; i < foodCount; i++)
                {
                    var choice = rnd.Next(0, 2);
                    IGameObject item;
                    var itemCenter = new Point(x + OtherObjectSize / 2,
                        y + OtherObjectSize / 2);
                    if (choice == 1)
                        item = new Food(itemCenter);
                    else
                    {
                        var isGenerateCow = IsCreate(15);
                        item = isGenerateCow ? new Cow(itemCenter) : new Milk(itemCenter);
                    }
                    PlaceGameObject(map, item);
                }
            }
        }

        private static void PlacePowerUps()
        {
            throw new NotImplementedException();
        }

        private static bool IsCreate(int chance)
        {
            var chances = new List<bool>();
            for (var i = 0; i < chance; i++)
                chances.Add(true);
            for (var i = 0; i < 100 - chance; i++)
                chances.Add(false);
            var rnd = new Random();
            return chances.OrderBy(e => rnd.Next(100)).First();
        }

        private static Bomb GenerateBomb(Platform platform)
        {
            var isGenerateBomb = IsCreate(20);
            return isGenerateBomb
                ? new Bomb(new Point(platform.Center.X,
                    platform.Center.Y + PlatformHeight / 2 + OtherObjectSize / 2))
                : null;
        }

        private static Dictionary<int, int> GeneratePlatforms(IGameObject[,] map, int x)
        {
            var result = new Dictionary<int, int>(); // <y, width>
            var ys = GetYs(50, GameHeight - 100, 50);
            var rnd = new Random();
            ys = ys.OrderBy(item => rnd.Next(650 / 50));
            foreach (var y in ys)
            {
                if (map[x, y] == null)
                {
                    var width = Math.Min(rnd.Next(100 - 1, 400), GameWidth - x);
                    if (width % 2 == 1)
                        width--;
                    result[y] = width;
                }
                if (result.Count == 5)
                    break;
            }
            return result;
        }

        private static IEnumerable<int> GetYs(int begin, int end, int step)
        {
            for (var i = 0; i < end / step; i++)
            {
                var y = begin + i * (step + 26);
                if (y > end)
                    yield break;
                yield return y;
            }
        }

        public static IGameObject[,] CreateMap(int width, int height, params IGameObject[] gameObjects)
        {
            var map = new IGameObject[width, height];
            foreach (var gameObject in gameObjects)
                PlaceGameObject(map, gameObject);
            return map;
        }

        private static void PlaceGameObject(IGameObject[,] map, IGameObject gameObject)
        {
            var beginX = gameObject.Center.X - gameObject.Width / 2;
            var endX = gameObject.Center.X + gameObject.Width / 2;
            var beginY = gameObject.Center.Y - gameObject.Height / 2;
            var endY = gameObject.Center.Y + gameObject.Height / 2;
            for (var y = beginY; y < endY + 1; y++)
                for (var x = beginX; x < endX + 1; x++)
                    map[x, y] = gameObject;
        }
    }
}
