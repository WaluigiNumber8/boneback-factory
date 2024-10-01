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
        [SerializeField] private string title;
        [SerializeField] private RectTransform content;
        [SerializeField] private AssetCardControllerV2 cardPrefab;

        private IList<AssetCardControllerV2> cards;
        private Stack<AssetCardControllerV2> disabledCards;

        private void Awake()
        {
            cards = new List<AssetCardControllerV2>();
            disabledCards = new Stack<AssetCardControllerV2>();
        }


        /// <summary>
        /// Loads up asset cards.
        /// </summary>
        /// <param name="data">Data for the card.</param>
        public void Load(SelectionMenuData data) => Load(data, data.GetAssetList.Invoke());

        /// <summary>
        /// Pools asset cards into the content.
        /// </summary>
        /// <param name="data">Data for loading.</param>
        /// <param name="assets">All assets for which to load cards for.</param>
        public void Load(SelectionMenuData data, IList<IAsset> assets)
        {
            //Foreach card, check if it's in the list on the position
            for (int i = 0; i < assets.Count; i++)
            {
                IAsset asset = assets[i];
                AssetCardControllerV2 card = (i < cards.Count) ? cards[i] : ((disabledCards.Count > 0) ? disabledCards.Pop() : Instantiate(cardPrefab, content));
                card.gameObject.SetActive(true);
                card.Construct(new AssetCardData.Builder()
                    .WithIndex(i)
                    .WithTitle(asset.Title)
                    .WithIcon(asset.Icon)
                    .WithEditButton(data.WhenAssetEdit)
                    .WithConfigButton(data.WhenAssetConfig)
                    .WithDeleteButton(data.WhenAssetDelete)
                    .Build());
                if (i >= cards.Count) cards.Add(card);
            }
            
            if (assets.Count >= cards.Count) return;
            for (int i = assets.Count; i < cards.Count; i++)
            {
                cards[i].gameObject.SetActive(false);
                disabledCards.Push(cards[i]);
                cards.RemoveAt(i);
            }
        }
        
        public RectTransform Content { get => content; }
    }
}
