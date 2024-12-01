using System.Collections.Generic;
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

        public AssetSelector Selector { get => selector; }
        public int SelectedAssetsCount { get => selectedAssetIndexes.Count; }
    }
}