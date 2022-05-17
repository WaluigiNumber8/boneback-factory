using System;
using System.Collections.Generic;
using System.Linq;
using RedRats.Safety;
using RedRats.Systems.Randomization;
using Rogium.Editors.Rooms;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Rogium.Gameplay.AssetRandomGenerator
{
    /// <summary>
    /// A Generator that will categorize rooms and pick random values.
    /// </summary>
    public class RRG
    {
        private readonly IDictionary<int, IList<int>> normalRooms;
        private readonly IDictionary<int, IList<int>> rareRooms;
        private readonly IDictionary<int, int> entranceRooms;
        private readonly IDictionary<int, int> exitRooms;
        private readonly IDictionary<int, int> shopRooms;

        private IRandomizer randomizerNormal;
        private IRandomizer randomizerRare;
        
        private SortedDictionary<int, int> difficultyData;
        private int currentTier;
        private int previousTier;
        private int currentDifficultyPos;

        /// <summary>
        /// The Room Random Generator, used for determining, what room to load next, based on it's type and difficulty. 
        /// </summary>
        /// <param name="allRooms">All the rooms to work with.</param>
        /// <param name="roomAmount">How many rooms to allot, until teh finish state is reached.</param>
        public RRG(IList<RoomAsset> allRooms, int roomAmount)
        {
            SafetyNet.EnsureListIsNotNullOrEmpty(allRooms, "Campaign rooms to use for RRG");
            
            difficultyData = new SortedDictionary<int, int>();
            
            normalRooms = new Dictionary<int, IList<int>>();
            rareRooms = new Dictionary<int, IList<int>>();
            entranceRooms = new Dictionary<int, int>();
            exitRooms = new Dictionary<int, int>();
            shopRooms = new Dictionary<int, int>();
            
            LoadUpLists(allRooms);
            IdentifyRoomDifficulties(allRooms);
            CalculateRoomCountPerTier(roomAmount);
            
            SafetyNet.EnsureIntIsBiggerOrEqualTo(difficultyData.Count, 1, "Detected Room Difficulty Tiers");
            currentTier = difficultyData.Keys.First();
            
            UpdateRandomizers();
        }

        /// <summary>
        /// Complete the current room and move to the next one.
        /// If no more rooms can be loaded, will return -1.
        /// </summary>
        /// <param name="nextRoomType">The next room type.</param>
        public int GetNext(RoomType nextRoomType)
        {
            if (difficultyData.Count <= 0) return -1;
            
            RoomType nextType = nextRoomType;
            difficultyData[currentTier]--;

            //If next room is an exit, shorten the difficulty.
            if (nextType == RoomType.Exit && difficultyData[currentTier] > 1) difficultyData[currentTier] = 1;
            
            if (difficultyData[currentTier] <= 0)
            {
                //If next room is rare, prolong the current tier.
                if (nextRoomType == RoomType.Rare) difficultyData[currentTier]++;
                else
                {
                    difficultyData.Remove(currentTier);

                    //Check if no more tiers exist, then finish the game.
                    if (difficultyData.Count <= 0) { return -1; }
                    
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

            return LoadNext(nextType);
        }
        
        /// <summary>
        /// Grab the index of the next room.
        /// </summary>
        /// <param name="roomType">The type of room to grab.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentOutOfRangeException">Is thrown when the room type is not supported.</exception>
        private int LoadNext(RoomType roomType)
        {
            if (currentTier != previousTier) UpdateRandomizers();
            
            int index = roomType switch
            {
                RoomType.Normal => TryReturnNormal(),
                RoomType.Rare => TryReturnRare(),
                RoomType.Entrance => TryReturnEntrance(),
                RoomType.Exit => TryReturnExit(),
                RoomType.Shop => TryReturnShop(),
                _ => throw new ArgumentOutOfRangeException($"The Room Type of '{roomType}' is not supported.")
            };
            previousTier = currentTier;
            return index;
        }

        /// <summary>
        /// Tries to return an index of random normal room.
        /// </summary>
        /// <returns>An index of a normal room.</returns>
        private int TryReturnNormal()
        {
            if (TierHasNoRooms(normalRooms)) return TryReturnEntrance();
            return normalRooms[currentTier][randomizerNormal.GetNext()];
        }
        
        /// <summary>
        /// Tries to return an index of random rare room.
        /// </summary>
        /// <returns>An index of a rare room.</returns>
        private int TryReturnRare()
        {
            if (TierHasNoRooms(rareRooms)) return TryReturnNormal();
            return rareRooms[currentTier][randomizerRare.GetNext()];
        }
        
        /// <summary>
        /// Tries to return an index of the current tier entrance room.
        /// </summary>
        /// <returns>An index of an entrance room.</returns>
        private int TryReturnEntrance()
        {
            if (TierHasNoRooms(entranceRooms))
            {
                if (entranceRooms.Count > 0) return entranceRooms.First().Value;
                if (normalRooms.Count > 0)
                {
                    IList<int> firstValue = normalRooms.First().Value;
                    if (firstValue != null && firstValue.Count > 0) return firstValue[0];
                }
                throw new InvalidOperationException("The Difficulty Tier must have an Entrance Room set.");
            }
            return entranceRooms[currentTier];
        }
        
        /// <summary>
        /// Tries to return an index of the current tier exit room.
        /// </summary>
        /// <returns>An index of an exit room.</returns>
        private int TryReturnExit()
        {
            if (TierHasNoRooms(exitRooms)) return TryReturnNormal();
            return exitRooms[currentTier];
        }
        
        /// <summary>
        /// Tries to return an index of the current tier shop room.
        /// </summary>
        /// <returns>An index of an shop room.</returns>
        private int TryReturnShop()
        {
            if (TierHasNoRooms(shopRooms)) return TryReturnRare();
            return shopRooms[currentTier];
        }

        /// <summary>
        /// Categorizes all rooms into different lists.
        /// </summary>
        /// <param name="allRooms">A list containing all rooms.</param>
        private void LoadUpLists(IList<RoomAsset> allRooms)
        {
            for (int i = 0; i < allRooms.Count; i++)
            {
                RoomAsset room = allRooms[i];
                int level = room.DifficultyLevel;
                
                switch (room.Type)
                {
                    case RoomType.Normal:
                        if (!normalRooms.ContainsKey(level)) normalRooms.Add(level, new List<int>());
                        normalRooms[level].Add(i);
                        break;
                    case RoomType.Rare:
                        if (!rareRooms.ContainsKey(level)) rareRooms.Add(level, new List<int>());
                        rareRooms[level].Add(i);
                        break;
                    case RoomType.Entrance:
                        if (!entranceRooms.ContainsKey(level)) entranceRooms.Add(level, -1);
                        entranceRooms[level] = i;
                        break;
                    case RoomType.Exit:
                        if (!exitRooms.ContainsKey(level)) exitRooms.Add(level, -1);
                        exitRooms[level] = i;
                        break;
                    case RoomType.Shop:
                        if (!shopRooms.ContainsKey(level)) shopRooms.Add(level, -1);
                        shopRooms[level] = i;
                        break;
                    default:
                        continue;
                }
            }
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

            int totalRooms = difficultyData.Values.Sum();
            foreach (int tier in difficultyData.Keys)
            {
                if (TierHasNoRooms(normalRooms, tier))
                {
                    if (TierHasNoRooms(entranceRooms, tier) || TierHasNoRooms(exitRooms, tier))
                    {
                        updateDifficultyData.Add(tier, 1);
                        continue;
                    }
                    updateDifficultyData.Add(tier, 2);
                    continue;
                }
                
                float percentageOfTotal = (float)difficultyData[tier] / totalRooms;
                int roomAmount = Mathf.RoundToInt(percentageOfTotal * campaignLength);
                roomAmount += Random.Range(0, 3);
                updateDifficultyData.Add(tier, roomAmount);
            }
            
            difficultyData = updateDifficultyData;
        }
        

        /// <summary>
        /// Update randomizers to current difficulty tier.
        /// </summary>
        private void UpdateRandomizers()
        {
            if (!TierHasNoRooms(normalRooms)) randomizerNormal = new RandomizerRegion(0, normalRooms[currentTier].Count);
            if (!TierHasNoRooms(rareRooms)) randomizerRare = new RandomizerRegion(0, rareRooms[currentTier].Count);
        }
        
        /// <summary>
        /// Checks if a dictionary does not contain the current difficulty tier.
        /// </summary>
        /// <param name="roomsContainer">The dictionary of rooms.</param>
        /// <returns>TRUE if nothing was found in the dictionary.</returns>
        private bool TierHasNoRooms(IDictionary<int, IList<int>> roomsContainer)
        {
            return TierHasNoRooms(roomsContainer, currentTier);
        }
        
        /// <summary>
        /// Checks if a dictionary does not contain the current difficulty tier.
        /// </summary>
        /// <param name="roomsContainer">The dictionary of rooms.</param>
        /// <returns>TRUE if nothing was found in the dictionary.</returns>
        private bool TierHasNoRooms(IDictionary<int, int> roomsContainer)
        {
            return TierHasNoRooms(roomsContainer, currentTier);
        }

        /// <summary>
        /// Checks if a dictionary does not contain a specific difficulty tier.
        /// </summary>
        /// <param name="roomsContainer">The dictionary of rooms.</param>
        /// <param name="tier">The tier to check.</param>
        /// <returns>TRUE if nothing was found in the dictionary.</returns>
        private bool TierHasNoRooms(IDictionary<int, IList<int>> roomsContainer, int tier)
        {
            return (!roomsContainer.ContainsKey(tier) || roomsContainer[tier] == null || roomsContainer[tier].Count <= 0);
        }

        /// <summary>
        /// Checks if a dictionary does not contain a specific difficulty tier.
        /// </summary>
        /// <param name="roomsContainer">The dictionary of rooms.</param>
        /// <param name="tier">The tier to check for.</param>
        /// <returns>TRUE if nothing was found in the dictionary.</returns>
        private bool TierHasNoRooms(IDictionary<int, int> roomsContainer, int tier)
        {
            return (!roomsContainer.ContainsKey(tier) || roomsContainer[tier] == -1);
        }
    }
}