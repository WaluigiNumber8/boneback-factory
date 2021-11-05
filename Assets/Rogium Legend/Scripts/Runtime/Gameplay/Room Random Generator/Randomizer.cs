using UnityEngine;

namespace Rogium.Gameplay.AssetRandomGenerator
{
    /// <summary>
    /// Stores data for a randomizer id.
    /// </summary>
    public class Randomizer
    {
        private const int emptyMemorySlot = -1;
        
        private int id;
        private int collectionLength;
        private int[] memory;

        private int memoryPos;

        public Randomizer(int id, int collectionLength, int memorySize)
        {
            this.id = id;
            this.collectionLength = collectionLength;
            
            this.memory = new int[memorySize];
            for (int i = 0; i < memory.Length; i++)
            {
                memory[i] = emptyMemorySlot;
            }
        }

        /// <summary>
        /// Gets the next random value.
        /// </summary>
        /// <returns>A new random value.</returns>
        public int GetNext()
        {
            return GenerateValue();
        }

        /// <summary>
        /// Generates a new random value.
        /// </summary>
        /// <returns>The generated value.</returns>
        private int GenerateValue()
        {
            int newValue = Random.Range(0, collectionLength);
            
            //If collection is too small, dont use memory.
            if (collectionLength > 2)
            {
                newValue = AdjustBasedOnMemory(newValue);
                RegisterToMemory(newValue);
            }

            return newValue;
        }
        
        /// <summary>
        /// Registers a value in memory. If memory is full, the oldest one will get overriden.
        /// </summary>
        /// <param name="value">The new value to register.</param>
        private void RegisterToMemory(int value)
        {
            memory[memoryPos] = value;
            memoryPos++;

            if (memoryPos >= memory.Length)
                memoryPos = 0;
        }

        /// <summary>
        /// Adjusts a random value based on memory.
        /// </summary>
        /// <param name="value">The value to adjust.</param>
        /// <returns>An adjusted value.</returns>
        private int AdjustBasedOnMemory(int value)
        {
            //If value is same as in memory, adjust it slightly.
            foreach (int memorySlot in memory)
            {
                if (memorySlot != emptyMemorySlot || value == memorySlot)
                {
                    value += Random.Range(1, 3);
                    value = FitValue(value);
                }
            }

            return value;
        }

        /// <summary>
        /// Fits the value in the collection size.
        /// </summary>
        /// <param name="value">The value to fit.</param>
        /// <returns>Value that will be withing the collection size.</returns>
        private int FitValue(int value)
        {
            if (value > collectionLength) value -= collectionLength;
            value = Mathf.Max(value, 0);
            value = Mathf.Min(collectionLength-1, value);
            return value;
        }
        
        public int ID { get => id; }
        public int CollectionLength { get => collectionLength; }
        public int MemorySize { get => memory.Length; }
    }
}