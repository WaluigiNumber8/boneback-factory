using System;
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
        [SerializeField] private RoomEditorShortcutsData roomEditor;
        
        private InputSystem input;

        private void Awake() => input = InputSystem.GetInstance();

        public void ActivateRoomShortcuts()
        {
            DeactivateAllShortcuts();
            input.ShortcutsGeneral.Undo.OnPress += roomEditor.undo.Invoke;
            input.ShortcutsDrawingEditors.SelectTool.OnPress += roomEditor.selectTool.Invoke;
        }
        
        private void DeactivateAllShortcuts()
        {
            input.ShortcutsGeneral.Undo.OnPress -= roomEditor.undo.Invoke;
            input.ShortcutsDrawingEditors.SelectTool.OnPress -= roomEditor.selectTool.Invoke;
        }

        [Serializable]
        public struct RoomEditorShortcutsData
        {
            public UnityEvent undo;
            public UnityEvent selectTool;
        }
    }
}