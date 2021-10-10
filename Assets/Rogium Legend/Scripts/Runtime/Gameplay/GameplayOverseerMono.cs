using System;
using BoubakProductions.Core;
using Rogium.Global.TransferService;
using UnityEngine;

namespace Rogium.Gameplay.Core
{
    /// <summary>
    /// Overseers the gameplay as a MonoBehaviour. Communicates with <see cref="GameplayOverseer"/>.
    /// </summary>
    public class GameplayOverseerMono : MonoSingleton<GameplayOverseerMono>
    {
        private GameplayOverseer overseer;
        
        private void Start()
        {
            overseer = GameplayOverseer.Instance;
            overseer.PrepareGame(SceneTransferService.CurrentCampaign);
        }
    }
}