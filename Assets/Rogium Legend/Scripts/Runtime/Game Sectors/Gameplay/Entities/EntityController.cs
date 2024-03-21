using System.Collections;
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

        protected Transform ttransform;
        private Rigidbody2D rb;

        private Vector2 previousPos;
        private float currentSpeed;
        private Vector2 velocityChange;
        protected Vector2 faceDirection;
        protected bool faceDirectionLocked;
        protected bool actionsLocked;
        protected bool movementLocked;
        private CountdownTimer movementLockTimer;
        private CountdownTimer faceDirectionLockTimer;

        protected virtual void Awake()
        {
            ttransform = transform;
            rb = GetComponent<Rigidbody2D>();
            movementLockTimer = new CountdownTimer(() => movementLocked = true, () => movementLocked = false);
            faceDirectionLockTimer = new CountdownTimer(() => faceDirectionLocked = true, () => faceDirectionLocked = false);
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
            // Do nothing if movement is locked and new time is less than the current time.
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
            ForceMove(direction, forceInfo.force, forceInfo.lockInputTime, forceInfo.lockFaceDirection);
        }
        /// <summary>
        /// Force movement in a specific direction.
        /// </summary>
        /// <param name="direction">The direction of the movement.</param>
        /// <param name="force">The force of the movement.</param>
        /// <param name="lockInputTime">The time to take for the movement.</param>
        /// <param name="lockFaceDirection">Lock the face direction during the movement.</param>
        public void ForceMove(Vector2 direction, float force, float lockInputTime = 0f, bool lockFaceDirection = true)
        {
            if (lockInputTime > 0) LockMovement(lockInputTime);
            if (lockFaceDirection) LockFaceDirection(lockInputTime);
            
            rb.AddForce(rb.velocity + force * 10000 * direction);
        }

        /// <summary>
        /// Updates the readable entity parameters.
        /// </summary>
        private void UpdateParameters()
        {
            movementLockTimer.Tick();
            faceDirectionLockTimer.Tick();
            
            if (Time.frameCount % 3 != 0) return;
            if (!faceDirectionLocked) UpdateFaceDirection();
            
            velocityChange = (rb.position - previousPos) / Time.deltaTime;
            currentSpeed = velocityChange.magnitude;
            previousPos = rb.position;
        }
        
        /// <summary>
        /// Updates the Entity's Face Direction.
        /// </summary>
        protected virtual void UpdateFaceDirection()
        {
            faceDirection = (velocityChange.IsZero(0.05f)) ? velocityChange.normalized.Round() : faceDirection;
        }

        protected void OnDrawGizmos()
        {
            if (!showGizmos) return;
            Gizmos.color = Color.yellow;
        }
        
        public Transform Transform { get => ttransform; }
        public Rigidbody2D Rigidbody { get => rb; }
        public Vector2 FaceDirection { get => faceDirection; }
        public float CurrentSpeed { get => currentSpeed; }
        public bool ActionsLocked { get => actionsLocked; }
    }
}