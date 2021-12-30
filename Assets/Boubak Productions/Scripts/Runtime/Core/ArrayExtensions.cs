using UnityEngine;

namespace BoubakProductions.Core
{
    /// <summary>
    /// Contains helpful methods for working with arrays.
    /// </summary>
    public static class ArrayExtensions
    {
        public static Color[] GenerateEmptyColorArray(int size)
        {
            Color[] array = new Color[size];
            for (int i = 0; i < array.Length; i++)
            {
                array[i] = Color.black;
            }
            return array;
        }
    }
}