using Rogium.Editors.NewAssetSelection.Campaigns;
using UnityEngine;

namespace Rogium.UserInterface.Interactables
{
    /// <summary>
    /// Takes an ID from <see cref="AssetWallpaperController"/> and sends it to an <see cref="InteractableButton"/>.
    /// </summary>
    [RequireComponent(typeof(InteractableButton))]
    public class InteractableButtonIDSenderWallpaper : MonoBehaviour
    {
        [SerializeField] private InteractableButton interactableButton;
        [SerializeField] private AssetWallpaperController assetController;
        
        private void OnEnable() => assetController.OnConstruct += UpdateSignal;
        private void OnDisable() => assetController.OnConstruct -= UpdateSignal;

        private void UpdateSignal(int newValue) => interactableButton.Index = newValue;
    }
}