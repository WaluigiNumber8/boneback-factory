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
        [SerializeField] private Button button;

        private InteractableButton interactableButton;
        
        private void Start()
        {
            interactableButton = GetComponent<InteractableButton>();
            button = GetComponent<Button>();
        }

        private void OnEnable()
        {
            button.onClick.AddListener(SendSignal);
        }

        private void OnDisable()
        {
            button.onClick.RemoveListener(SendSignal);
        }

        private void SendSignal()
        {
            interactableButton.Number = assetController.ID;
        }
    }
}