namespace Rogium.Editors.Projectiles
{
    /// <summary>
    /// All the different types a projectile can pierce objects.
    /// </summary>
    public enum PierceType
    {
        /// <summary>
        /// Is blocked by everything.
        /// </summary>
        None = 0,
        /// <summary>
        /// Pierces through entities.
        /// </summary>
        Entities = 1,
        /// <summary>
        /// Pierces through walls.
        /// </summary>
        Walls = 2,
        /// <summary>
        /// Pierces through everything.
        /// </summary>
        All = 3,
    }
}