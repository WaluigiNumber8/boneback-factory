using Rogium.UserInterface.AssetSelection;
using UnityEngine;

namespace Rogium.UserInterface.Interactables
{
    /// <summary>
    /// Takes an ID from <see cref="AssetCardController"/> and sends it to an <see cref="InteractableButton"/>.
    /// </summary>
    [RequireComponent(typeof(InteractableButton))]
    public class InteractableButtonIDSenderCard : MonoBehaviour
    {
        [SerializeField] private AssetCardController assetController;

        private void OnEnable()
        {
            GetComponent<InteractableButton>().Index = assetController.ID;
        }
    }
}