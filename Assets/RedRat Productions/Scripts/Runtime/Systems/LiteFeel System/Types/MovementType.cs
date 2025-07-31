namespace RedRats.Systems.LiteFeel.Effects
{
    /// <summary>
    /// The different types an object can move.
    /// </summary>
    public enum MovementType
    {
        /// <summary>
        /// Move in world space.
        /// </summary>
        Absolute = 0,
        /// <summary>
        /// Move relatively to own position.
        /// </summary>
        Relative = 1,
    }
}