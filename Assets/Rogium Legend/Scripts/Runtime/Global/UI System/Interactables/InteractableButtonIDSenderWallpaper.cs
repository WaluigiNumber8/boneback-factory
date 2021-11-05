using Rogium.Global.UISystem.AssetSelection;
using UnityEngine;
using UnityEngine.UI;

namespace Rogium.Global.UISystem.Interactables
{
    /// <summary>
    /// Takes an ID from <see cref="AssetWallpaperController"/> and sends it to an <see cref="InteractableButton"/>.
    /// </summary>
    [RequireComponent(typeof(InteractableButton))]
    public class InteractableButtonIDSenderWallpaper : MonoBehaviour
    {
        [SerializeField] private AssetWallpaperController assetController;

        private InteractableButton interactableButton;
        
        private void Start()
        {
            interactableButton = GetComponent<InteractableButton>();
        }

        private void OnEnable()
        {
            assetController.OnConstruct += UpdateSignal;
        }

        private void OnDisable()
        {
            assetController.OnConstruct -= UpdateSignal;
        }

        private void UpdateSignal(int newValue)
        {
            interactableButton.Number = newValue;
        }
    }
}