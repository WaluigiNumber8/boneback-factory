namespace Rogium.Gameplay.Entities
{
    /// <summary>
    /// Contains data required for a forced movement.
    /// </summary>
    [System.Serializable]
    public struct ForcedMoveInfo
    {
        public float forceSpeed;
        public float time;

        public ForcedMoveInfo(float forceSpeed, float time)
        {
            this.forceSpeed = forceSpeed;
            this.time = time;
        }
    }
}