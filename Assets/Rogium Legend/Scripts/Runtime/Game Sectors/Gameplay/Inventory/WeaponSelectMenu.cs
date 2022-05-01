using System;
using BoubakProductions.Core;
using BoubakProductions.Systems.ObjectSwitching;
using UnityEngine;

namespace Rogium.Gameplay.Inventory
{
    /// <summary>
    /// Controls the Weapon Select Menu.
    /// </summary>
    public class WeaponSelectMenu : MonoSingleton<WeaponSelectMenu>
    {
        [SerializeField] private Transform snapTransform;
        [SerializeField] private ObjectSwitcherMono layoutSwitcher;
        [SerializeField] private UIInfo ui;

        private Transform ttransform;
        private Action<int> methodToRun;

        protected override void Awake()
        {
            base.Awake();
            ttransform = transform;
            Hide();
        }

        /// <summary>
        /// Opens the Weapon Select Menu with the normal layout..
        /// </summary>
        /// <param name="selectedMethod">The method to run when a slot is selected.</param>
        public void OpenForNormal(Action<int> selectedMethod)
        {
            methodToRun = selectedMethod;
            layoutSwitcher.Switch(ui.normalLayout);
            Show();
        }
        
        /// <summary>
        /// Opens the Weapon Select Menu with the Dash layout.
        /// </summary>
        /// <param name="selectedMethod">The method to run when a slot is selected.</param>
        public void OpenForDash(Action<int> selectedMethod)
        {
            methodToRun = selectedMethod;
            layoutSwitcher.Switch(ui.dashLayout);
            Show();
        }

        /// <summary>
        /// Select a slot in the menu.
        /// </summary>
        /// <param name="index">The index pf the slot to select.</param>
        public void Select(int index)
        {
            methodToRun.Invoke(index);
            Hide();
        }
        
        /// <summary>
        /// Show the Menu.
        /// </summary>
        private void Show()
        {
            ttransform.position = snapTransform.position;
            ui.ui.SetActive(true);
        }

        /// <summary>
        /// Hide the Menu.
        /// </summary>
        private void Hide()
        {
            ui.ui.SetActive(false);
        }

        [System.Serializable]
        private struct UIInfo
        {
            public Transform parent;
            public GameObject ui;
            public GameObject normalLayout;
            public GameObject dashLayout;
        }
    }
}