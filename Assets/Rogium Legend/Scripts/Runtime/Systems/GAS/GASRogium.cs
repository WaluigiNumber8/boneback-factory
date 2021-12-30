using System;
using BoubakProductions.Safety;
using Rogium.Core;
using Rogium.Global.ThemeSystem;
using Rogium.UserInterface;
using Rogium.UserInterface.AssetSelection;

namespace Rogium.Global.GASExtension
{
    /// <summary>
    /// A GAS Extension class, containing specific actions to Rogium Legend.
    /// </summary>
    public static class GASRogium
    {
        public static AssetSelectionOverseerMono assetSelection;

        public static void OpenSelectionMenu(AssetType type)
        {
            SafetyNet.EnsureIsNotNull(assetSelection, "GAS Asset Selection");
            switch (type)
            {
                case AssetType.Pack:
                    assetSelection.ReopenForPacks();
                    break;
                case AssetType.Palette:
                    assetSelection.ReopenForPalettes();
                    break;
                case AssetType.Sprite:
                    assetSelection.ReopenForSprites();
                    break;
                case AssetType.Weapon:
                    assetSelection.ReopenForWeapons();
                    break;
                case AssetType.Enemy:
                    assetSelection.ReopenForEnemies();
                    break;
                case AssetType.Room:
                    assetSelection.ReopenForRooms();
                    break;
                case AssetType.Tile:
                    assetSelection.ReopenForTiles();
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