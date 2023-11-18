namespace RedRats.Systems.Effectors.Effects
{
    /// <summary>
    /// The different types an object moves relative to its parent.
    /// </summary>
    public enum WorldType
    {
        /// <summary>
        /// Move in world coordinates.
        /// </summary>
        World = 0,
        /// <summary>
        /// Move in local coordinates.
        /// </summary>
        Local = 1
    }
}