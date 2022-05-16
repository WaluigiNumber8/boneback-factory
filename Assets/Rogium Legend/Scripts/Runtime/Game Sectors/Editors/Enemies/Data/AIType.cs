namespace Rogium.Editors.Enemies
{
    /// <summary>
    /// The different types of enemy AI.
    /// </summary>
    public enum AIType
    {
        /// <summary>
        /// Constantly look into 1 direction.
        /// </summary>
        LookInDirection = 0,
        
        /// <summary>
        /// Keeps rotating towards the player.
        /// </summary>
        RotateTowardsPlayer = 1,
    }
}