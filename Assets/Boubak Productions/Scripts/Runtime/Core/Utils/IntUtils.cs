namespace BoubakProductions.Core
{
    /// <summary>
    /// Contains various utilities for working with integers.
    /// </summary>
    public static class IntUtils
    {
        /// <summary>
        /// Wraps an index in a range of integers (both inclusive).
        /// </summary>
        /// <param name="index">The index to wrap.</param>
        /// <param name="min">Range Minimum.</param>
        /// <param name="max">Range Maximum.</param>
        /// <returns>The new index position.</returns>
        public static int Wrap(int index, int min, int max)
        {
            return (index > max) ? min : ((index < min) ? max : index);
        }

        /// <summary>
        /// Flips a value in a range to other side.
        /// </summary>
        /// <param name="value">The value to flip.</param>
        /// <param name="rangeLength">Length of the range.</param>
        /// <returns></returns>
        public static int Flip(int value, int rangeLength)
        {
            return (rangeLength - value) - 1;
        }
    }
}