namespace Rogium.Gameplay.Entities.Characteristics
{
    /// <summary>
    /// Contains data of the <see cref="CharacteristicMove"/> characteristic.
    /// </summary>
    [System.Serializable]
    public struct CharMoveInfo
    {
        public float maxSpeed;
        public float acceleration;
        public float brakeForce;

        public CharMoveInfo(float maxSpeed, float acceleration, float brakeForce)
        {
            this.maxSpeed = maxSpeed;
            this.acceleration = acceleration;
            this.brakeForce = brakeForce;
        }
    }
}