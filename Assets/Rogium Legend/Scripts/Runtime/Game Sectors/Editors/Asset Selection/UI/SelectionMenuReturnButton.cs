using Rogium.Core;
using Rogium.Systems.GASExtension;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Rogium.Editors.AssetSelection.UI
{
    /// <summary>
    /// Changes action of the return button in the Selection Menu based on currently open asset type.
    /// </summary>
    [RequireComponent(typeof(Button))]
    public class SelectionMenuReturnButton : MonoBehaviour
    {
        private Button button;
        private SelectionMenuOverseerMono selectionMenu;
        
        private void Awake()
        {
            button = GetComponent<Button>();
            selectionMenu = SelectionMenuOverseerMono.GetInstance();
        }
        
        private void OnEnable() => selectionMenu.OnOpen += AdjustAction;
        private void OnDisable() => selectionMenu.OnOpen -= AdjustAction;

        /// <summary>
        /// Trigger the return action.
        /// </summary>
        public void Click() => button.onClick.Invoke();
        
        private void AdjustAction(AssetType type)
        {
            button.onClick.RemoveAllListeners();
            UnityAction whenClick = (type == AssetType.Pack) ? GASActions.ReturnToMainMenuSelection : () => selectionMenu.Open(AssetType.Pack);
            button.onClick.AddListener(whenClick);
        }
    }
}