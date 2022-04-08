using System;
using System.Collections.Generic;
using System.Linq;
using BoubakProductions.Safety;
using BoubakProductions.Systems.Randomization;
using Rogium.Editors.Rooms;

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

        private int currentTier;
        private int previousTier;

        public RRG(IList<RoomAsset> allRooms, int initialDifficultyTier)
        {
            SafetyNet.EnsureListIsNotNullOrEmpty(allRooms, "Campaign rooms to use for RRG");
            
            currentTier = initialDifficultyTier;
            previousTier = currentTier;
            
            normalRooms = new Dictionary<int, IList<int>>();
            rareRooms = new Dictionary<int, IList<int>>();
            entranceRooms = new Dictionary<int, int>();
            exitRooms = new Dictionary<int, int>();
            shopRooms = new Dictionary<int, int>();
            
            LoadUpLists(allRooms);
        }

        /// <summary>
        /// Grab the index of the next room.
        /// </summary>
        /// <param name="roomType">The type of room to grab.</param>
        /// <param name="difficultyTier">The difficulty level of the room.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentOutOfRangeException">Is thrown when the room type is not supported.</exception>
        public int LoadNext(RoomType roomType, int difficultyTier)
        {
            if (currentTier != difficultyTier)
            {
                previousTier = currentTier;
                currentTier = difficultyTier;
            }

            return roomType switch
            {
                RoomType.Normal => TryReturnNormal(),
                RoomType.Rare => TryReturnRare(),
                RoomType.Entrance => TryReturnEntrance(),
                RoomType.Exit => TryReturnExit(),
                RoomType.Shop => TryReturnShop(),
                _ => throw new ArgumentOutOfRangeException($"The Room Type of '{roomType}' is not supported.")
            };
        }

        /// <summary>
        /// Tries to return an index of random normal room.
        /// </summary>
        /// <returns>An index of a normal room.</returns>
        private int TryReturnNormal()
        {
            if (TierHasNoRooms(normalRooms)) return TryReturnEntrance();
            if (previousTier != currentTier || randomizerNormal == null) randomizerNormal = new RandomizerRegion(0, normalRooms[currentTier].Count);
            return normalRooms[currentTier][randomizerNormal.GetNext()];
        }
        
        /// <summary>
        /// Tries to return an index of random rare room.
        /// </summary>
        /// <returns>An index of a rare room.</returns>
        private int TryReturnRare()
        {
            if (TierHasNoRooms(rareRooms)) return TryReturnNormal();
            if (previousTier != currentTier || randomizerRare == null) randomizerRare = new RandomizerRegion(0, rareRooms[currentTier].Count);
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
        /// Checks if a dictionary does not contain the current difficulty tier.
        /// </summary>
        /// <param name="roomsContainer">The dictionary of rooms.</param>
        /// <returns>TRUE if nothing was found in the dictionary.</returns>
        private bool TierHasNoRooms(IDictionary<int, IList<int>> roomsContainer)
        {
            return (!roomsContainer.ContainsKey(currentTier) || roomsContainer[currentTier] == null || roomsContainer[currentTier].Count <= 0);
        }
        
        /// <summary>
        /// Checks if a dictionary does not contain the current difficulty tier.
        /// </summary>
        /// <param name="roomsContainer">The dictionary of rooms.</param>
        /// <returns>TRUE if nothing was found in the dictionary.</returns>
        private bool TierHasNoRooms(IDictionary<int, int> roomsContainer)
        {
            return (!roomsContainer.ContainsKey(currentTier) || roomsContainer[currentTier] == -1);
        }
    }
}