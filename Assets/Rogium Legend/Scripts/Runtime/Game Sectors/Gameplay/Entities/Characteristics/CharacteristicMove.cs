using System;
using UnityEngine;

namespace Rogium.Gameplay.Entities.Characteristics
{
    /// <summary>
    /// Gives entities the power to move.
    /// </summary>
    public class CharacteristicMove : CharacteristicBase
    {
        [SerializeField] private CharMoveInfo defaultData;
        
        private Rigidbody2D rb;

        private void Start() => rb = entity.Rigidbody;

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
            float maxSpeed = 0.1f * defaultData.maxSpeed;
            float accel = 1000 * defaultData.acceleration;
            float brakeForce = 100 * defaultData.brakeForce;
            
            Vector2 force = (direction != Vector2.zero) ? rb.velocity + accel * direction : rb.velocity * -brakeForce;
            rb.AddForce(force, ForceMode2D.Force);
            rb.velocity = Vector2.ClampMagnitude(rb.velocity, maxSpeed);
            
            Debug.Log($"{rb.velocity.magnitude}/{maxSpeed}");
        }
        
        /// <summary>
        /// Stops all movement.
        /// </summary>
        public void Stop() => entity.Rigidbody.velocity = Vector2.zero;

    }
}