using BoubakProductions.Core;
using BoubakProductions.Systems.ClockOfTheGame;
using BoubakProductions.UI;
using Rogium.Gameplay.Core;
using Rogium.Systems.Input;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Rogium.UserInterface.Gameplay.PauseMenu
{
    /// <summary>
    /// Controls the pause menu of the game.
    /// </summary>
    public class PauseMenuOverseerMono : MonoSingleton<PauseMenuOverseerMono>, IObjectVisibilityController
    {
        [SerializeField] private GameObject pauseMenuObject;
        [SerializeField] private GameObject firstSelectedButton;
        [SerializeField] private ModalWindow modalWindow;

        private bool isActive;
        
        private void Start() => SwitchVisibilityStatus(false);
        private void OnEnable() => InputSystem.Instance.Player.ButtonStart.OnPress += SwitchMenuState;
        private void OnDisable() => InputSystem.Instance.Player.ButtonStart.OnPress -= SwitchMenuState;

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
                GameClock.Instance.Resume();
                EventSystem.current.firstSelectedGameObject = null;
                isActive = false;
                return;
            }

            SwitchVisibilityStatus(true);
            GameClock.Instance.Pause();
            if (firstSelectedButton != null) EventSystem.current.firstSelectedGameObject = firstSelectedButton;
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