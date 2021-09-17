using BoubakProductions.Safety;
using System.Collections.Generic;
using UnityEngine;

namespace BoubakProductions.UI
{
    /// <summary>
    /// A new type of a UI Layout group, the Tab, allows for swithcing on and off different screens.
    /// </summary>
    public class TabGroup : MonoBehaviour
    {
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
            if (buttons == null)
                buttons = new List<TabPageButton>();
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
            HideAllPagesExcept(button);
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
        /// Disables all gameobjects in the list except the one with the button.
        /// </summary>
        /// <param name="button"></param>
        private void HideAllPagesExcept(TabPageButton button)
        {
            foreach (TabPageButton tabButton in buttons)
            {
                tabButton.Page.SetActive(false);
            }
            button.Page.SetActive(true);
        }

    }
}

