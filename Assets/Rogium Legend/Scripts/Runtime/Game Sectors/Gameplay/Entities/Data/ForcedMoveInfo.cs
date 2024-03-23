namespace Rogium.Gameplay.Entities
{
    /// <summary>
    /// Contains data required for a forced movement.
    /// </summary>
    [System.Serializable]
    public struct ForcedMoveInfo
    {
        public float force;
        public bool lockInput;
        public bool lockFaceDirection;

        public ForcedMoveInfo(float force, float lockInput, bool lockFaceDirection)
        {
            this.force = force;
            this.lockInput = lockInput > 0;
            this.lockFaceDirection = lockFaceDirection;
        }
        
        public ForcedMoveInfo(float force, bool lockInput, bool lockFaceDirection)
        {
            this.force = force;
            this.lockInput = lockInput;
            this.lockFaceDirection = lockFaceDirection;
        }
    }
}