namespace RedRats.Systems.Effectors.Effects
{
    /// <summary>
    /// The different types of transitions between 2 parameters. 
    /// </summary>
    public enum TransitionType
    {
        /// <summary>
        /// Go from current value to a set destination value.
        /// </summary>
        ToDestination = 0,
        /// <summary>
        /// Go between 2 set values.
        /// </summary>
        AtoB = 1,
    }
}