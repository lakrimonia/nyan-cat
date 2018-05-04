using System;
using System.Collections.Generic;
using System.Drawing;

namespace nyan_cat
{
    public static class GameObjectExtensions
    {
        private static readonly Dictionary<IGameObject, string> images
            = new Dictionary<IGameObject, string>();
        private static readonly Dictionary<PowerUpKind, string> powerUpImages
            = new Dictionary<PowerUpKind, string>
            {
                [PowerUpKind.BigNyan] = "big-nyan.png",
                [PowerUpKind.DoggieNyan] = "doggie-nyan.png",
                [PowerUpKind.FloristNyan] = "florist-nyan.png",
                [PowerUpKind.LoveNyan] = "love-nyan.png",
                [PowerUpKind.MilkGlasses] = "milk-glasses.png",
                [PowerUpKind.Piano] = "piano.png",
                [PowerUpKind.TurboNyan] = "turbo-nyan.png"
            };
        private static readonly Dictionary<GemKind, string> gemImages
            = new Dictionary<GemKind, string>
            {
                [GemKind.DoubleCombo] = "red-gem.png",
                [GemKind.Invulnerable] = "green-gem.png",
                [GemKind.MilkLongLife] = "grey-gem.png"
            };
        private static readonly List<string> foodImages
            = new List<string>
            {
                "cake.png", "pizza.png", "burger.png",
                "donut.png", "ice-cream.png"
            };
        private static readonly Dictionary<Type, string> otherImages
            = new Dictionary<Type, string>
            {
                [typeof(TacNyan)] = "tac-nyan.png",
                [typeof(UFO)] = "ufo.png",
                [typeof(Cow)] = "cow.png",
                [typeof(Milk)] = "milk.png",
                [typeof(Bomb)] = "bomb.png",
            };

        public static void Draw(this IGameObject gameObject, Graphics e)
        {
            var rnd = new Random();
            var rect = new Rectangle(gameObject.LeftTopCorner.X,
                gameObject.LeftTopCorner.Y, gameObject.Width,
                gameObject.Height);
            string image;
            if (images.ContainsKey(gameObject))
                image = images[gameObject];
            else
            {
                switch (gameObject)
                {
                    case NyanCat _:
                        image = "nyan-cat.png";
                        break;
                    case Platform _:
                        image = $"p{gameObject.Width}.png";
                        break;
                    case Food _:
                        image = foodImages[rnd.Next(foodImages.Count)];
                        break;
                    case Gem _:
                        image = gemImages[((Gem) gameObject).Kind];
                        break;
                    case PowerUp _:
                        image = powerUpImages[((PowerUp) gameObject).Kind];
                        break;
                    default:
                        image = otherImages[gameObject.GetType()];
                        break;
                }
                images[gameObject] = image;
            }
            e.DrawImage(Image.FromFile(image), rect);
        }
    }
}
