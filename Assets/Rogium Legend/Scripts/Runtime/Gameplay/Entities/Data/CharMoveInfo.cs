using UnityEngine;

namespace Rogium.Gameplay.Entities.Characteristics
{
    /// <summary>
    /// Contains data of the <see cref="CharacteristicMove"/>
    /// </summary>
    [System.Serializable]
    public struct CharMoveInfo
    {
        public float maxSpeed;
        [Range(0.005f, 0.2f)] public float acceleration;
        [Range(0.005f, 0.2f)] public float brakeForce;
    }
}