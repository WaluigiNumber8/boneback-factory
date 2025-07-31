using System;
using System.Collections.Generic;
using System.Linq;
using RedRats.Core;
using RedRats.Safety;
using UnityEngine;

namespace RedRats.UI.MenuSwitching
{

    /// <summary>
    /// Handles switching between different menus.
    /// </summary>
    public class MenuSwitcher : MonoSingleton<MenuSwitcher>, IMenuSwitcher
    {
        public event Action<MenuType> OnSwitch;
        
        [SerializeField] private MenuType defaultMenu = MenuType.MainMenu;

        private List<MenuObject> menus = new();
        
        private MenuType currentMenu;
        private MenuObject lastOpenMenu;

        private void Start()
        {
            base.Awake();
            ReloadMenus();
            SwitchTo(defaultMenu);
        }

        /// <summary>
        /// Search this objects children for all Canvas Menu Objects and return them as a list.
        /// </summary>
        /// <returns>List of menu objects.</returns>
        public void ReloadMenus()
        {
            List<MenuObject> gatheredMenus = GetComponentsInChildren<MenuObject>(true).ToList();
            Preconditions.isListWithoutDuplicates(gatheredMenus, nameof(gatheredMenus));
            menus = gatheredMenus;
        }

        public void SwitchTo(MenuType newMenu)
        {
            if (menus == null) menus = new List<MenuObject>();
            if (menus.Count <= 0) ReloadMenus();

            //Disable all canvases.
            if (lastOpenMenu == null)
            {
                menus.ForEach(canvasObject => canvasObject.gameObject.SetActive(false));
            }
            else lastOpenMenu.gameObject.SetActive(false);

            //Show wanted canvas.
            MenuObject menuToShow = menus.Find(canvasObject => canvasObject.MenuType == newMenu);
            
            if (menuToShow == null) return;
            menuToShow.gameObject.SetActive(true);
            lastOpenMenu = menuToShow;
            currentMenu = newMenu;
            OnSwitch?.Invoke(currentMenu);
        }

        public MenuType CurrentMenu { get => currentMenu; }
    }
}