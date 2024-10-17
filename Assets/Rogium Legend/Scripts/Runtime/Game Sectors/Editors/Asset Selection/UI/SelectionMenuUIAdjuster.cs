using Rogium.Core;
using UnityEngine;

namespace Rogium.Editors.NewAssetSelection
{
    /// <summary>
    /// Adjusts the UI of the Selection Menu for different types of assets.
    /// </summary>
    public class SelectionMenuUIAdjuster : MonoBehaviour
    {
        [SerializeField] private GameObject packBanner;
        [SerializeField] private GameObject categoryTabsHolder;
        [SerializeField] private GameObject packScrollRect;
        [SerializeField] private GameObject otherAssetScrollRects;
        
        private SelectionMenuOverseerMono selectionMenu;

        private void Awake() => selectionMenu = SelectionMenuOverseerMono.GetInstance();
        private void OnEnable() => selectionMenu.OnOpen += AdjustUI;
        private void OnDisable() => selectionMenu.OnOpen -= AdjustUI;

        private void AdjustUI(AssetType type)
        {
            packBanner.SetActive(type != AssetType.Pack);
            categoryTabsHolder.SetActive(type != AssetType.Pack);
            otherAssetScrollRects.SetActive(type != AssetType.Pack);
            packScrollRect.SetActive(type == AssetType.Pack);
        }

        public bool PackBannerActive => packBanner.activeSelf;
        public bool CategoryTabsActive => categoryTabsHolder.activeSelf;
        public bool OtherAssetScrollRectsActive => otherAssetScrollRects.activeSelf;
        public bool PackScrollRectActive => packScrollRect.activeSelf;
    }
}