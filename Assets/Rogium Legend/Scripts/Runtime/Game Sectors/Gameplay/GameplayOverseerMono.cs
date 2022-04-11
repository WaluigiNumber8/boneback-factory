using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using BoubakProductions.Core;
using BoubakProductions.Safety;
using BoubakProductions.Systems.GASCore;
using Rogium.Editors.Campaign;
using Rogium.Editors.Core;
using Rogium.Editors.Rooms;
using Rogium.Gameplay.InteractableObjects;
using Rogium.Gameplay.Sequencer;
using Rogium.Systems.Input;
using Rogium.Systems.SceneTransferService;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Rogium.Gameplay.Core
{
    /// <summary>
    /// Overseers the gameplay portion of the game.
    /// </summary>
    public class GameplayOverseerMono : MonoSingleton<GameplayOverseerMono>
    {
        [SerializeField] private GameplaySequencer sequencer;
        
        private CampaignAsset currentCampaign;

        private SortedDictionary<int, int> difficultyData;
        private int currentTier;
        private int currentDifficultyPos;

        protected override void Awake()
        {
            base.Awake();
            difficultyData = new SortedDictionary<int, int>();
            
            try { currentCampaign = new CampaignAsset(SceneTransferOverseer.GetInstance().PickUpCampaign()); }
            catch (Exception) { currentCampaign = ExternalLibraryOverseer.Instance.GetCampaignsCopy[0]; }
            
        }
        private void OnEnable() => InteractObjectDoorLeave.OnTrigger += AdvanceRoom;
        private void OnDisable() => InteractObjectDoorLeave.OnTrigger -= AdvanceRoom;
        private void Start() => PrepareGame();

        /// <summary>
        /// Prepares the game scene.
        /// </summary>
        public void PrepareGame()
        {
            PrepareDifficultyTiers(currentCampaign.DataPack.Rooms, currentCampaign.AdventureLength);
            
            SafetyNet.EnsureIntIsBiggerOrEqualTo(difficultyData.Count, 1, "Detected Room Difficulty Tiers");
            
            InputSystem.Instance.EnablePlayerMap();
            sequencer.RunIntro(currentTier);
        }

        /// <summary>
        /// Complete the current room and move to the next one.
        /// </summary>
        /// <param name="nextRoomType">The next room type.</param>
        /// <param name="direction">The direction of entrance.</param>
        private void AdvanceRoom(RoomType nextRoomType, Vector2 direction)
        {
            if (difficultyData.Count <= 0) return;
            
            RoomType nextType = nextRoomType;
            difficultyData[currentTier]--;

            if (difficultyData[currentTier] <= 0)
            {
                //If next room is rare, prolong the current tier.
                if (nextRoomType == RoomType.Rare) difficultyData[currentTier]++;
                else
                {
                    difficultyData.Remove(currentTier);

                    //Check if no more tiers exist, then finish the game.
                    if (difficultyData.Count <= 0)
                    {
                        StartCoroutine(FinishGame(direction));
                        return;
                    }
                    
                    //Begin next tier.
                    currentTier = difficultyData.First().Key;
                    nextType = RoomType.Entrance;
                }
            }

            //Try to finish current tier in exit room.
            if (difficultyData[currentTier] == 1)
            {
                //If next room is rare, prolong the current tier.
                if (nextRoomType == RoomType.Rare) difficultyData[currentTier]++;
                else nextType = RoomType.Exit;
            }
            
            sequencer.RunTransition(nextType, direction, currentTier);
        }

        /// <summary>
        /// Prepares how many rooms will be played for each difficulty.
        /// </summary>
        /// <param name="rooms">All rooms of the campaign.</param>
        /// <param name="campaignLength">The amount of rooms to visit in the campaign.</param>
        private void PrepareDifficultyTiers(IList<RoomAsset> rooms, int campaignLength)
        {
            IdentifyRoomDifficulties(rooms);
            CalculateRoomCountPerTier(campaignLength);
            currentTier = difficultyData.First().Key;
        }

        /// <summary>
        /// Determines how many rooms are in each difficulty tier.
        /// </summary>
        /// <param name="rooms">All rooms of the campaign.</param>
        private void IdentifyRoomDifficulties(IList<RoomAsset> rooms)
        {
            foreach (RoomAsset room in rooms)
            {
                int level = room.DifficultyLevel;
                if (!difficultyData.ContainsKey(level)) difficultyData.Add(level, 0);
                difficultyData[level]++;
            }
        }

        /// <summary>
        /// Calculates how many rooms will appear in each difficulty tier.
        /// </summary>
        /// <param name="campaignLength">How many rooms will be visited in the campaign</param>
        private void CalculateRoomCountPerTier(int campaignLength)
        {
            SortedDictionary<int, int> updateDifficultyData = new();
            int average = 1 / difficultyData.Keys.Count;
            int normalRoomAmount = campaignLength / difficultyData.Keys.Count - 1;
            
            foreach (int tier in difficultyData.Keys)
            {
                int percentageOfTotal = difficultyData[tier] / difficultyData.Count;
                int roomAmount = (percentageOfTotal < average) ? normalRoomAmount * percentageOfTotal : normalRoomAmount;
                roomAmount += Random.Range(0, 3);
                updateDifficultyData.Add(tier, roomAmount);
            }
            difficultyData = updateDifficultyData;
        }

        private IEnumerator FinishGame(Vector2 direction)
        {
            InputSystem.Instance.EnableUIMap();
            yield return sequencer.RunEndCoroutine(direction);
            GAS.SwitchScene(0);
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