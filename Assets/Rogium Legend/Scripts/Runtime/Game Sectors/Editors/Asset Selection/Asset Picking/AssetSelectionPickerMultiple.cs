using System;
using System.Collections.Generic;
using System.Linq;
using Rogium.Core;
using Rogium.Editors.Core;
using static Rogium.Editors.NewAssetSelection.AssetSelectionUtils;

namespace Rogium.Editors.NewAssetSelection
{
    /// <summary>
    /// Works with <see cref="AssetSelector"/> to pick multiple assets.
    /// </summary>
    public class AssetSelectionPickerMultiple : AssetSelectionPickerBase
    {
        private Action<ISet<IAsset>> whenConfirmed;

        public void Pick(AssetType type, Action<ISet<IAsset>> whenConfirmed, ISet<IAsset> preselectedAssets = null, bool canSelectEmpty = false)
        {
            this.whenConfirmed = whenConfirmed;
            data = new SelectionMenuData.Builder()
                .AsCopy(data)
                .WithGetAssetList(GetAssetListByType(type))
                .WithTheme(GetThemeByType(type))
                .Build();
            
            selector.Load(data, preselectedAssets);

            if (!canSelectEmpty) return;
            AssetCardControllerV2 emptyCard = Instantiate(emptyCardPrefab, selector.Content);
            emptyCard.transform.SetAsFirstSibling();
            emptyCard.Construct(new AssetCardData.Builder().WithIndex(-1).Build());
        }
        
        public override void ConfirmSelection()
        {
            if (selectedAssetIndexes == null || selectedAssetIndexes.Count == 0) return;
            IList<IAsset> assets = data.GetAssetList();
            ISet<IAsset> selectedAssets = selectedAssetIndexes.Where(i => i != -1).Select(i => assets[i]).ToHashSet();
            whenConfirmed?.Invoke(selectedAssets);
            whenConfirmed = null;
        }
    }
}