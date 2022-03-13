using BoubakProductions.Safety;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace BoubakProductions.UI
{
    /// <summary>
    /// A new type of a UI Layout group, the Tab, allows for switching on and off different screens.
    /// </summary>
    public class TabGroup : MonoBehaviour
    {
        public event Action<GameObject> onTabSwitch;
        
        [SerializeField] private int defaultTabIndex;
        [SerializeField] private Sprite tabIdle;
        [SerializeField] private Sprite tabHover;
        [SerializeField] private Sprite tabActive;

        private List<TabPageButton> buttons;
        private TabPageButton selectedTab;

        private void Start()
        {
            SafetyNet.EnsureIntIsInRange(defaultTabIndex, 0, buttons.Count, "Default Tab Index");
            OnTabSelect(buttons[defaultTabIndex]);
        }

        /// <summary>
        /// Register TabButton as part of this TabGroup.
        /// </summary>
        /// <param name="button">The button to register.</param>
        public void Subscribe(TabPageButton button)
        {
            buttons ??= new List<TabPageButton>();
            buttons.Add(button);
        }

        public void OnTabEnter(TabPageButton button)
        {
            ResetAllTabs();
            if (selectedTab == null || selectedTab != button)
                button.Background.sprite = tabHover;
        }

        public void OnTabExit(TabPageButton button)
        {
            ResetAllTabs();
        }

        public void OnTabSelect(TabPageButton button)
        {
            selectedTab = button;
            ResetAllTabs();
            button.Background.sprite = tabActive;
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
                button.Background.sprite = tabIdle;
            }
        }

        /// <summary>
        /// Get all pages from tab group as an array of GameObjects.
        /// </summary>
        /// <returns>An array of pages.</returns>
        public GameObject[] GetButtonsAsArray()
        {
            return buttons.Select(button => button.Page)
                          .ToArray();
        }
    }
}

