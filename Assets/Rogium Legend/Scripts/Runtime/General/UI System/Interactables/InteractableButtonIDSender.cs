using Rogium.Global.UISystem.UI;
using System.Collections;
using UnityEngine;

namespace Rogium.Global.UISystem.Interactables
{
    /// <summary>
    /// Takes an ID from <see cref="AssetCardController"/> and sends it to an <see cref="InteractableButton"/>.
    /// </summary>
    [RequireComponent(typeof(InteractableButton))]
    public class InteractableButtonIDSender : MonoBehaviour
    {
        [SerializeField] private AssetCardController assetController;

        private void OnEnable()
        {
            GetComponent<InteractableButton>().Number = assetController.ID;
        }
    }
}