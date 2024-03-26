using RedRats.Core;
using RedRats.Systems.Clocks;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Rogium.Gameplay.Entities
{
    /// <summary>
    /// The base for all entities.
    /// </summary>
    [RequireComponent(typeof(Rigidbody2D))]
    public abstract class EntityController : MonoBehaviour
    {
        [Title("Collision")]
        [SerializeField] private new Collider2D collider;
        [SerializeField] private Collider2D trigger;
        [SerializeField] private bool showGizmos;

        private Transform ttransform;
        private Rigidbody2D rb;

        protected Vector2 faceDirection;
        private Vector3 lastPos;
        private float currentSpeed;
        private CountdownTimer movementLockTimer;
        private CountdownTimer faceDirectionLockTimer;
        protected bool movementLocked;
        protected bool faceDirectionLocked;
        protected bool actionsLocked;

        protected virtual void Awake()
        {
            ttransform = transform;
            rb = GetComponent<Rigidbody2D>();
            movementLockTimer = new CountdownTimer(() => movementLocked = false, () => movementLocked = true);
            faceDirectionLockTimer = new CountdownTimer(() => faceDirectionLocked = false, () => faceDirectionLocked = true);
        }

        protected virtual void Update() => UpdateParameters();

        /// <summary>
        /// Enables/Disables the entities ability to collide with objects and trigger events.
        /// </summary>
        /// <param name="isEnabled">Changes the collision state.</param>
        public void ChangeCollideMode(bool isEnabled)
        {
            actionsLocked = !isEnabled;
            if (collider != null) collider.enabled = isEnabled;
            if (trigger != null) trigger.enabled = isEnabled;
        }
        
        /// <summary>
        /// Stops the entity's movement.
        /// </summary>
        public void StopMoving()
        {
            rb.AddForce(-rb.velocity, ForceMode2D.Impulse);
        }

        /// <summary>
        /// Locks the entity's movement for a certain amount of time.
        /// </summary>
        /// <param name="time">The time to lock movement for.</param>
        public void LockMovement(float time)
        {
            // Do nothing if movement is locked and new time is less than the current time.
            if (movementLocked && movementLockTimer.TimeLeft > time) return;
            movementLockTimer.Start(time);
        }
        
        /// <summary>
        /// Locks the entity's direction facing for a certain amount of time.
        /// </summary>
        /// <param name="time">The time to lock face direction for.</param>
        public void LockFaceDirection(float time)
        {
            // Do nothing if faceDirection is locked and new time is less than the current time.
            if (faceDirectionLocked && faceDirectionLockTimer.TimeLeft > time) return;
            faceDirectionLockTimer.Start(time);
        }
        
        /// <summary>
        /// Force movement in a specific direction.
        /// </summary>
        /// <param name="direction">The direction of the movement.</param>
        /// <param name="forceInfo">The data to use for the force.</param>
        public void ForceMove(Vector2 direction, ForcedMoveInfo forceInfo)
        {
            ForceMove(direction, forceInfo.force, forceInfo.lockInput, forceInfo.lockFaceDirection);
        }

        /// <summary>
        /// Force movement in a specific direction.
        /// </summary>
        /// <param name="direction">The direction of the movement.</param>
        /// <param name="force">The force of the movement.</param>
        /// <param name="lockInput">Lock entity's movement inputs.</param>
        /// <param name="lockFaceDirection">Lock the face direction during the movement.</param>
        public void ForceMove(Vector2 direction, float force, bool lockInput = false, bool lockFaceDirection = false)
        {
            Vector2 f =  10 * force * direction;
            rb.AddForce(rb.velocity + f, ForceMode2D.Impulse);

            if (!lockInput && !lockFaceDirection) return;
            float time = 0.21f * Mathf.Exp(0.05f * RedRatUtils.GetTimeOfForce(f.magnitude, rb) + 0.01f); // The time increases with time exponentially.
                                                                                                         // For the love of god, DON'T TOUCH THE NUMBERS.
            if (lockInput) LockMovement(time);
            if (lockFaceDirection) LockFaceDirection(time * 1.6f);
        }

        /// <summary>
        /// Updates the readable entity parameters.
        /// </summary>
        private void UpdateParameters()
        {
            movementLockTimer.Tick();
            faceDirectionLockTimer.Tick();
            
            currentSpeed = (ttransform.position - lastPos).sqrMagnitude * 1000;
            if (!faceDirectionLocked) UpdateFaceDirection();
            
            lastPos = ttransform.position;
        }
        
        /// <summary>
        /// Updates the Entity's Face Direction.
        /// </summary>
        protected virtual void UpdateFaceDirection()
        {
            faceDirection = (currentSpeed > 0.001f) ? (ttransform.position - lastPos).normalized : faceDirection;
        }

        protected void OnDrawGizmos()
        {
            if (!showGizmos) return;
            Gizmos.color = Color.yellow;
        }
        
        public Transform TTransform { get => ttransform; }
        public Rigidbody2D Rigidbody { get => rb; }
        public Vector2 FaceDirection { get => faceDirection; }
        public float CurrentSpeed { get => currentSpeed; }
        public bool ActionsLocked { get => actionsLocked; }
    }
}