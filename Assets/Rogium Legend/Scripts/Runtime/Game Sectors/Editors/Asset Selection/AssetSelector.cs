using System;
using System.Collections.Generic;
using RedRats.Safety;
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
        public event Action OnSelectNone;
        
        [SerializeField] private string title;
        [SerializeField] private RectTransform content;
        [SerializeField] private AssetCardControllerV2 cardPrefab;
        [SerializeField] private InteractableButton assetCreateButtonPrefab;

        private List<AssetCardControllerV2> cards;
        private Stack<AssetCardControllerV2> disabledCards;
        private int lastSelectedIndex = -1;

        private void Awake()
        {
            cards = new List<AssetCardControllerV2>();
            disabledCards = new Stack<AssetCardControllerV2>();
        }

        private void OnEnable() => AssetCardControllerV2.OnSelect += TrackSelected;
        private void OnDisable() => AssetCardControllerV2.OnSelect -= TrackSelected;


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
                AssetCardControllerV2 card = GenerateCard(i);
                card.gameObject.SetActive(true);
                card.Construct(new AssetCardData.Builder()
                    .WithIndex(i)
                    .WithTitle(asset.Title)
                    .WithIcon(asset.Icon)
                    .WithEditButton(data.WhenAssetEdit)
                    .WithConfigButton(data.WhenAssetConfig)
                    .WithDeleteButton(data.WhenAssetDelete)
                    .Build());
                if (preselectedAssets?.Contains(asset) == true)
                {
                    card.SetToggle(true);
                    OnSelectCard?.Invoke(i);
                }
                ThemeUpdaterRogium.UpdateAssetCard(card);
            }
            
            //Select the last card if no preselected assets and only if one was selected before
            if (preselectedAssets != null && preselectedAssets.Count != 0) return;
            if (lastSelectedIndex != -1) OnSelectCard?.Invoke(lastSelectedIndex);
            else OnSelectNone?.Invoke();
        }

        public AssetCardControllerV2 GetCard(int index)
        {
            SafetyNet.EnsureIndexWithingCollectionRange(index, cards, nameof(cards));
            return cards[index];
        }
        
        private AssetCardControllerV2 GenerateCard(int assetIndex)
        {
            if (assetIndex < cards.Count) return cards[assetIndex];
            
            AssetCardControllerV2 card = (disabledCards.Count > 0) ? disabledCards.Pop() : Instantiate(cardPrefab, content);
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

        private void TrackSelected(int index) => lastSelectedIndex = index;

        public RectTransform Content { get => content; }
    }
}
