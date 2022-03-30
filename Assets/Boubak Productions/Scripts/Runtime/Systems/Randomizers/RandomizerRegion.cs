using System.Linq;
using BoubakProductions.Core;
using BoubakProductions.Safety;
using UnityEngine;

namespace BoubakProductions.Systems.Randomization
{
    /// <summary>
    /// Uses Modulo regions to randomize a value, limiting value repeating.
    /// Works best with ranges higher or equal to 4.
    /// </summary>
    public class RandomizerRegion : IRandomizer
    {
        private const int EmptyValue = -1;
        
        private readonly int min;
        private readonly int max;
        private readonly float errorChance;
        private readonly int leadway;
        private readonly int rerolls;
        
        private readonly int regions;
        private readonly int regionMemoryPortion;

        private readonly int[] regionMemory;
        private readonly int[] regionValuesMemory;
        private readonly int[] regionValuesMemoryPositions;

        private int regionMemoryPosition;
        private int allowedRerolls;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="min">Minimum allowed value to roll.</param>
        /// <param name="max">Maximum allowed value to roll.</param>
        /// <param name="errorChance">A chance to throw a totally random value, not based on memory.</param>
        /// <param name="rerolls">he amount of times a bad value can be rerolled.</param>
        /// <param name="leadway">The higher this value is, the less the randomizer depends on it's memory.</param>
        public RandomizerRegion(int min, int max, float errorChance = 0f, int rerolls = 2, int leadway = 0)
        {
            SafetyNet.EnsureIntIsBiggerOrEqualTo(leadway, 0, "leadway");
            SafetyNet.EnsureFloatIsInRange(errorChance, 0, 1, "Error Chance");
            
            this.min = min;
            this.max = max;
            this.errorChance = errorChance;
            this.leadway = leadway;
            this.rerolls = rerolls;
            
            this.regions = (int)Mathf.Floor(Mathf.Sqrt(max));
            this.regionMemoryPortion = max / (regions+leadway+1);
            
            regionValuesMemory = InitArray(regionMemoryPortion * regions);
            regionValuesMemoryPositions = new int[regions - leadway];
            
            regionMemory = InitArray(regions - leadway);
        }
        
        public int GetNext()
        {
            allowedRerolls = rerolls;
            
            int newValue = Random.Range(min, max);
            int address = newValue % regions;

            if (regions <= 1) return newValue;
            
            for (int i = 0; i < regionMemory.Length; i++)
            {
                if (regionMemory[i] != EmptyValue)
                {
                    if (errorChance != 0 && ErrorCheckIsPositive(newValue)) return newValue;
                    if (address != regionMemory[i]) continue;
                    
                    //Region is same => process it's values memory
                    int start = address * regionMemoryPortion;
                    int end = start + regionMemoryPortion;
                    for (int j = start; j < end; j++)
                    {
                        if (regionValuesMemory[j] != EmptyValue)
                        {
                            if (errorChance != 0 && ErrorCheckIsPositive(newValue)) return newValue;
                            if (newValue != regionValuesMemory[j]) continue;
                        
                            //If value is already in memory, reroll it.
                            return DecideReroll(newValue, address);
                        }
                    
                        //If empty slots exist in region values memory.
                        RegisterValue(address, newValue);
                        return newValue;
                    }
                
                    //If not in memory, register and return it.
                    RegisterValue(address, newValue);
                    return newValue;
                    
                }
                
                //Is from a new region, return it.
                RegisterRegion(address, newValue);
                return newValue;
            }

            //If not in memory, register and return it.
            RegisterRegion(address, newValue);
            return newValue;
        }

        /// <summary>
        /// Register a region and it's first value into the memory.
        /// </summary>
        /// <param name="regionAddress">The address of the new region to register.</param>
        /// <param name="value">The first value rolled for that region.</param>
        private void RegisterRegion(int regionAddress, int value)
        {
            regionMemory[regionMemoryPosition] = regionAddress;
            regionMemoryPosition++;
            regionMemoryPosition = IntUtils.Wrap(regionMemoryPosition, 0, regions - leadway - 1);
            RegisterValue(regionAddress, value);
        }
        
        /// <summary>
        /// Register a value into the memory.
        /// </summary>
        /// <param name="regionAddress">Address of the region to register under.</param>
        /// <param name="value">The value to register.</param>
        private void RegisterValue(int regionAddress, int value)
        {
            int start = regionAddress * regionMemoryPortion;
            regionValuesMemory[start + regionValuesMemoryPositions[regionAddress]] = value;
            regionValuesMemoryPositions[regionAddress]++;
            regionValuesMemoryPositions[regionAddress] = IntUtils.Wrap(regionValuesMemoryPositions[regionAddress], 0, regionMemoryPortion - leadway - 1);
        }

        /// <summary>
        /// If it can, the randomizer will roll a new value.
        /// </summary>
        /// <param name="value">The previously rolled value.</param>
        /// <param name="address">Address of the previous region,</param>
        /// <returns></returns>
        private int DecideReroll(int value, int address)
        {
            if (errorChance != 0 && ErrorCheckIsPositive(value)) return value;
            
            if (allowedRerolls <= 0)
            {
                RegisterValue(value, address);
                return value;
            }

            allowedRerolls--;
            return GetNext();
        }

        /// <summary>
        /// Checks if the randomizer made a mistake.
        /// </summary>
        /// <param name="value">The value currently being processed.</param>
        /// <returns></returns>
        private bool ErrorCheckIsPositive(int value) => Random.Range(0f, 1f) < errorChance;

        /// <summary>
        /// Initializes array with default value of "-1."
        /// </summary>
        /// <param name="length">The length of the array.</param>
        /// <returns>An array of full of empty values.</returns>
        private int[] InitArray(int length) => Enumerable.Repeat(EmptyValue, length).ToArray();
        
        public int Min { get => min; }

        public int Max { get => max; }
    }
}