namespace RedRats.Systems.Randomization
{
    public interface IRandomizer
    {
        /// <summary>
        /// Gets the next random value.
        /// </summary>
        /// <returns>A new random value.</returns>
        public int GetNext();
        
        public int Min { get; }
        public int Max { get; }
    }
}