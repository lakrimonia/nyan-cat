using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace nyan_cat
{
    public static class MapCreator
    {
        public const int GameWidth = 1000;
        public const int GameHeight = 700;
        private const int EnemyWidth = 80;
        private const int ObjUsualSize = 50;
        private const int BombHeight = 25;

        private static int addX;
        private static bool withoutEnemiesAndBombs;

        private static int maxCowCount;
        private static int maxBombCount;
        private static int maxGemCount;
        private static int maxEnemyCount;
        private static int maxPowerUpCount;

        private static Random random = new Random();

        public static List<IGameObject> CreateRandomMap(bool isFuture = false, bool enemiesAndBombs = false)
        {
            withoutEnemiesAndBombs = !enemiesAndBombs;
            maxBombCount = enemiesAndBombs && IsThereChance(20) ? random.Next(1, 3 + 1) : 0;
            maxEnemyCount = enemiesAndBombs && IsThereChance(20) ? random.Next(1, 2 + 1) : 0;
            maxCowCount = IsThereChance(15) ? 1 : 0;
            maxGemCount = IsThereChance(12) ? 1 : 0;
            maxPowerUpCount = IsThereChance(40) ? random.Next(1, 3 + 1) : 0;
            addX = isFuture ? GameWidth : 0;
            var map = new List<IGameObject>();
            for (var x = 0 + addX; x < GameWidth - 100 + addX; x += 250)
            {
                PlacePlatformsBombsAndEnemies(map, x);
                PlaceFoodAndMilk(map, x);
                if ((maxPowerUpCount != 0 || maxGemCount != 0) && IsThereChance(75))
                    PlacePowerUpOrGem(map, x);
            }
            return map;
        }

        private static void PlacePowerUpOrGem(List<IGameObject> map, int x)
        {
            var ys = new List<int>();
            for (var i = 50; i < GameHeight - ObjUsualSize; i += 125)
                ys.Add(i);
            var y = ys.OrderBy(e => random.Next(ys.Count)).FirstOrDefault();
            var leftTopCorner = new Point(x + 200, y);
            var gemKind = GetRandomEnumValue<GemKind>();
            var powerUpKind = GetRandomEnumValue<PowerUpKind>();
            var powerUpOrGem = random.Next(0, 1 + 1);
            if (powerUpOrGem == 0 || maxGemCount == 0)
            {
                PlaceGameObject(map, new PowerUp(leftTopCorner, powerUpKind));
                maxPowerUpCount--;
            }
            else
            {
                PlaceGameObject(map, new Gem(leftTopCorner, gemKind));
                maxGemCount--;
            }

        }

        private static void PlaceFoodAndMilk(List<IGameObject> map, int x)
        {
            for (var y = 50; y < GameHeight - 125; y += 125)
            {
                if (!IsThereChance(50))
                    continue;
                var count = random.Next(1, 4 + 1);
                for (var i = 0; i < count; i++)
                {
                    var foodOrMilk = random.Next(0, 1 + 1);
                    IGameObject item;
                    var leftTopCorner = new Point(x + 50 * i, y);
                    if (foodOrMilk == 0)
                    {
                        item = new Food(leftTopCorner);
                    }
                    else
                    {
                        var isCow = maxCowCount > 0 && IsThereChance(10);
                        item = isCow
                            ? new Cow(leftTopCorner)
                            : new Milk(leftTopCorner);
                        maxCowCount -= isCow ? 1 : 0;
                    }
                    PlaceGameObject(map, item);
                }
            }
        }

        private static void PlacePlatformsBombsAndEnemies(List<IGameObject> map, int x)
        {
            foreach (var p in GeneratePlatforms(map, x))
            {
                var platform = new Platform(new Point(x, p.Item1), p.Item2);
                PlaceGameObject(map, platform);
                if (withoutEnemiesAndBombs)
                    continue;
                if (maxBombCount > 0)
                {
                    var bomb = GenerateBomb(platform);
                    if (bomb != null)
                    {
                        PlaceGameObject(map, bomb);
                        maxBombCount--;
                    }
                }
                if(maxEnemyCount>0)
                {
                    var enemy = GenerateEnemy(platform);
                    if (enemy != null)
                    {
                        PlaceGameObject(map, enemy);
                        maxEnemyCount--;
                    }
                }
            }
        }

        private static IEnemy GenerateEnemy(Platform platform)
        {
            if (!IsThereChance(10))
                return null;
            if (random.Next(0, 1 + 1) == 0)
                return new UFO(new Point(random.Next(
                        platform.LeftTopCorner.X,
                        platform.LeftTopCorner.X + platform.Width - EnemyWidth),
                    platform.LeftTopCorner.Y - ObjUsualSize));
            return new Animal(platform);
        }

        private static IEnumerable<Tuple<int, int>> GeneratePlatforms(List<IGameObject> map, int x)
        {
            var count = random.Next(3, 6 + 1);
            var ys = new List<int>();
            for (var i = 125; i < GameHeight; i += 125)
                ys.Add(i);
            ys = ys.OrderBy(e => random.Next(ys.Count)).ToList();
            foreach (var y in ys)
            {
                if (map.Any(p => p.LeftTopCorner.Y == y
                                 && p.LeftTopCorner.X + p.Width >= x))
                    continue;
                var width = random.Next(1, 4 + 1) * 100;
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
            var x = random.Next(platform.LeftTopCorner.X,
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
            var a = answer.OrderBy(e => random.Next(100));
            return a.FirstOrDefault();
        }

        private static void PlaceGameObject(List<IGameObject> map, IGameObject gameObject)
        {
            map.Add(gameObject);
        }

        private static T GetRandomEnumValue<T>()
        {
            var rnd = new Random();
            var values = Enum.GetValues(typeof(T));
            return (T)values.GetValue(rnd.Next(values.Length));
        }

        public static List<IGameObject> CreateMap(int width, int height, params IGameObject[] gameObjects)
            => gameObjects.ToList();
    }
}
