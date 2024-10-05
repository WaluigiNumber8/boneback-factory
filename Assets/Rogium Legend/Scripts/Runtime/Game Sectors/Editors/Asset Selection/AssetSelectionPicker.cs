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
    /// Works with <see cref="AssetSelector"/> to pick assets.
    /// </summary>
    public class AssetSelectionPicker : MonoBehaviour
    {
        [SerializeField] private AssetSelector selector;
        [SerializeField] private AssetCardControllerV2 emptyCardPrefab;

        private SelectionMenuData data;
        private ISet<int> selectedAssetIndexes;
        private Action<IAsset> whenConfirmedSingle;
        private Action<ISet<IAsset>> whenConfirmedMultiple;
        private int lastIndex;

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

        public void Pick(AssetType type, Action<ISet<IAsset>> whenConfirmed, ISet<IAsset> preselectedAssets = null, bool canSelectEmpty = false)
        {
            whenConfirmedMultiple = whenConfirmed;
            lastIndex = -2; //-1 is empty card so has to lower
            data = new SelectionMenuData(data, GetAssetListByType(type));
            
            selector.Load(data, preselectedAssets);

            if (!canSelectEmpty) return;
            AssetCardControllerV2 emptyCard = Instantiate(emptyCardPrefab, selector.Content);
            emptyCard.transform.SetAsFirstSibling();
            emptyCard.Construct(new AssetCardData.Builder().WithIndex(-1).Build());
        }
        
        public void Pick(AssetType type, Action<IAsset> whenConfirmed, IAsset preselectedAsset = null, bool canSelectEmpty = false)
        {
            whenConfirmedSingle = whenConfirmed;
            lastIndex = -2; //-1 is empty card so has to lower
            data = new SelectionMenuData(data, GetAssetListByType(type));

            selector.Load(data, new HashSet<IAsset> {preselectedAsset});

            if (!canSelectEmpty) return;
            AssetCardControllerV2 emptyCard = Instantiate(emptyCardPrefab, selector.Content);
            emptyCard.transform.SetAsFirstSibling();
            emptyCard.Construct(new AssetCardData.Builder().WithIndex(-1).Build());
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
            int amount = Random.Range(1, SelectorContent.childCount + 1);
            for (int i = 0; i < amount; i++)
            {
                Select(Random.Range(0, SelectorContent.childCount));
            }
        }
        
        public void Select(int index, bool value = true)
        {
            if (index < 0 || index >= selector.Content.childCount) return;
            selector.Content.GetChild(index).GetComponent<AssetCardControllerV2>().SetToggle(value);
        }
        
        public void ConfirmSelection()
        {
            if (selectedAssetIndexes == null || selectedAssetIndexes.Count == 0) return;
            IList<IAsset> assets = data.GetAssetList();
            IAsset selectedAsset = (selectedAssetIndexes.Contains(-1)) ? new EmptyAsset() : assets[selectedAssetIndexes.First()];
            whenConfirmedSingle?.Invoke(selectedAsset);

            ISet<IAsset> selectedAssets = selectedAssetIndexes.Where(i => i != -1).Select(i => assets[i]).ToHashSet();
            whenConfirmedMultiple?.Invoke(selectedAssets);
            
            whenConfirmedSingle = null;
            whenConfirmedMultiple = null;
        }
        
        private void SelectAsset(int index)
        {
            if (index == lastIndex)
            {
                ConfirmSelection();
                return;
            }
            selectedAssetIndexes.Add(index);
            lastIndex = index;
        }

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