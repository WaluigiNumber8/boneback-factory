using System;
using System.Collections.Generic;
using System.Linq;
using RedRats.Systems.Themes;
using Rogium.Core;
using Rogium.Editors.Core;
using Rogium.Editors.Packs;

namespace Rogium.Editors.NewAssetSelection
{
    public static class AssetSelectionUtils
    {
        public static Func<IList<IAsset>> GetAssetListByType(AssetType type)
        {
            return (type) switch {
                AssetType.Pack => ExternalLibraryOverseer.Instance.Packs.Cast<IAsset>().ToList,
                AssetType.Palette => PackEditorOverseer.Instance.CurrentPack.Palettes.Cast<IAsset>().ToList,
                AssetType.Sprite => PackEditorOverseer.Instance.CurrentPack.Sprites.Cast<IAsset>().ToList,
                AssetType.Weapon => PackEditorOverseer.Instance.CurrentPack.Weapons.Cast<IAsset>().ToList,
                AssetType.Projectile => PackEditorOverseer.Instance.CurrentPack.Projectiles.Cast<IAsset>().ToList,
                AssetType.Enemy => PackEditorOverseer.Instance.CurrentPack.Enemies.Cast<IAsset>().ToList,
                AssetType.Room => PackEditorOverseer.Instance.CurrentPack.Rooms.Cast<IAsset>().ToList,
                AssetType.Tile => PackEditorOverseer.Instance.CurrentPack.Tiles.Cast<IAsset>().ToList,
                AssetType.Object => InternalLibraryOverseer.GetInstance().Objects.Cast<IAsset>().ToList,
                AssetType.Sound => InternalLibraryOverseer.GetInstance().Sounds.Cast<IAsset>().ToList,
                _ => throw new ArgumentOutOfRangeException(nameof(type), type, null)
            };
        }

        public static ThemeType GetThemeByType(AssetType assetType)
        {
            return assetType switch
            {
                AssetType.Pack => ThemeType.Blue,
                AssetType.Palette => ThemeType.Purple,
                AssetType.Sprite => ThemeType.Pink,
                AssetType.Weapon => ThemeType.Green,
                AssetType.Projectile => ThemeType.Teal,
                AssetType.Enemy => ThemeType.Red,
                AssetType.Room => ThemeType.Blue,
                AssetType.Tile => ThemeType.Yellow,
                _ => throw new ArgumentOutOfRangeException(nameof(assetType), assetType, null)
            };
        }
    }
}