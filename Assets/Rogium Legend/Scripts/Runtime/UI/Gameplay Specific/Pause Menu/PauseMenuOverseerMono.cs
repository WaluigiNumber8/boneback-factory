using RedRats.Core;
using RedRats.Systems.ClockOfTheGame;
using RedRats.UI;
using Rogium.Gameplay.Core;
using Rogium.Systems.Input;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Rogium.UserInterface.Gameplay.PauseMenu
{
    /// <summary>
    /// Controls the pause menu of the game.
    /// </summary>
    public class PauseMenuOverseerMono : MonoSingleton<PauseMenuOverseerMono>, IObjectVisibilityController
    {
        [SerializeField] private GameObject pauseMenuObject;
        [SerializeField] private Selectable firstSelectedButton;
        [SerializeField] private ModalWindow modalWindow;

        private bool isActive;
        
        private void Start() => SwitchVisibilityStatus(false);
        private void OnEnable()
        {
            InputSystem.Instance.Player.ButtonStart.OnPress += SwitchMenuState;
            InputSystem.Instance.UI.Menu.OnPress += SwitchMenuState;
        }

        private void OnDisable()
        {
            InputSystem.Instance.Player.ButtonStart.OnPress -= SwitchMenuState;
            InputSystem.Instance.UI.Menu.OnPress -= SwitchMenuState;
        }

        public void SwitchVisibilityStatus() => SwitchVisibilityStatus(!pauseMenuObject.activeSelf);
        public void SwitchVisibilityStatus(bool isVisible) => pauseMenuObject.SetActive(isVisible);

        /// <summary>
        /// Open a message to return back to main menu.
        /// </summary>
        public void ReturnToMainMenu()
        {
            modalWindow.OpenAsMessage("Are you sure? All progress will be lost!", ThemeType.Blue, "Yes", CloseGame, true);
        }
        
        /// <summary>
        /// Toggles between the menu being active and hidden.
        /// </summary>
        public void SwitchMenuState()
        {
            if (isActive)
            {
                SwitchVisibilityStatus(false);
                GameplayOverseerMono.GetInstance().DisableUI();
                EventSystem.current.SetSelectedGameObject(null);
                isActive = false;
                return;
            }

            SwitchVisibilityStatus(true);
            GameplayOverseerMono.GetInstance().EnableUI();
            firstSelectedButton.Select();
            isActive = true;
        }

        /// <summary>
        /// Calls for a premature game end.
        /// </summary>
        private void CloseGame()
        {
            SwitchMenuState();
            GameplayOverseerMono.GetInstance().EndGame();
        }
    }
}