using System.Collections;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.UI;

namespace RogiumLegend.Global.MenuSystem.Interactables
{
    /// <summary>
    /// Handles input from the button component via the GAS System.
    /// </summary>
    [RequireComponent(typeof(Button))]
    public class InteractableButton : MonoBehaviour, IInteractableButton
    {
        [SerializeField] private ButtonType action;
        private Button button;

        private void Start()
        {
            button = GetComponent<Button>();
            button.onClick.AddListener(OnButtonClicked);
        }

        public void OnButtonClicked()
        {
            switch (action)
            {
                case ButtonType.DoNothing:
                    Debug.LogError("This button Currently does nothing...");
                    break;
                case ButtonType.OpenPackSelection:
                    GASButtonActions.OpenPackSelection();
                    break;
                case ButtonType.ReturnToMainMenu:
                    GASButtonActions.ReturnToMainMenu();
                    break;
                default:
                    throw new InvalidEnumArgumentException("Uknown Button Type.");
            }
        }

    }
}