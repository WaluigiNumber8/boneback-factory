using System;
using System.Collections.Generic;
using System.Linq;
using RedRats.Core;
using Rogium.Systems.Input;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;
using static Rogium.Systems.Input.InputSystemUtils;

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
            if (shortcuts == null || shortcuts.Length == 0) return;
            foreach (ShortcutData shortcut in shortcuts)
            {
                shortcut.RefreshInput();
            }
        }

        private void OnEnable()
        {
            FindAndDisableProfiles();
            LinkAll();
        }

        private void OnDisable()
        {
            EnableLastProfiles();
            UnlinkAll();
        }

        public void Set(ShortcutData[] newShortcuts)
        {
            UnlinkAll();
            shortcuts = newShortcuts.AsCopy();
            LinkAll();
        }

        public void SetAsOverrideAll(bool value)
        {
            overrideAll = value;
            if (!gameObject.activeSelf) return;
            FindAndDisableProfiles();
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

        private void FindAndDisableProfiles()
        {
            if (!overrideAll) return;
            lastProfiles = FindObjectsByType<ShortcutProfile>(FindObjectsInactive.Exclude, FindObjectsSortMode.None).Where(p => p != this).ToList();
            lastProfiles.ForEach(profile => profile.enabled = false);
        }

        private void EnableLastProfiles()
        {
            if (!overrideAll) return;
            if (lastProfiles == null) return;
            lastProfiles.ForEach(profile =>
            {
                if (profile != null) profile.enabled = true;
            });
            lastProfiles = null;
        }

        [Serializable]
        public class ShortcutData
        {
            private string GroupTitle() => trigger.ToString();
            [FoldoutGroup("$GroupTitle")] public ShortcutType trigger;
            [FoldoutGroup("$GroupTitle")] public UnityEvent action;

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
        }
    }
}