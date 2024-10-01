using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using RedRats.Core;
using RedRats.Safety;
using Rogium.Core;
using Rogium.Editors.Core;
using Rogium.Editors.Packs;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Rogium.Editors.NewAssetSelection
{
    /// <summary>
    /// Overseers the Asset Selection Menu.
    /// </summary>
    public sealed class SelectionMenuOverseerMono : MonoSingleton<SelectionMenuOverseerMono>
    {
        [SerializeField] private SelectionDataInfo data;
        [Button] public void TestFill() => Open(AssetType.Pack);
        
        private IDictionary<AssetType, SelectionMenuData> menuData;
        private AssetType currentType;

        protected override void Awake()
        {
            base.Awake();
            menuData = new Dictionary<AssetType, SelectionMenuData>
            {
                {AssetType.Pack, new SelectionMenuData(data.packSelection, ExternalLibraryOverseer.Instance.Packs.Cast<IAsset>().ToList)},
                {AssetType.Palette, new SelectionMenuData(data.paletteSelection, GetPaletteList)},
                {AssetType.Sprite, new SelectionMenuData(data.spriteSelection, GetSpriteList)},
                {AssetType.Weapon, new SelectionMenuData(data.weaponSelection, GetWeaponList)},
                {AssetType.Projectile, new SelectionMenuData(data.projectileSelection, GetProjectileList)},
                {AssetType.Enemy, new SelectionMenuData(data.enemySelection, GetEnemyList)},
                {AssetType.Room, new SelectionMenuData(data.roomSelection, GetRoomList)},
                {AssetType.Tile, new SelectionMenuData(data.tileSelection, GetTileList)}
            };
        }

        private static IList<IAsset> GetPaletteList() => PackEditorOverseer.Instance.CurrentPack.Palettes.Cast<IAsset>().ToList();
        private static IList<IAsset> GetSpriteList() => PackEditorOverseer.Instance.CurrentPack.Sprites.Cast<IAsset>().ToList();
        private static IList<IAsset> GetWeaponList() => PackEditorOverseer.Instance.CurrentPack.Weapons.Cast<IAsset>().ToList();
        private static IList<IAsset> GetProjectileList() => PackEditorOverseer.Instance.CurrentPack.Projectiles.Cast<IAsset>().ToList();
        private static IList<IAsset> GetEnemyList() => PackEditorOverseer.Instance.CurrentPack.Enemies.Cast<IAsset>().ToList();
        private static IList<IAsset> GetRoomList() => PackEditorOverseer.Instance.CurrentPack.Rooms.Cast<IAsset>().ToList();
        private static IList<IAsset> GetTileList() => PackEditorOverseer.Instance.CurrentPack.Tiles.Cast<IAsset>().ToList();

        public void Open(AssetType type)
        {
            currentType = type;
            GetData(currentType).Load();
        }

        private SelectionMenuData GetData(AssetType type)
        {
            SafetyNet.EnsureDictionaryContainsKey(menuData, type, nameof(menuData));
            return menuData[type];
        }

        public AssetSelector CurrentSelector { get => GetData(currentType).AssetSelector; }
        public AssetType CurrentType { get => currentType; }

        [Serializable]
        public struct SelectionDataInfo
        {
            public SelectionMenuData packSelection;
            public SelectionMenuData paletteSelection;
            public SelectionMenuData spriteSelection;
            public SelectionMenuData weaponSelection;
            public SelectionMenuData projectileSelection;
            public SelectionMenuData enemySelection;
            public SelectionMenuData roomSelection;
            public SelectionMenuData tileSelection;
        }
    }
}