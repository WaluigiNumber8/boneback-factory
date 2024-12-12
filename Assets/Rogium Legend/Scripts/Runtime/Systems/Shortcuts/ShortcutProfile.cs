using System;
using System.Collections.Generic;
using System.Linq;
using RedRats.Core;
using Rogium.Systems.Input;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

namespace Rogium.Core.Shortcuts
{
    /// <summary>
    /// Stores a set of actions that can be triggered via button shortcuts.
    /// </summary>
    public class ShortcutProfile : MonoBehaviour
    {
        [SerializeField] private ShortcutData[] shortcuts;
        [SerializeField] private bool overrideAll;

        private List<ShortcutProfile> lastProfiles;
        

        private void Awake()
        {
            foreach (ShortcutData shortcut in shortcuts)
            {
                shortcut.RefreshInput();
            }
        }

        private void OnEnable()
        {
            if (overrideAll)
            {
                lastProfiles = FindObjectsByType<ShortcutProfile>(FindObjectsInactive.Exclude, FindObjectsSortMode.None).Where(p => p != this).ToList();
                lastProfiles.ForEach(profile => profile.enabled = false);
            }
            LinkAll();
        }

        private void OnDisable()
        {
            if (overrideAll)
            {
                lastProfiles.ForEach(profile => profile.enabled = true);
                lastProfiles = null;
            }
            UnlinkAll();
        }

        public void Set(ShortcutData[] newShortcuts)
        {
            UnlinkAll();
            shortcuts = newShortcuts.AsCopy();
            LinkAll();
        }
        
        private void LinkAll()
        {
            if (shortcuts == null || shortcuts.Length == 0) return;
            foreach (ShortcutData shortcut in shortcuts)
            {
                shortcut.Link();
            }
        }

        private void UnlinkAll()
        {
            if (shortcuts == null || shortcuts.Length == 0) return;
            foreach (ShortcutData shortcut in shortcuts)
            {
                shortcut.Unlink();
            }
        }

        [Serializable]
        public class ShortcutData
        {
            private string GroupTitle() => trigger.ToString();
            [FoldoutGroup("$GroupTitle")]
            public ShortcutType trigger;
            [FoldoutGroup("$GroupTitle")]
            public UnityEvent action;
            
            private InputButton input;
            public ShortcutData(ShortcutType trigger, UnityEvent action)
            {
                this.trigger = trigger;
                this.action = action;
                this.input = GetInput(trigger);
            }

            public void RefreshInput() => input = GetInput(trigger);

            public void Link() => input.OnPress += action.Invoke;
            public void Unlink() => input.OnPress -= action.Invoke;

            private static InputButton GetInput(ShortcutType shortcut)
            {
                InputSystem input = InputSystem.GetInstance();
                InputButton shortcutEvent = shortcut switch
                {
                    ShortcutType.Undo => input.Shortcuts.Undo,
                    ShortcutType.Redo => input.Shortcuts.Redo,
                    ShortcutType.Save => input.Shortcuts.Save,
                    ShortcutType.Cancel => input.Shortcuts.Cancel,
                    ShortcutType.SelectionTool => input.Shortcuts.SelectionTool,
                    ShortcutType.BrushTool => input.Shortcuts.BrushTool,
                    ShortcutType.EraserTool => input.Shortcuts.EraserTool,
                    ShortcutType.FillTool => input.Shortcuts.FillTool,
                    ShortcutType.PickerTool => input.Shortcuts.PickerTool,
                    ShortcutType.ClearCanvas => input.Shortcuts.ClearCanvas,
                    ShortcutType.ToggleGrid => input.Shortcuts.ToggleGrid,
                    ShortcutType.LayerTiles => input.Shortcuts.TilesLayer,
                    ShortcutType.LayerDecors => input.Shortcuts.DecorLayer,
                    ShortcutType.LayerObjects => input.Shortcuts.ObjectsLayer,
                    ShortcutType.LayerEnemies => input.Shortcuts.EnemiesLayer,
                    _ => throw new ArgumentOutOfRangeException($"Shortcut {shortcut} not implemented.")
                };
                return shortcutEvent;
            }
        }
    }
}