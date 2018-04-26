using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace nyan_cat
{
    public static class MapCreator
    {
        public static IGameObject[,] CreateRandomMap(int width, int height)
        {
            var map = new IGameObject[width, height];

            return map;
        }

        public static IGameObject[,] CreateMap(int width, int height, params IGameObject[] gameObjects)
        {
            var map = new IGameObject[width, height];
            foreach (var gameObject in gameObjects)
            {
                if (gameObject is Platform)
                {
                    var platform = gameObject as Platform;
                    var begin = platform.Center.X - platform.Length / 2;
                    var end = platform.Center.X + platform.Length / 2;
                    for (var x = begin; x < end + 1; x++)
                        map[x, platform.Center.Y] = platform;
                }
            }
            return map;
        }
    }
}
