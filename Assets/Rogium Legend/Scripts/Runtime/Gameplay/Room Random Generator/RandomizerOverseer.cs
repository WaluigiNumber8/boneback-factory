using System.Collections.Generic;
using BoubakProductions.Safety;

namespace Rogium.Gameplay.AssetRandomGenerator
{
    /// <summary>
    /// Contains registered randomizer ids and keeps track of them.
    /// </summary>
    public class RandomizerOverseer
    {
        private readonly IList<Randomizer> ids;

        public RandomizerOverseer()
        {
            ids = new List<Randomizer>();
        }
        
        /// <summary>
        /// Registers a collection in the randomizer.
        /// </summary>
        /// <param name="id">The id the collection will hold.</param>
        /// <param name="collectionLength">The size of the collection.</param>
        public void Register(int id, int collectionLength, int memorySize)
        {
            SafetyNet.EnsureIntIsBiggerOrEqualTo(id, 0, "Newly registered ID");
            SafetyNet.EnsureIntIsBiggerOrEqualTo(collectionLength, 0, "Newly registered collectionLength");
            ids.Add(new Randomizer(id, collectionLength, memorySize));
        }

        /// <summary>
        /// Get a new random value for a registered id.
        /// </summary>
        /// <param name="id"></param>
        public int GetNext(int id)
        {
            SafetyNet.EnsureIntIsInRange(id, 0, ids.Count, "Registered IDs");
            return ids[id].GetNext();
        }

        public IList<Randomizer> IDs {get => ids; }
    }
}