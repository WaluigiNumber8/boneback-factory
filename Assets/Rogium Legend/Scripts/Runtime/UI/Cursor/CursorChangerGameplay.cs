using RedRats.UI.Core.Cursors;
using Rogium.Gameplay.Core;
using UnityEngine;

namespace Rogium.UserInterface.Cursors
{
    /// <summary>
    /// Enables the gameplay cursor when gameplay is running.
    /// </summary>
    public class CursorChangerGameplay : MonoBehaviour
    {
        private CursorOverseerMono overseer;
        private GameplayOverseerMono gameplay;
        
        private CursorType currentCursor;

        private void Awake()
        {
            overseer = CursorOverseerMono.GetInstance();
            gameplay = GameplayOverseerMono.GetInstance();
        }
        
        private void OnEnable()
        {
            gameplay.OnGameplayPause += ToDefault;
            gameplay.OnGameplayResume += ToGameplay;
        }

        private void OnDisable()
        {
            gameplay.OnGameplayPause -= ToDefault;
            gameplay.OnGameplayResume -= ToGameplay;
        }

        private void ToDefault() => overseer.SetDefault();
        private void ToGameplay() => overseer.Set(CursorType.Gameplay);
        
    }
}