using System;
using Rogium.Global.UISystem.Interactables;
using UnityEngine;

namespace Rogium.Core
{
    /// <summary>
    /// Overseers the game as a MonoBehaviour. Communicates with <see cref="GameOverseer"/>.
    /// </summary>
    public class GameOverseerMono : MonoBehaviour
    {
        private GameOverseer gameOverseer;

        private void Awake()
        {
            gameOverseer = GameOverseer.Instance;
            GASButtonActions.GameStart();
        }
    }
}