using UnityEngine;

namespace Rogium.Gameplay.Entities.Characteristics
{
    /// <summary>
    /// Gives entities the power to move.
    /// </summary>
    public class CharacteristicMove : CharacteristicBase
    {
        [SerializeField] private CharMoveInfo defaultData;
        [SerializeField] private ForceMode2D breakForceType = ForceMode2D.Force;
        
        private Rigidbody2D rb;
        
        private Vector2 direction;
        private float maxSpeed;
        private float accel;
        private float brakeForce;

        private void Start()
        {
            rb = entity.Rigidbody;
            Construct(defaultData);
        }

        /// <summary>
        /// Constructs the characteristic.
        /// </summary>
        /// <param name="newInfo">New data to use.</param>
        public void Construct(CharMoveInfo newInfo)
        {
            defaultData = newInfo;
            
            maxSpeed = 0.1f * defaultData.maxSpeed;
            accel = 1000 * defaultData.acceleration;
            brakeForce = 100 * defaultData.brakeForce;
        }

        /// <summary>
        /// Move in a specific direction.
        /// </summary>
        /// <param name="direction">The direction of the movement.</param>
        public void Move(Vector2 direction)
        {
            Vector2 force = (direction != Vector2.zero) ? rb.velocity + accel * direction : Vector2.zero;
            Vector2 decelerationForce = (direction != Vector2.zero) ? Vector2.zero : -brakeForce * rb.velocity;
            
            rb.AddForce(force, ForceMode2D.Force);
            rb.AddForce(decelerationForce, breakForceType);
            rb.velocity = Vector2.ClampMagnitude(rb.velocity, maxSpeed);
        }
    }
}