namespace BoubakProductions.Core
{
    /// <summary>
    /// Extension methods for the float type.
    /// </summary>
    public static class FloatExtensions
    {
        /// <summary>
        /// Works like Mathf.Sign but with a 0.
        /// </summary>
        public static float Sign0(this float number)
        {
            return number == 0 ? 0 : number > 0 ? 1 : -1;
        }
    }
}