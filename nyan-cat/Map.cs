using System.Collections.Generic;

namespace nyan_cat
{
    public class Map
    {
        public IGameObject[,] Field { get; }
        public List<IGameObject> GameObjects { get; }

        public Map(int width, int height)
        {
            Field = new IGameObject[width, height];
            GameObjects = new List<IGameObject>();
        }
    }
}
