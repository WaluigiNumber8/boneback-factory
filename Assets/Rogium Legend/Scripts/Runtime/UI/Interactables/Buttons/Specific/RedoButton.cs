using Rogium.Systems.ActionHistory;
using UnityEngine;
using UnityEngine.UI;

namespace Rogium.UserInterface.Interactables.Buttons
{
    /// <summary>
    /// Represents the redo button. Disables/Enables it based on <see cref="ActionHistorySystem"/>.
    /// </summary>
    [RequireComponent(typeof(Button))]
    public class RedoButton : MonoBehaviour
    {
        private Button button;
        private void Awake() => button = GetComponent<Button>();
        private void OnEnable() => ActionHistorySystem.OnUpdateRedoHistory += UpdateInteractableStatus;
        private void OnDisable() => ActionHistorySystem.OnUpdateRedoHistory -= UpdateInteractableStatus;

        /// <summary>
        /// Updates interactable status based on redo history count.
        /// </summary>
        private void UpdateInteractableStatus() => button.interactable = ActionHistorySystem.RedoCount > 0;
    }
}