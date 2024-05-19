using Rogium.Systems.ActionHistory;
using UnityEngine;
using UnityEngine.UI;

namespace Rogium.UserInterface.Interactables.Buttons
{
    /// <summary>
    /// Represents the undo button. Disables/Enables it based on <see cref="ActionHistorySystem"/>.
    /// </summary>
    [RequireComponent(typeof(Button))]
    public class UndoButton : MonoBehaviour
    {
        private Button button;

        private void Awake() => button = GetComponent<Button>();
        private void OnEnable() => ActionHistorySystem.OnUpdateUndoHistory += UpdateInteractableStatus;
        private void OnDisable() => ActionHistorySystem.OnUpdateUndoHistory -= UpdateInteractableStatus;

        /// <summary>
        /// Updates interactable status based on undo history count.
        /// </summary>
        private void UpdateInteractableStatus() => button.interactable = ActionHistorySystem.UndoCount > 0;
    }
}