using System;
using RedRats.Core;
using RedRats.Systems.ObjectSwitching;
using Rogium.Gameplay.Core;
using Rogium.Systems.Input;
using UnityEngine;
using UnityEngine.UI;

namespace Rogium.Gameplay.Inventory
{
    /// <summary>
    /// Represents the Weapon Select Menu where the player decides into which slot the newly picked up weapon goes.
    /// </summary>
    public class WeaponSelectMenu : MonoSingleton<WeaponSelectMenu>
    {
        public event Action OnOpen;
        public event Action OnClose;
        
        [SerializeField] private Transform snapTransform;
        [SerializeField] private ObjectSwitcherMono layoutSwitcher;
        [SerializeField, Range(0f, 4f)] private float inputStartDelay = 1;
        [SerializeField] private UIInfo ui;

        private Action<int> whenSelectedWeapon;

        protected override void Awake()
        {
            ui.ui.SetActive(false);
            Hide();
        }

        /// <summary>
        /// Opens the Weapon Select Menu with the normal layout..
        /// </summary>
        /// <param name="whenSelectedWeapon">The method to run when a slot is selected.</param>
        public void OpenForNormal(Action<int> whenSelectedWeapon)
        {
            this.whenSelectedWeapon = whenSelectedWeapon;
            layoutSwitcher.Switch(ui.normalLayout);
            ui.firstSelectedNormal.Select();
            Show();
        }
        
        /// <summary>
        /// Opens the Weapon Select Menu with the Dash layout.
        /// </summary>
        /// <param name="selectedMethod">The method to run when a slot is selected.</param>
        public void OpenForDash(Action<int> selectedMethod)
        {
            whenSelectedWeapon = selectedMethod;
            layoutSwitcher.Switch(ui.dashLayout);
            ui.firstSelectedDash.Select();
            Show();
        }

        /// <summary>
        /// Select a slot in the menu.
        /// </summary>
        /// <param name="index">The index pf the slot to select.</param>
        public void Select(int index)
        {
            whenSelectedWeapon.Invoke(index);
            Hide();
        }
        
        /// <summary>
        /// Show the Menu.
        /// </summary>
        private void Show()
        {
            ui.parent.position = snapTransform.position;
            ui.ui.SetActive(true);
            GameplayOverseerMono.GetInstance().EnableUI();
            InputSystem.GetInstance().DisableInput(this, inputStartDelay);
            
            OnOpen?.Invoke();
        }

        /// <summary>
        /// Hide the Menu.
        /// </summary>
        private void Hide()
        {
            ui.ui.SetActive(false);
            GameplayOverseerMono.GetInstance().DisableUI();
            
            OnClose?.Invoke();
        }

        [Serializable]
        private struct UIInfo
        {
            public Transform parent;
            public GameObject ui;
            public GameObject normalLayout;
            public Selectable firstSelectedNormal;
            public GameObject dashLayout;
            public Selectable firstSelectedDash;
        }
    }
}