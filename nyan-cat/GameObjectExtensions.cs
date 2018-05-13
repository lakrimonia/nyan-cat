using System;
using System.Collections.Generic;
using System.Drawing;

namespace nyan_cat
{
    public static class GameObjectExtensions
    {
        private static readonly Dictionary<IGameObject, string> images
            = new Dictionary<IGameObject, string>();
        private static int currentCatFrame;
        private static int currentPlatformFrame;
        public static readonly Dictionary<PowerUpKind, string> PowerUpImages
            = new Dictionary<PowerUpKind, string>
            {
                [PowerUpKind.BigNyan] = "big-nyan.png",
                [PowerUpKind.DoggieNyan] = "doggie-nyan.png",
                [PowerUpKind.FloristNyan] = "florist-nyan.png",
                [PowerUpKind.LoveNyan] = "love-nyan.png",
                [PowerUpKind.MilkGlasses] = "milk-glasses.png",
                [PowerUpKind.Piano] = "piano.png",
                [PowerUpKind.TurboNyan] = "turbo-nyan.png",
                [PowerUpKind.Ghost] = "ghost.png",
                [PowerUpKind.ShadowNyan] = "shadow-nyan.png"
            };
        public static readonly Dictionary<GemKind, string> GemImages
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

        public static void Draw(this IGameObject gameObject, Game game, Graphics e)
        {
            var rnd = new Random();
            var rect = new Rectangle(gameObject.LeftTopCorner.X,
                gameObject.LeftTopCorner.Y, gameObject.Width,
                gameObject.Height);
            string image;
            if (images.ContainsKey(gameObject)
                && (game.NyanCat.CurrentPowerUp?.Kind != PowerUpKind.ShadowNyan || !(gameObject is NyanCat))
                && (game.NyanCat.CurrentPowerUp?.Kind != PowerUpKind.Ghost || !(gameObject is Platform))
                && (game.NyanCat.CurrentPowerUp?.Kind != PowerUpKind.DoggieNyan || !(gameObject is NyanCat)))
                image = images[gameObject];
            else
            {
                switch (gameObject)
                {
                    case Dog _:
                        image = "dog.png";
                        break;
                    case NyanCat _:
                        image = (gameObject as NyanCat).CurrentPowerUp?.Kind == PowerUpKind.DoggieNyan
                                ? "doggie-nyan-cat.png"
                                : (gameObject as NyanCat).CurrentPowerUp?.Kind == PowerUpKind.ShadowNyan && currentCatFrame > 24
                                ? $"nyan-cat-s{currentCatFrame / 25}.png"
                                : "nyan-cat.png";
                        currentCatFrame = (gameObject as NyanCat).CurrentPowerUp?.Kind == PowerUpKind.ShadowNyan
                                ? (currentCatFrame + 1) % 125
                                : 0;
                        break;
                    case Platform _:
                        image = game.NyanCat.CurrentPowerUp?.Kind == PowerUpKind.Ghost && currentPlatformFrame > 199
                                ? $"p{gameObject.Width}-g{currentPlatformFrame / 200}.png"
                                : $"p{gameObject.Width}.png";
                        currentPlatformFrame = game.NyanCat.CurrentPowerUp?.Kind == PowerUpKind.Ghost
                            ? (currentPlatformFrame + 1) % 1000
                            : 0;
                        break;
                    case Food _:
                        image = game.NyanCat.CurrentPowerUp?.Kind == PowerUpKind.MilkGlasses
                                ? "milk.png"
                                : foodImages[rnd.Next(foodImages.Count)];
                        break;
                    case Gem _:
                        image = GemImages[((Gem)gameObject).Kind];
                        break;
                    case PowerUp _:
                        image = PowerUpImages[((PowerUp)gameObject).Kind];
                        break;
                    default:
                        image = otherImages[gameObject.GetType()];
                        break;
                }
                if ((game.NyanCat.CurrentPowerUp?.Kind != PowerUpKind.ShadowNyan || !(gameObject is NyanCat))
                    && (game.NyanCat.CurrentPowerUp?.Kind != PowerUpKind.Ghost || !(gameObject is Platform))
                    && (game.NyanCat.CurrentPowerUp?.Kind != PowerUpKind.DoggieNyan || !(gameObject is NyanCat)))
                    images[gameObject] = image;
            }
            e.DrawImage(Image.FromFile(image), rect);
        }
    }
}
