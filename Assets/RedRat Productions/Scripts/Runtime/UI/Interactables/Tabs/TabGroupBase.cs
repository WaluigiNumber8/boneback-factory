using System;
using System.Linq;
using RedRats.Safety;
using Sirenix.OdinInspector;
using UnityEngine;

namespace RedRats.UI.Tabs
{
    public abstract class TabGroupBase : MonoBehaviour
    {
        public event Action<GameObject> onTabSwitch;
        
        [SerializeField] private int defaultTabIndex;
        [BoxGroup]
        [SerializeField] private ButtonVisualInfo visuals;

        private TabPageButton[] pageButtons;
        private TabPageButton selectedTab;

        public int DefaultTabIndex { get => defaultTabIndex; }

        private void Awake() => pageButtons = GetButtons();
        
        private void Start()
        {
            SafetyNet.EnsureIntIsInRange(defaultTabIndex, 0, pageButtons.Length, "Default Tab Index");
            Switch(defaultTabIndex);
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
        /// <param name="button">The button to switch to.</param>
        public void Switch(TabPageButton button)
        {
            if (!pageButtons.Contains(button)) throw new ArgumentException($"{button} is not part of the Tab Group.");
            OnTabSelect(button);
        }
        
        /// <summary>
        /// Switches to a button with given index.
        /// </summary>
        /// <param name="index">The index of the button to switch.</param>
        public void Switch(int index)
        {
            SafetyNet.EnsureIndexWithingCollectionRange(index, pageButtons, "Tab Buttons");
            OnTabSelect(pageButtons[index]);
        }

        protected abstract TabPageButton[] GetButtons();
        
        /// <summary>
        /// Resets all tabs to their default state.
        /// </summary>
        private void ResetAllTabs()
        {
            foreach (TabPageButton button in pageButtons)
            {
                if (selectedTab != null && selectedTab == button) continue;
                button.Background.sprite = visuals.tabIdle;
            }
        }

        /// <summary>
        /// Get all pages from tab group as an array of GameObjects.
        /// </summary>
        /// <returns>An array of pages.</returns>
        public GameObject[] GetButtonsAsArray() => pageButtons.Select(button => button.Page).ToArray();

        [Serializable]
        protected struct ButtonVisualInfo
        {
            [PreviewField(60)]public Sprite tabIdle;
            [PreviewField(60)]public Sprite tabHover;
            [PreviewField(60)]public Sprite tabActive;
        }
    }
}