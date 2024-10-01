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
        
        private SelectionMenuOverseerMono selectionMenu;

        private void Awake() => selectionMenu = SelectionMenuOverseerMono.GetInstance();
        private void OnEnable() => selectionMenu.OnOpen += AdjustUI;
        private void OnDisable() => selectionMenu.OnOpen -= AdjustUI;

        private void AdjustUI(AssetType type)
        {
            packBanner.SetActive(type != AssetType.Pack);
        }

        public bool PackBannerActive => packBanner.activeSelf;
    }
}