using System;
using RedRats.UI.MenuSwitching;
using Rogium.Systems.Input;
using UnityEngine;

namespace Rogium.Core.Shortcuts
{
    /// <summary>
    /// Switches shortcut input action maps based on active menus/UI elements.
    /// </summary>
    public class ShortcutProfileSwitcher : MonoBehaviour
    {
        [SerializeField] private ShortcutActionsLibrary shortcutLibrary;
        
        
        private InputSystem input;
        private MenuSwitcher menuSwitcher;

        private void Awake()
        {
            input = InputSystem.GetInstance();
            menuSwitcher = MenuSwitcher.GetInstance();
        }

        private void OnEnable() => menuSwitcher.OnSwitch += SwitchProfile;

        private void OnDisable() => menuSwitcher.OnSwitch -= SwitchProfile;

        private void SwitchProfile(MenuType newMenu)
        {
            input.DisableAllShortcuts();
            switch (newMenu)
            {
                case MenuType.MainMenu:
                    break;
                case MenuType.OptionsMenu:
                    break;
                case MenuType.AssetSelection:
                    break;
                case MenuType.AssetTypeSelection:
                    break;
                case MenuType.CampaignSelection:
                    break;
                case MenuType.CampaignEditor:
                    break;
                case MenuType.RoomEditor:
                    input.EnableShortcutsRoomMap();
                    shortcutLibrary.ActivateRoomShortcuts();
                    break;
                case MenuType.PaletteEditor:
                    break;
                case MenuType.SpriteEditor:
                    break;
                case MenuType.PropertyEditor:
                    break;
                case MenuType.Changelog:
                    break;
                default: throw new ArgumentOutOfRangeException(nameof(newMenu), newMenu, null);
            }
        }

    }
}