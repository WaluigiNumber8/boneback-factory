using System.Collections.Generic;
using Rogium.Editors.Core;
using Rogium.UserInterface.Interactables;
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
        [SerializeField] private InteractableButton assetCreateButtonPrefab;

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
        /// <param name="preselectedAssets">Assets that will selected at the start.</param>
        public void Load(SelectionMenuData data, ISet<IAsset> preselectedAssets = null) => Load(data, data.GetAssetList.Invoke(), preselectedAssets);
        /// <summary>
        /// Loads up asset cards into the content.
        /// </summary>
        /// <param name="data">Data for loading.</param>
        /// <param name="assets">All assets for which to load cards for.</param>
        /// <param name="preselectedAssets">Assets that will selected at the start.</param>
        public void Load(SelectionMenuData data, IList<IAsset> assets, ISet<IAsset> preselectedAssets = null)
        {
            PrepareAddButton(data);
            
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
                card.SetToggle(preselectedAssets?.Contains(asset) ?? false);
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

        private void PrepareAddButton(SelectionMenuData data)
        {
            if (data.WhenAssetCreate == ButtonType.None) return;
            if (assetCreateButtonPrefab == null) return;
            
            InteractableButton addButton = (content.childCount > 0 && content.GetChild(0).TryGetComponent(out InteractableButton button)) ? button : Instantiate(assetCreateButtonPrefab, content);
            addButton.Action = data.WhenAssetCreate;
        }

        public RectTransform Content { get => content; }
    }
}
