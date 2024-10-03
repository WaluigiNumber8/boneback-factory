using System;
using System.Collections.Generic;
using System.Linq;
using Rogium.Core;
using Rogium.Editors.Core;
using Rogium.Editors.Packs;
using Rogium.UserInterface.Interactables;
using UnityEngine;

namespace Rogium.Editors.NewAssetSelection
{
    /// <summary>
    /// Works with <see cref="AssetSelector"/> to pick assets.
    /// </summary>
    public class AssetSelectionPicker : MonoBehaviour
    {
        [SerializeField] private AssetSelector selector;

        private SelectionMenuData data;
        private ISet<int> selectedAssetIndexes;
        private Action<IAsset> whenConfirmed;

        private void Awake()
        {
            data = new SelectionMenuData(selector, ButtonType.None, ButtonType.None, ButtonType.None, ButtonType.None, Array.Empty<IAsset>);
            selectedAssetIndexes = new HashSet<int>();
        }

        private void OnEnable()
        {
            AssetCardControllerV2.OnSelect += SelectAsset;
            AssetCardControllerV2.OnDeselect += DeselectAsset;
        }

        private void OnDisable()
        {
            AssetCardControllerV2.OnSelect -= SelectAsset;
            AssetCardControllerV2.OnDeselect -= DeselectAsset;
        }

        
        public void Pick(AssetType type, Action<IAsset> whenConfirmed, IAsset preselectedAsset = null, bool canSelectEmpty = false)
        {
            this.whenConfirmed = whenConfirmed;
            data = new SelectionMenuData(data, GetAssetListByType(type));
            selector.Load(data);
        }

        public void ConfirmSelection()
        {
            if (selectedAssetIndexes?.Count == 0) return;
            whenConfirmed?.Invoke(data.GetAssetList()[selectedAssetIndexes.First()]);
        }

        public void CancelSelection()
        {
        }
        
        private void SelectAsset(int index) => selectedAssetIndexes.Add(index);
        private void DeselectAsset(int index) => selectedAssetIndexes.Remove(index);
        
        private Func<IList<IAsset>> GetAssetListByType(AssetType type)
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
        
        public RectTransform SelectorContent { get => selector.Content; }
        public int SelectedAssetsCount { get => selectedAssetIndexes.Count; }
    }
}