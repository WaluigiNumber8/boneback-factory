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
        /// Pools asset cards into the content.
        /// </summary>
        /// <param name="assets">The list of assets to load.</param>
        /// <param name="data">Data for the card.</param>
        public void Load(IList<IAsset> assets, AssetCardData data)
        {
            foreach (IAsset asset in assets)
            {
                AssetCardControllerV2 card = Instantiate(cardPrefab, content);
                card.Construct(asset, data);
            }   
        }
    }
}