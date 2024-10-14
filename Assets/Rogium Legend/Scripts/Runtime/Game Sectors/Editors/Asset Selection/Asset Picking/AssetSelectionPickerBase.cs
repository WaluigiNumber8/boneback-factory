using System;
using System.Collections.Generic;
using System.Linq;
using Rogium.Core;
using Rogium.Editors.Core;
using Rogium.Editors.Packs;
using Rogium.UserInterface.Interactables;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Rogium.Editors.NewAssetSelection
{
    /// <summary>
    /// A base for asset selection pickers.
    /// </summary>
    public abstract class AssetSelectionPickerBase : MonoBehaviour
    {
        [SerializeField] protected AssetSelector selector;
        [SerializeField] protected AssetCardControllerV2 emptyCardPrefab;

        protected SelectionMenuData data;
        protected ISet<int> selectedAssetIndexes;

        protected virtual void Awake()
        {
            data = new SelectionMenuData.Builder()
                .WithAssetSelector(selector)
                .WithWhenCardSelected(SelectAsset)
                .WithWhenCardDeselected(DeselectAsset)
                .Build();
            selectedAssetIndexes = new HashSet<int>();
        }

        public void SelectAll(bool value)
        {
            for (int i = 0; i < selector.Content.childCount; i++)
            {
                Select(i, value);
            }
        }
        
        public void SelectRandom()
        {
            SelectAll(false);
            int amount = Random.Range(1, Selector.Content.childCount + 1);
            for (int i = 0; i < amount; i++)
            {
                Select(Random.Range(0, Selector.Content.childCount));
            }
        }
        
        public void Select(int index, bool value = true) => selector.GetCard(index).SetToggle(value);

        public abstract void ConfirmSelection();
        
        protected virtual void SelectAsset(int index) => selectedAssetIndexes.Add(index);
        protected virtual void DeselectAsset(int index) => selectedAssetIndexes.Remove(index);
        
        protected Func<IList<IAsset>> GetAssetListByType(AssetType type)
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
        
        public AssetSelector Selector { get => selector; }
        public int SelectedAssetsCount { get => selectedAssetIndexes.Count; }
    }
}