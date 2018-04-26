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
                var beginX = gameObject.Center.X - gameObject.Width / 2;
                var endX = gameObject.Center.X + gameObject.Width / 2;
                var beginY = gameObject.Center.Y - gameObject.Height / 2;
                var endY = gameObject.Center.Y + gameObject.Height / 2;
                for (var y = beginY; y < endY + 1; y++)
                    for (var x = beginX; x < endX + 1; x++)
                        map[x, y] = gameObject;
            }
            return map;
        }
    }
}
