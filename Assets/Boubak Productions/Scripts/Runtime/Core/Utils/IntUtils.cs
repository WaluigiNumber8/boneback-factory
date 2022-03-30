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
        /// <param name="value">The value to wrap and return.</param>
        /// <param name="min">Range Minimum. (inclusive)</param>
        /// <param name="max">Range Maximum. (inclusive)</param>
        /// <returns>The new index position.</returns>
        public static int Wrap(int value, int min, int max)
        {
            return (value > max) ? min : ((value < min) ? max : value);
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