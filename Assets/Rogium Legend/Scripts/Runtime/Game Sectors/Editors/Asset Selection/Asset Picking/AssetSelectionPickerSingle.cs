using System;
using System.Collections.Generic;
using System.Linq;
using Rogium.Core;
using Rogium.Editors.Core;

namespace Rogium.Editors.NewAssetSelection
{
    /// <summary>
    /// Works with <see cref="AssetSelector"/> to pick a single asset.
    /// </summary>
    public class AssetSelectionPickerSingle : AssetSelectionPickerBase
    {
        private Action<IAsset> whenConfirmed;
        private int lastIndex;

        public void Pick(AssetType type, Action<IAsset> whenConfirmed, IAsset preselectedAsset = null, bool canSelectEmpty = false)
        {
            this.whenConfirmed = whenConfirmed;
            lastIndex = -2; //-1 is empty card so has to lower
            data = new SelectionMenuData.Builder().AsCopy(data).WithGetAssetList(GetAssetListByType(type)).Build();

            selector.Load(data, new HashSet<IAsset> {preselectedAsset});

            if (!canSelectEmpty) return;
            PrepareSelectNoneButton(preselectedAsset);
        }

        public override void ConfirmSelection()
        {
            if (selectedAssetIndexes == null || selectedAssetIndexes.Count == 0) return;
            IList<IAsset> assets = data.GetAssetList();
            IAsset selectedAsset = (selectedAssetIndexes.Contains(-1)) ? new EmptyAsset() : assets[selectedAssetIndexes.First()];
            whenConfirmed?.Invoke(selectedAsset);
            whenConfirmed = null;
        }
        
        protected override void SelectAsset(int index)
        {
            if (index == lastIndex)
            {
                ConfirmSelection();
                return;
            }
            base.SelectAsset(index);
            lastIndex = index;
        }
        
        private void PrepareSelectNoneButton(IAsset preselectedAsset)
        {
            AssetCardControllerV2 emptyCard = selector.Content.GetChild(0).GetComponent<AssetCardControllerV2>();
            emptyCard = (emptyCard.Index != -1) ? Instantiate(emptyCardPrefab, selector.Content) : emptyCard;
            emptyCard.RemoveAllListeners();
            emptyCard.OnSelect += data.WhenCardSelected;
            emptyCard.OnDeselect += data.WhenCardDeselected;
            emptyCard.SetToggle(preselectedAsset == null);
            emptyCard.transform.SetAsFirstSibling();
            emptyCard.Construct(new AssetCardData.Builder().WithIndex(-1).Build());
        }
    }
}