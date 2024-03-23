using UnityEngine;

namespace Rogium.Gameplay.Entities.Characteristics
{
    /// <summary>
    /// Gives entities the power to move.
    /// </summary>
    public class CharacteristicMove : CharacteristicBase
    {
        [SerializeField] private CharMoveInfo defaultData;

        /// <summary>
        /// Constructs the characteristic.
        /// </summary>
        /// <param name="newInfo">New data to use.</param>
        public void Construct(CharMoveInfo newInfo) => defaultData = newInfo;
        
        /// <summary>
        /// Move in a specific direction.
        /// </summary>
        /// <param name="direction">The direction of the movement.</param>
        public void Move(Vector2 direction)
        {
            Rigidbody2D rb = entity.Rigidbody;
            Vector2 force = (direction != Vector2.zero) ? rb.velocity + 100 * defaultData.acceleration * direction : rb.velocity * -defaultData.brakeForce;
            float maxSpeed = (defaultData.maxSpeed * 0.01f * direction).magnitude;
            rb.AddForce(force, ForceMode2D.Force);
            if (direction != Vector2.zero) rb.velocity = Vector2.ClampMagnitude(rb.velocity, maxSpeed);
        }
        
        /// <summary>
        /// Stops all movement.
        /// </summary>
        public void Stop() => entity.Rigidbody.velocity = Vector2.zero;

    }
}