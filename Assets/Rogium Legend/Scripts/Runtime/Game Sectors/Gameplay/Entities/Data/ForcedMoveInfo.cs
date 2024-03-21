namespace Rogium.Gameplay.Entities
{
    /// <summary>
    /// Contains data required for a forced movement.
    /// </summary>
    [System.Serializable]
    public struct ForcedMoveInfo
    {
        public float force;
        public float lockInputTime;
        public bool lockFaceDirection;

        public ForcedMoveInfo(float force, float lockInputTime, bool lockFaceDirection)
        {
            this.force = force;
            this.lockInputTime = lockInputTime;
            this.lockFaceDirection = lockFaceDirection;
        }
    }
}