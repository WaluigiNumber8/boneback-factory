using RedRats.Core;
using RedRats.UI.ModalWindows;
using Rogium.Gameplay.Core;
using Rogium.Systems.Input;
using Rogium.UserInterface.ModalWindows;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace Rogium.UserInterface.Gameplay.PauseMenu
{
    /// <summary>
    /// Controls the pause menu of the game.
    /// </summary>
    public class PauseMenuOverseerMono : MonoSingleton<PauseMenuOverseerMono>, IObjectVisibilityController
    {
        [SerializeField] private GameObject pauseMenuObject;
        [SerializeField] private Selectable firstSelectedButton;

        private InputSystem inputSystem;
        private bool isActive;

        protected override void Awake()
        {
            base.Awake();
            inputSystem = InputSystem.GetInstance();
        }

        private void Start() => SwitchVisibilityStatus(false);
        private void OnEnable()
        {
            inputSystem.Player.ButtonStart.OnPress += SwitchMenuState;
            inputSystem.UI.Menu.OnPress += SwitchMenuState;
        }

        private void OnDisable()
        {
            if (inputSystem == null) return;
            inputSystem.Player.ButtonStart.OnPress -= SwitchMenuState;
            inputSystem.UI.Menu.OnPress -= SwitchMenuState;
        }

        public void SwitchVisibilityStatus(bool isVisible) => pauseMenuObject.SetActive(isVisible);

        /// <summary>
        /// Open a message to return back to main menu.
        /// </summary>
        public void ReturnToMainMenu() => CloseGame();

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
            GameplayOverseerMono.GetInstance().EndGame(Vector2.down * 1); 
        }
    }
}