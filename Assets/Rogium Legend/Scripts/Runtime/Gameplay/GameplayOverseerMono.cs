using BoubakProductions.Core;
using Rogium.Editors.Core;
using Rogium.Gameplay.DataLoading;
using Rogium.Systems.SceneTransferService;
using UnityEngine;

namespace Rogium.Gameplay.Core
{
    /// <summary>
    /// Overseers the gameplay as a MonoBehaviour. Communicates with <see cref="GameplayOverseer"/>.
    /// </summary>
    public class GameplayOverseerMono : MonoSingleton<GameplayOverseerMono>
    {
        private GameplayOverseer overseer;

        [SerializeField] private RoomLoader roomLoader;
        
        private void OnEnable()
        {
            overseer = GameplayOverseer.Instance;
            overseer.PrepareGame(SceneTransferOverseer.GetInstance().PickUpCampaign(), roomLoader);
            // overseer.PrepareGame(LibraryOverseer.Instance.GetCampaignsCopy[0], roomLoader);
        }
    }
}