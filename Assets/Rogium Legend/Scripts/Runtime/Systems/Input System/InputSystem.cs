using System.Collections;
using RedRats.Core;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

namespace Rogium.Systems.Input
{
    /// <summary>
    /// Overseers all input profiles and deals with their switching.
    /// </summary>
    public class InputSystem : PersistentMonoSingleton<InputSystem>
    {
        private InputProfilePlayer inputPlayer;
        private InputProfileUI inputUI;
        private EventSystem eventSystem;

        protected override void Awake()
        {
            base.Awake();
            RogiumInputActions input = new();
            inputPlayer = new InputProfilePlayer(input);
            inputUI = new InputProfileUI(input);
            SceneManager.sceneLoaded += (_, __) => eventSystem = FindFirstObjectByType<EventSystem>();
        }

        /// <summary>
        /// Enables the UI Action Map.
        /// </summary>
        public void EnableUIMap()
        {
            DisableAll();
            inputUI.Enable();
        }
        
        /// <summary>
        /// Enables the Player Action Map.
        /// </summary>
        public void EnablePlayerMap()
        {
            DisableAll();
            inputPlayer.Enable();
        }
        
        /// <summary>
        /// Disables all input for a specified amount of time.
        /// </summary>
        /// <param name="caller">The <see cref="MonoBehaviour"/> that called for this method.</param>
        /// <param name="delay">How long to suspend all input for.</param>
        public void DisableInput(MonoBehaviour caller, float delay)
        {
            caller.StartCoroutine(DisableAllCoroutine(delay));
            IEnumerator DisableAllCoroutine(float delayTime)
            {
                eventSystem.sendNavigationEvents = false;
                yield return new WaitForSecondsRealtime(delayTime);
                eventSystem.sendNavigationEvents = true;
            }
        }
        
        /// <summary>
        /// Disables all Action Maps except UI.
        /// </summary>
        private void DisableAll()
        {
            inputPlayer.Disable();
        }
        
        public InputProfilePlayer Player { get => inputPlayer; }
        public InputProfileUI UI { get => inputUI; }
    }
}