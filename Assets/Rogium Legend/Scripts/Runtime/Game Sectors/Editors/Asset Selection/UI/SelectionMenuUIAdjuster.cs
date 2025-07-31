using Rogium.Core;
using UnityEngine;

namespace Rogium.Editors.AssetSelection.UI
{
    /// <summary>
    /// Adjusts the UI of the Selection Menu for different types of assets.
    /// </summary>
    public class SelectionMenuUIAdjuster : MonoBehaviour
    {
        [SerializeField] private PackBanner packBanner;
        [SerializeField] private GameObject categoryTabsHolder;
        [SerializeField] private GameObject packScrollRect;
        [SerializeField] private GameObject otherAssetScrollRects;
        
        private SelectionMenuOverseerMono selectionMenu;

        private void Awake() => selectionMenu = SelectionMenuOverseerMono.Instance;
        private void OnEnable() => selectionMenu.OnOpen += AdjustUI;
        private void OnDisable() => selectionMenu.OnOpen -= AdjustUI;

        private void AdjustUI(AssetType type)
        {
            packBanner.gameObject.SetActive(type != AssetType.Pack);
            if (type != AssetType.Pack) packBanner.RefreshWithCurrentPack();
            categoryTabsHolder.SetActive(type != AssetType.Pack);
            otherAssetScrollRects.SetActive(type != AssetType.Pack);
            packScrollRect.SetActive(type == AssetType.Pack);
        }

        public bool PackBannerActive => packBanner.gameObject.activeSelf;
        public bool CategoryTabsActive => categoryTabsHolder.activeSelf;
        public bool OtherAssetScrollRectsActive => otherAssetScrollRects.activeSelf;
        public bool PackScrollRectActive => packScrollRect.activeSelf;
    }
}