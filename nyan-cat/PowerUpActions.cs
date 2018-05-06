using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;

namespace nyan_cat
{
    public static class PowerUpActions
    {
        public static readonly Dictionary<PowerUpKind, Action<Game>> Activate
            = new Dictionary<PowerUpKind, Action<Game>>
            {
                [PowerUpKind.BigNyan] = BigNyanActivate,
                [PowerUpKind.DoggieNyan] = DoggieNyanActivate,
                [PowerUpKind.TurboNyan] = TurboNyanActivate
            };
        public static readonly Dictionary<PowerUpKind, Action<Game>> Deactivate
            = new Dictionary<PowerUpKind, Action<Game>>
            {
                [PowerUpKind.BigNyan] = BigNyanDeactivate,
                [PowerUpKind.DoggieNyan] = DoggieNyanDeactive,
                [PowerUpKind.TurboNyan] = TurboNyanDeactivate
            };

        private static void BigNyanActivate(Game game)
        {
            game.NyanCat.ProtectedFromEnemies = true;
            game.NyanCat.ProtectedFromBombs = true;
            game.ComboProtectedFromEnemies = true;
            game.NyanCat.Height *= 2;
            game.NyanCat.Width *= 2;
            var x = game.NyanCat.LeftTopCorner.X;
            var y = game.NyanCat.LeftTopCorner.Y - game.NyanCat.Height;
            game.NyanCat.LeftTopCorner = new Point(x, y);
        }

        private static void BigNyanDeactivate(Game game)
        {
            game.NyanCat.ProtectedFromEnemies = game.ProtectingFromEnemiesGem;
            game.NyanCat.ProtectedFromBombs = game.ProtectingFromBombsGem;
            game.ComboProtectedFromEnemies = game.ProtectingComboGem;
            game.NyanCat.Height /= 2;
            game.NyanCat.Width /= 2;
            var x = game.NyanCat.LeftTopCorner.X;
            var y = game.NyanCat.LeftTopCorner.Y + game.NyanCat.Height;
            game.NyanCat.LeftTopCorner = new Point(x, y);
        }

        private static void DoggieNyanActivate(Game game)
        {
            game.ComboProtectedFromEnemies = true;
            game.NyanCat.ProtectedFromEnemies = true;
            game.ProtectingComboPowerUp = true;
            game.ProtectingFromEnemiesPowerUp = true;
        }

        private static void DoggieNyanDeactive(Game game)
        {
            game.ComboProtectedFromEnemies = game.ProtectingComboGem;
            game.NyanCat.ProtectedFromEnemies = game.ProtectingFromEnemiesGem;
            game.ProtectingComboPowerUp = true;
            game.ProtectingFromEnemiesPowerUp = true;
        }

        private static void TurboNyanActivate(Game game)
        {
            var acceleration = new Vector2(-5, 0);
            foreach (var gameObject in game.GameObjects.Concat(game.FutureGameObjects))
                gameObject.Accelerate(acceleration);
        }

        private static void TurboNyanDeactivate(Game game)
        {
            var acceleration = new Vector2(5, 0);
            foreach (var gameObject in game.GameObjects.Concat(game.FutureGameObjects))
                gameObject.Accelerate(acceleration);
        }
    }
}
