using System;
using Rogium.Core;
using Rogium.Global.UISystem;
using Rogium.Global.UISystem.AssetSelection;

namespace Rogium.GASExtensions
{
    /// <summary>
    /// A GAS Extension class, containing specific actions to Rogium Legend.
    /// </summary>
    public static class GASRogium
    {
        public static void SwitchMenu(MenuType newMenu)
        {
            MenuSwitcher.GetInstance().SwitchTo(newMenu);
        }

        public static void ReopenSelectionMenu(AssetType type)
        {
            AssetSelectionOverseerMono selection = AssetSelectionOverseerMono.GetInstance();
            switch (type)
            {
                case AssetType.Pack:
                    selection.ReopenForPacks();
                    break;
                case AssetType.Palette:
                    selection.ReopenForPalettes();
                    break;
                case AssetType.Sprite:
                    selection.ReopenForSprites();
                    break;
                case AssetType.Weapon:
                    selection.ReopenForWeapons();
                    break;
                case AssetType.Enemy:
                    selection.ReopenForEnemies();
                    break;
                case AssetType.Room:
                    selection.ReopenForRooms();
                    break;
                case AssetType.Tile:
                    selection.ReopenForTiles();
                    break;
                default:
                    throw new ArgumentOutOfRangeException($"'{type}' is not a supported Asset Type.");
            }
        }
    }
}