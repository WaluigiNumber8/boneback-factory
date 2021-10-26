using BoubakProductions.Core;
using Rogium.Editors.Campaign;
using Rogium.Editors.Core.Defaults;
using Rogium.Gameplay.DataLoading;
using Rogium.Global.SceneTransferService;
using UnityEngine;

namespace Rogium.Gameplay.Core
{
    /// <summary>
    /// Overseers the gameplay as a MonoBehaviour. Communicates with <see cref="GameplayOverseer"/>.
    /// </summary>
    public class GameplayOverseerMono : MonoSingleton<GameplayOverseerMono>
    {
        private GameplayOverseer overseer;

        [SerializeField] private Vector3Int positionOffset;
        [SerializeField] private TilemapLayer[] tilemaps;
        
        private void OnEnable()
        {
            overseer = GameplayOverseer.Instance;
            overseer.PrepareGame(SceneTransferOverseer.GetInstance().PickUpCampaign(), tilemaps, positionOffset);
        }
    }
}