using System;
using System.Collections;
using RedRats.Core;
using RedRats.Systems.ClockOfTheGame;
using RedRats.Systems.GASCore;
using Rogium.Editors.Campaign;
using Rogium.Editors.Core;
using Rogium.Editors.Rooms;
using Rogium.Gameplay.AssetRandomGenerator;
using Rogium.Gameplay.Entities.Player;
using Rogium.Gameplay.InteractableObjects;
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
        [SerializeField] private PlayerController player;

        /// <summary>
        /// Time during which the player cannot be attacked after loading a room.
        /// </summary>
        [Tooltip("Time during which the player cannot be attacked after loading a room.")]
        [SerializeField] private float safePeriodTime = 1f;
        
        private RRG rrg;
        private CampaignAsset currentCampaign;
        private float safePeriodTimer;

        protected override void Awake()
        {
            base.Awake();
            try { currentCampaign = new CampaignAsset(SceneTransferOverseer.GetInstance().PickUpCampaign()); }
            catch (Exception) { currentCampaign = ExternalLibraryOverseer.Instance.GetCampaignsCopy[0]; }
            
        }
        private void OnEnable()
        {
            InteractObjectDoorLeave.OnTrigger += AdvanceRoom;
            player.OnDeath += GameOverGame;
            sequencer.OnRoomLoaded += ActivateSafePeriod;
        }

        private void OnDisable()
        {
            InteractObjectDoorLeave.OnTrigger -= AdvanceRoom;
            player.OnDeath -= GameOverGame;
            sequencer.OnRoomLoaded -= ActivateSafePeriod;
        }

        private void Start() => PrepareGame();

        /// <summary>
        /// Prepares the game scene.
        /// </summary>
        public void PrepareGame()
        {
            rrg = new RRG(currentCampaign.DataPack.Rooms, currentCampaign.AdventureLength);
            InputSystem.GetInstance().EnablePlayerMap();
            sequencer.RunIntro(rrg.GetNext(RoomType.Entrance));
        }

        public void EndGame(Vector2 direction)
        {
            StartCoroutine(FinishGameCoroutine(direction));
            IEnumerator FinishGameCoroutine(Vector2 dir)
            {
                yield return sequencer.RunEndCoroutine(dir);
                InputSystem.GetInstance().EnableUIMap();
                GAS.SwitchScene(0);
            }
        }

        public void GameOverGame()
        {
            StartCoroutine(GameOverGameCoroutine());
            IEnumerator GameOverGameCoroutine()
            {
                InputSystem.GetInstance().EnableUIMap();
                yield return sequencer.RunGameOverCoroutine();
                GAS.SwitchScene(0);
            }
        }

        /// <summary>
        /// Prepares the game for opening of UI.
        /// </summary>
        public void EnableUI()
        {
            GameClock.Instance.Pause();
            InputSystem.GetInstance().EnableUIMap();
        }

        /// <summary>
        /// Prepares the game for resuming fom UI.
        /// </summary>
        public void DisableUI()
        {
            GameClock.Instance.Resume();
            StartCoroutine(EnableMap());
            IEnumerator EnableMap()
            {
                yield return new WaitForSeconds(0.1f);
                InputSystem.GetInstance().EnablePlayerMap();
            }
        }

        /// <summary>
        /// Is the game currently in safe period? (Short amount of time after loading a room.)
        /// </summary>
        /// <returns>TRUE if it is.</returns>
        public bool IsInSafePeriod() => safePeriodTimer > Time.time;
        
        /// <summary>
        /// Complete the current room and move to the next one.
        /// </summary>
        /// <param name="nextRoomType">The next room type.</param>
        /// <param name="direction">The direction of entrance.</param>
        private void AdvanceRoom(RoomType nextRoomType, Vector2 direction)
        {
            int nextRoomIndex = rrg.GetNext(nextRoomType);
            
            if (nextRoomIndex == -1) EndGame(Vector2.up * 10); 
            else sequencer.RunTransition(nextRoomIndex, direction);
        }

        /// <summary>
        /// Activates a safe period, during which the player cannot be attacked.
        /// </summary>
        private void ActivateSafePeriod()
        {
            safePeriodTimer = Time.time + safePeriodTime;
            player.BecomeInvincible(safePeriodTime);
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