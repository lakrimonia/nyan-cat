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
        public const int GameWidth = 1000;
        public const int GameHeight = 788;
        private const int PlatformHeight = 26;
        private const int BombHeight = 25;
        private const int OtherObjectSize = 50;

        public static Map CreateRandomMap()
        {
            var map = new Map(GameWidth, GameHeight);
            for (var x = 0; x <= GameWidth - 200; x += 250)
            {
                PlacePlatformsAndBombs(map, x);
                PlaceFoodAndMilk(map, x);
                PlacePowerUpOrGem(map, x);
            }
            return map;
        }

        private static void PlacePlatformsAndBombs(Map map, int x)
        {
            foreach (var p in GeneratePlatforms(map, x))
            {
                var leftTopCorner = new Point(x, p.Key);
                var platform = new Platform(leftTopCorner, p.Value);
                var bomb = GenerateBomb(platform);
                PlaceGameObject(map, platform);
                if (bomb != null)
                    PlaceGameObject(map, bomb);
            }
        }

        private static void PlaceFoodAndMilk(Map map, int x)
        {
            for (var y = 0; y < GameHeight - 100; y += 50 + PlatformHeight)
            {
                var rnd = new Random();
                var foodCount = rnd.Next(0, 4 + 1);
                for (var i = 0; i < foodCount; i++)
                {
                    var choice = rnd.Next(0, 1 + 1);
                    IGameObject item;
                    var leftTopCorner = new Point(x + 50 * i + 3, y);
                    if (choice == 1)
                        item = new Food(leftTopCorner);
                    else
                    {
                        var isGenerateCow = IsCreate(15);
                        item = isGenerateCow ? new Cow(leftTopCorner) : new Milk(leftTopCorner);
                    }
                    PlaceGameObject(map, item);
                }
            }
        }

        private static void PlacePowerUpOrGem(Map map, int x)
        {
            for (var y = 0; y < GameHeight - 100; y += 50 + PlatformHeight)
            {
                if (!IsCreate(10))
                    continue;
                var itemCenter = new Point(x + OtherObjectSize / 2,
                    y + OtherObjectSize / 2);
                var isGem = IsCreate(30);
                IGameObject item;
                if (isGem)
                    item = new Gem(itemCenter, GetRandomEnumValue<GemKind>());
                else
                    item = new PowerUp(itemCenter, GetRandomEnumValue<PowerUpKind>());
                PlaceGameObject(map, item);
            }
        }

        private static T GetRandomEnumValue<T>()
        {
            var rnd = new Random();
            var values = Enum.GetValues(typeof(T));
            return (T)values.GetValue(rnd.Next(values.Length));
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
            if (!isGenerateBomb)
                return null;
            var rnd = new Random();
            var x = rnd.Next(platform.LeftTopCorner.X,
                platform.LeftTopCorner.X + platform.Width - OtherObjectSize);
            var y = platform.LeftTopCorner.Y - BombHeight;
            return new Bomb(new Point(x, y));
        }

        private static Dictionary<int, int> GeneratePlatforms(Map map, int x)
        {
            var result = new Dictionary<int, int>(); // <y, width>
            var ys = GetYs(50, GameHeight - 100, 50);
            var rnd = new Random();
            var platformsCount = rnd.Next(3, 5 + 1);
            ys = ys.OrderBy(item => rnd.Next(650 / 50));
            foreach (var y in ys)
            {
                if (map.Field[x, y] == null)
                {
                    var width = Math.Min(rnd.Next(100, 400 + 1), GameWidth - x - 1);
                    result[y] = width;
                }
                if (result.Count == platformsCount)
                    break;
            }
            return result;
        }

        private static IEnumerable<int> GetYs(int begin, int end, int step)
        {
            for (var i = 0; i < end / step; i++)
            {
                var y = begin + i * (step + PlatformHeight);
                if (y > end)
                    yield break;
                yield return y;
            }
        }

        public static Map CreateMap(int width, int height, params IGameObject[] gameObjects)
        {
            var map = new Map(width, height);
            foreach (var gameObject in gameObjects)
                PlaceGameObject(map, gameObject);
            return map;
        }

        private static void PlaceGameObject(Map map, IGameObject gameObject)
        {
            map.GameObjects.Add(gameObject);
            var beginX = gameObject.LeftTopCorner.X;
            var endX = gameObject.LeftTopCorner.X + gameObject.Width;
            var beginY = gameObject.LeftTopCorner.Y;
            var endY = gameObject.LeftTopCorner.Y + gameObject.Height;
            for (var y = beginY; y < endY + 1; y++)
                for (var x = beginX; x < endX + 1; x++)
                    map.Field[x, y] = gameObject;
        }
    }
}
