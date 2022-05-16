using System;
using System.Linq;
using RedRats.Safety;
using UnityEngine;

namespace RedRats.UI.Tabs
{
    /// <summary>
    /// A new type of a UI Layout group, the Tab, allows for switching on and off different screens.
    /// </summary>
    public class TabGroup : MonoBehaviour
    {
        public event Action<GameObject> onTabSwitch;
        
        [SerializeField] private int defaultTabIndex;
        [SerializeField] private ButtonVisualInfo visuals;
        [SerializeField] private TabPageButton[] buttons;

        private TabPageButton selectedTab;

        private void Start()
        {
            SafetyNet.EnsureIntIsInRange(defaultTabIndex, 0, buttons.Length, "Default Tab Index");
            OnTabSelect(buttons[defaultTabIndex]);
        }

        public void OnTabEnter(TabPageButton button)
        {
            ResetAllTabs();
            if (selectedTab == null || selectedTab != button)
                button.Background.sprite = visuals.tabHover;
        }

        public void OnTabExit(TabPageButton button)
        {
            ResetAllTabs();
        }

        public void OnTabSelect(TabPageButton button)
        {
            selectedTab = button;
            ResetAllTabs();
            button.Background.sprite = visuals.tabActive;
            onTabSwitch?.Invoke(button.Page);
        }

        /// <summary>
        /// Switches to a button with given index.
        /// </summary>
        /// <param name="index">The index of the button to switch.</param>
        public void Switch(int index)
        {
            SafetyNet.EnsureIndexWithingCollectionRange(index, buttons, "Tab Buttons");
            OnTabSelect(buttons[index]);
        }
        
        /// <summary>
        /// Resets all tabs to their default state.
        /// </summary>
        private void ResetAllTabs()
        {
            foreach (TabPageButton button in buttons)
            {
                if (selectedTab != null && selectedTab == button) continue;
                button.Background.sprite = visuals.tabIdle;
            }
        }

        /// <summary>
        /// Get all pages from tab group as an array of GameObjects.
        /// </summary>
        /// <returns>An array of pages.</returns>
        public GameObject[] GetButtonsAsArray() => buttons.Select(button => button.Page).ToArray();

        [Serializable]
        private struct ButtonVisualInfo
        {
            public Sprite tabIdle;
            public Sprite tabHover;
            public Sprite tabActive;
        }
    }
}

