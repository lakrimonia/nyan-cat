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
                    var beginX = platform.Center.X - platform.Length / 2;
                    var endX = platform.Center.X + platform.Length / 2;
                    var beginY = platform.Center.Y - 13;
                    var endY = platform.Center.Y + 13;
                    for (var y = beginY; y < endY + 1; y++)
                        for (var x = beginX; x < endX + 1; x++)
                            map[x, y] = platform;
                }
                else
                {
                    var beginX = gameObject.Center.X - 25;
                    var endX = gameObject.Center.X + 25;
                    var beginY = gameObject.Center.Y - 25;
                    var endY = gameObject.Center.Y + 25;
                    for (var y = beginY; y < endY + 1; y++)
                        for (var x = beginX; x < endX + 1; x++)
                            map[x, y] = gameObject;
                }
            }
            return map;
        }
    }
}
