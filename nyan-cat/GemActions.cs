using System;
using System.Collections.Generic;

namespace nyan_cat
{
    public static class GemActions
    {
        public static Dictionary<GemKind, Action<Game>> Activate
            = new Dictionary<GemKind, Action<Game>>
            {
                [GemKind.DoubleCombo] = DoubleComboActivate,
                [GemKind.Invulnerable] = InvulnerableActivate,
                [GemKind.MilkLongLife] = MilkLongLifeActivate
            };
        public static Dictionary<GemKind, Action<Game>> Deactivate
            = new Dictionary<GemKind, Action<Game>>
            {
                [GemKind.DoubleCombo] = DoubleComboDeactivate,
                [GemKind.Invulnerable] = InvulnerableDeactivate,
                [GemKind.MilkLongLife] = MilkLongLifeDeactivate
            };

        private static void DoubleComboActivate(Game game)
        {
            game.Combo *= 2;
            game.AddCombo *= 2;
        }

        private static void DoubleComboDeactivate(Game game)
        {
            game.Combo /= 2;
            game.AddCombo /= 2;
        }

        private static void InvulnerableActivate(Game game)
        {
            game.NyanCat.ProtectedFromBombs = true;
            game.NyanCat.ProtectedFromEnemies = true;
        }

        private static void InvulnerableDeactivate(Game game)
        {
            if (game.NyanCat.CurrentPowerUp?.Kind != PowerUpKind.BigNyan)
            {
                game.NyanCat.ProtectedFromBombs = false;
                if (game.NyanCat.CurrentPowerUp?.Kind != PowerUpKind.DoggieNyan)
                    game.NyanCat.ProtectedFromEnemies = false;
            }
        }

        private static void MilkLongLifeActivate(Game game)
        {
            game.ComboProtectedFromEnemies = true;
        }

        private static void MilkLongLifeDeactivate(Game game)
        {
            game.ComboProtectedFromEnemies = false;
        }
    }
}
