namespace RedRats.Systems.Particles
{
    /// <summary>
    /// The different ways an effect can follow a target's rotation.
    /// </summary>
    public enum FollowRotationType
    {
        /// <summary>
        /// The effect does not follow the target's rotation.
        /// </summary>
        NoFollow = 0,
        /// <summary>
        /// The effect follows the target's rotation.
        /// </summary>
        Follow = 1,
        /// <summary>
        /// The effect aligns its rotation with the target when the effect begins.
        /// </summary>
        AlignOnPlay = 2,
        /// <summary>
        /// Set the rotation of the effect to a custom value.
        /// </summary>
        Custom = 3,
    }
}