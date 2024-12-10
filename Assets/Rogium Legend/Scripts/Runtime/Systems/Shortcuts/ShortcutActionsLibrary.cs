using Rogium.Systems.Input;
using UnityEngine;
using UnityEngine.Events;

namespace Rogium.Core.Shortcuts
{
    /// <summary>
    /// Contains all shortcut actions
    /// </summary>
    public class ShortcutActionsLibrary : MonoBehaviour
    {
        [Header("Room Editor")] 
        [SerializeField] private UnityEvent roomUndo;

        private InputSystem input;

        private void Awake() => input = InputSystem.GetInstance();

        public void ActivateRoomShortcuts()
        {
            DeactivateAllShortcuts();
            input.ShortcutsGeneral.Undo.OnPress += () => roomUndo.Invoke();
        }
        
        private void DeactivateAllShortcuts()
        {
            input.ShortcutsGeneral.Undo.OnPress -= roomUndo.Invoke;
        }
    }
}