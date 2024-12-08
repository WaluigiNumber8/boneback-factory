using System;
using System.Collections.Generic;
using System.Linq;
using RedRats.Core;
using RedRats.Safety;
using Rogium.Core;
using Rogium.Editors.Core;
using Rogium.Editors.AssetSelection.UI;
using Rogium.Editors.Packs;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Rogium.Editors.AssetSelection
{
    /// <summary>
    /// Overseers the Asset Selection Menu.
    /// </summary>
    public sealed partial class SelectionMenuOverseerMono : MonoSingleton<SelectionMenuOverseerMono>
    {
        public event Action<AssetType> OnOpen;

        [SerializeField] private SelectionInfoColumn infoColumn;
        [SerializeField] private AssetTabGroup categoryTabGroup;
        [SerializeField] private SelectionDataInfo data;
        [Button] public void TestFill() => Open(AssetType.Pack);
        
        private IDictionary<AssetType, SelectionMenuData> menuData;
        private AssetType currentType;

        protected override void Awake()
        {
            base.Awake();
            menuData = new Dictionary<AssetType, SelectionMenuData>
            {
                {AssetType.Pack, new SelectionMenuData.Builder().AsCopy(data.packSelection).WithGetAssetList(ExternalLibraryOverseer.Instance.Packs.Cast<IAsset>().ToList).WithWhenCardSelected(PrepareInfoColumn).Build()},
                {AssetType.Palette, new SelectionMenuData.Builder().AsCopy(data.paletteSelection).WithGetAssetList(GetPaletteList).WithWhenCardSelected(PrepareInfoColumn).Build()},
                {AssetType.Sprite, new SelectionMenuData.Builder().AsCopy(data.spriteSelection).WithGetAssetList(GetSpriteList).WithWhenCardSelected(PrepareInfoColumn).Build()},
                {AssetType.Weapon, new SelectionMenuData.Builder().AsCopy(data.weaponSelection).WithGetAssetList(GetWeaponList).WithWhenCardSelected(PrepareInfoColumn).Build()},
                {AssetType.Projectile, new SelectionMenuData.Builder().AsCopy(data.projectileSelection).WithGetAssetList(GetProjectileList).WithWhenCardSelected(PrepareInfoColumn).Build()},
                {AssetType.Enemy, new SelectionMenuData.Builder().AsCopy(data.enemySelection).WithGetAssetList(GetEnemyList).WithWhenCardSelected(PrepareInfoColumn).Build()},
                {AssetType.Room, new SelectionMenuData.Builder().AsCopy(data.roomSelection).WithGetAssetList(GetRoomList).WithWhenCardSelected(PrepareInfoColumn).Build()},
                {AssetType.Tile, new SelectionMenuData.Builder().AsCopy(data.tileSelection).WithGetAssetList(GetTileList).WithWhenCardSelected(PrepareInfoColumn).Build()},
            };
        }

        private void OnEnable()
        {
            data.SubscribeToCardSelection(PrepareInfoColumn);
            data.SubscribeToNoSelection(PrepareInfoColumnForEmpty);
        }

        private void OnDisable()
        {
            data.UnsubscribeFromCardSelection(PrepareInfoColumn);
            data.UnsubscribeFromNoSelection(PrepareInfoColumnForEmpty);
        }

        public void Open(AssetType type)
        {
            currentType = type;
            GetData(currentType).Load();
            if (type != AssetType.Pack && type != AssetType.Campaign) categoryTabGroup.Switch(type);
            OnOpen?.Invoke(currentType);
        }
        
        public void ResetTabGroup() => categoryTabGroup.Switch(0);

        private SelectionMenuData GetData(AssetType type)
        {
            SafetyNet.EnsureDictionaryContainsKey(menuData, type, nameof(menuData));
            return menuData[type];
        }

        private void PrepareInfoColumn(int index) => infoColumn.Construct(GetData(currentType).GetAssetList()[index]);
        private void PrepareInfoColumnForEmpty() => infoColumn.ConstructEmpty(currentType);
        
        private static IList<IAsset> GetPaletteList() => PackEditorOverseer.Instance.CurrentPack.Palettes.Cast<IAsset>().ToList();
        private static IList<IAsset> GetSpriteList() => PackEditorOverseer.Instance.CurrentPack.Sprites.Cast<IAsset>().ToList();
        private static IList<IAsset> GetWeaponList() => PackEditorOverseer.Instance.CurrentPack.Weapons.Cast<IAsset>().ToList();
        private static IList<IAsset> GetProjectileList() => PackEditorOverseer.Instance.CurrentPack.Projectiles.Cast<IAsset>().ToList();
        private static IList<IAsset> GetEnemyList() => PackEditorOverseer.Instance.CurrentPack.Enemies.Cast<IAsset>().ToList();
        private static IList<IAsset> GetRoomList() => PackEditorOverseer.Instance.CurrentPack.Rooms.Cast<IAsset>().ToList();
        private static IList<IAsset> GetTileList() => PackEditorOverseer.Instance.CurrentPack.Tiles.Cast<IAsset>().ToList();

        public AssetSelector CurrentSelector { get => GetData(currentType).AssetSelector; }
        public AssetType CurrentType { get => currentType; }
    }
}