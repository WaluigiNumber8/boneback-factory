using RogiumLegend.Global.MenuSystem.UI;
using System.Collections;
using UnityEngine;

namespace RogiumLegend.Global.MenuSystem.Interactables
{
    /// <summary>
    /// Takes an ID from <see cref="AssetCardController"/> and sends it to an <see cref="InteractableButton"/>.
    /// </summary>
    [RequireComponent(typeof(InteractableButton))]
    public class InteractableButtonIDSender : MonoBehaviour
    {
        [SerializeField] private AssetCardController assetController;

        private void Awake()
        {
            GetComponent<InteractableButton>().Number = assetController.ID;
        }
    }
}