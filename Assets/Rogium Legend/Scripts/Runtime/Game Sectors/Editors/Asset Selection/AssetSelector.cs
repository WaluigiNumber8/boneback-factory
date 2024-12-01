using System;
using System.Collections.Generic;
using RedRats.Safety;
using RedRats.Systems.Themes;
using Rogium.Editors.Core;
using Rogium.Systems.ThemeSystem;
using Rogium.UserInterface.Interactables;
using UnityEngine;

namespace Rogium.Editors.NewAssetSelection
{
    /// <summary>
    /// Controls a single instance of a selection menu.
    /// </summary>
    public class AssetSelector : MonoBehaviour
    {
        public event Action<int> OnSelectCard;
        public event Action<int> OnDeselectCard;
        public event Action OnSelectNone;
        
        [SerializeField] private string title;
        [SerializeField] private RectTransform content;
        [SerializeField] private AssetCardController cardPrefab;
        [SerializeField] private InteractableButton assetCreateButtonPrefab;

        private List<AssetCardController> cards;
        private Stack<AssetCardController> disabledCards;
        private int lastSelectedIndex = -1;

        private void Awake()
        {
            cards = new List<AssetCardController>();
            disabledCards = new Stack<AssetCardController>();
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
            int childCount = content.childCount - ((data.WhenAssetCreate == ButtonType.None) ? 0 : 1);
            
            //Disable cards that are not needed
            if (childCount > assets.Count)
            {
                for (int i = assets.Count; i < cards.Count; i++)
                {
                    cards[i].gameObject.SetActive(false);
                    disabledCards.Push(cards[i]);
                }
                cards.RemoveRange(assets.Count, cards.Count - assets.Count);
            }
            
            //Load up the cards with data
            for (int i = 0; i < assets.Count; i++)
            {
                IAsset asset = assets[i];
                AssetCardController card = GenerateCard(i);
                card.gameObject.SetActive(true);
                card.Construct(new AssetCardData.Builder()
                    .WithIndex(i)
                    .WithTitle(asset.Title)
                    .WithIcon(asset.Icon)
                    .WithEditButton(data.WhenAssetEdit)
                    .WithConfigButton(data.WhenAssetConfig)
                    .WithDeleteButton(data.WhenAssetDelete)
                    .Build());
                card.RemoveAllListeners();
                card.OnSelect += WhenCardSelect;
                card.OnDeselect += WhenCardDeselect;
                if (preselectedAssets?.Contains(asset) == true)
                {
                    card.SetToggle(true);
                    WhenCardSelect(i);
                }
                ThemeUpdaterRogium.UpdateAssetCard(card, data.CardTheme);
                continue;

                void WhenCardSelect(int index)
                {
                    data.WhenCardSelected?.Invoke(index);
                    OnSelectCard?.Invoke(index);
                    lastSelectedIndex = index;
                }
                void WhenCardDeselect(int index)
                {
                    data.WhenCardDeselected?.Invoke(index);
                    OnDeselectCard?.Invoke(index);
                    lastSelectedIndex = index;
                }
            }
            
            //Select the last card if no preselected assets and only if one was selected before
            if (preselectedAssets != null && preselectedAssets.Count != 0) return;
            if (lastSelectedIndex != -1 && lastSelectedIndex < assets.Count) OnSelectCard?.Invoke(lastSelectedIndex);
            else OnSelectNone?.Invoke();
        }

        public void TryRefreshCard(int index)
        {
            if (lastSelectedIndex != index) return;
            OnSelectCard?.Invoke(index);
        }
        
        public AssetCardController GetCard(int index)
        {
            SafetyNet.EnsureIndexWithingCollectionRange(index, cards, nameof(cards));
            return cards[index];
        }
        
        private AssetCardController GenerateCard(int assetIndex)
        {
            if (assetIndex < cards.Count) return cards[assetIndex];
            
            AssetCardController card = (disabledCards.Count > 0) ? disabledCards.Pop() : Instantiate(cardPrefab, content);
            cards.Add(card);
            return card;
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
