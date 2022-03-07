using UnityEngine;

namespace Rogium.Gameplay.Entities.Characteristics
{
    /// <summary>
    /// Gives entities the power to move.
    /// </summary>
    public class CharacteristicMove : MonoBehaviour
    {
        [SerializeField] private EntityController entity;
        [SerializeField] private CharMoveInfo data;

        private Vector2 lastDirection;
        private float accel;
        
        /// <summary>
        /// Constructs the characteristic.
        /// </summary>
        /// <param name="newInfo">New data to use.</param>
        public void Construct(CharMoveInfo newInfo) => data = newInfo;
        
        /// <summary>
        /// Move in a specific direction.
        /// </summary>
        /// <param name="direction">The direction of the movement.</param>
        public void Move(Vector2 direction)
        {
            CalculateAcceleration(direction);
            
            lastDirection = (direction == Vector2.zero) ? lastDirection : direction;
            float speed = data.maxSpeed * accel;
            
            entity.Rigidbody.MovePosition(entity.Rigidbody.position + lastDirection * speed * Time.fixedDeltaTime);
        }

        /// <summary>
        /// Calculates the acceleration of the movement.
        /// </summary>
        /// <param name="direction">The direction to move in.</param>
        private void CalculateAcceleration(Vector2 direction)
        {
            if (direction == Vector2.zero)
            {
                accel = Mathf.Max(0, accel -= data.brakeForce);
                return;
            }

            accel = Mathf.Min(accel += data.acceleration, 1);
        }

    }
}