using System.Collections.Generic;
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

        private readonly int chosenEntrance;
        private readonly int chosenShop;

        private readonly RandomizerOverseer randomizer;

        public RRG(IList<RoomAsset> allRooms)
        {
            normalRooms = new List<int>();
            rareRooms = new List<int>();
            entranceRooms = new List<int>();
            shopRooms = new List<int>();
            LoadUpLists(allRooms);

            randomizer = new RandomizerOverseer();
            randomizer.Register(0, normalRooms.Count, 5);
            randomizer.Register(1, rareRooms.Count, 5);
            randomizer.Register(2, entranceRooms.Count, 5);
            randomizer.Register(3, shopRooms.Count, 5);

            //TODO Deal with having no rooms in a collection.
            chosenEntrance = entranceRooms[randomizer.GetNext(2)];
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
            int value = randomizer.GetNext(0);
            return normalRooms[value];
        }
        
        /// <summary>
        /// Loads the next rare type room.
        /// </summary>
        /// <returns>The index of the room to grab.</returns>
        public int NextRareRoom()
        {
            return rareRooms[randomizer.GetNext(1)];
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