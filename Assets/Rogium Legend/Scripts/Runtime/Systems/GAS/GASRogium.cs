using System;
using BoubakProductions.Safety;
using Rogium.Core;
using Rogium.Systems.ThemeSystem;
using Rogium.UserInterface;
using Rogium.UserInterface.AssetSelection;

namespace Rogium.Systems.GASExtension
{
    /// <summary>
    /// A GAS Extension class, containing specific actions to Rogium Legend.
    /// </summary>
    public static class GASRogium
    {
        public static AssetSelectionMenu assetSelection;

        public static void OpenSelectionMenu(AssetType type)
        {
            SafetyNet.EnsureIsNotNull(assetSelection, "GAS Asset Selection");
            switch (type)
            {
                case AssetType.Pack:
                    assetSelection.OpenForPacks();
                    break;
                case AssetType.Palette:
                    assetSelection.OpenForPalettes();
                    break;
                case AssetType.Sprite:
                    assetSelection.OpenForSprites();
                    break;
                case AssetType.Weapon:
                    assetSelection.OpenForWeapons();
                    break;
                case AssetType.Projectile:
                    assetSelection.OpenForProjectiles();
                    break;
                case AssetType.Enemy:
                    assetSelection.OpenForEnemies();
                    break;
                case AssetType.Room:
                    assetSelection.OpenForRooms();
                    break;
                case AssetType.Tile:
                    assetSelection.OpenForTiles();
                    break;
                default:
                    throw new ArgumentOutOfRangeException($"'{type}' is not a supported Asset Type.");
            }
        }

        public static void ChangeTheme(ThemeType type)
        {
            ThemeOverseer.Instance.ChangeTheme(type);
        }
    }
}