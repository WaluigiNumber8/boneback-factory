using BoubakProductions.Core;
using Rogium.Editors.Campaign;
using Rogium.Editors.Core;
using Rogium.Gameplay.Sequencer;
using Rogium.Systems.Input;
using Rogium.Systems.SceneTransferService;
using UnityEngine;

namespace Rogium.Gameplay.Core
{
    /// <summary>
    /// Overseers the gameplay portion of the game.
    /// </summary>
    public class GameplayOverseerMono : MonoSingleton<GameplayOverseerMono>
    {
        [SerializeField] private GameplaySequencer sequencer;
        
        private CampaignAsset currentCampaign;

        protected override void Awake()
        {
            base.Awake();
            currentCampaign = new CampaignAsset(SceneTransferOverseer.GetInstance().PickUpCampaign());
            // currentCampaign = ExternalLibraryOverseer.Instance.GetCampaignsCopy[0];
        }

        private void Start() => PrepareGame();

        /// <summary>
        /// Prepares the game scene.
        /// </summary>
        public void PrepareGame()
        {
            InputSystem.Instance.EnablePlayerMap();
            sequencer.RunIntro();
        }

        public CampaignAsset CurrentCampaign
        {
            get 
            {
                if (currentCampaign == null) throw new MissingReferenceException("Current Campaign has not been set. Did you forget to activate gameplay mode?");
                return currentCampaign;
            }
        }
    }
}