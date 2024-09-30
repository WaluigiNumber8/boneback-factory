using System.Collections.Generic;
using Rogium.Editors.Core;
using UnityEngine;

namespace Rogium.Editors.NewAssetSelection
{
    /// <summary>
    /// Controls a single instance of a selection menu.
    /// </summary>
    public class AssetSelector : MonoBehaviour
    {
        [SerializeField] private RectTransform content;
        [SerializeField] private AssetCardControllerV2 cardPrefab;
        
        public RectTransform Content { get => content; }

        /// <summary>
        /// Loads up asset cards.
        /// </summary>
        /// <param name="data">Data for the card.</param>
        public void Load(SelectionMenuData data) => Load(data, data.getAssetList.Invoke());

        /// <summary>
        /// Pools asset cards into the content.
        /// </summary>
        /// <param name="data">Data for loading.</param>
        /// <param name="assets">All assets for which to load cards for.</param>
        public void Load(SelectionMenuData data, IList<IAsset> assets)
        {
            foreach (IAsset asset in assets)
            {
                AssetCardControllerV2 card = Instantiate(cardPrefab, content);
                card.Construct(new AssetCardData.Builder()
                                .WithTitle(asset.Title)
                                .WithIcon(asset.Icon)
                                .WithEditButton(data.whenAssetEdit)
                                .WithConfigButton(data.whenAssetConfig)
                                .WithDeleteButton(data.whenAssetDelete)
                                .Build());
            }   
        }
    }
}