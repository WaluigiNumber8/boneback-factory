using System.Collections.Generic;
using BoubakProductions.Systems.Randomization;
using Rogium.Editors.Rooms;

namespace Rogium.Gameplay.AssetRandomGenerator
{
    /// <summary>
    /// A Generator that will categorize rooms and pick random values.
    /// </summary>
    public class RRG
    {
        private readonly IList<int> normalRooms;
        private readonly IList<int> rareRooms;
        private readonly IList<int> entranceRooms;
        private readonly IList<int> shopRooms;

        private readonly IRandomizer randomizerNormal;
        private readonly IRandomizer randomizerRare;

        private readonly int chosenEntrance;
        private readonly int chosenShop;

        public RRG(IList<RoomAsset> allRooms)
        {
            normalRooms = new List<int>();
            rareRooms = new List<int>();
            entranceRooms = new List<int>();
            shopRooms = new List<int>();
            LoadUpLists(allRooms);

            randomizerNormal = new RandomizerRegion(0, normalRooms.Count);
            randomizerRare = new RandomizerRegion(0, rareRooms.Count);

            //TODO Deal with having no rooms in a collection.
            chosenEntrance = entranceRooms[0];
            // chosenShop = shopRooms[randomizer.GetNext(3)];
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
                switch (room.Type)
                {
                    case RoomType.Normal:
                        normalRooms.Add(i);
                        break;
                    case RoomType.Rare:
                        rareRooms.Add(i);
                        break;
                    case RoomType.Entrance:
                        entranceRooms.Add(i);
                        break;
                    case RoomType.Shop:
                        shopRooms.Add(i);
                        break;
                    default:
                        continue;
                }
            }
        }

        /// <summary>
        /// Loads the next normal type room.
        /// </summary>
        /// <returns>The index of the room to grab.</returns>
        public int NextNormalRoom()
        {
            return normalRooms[randomizerNormal.GetNext()];
        }
        
        /// <summary>
        /// Loads the next rare type room.
        /// </summary>
        /// <returns>The index of the room to grab.</returns>
        public int NextRareRoom()
        {
            return rareRooms[randomizerRare.GetNext()];
        }
        
        /// <summary>
        /// Loads the chosen entrance type room.
        /// </summary>
        /// <returns>The index of the room to grab.</returns>
        public int ChosenEntranceRoom()
        {
            return chosenEntrance;
        }
        
        /// <summary>
        /// Loads the chosen shop type room.
        /// </summary>
        /// <returns>The index of the room to grab.</returns>
        public int ChosenShopRoom()
        {
            return chosenShop;
        }
        
    }
}