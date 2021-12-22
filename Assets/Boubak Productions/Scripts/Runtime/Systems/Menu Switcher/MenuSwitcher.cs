using BoubakProductions.Core;
using BoubakProductions.Safety;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace BoubakProductions.UI.MenuSwitching
{

    /// <summary>
    /// Handles switching between different menus.
    /// </summary>
    public class MenuSwitcher : MonoSingleton<MenuSwitcher>, IMenuSwitcher
    {
        [SerializeField] private MenuType defaultMenu = MenuType.MainMenu;

        private List<MenuObject> menus = new List<MenuObject>();
        private MenuObject lastOpenMenu;

        private void Start()
        {
            base.Awake();
            menus = GatherMenus();
            SwitchTo(defaultMenu);
        }

        /// <summary>
        /// Search this objects children for all Canvas Menu Objects and return them as a list.
        /// </summary>
        /// <returns>List of menu objects.</returns>
        private List<MenuObject> GatherMenus()
        {
            List<MenuObject> gatheredMenus = GetComponentsInChildren<MenuObject>(true).ToList();
            int duplicatesCount = gatheredMenus.GroupBy(x => x.MenuType)
                                               .Where(_ => _.Count() > 1)
                                               .Sum(_ => _.Count());
            if (duplicatesCount > 0) throw new System.ArgumentException("List of gathered menus has found some duplicates! Only one MenuType per object is allowed.");
            return gatheredMenus;
        }

        public void SwitchTo(MenuType newMenu)
        {
            if (menus == null) menus = new List<MenuObject>();
            if (menus.Count <= 0) menus = GatherMenus();

            //Disable all canvases.
            if (lastOpenMenu == null)
            {
                menus.ForEach(canvas => canvas.gameObject.SetActive(false));
            }
            else lastOpenMenu.gameObject.SetActive(false);

            //Show wanted canvas.
            MenuObject menuToShow = menus.Find(canvas => canvas.MenuType == newMenu);
            if (menuToShow != null)
            {
                menuToShow.gameObject.SetActive(true);
                lastOpenMenu = menuToShow;
            }
        }

        public int GetAmountOfMenus => menus.Count();
    }
}