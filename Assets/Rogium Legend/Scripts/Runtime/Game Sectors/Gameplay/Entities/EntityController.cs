using System.Collections;
using RedRats.Core;
using UnityEngine;

namespace Rogium.Gameplay.Entities
{
    /// <summary>
    /// The base for all entities.
    /// </summary>
    [RequireComponent(typeof(Rigidbody2D))]
    public abstract class EntityController : MonoBehaviour
    {
        [SerializeField] private new Collider2D collider;
        [SerializeField] private Collider2D trigger;
        [SerializeField] private bool showGizmos;

        protected Transform ttransform;
        private Rigidbody2D rb;
        private ForceMoveInfo forceMove;

        private Vector2 previousPos;
        private float currentSpeed;
        private Vector2 velocityChange;
        protected Vector2 faceDirection;
        protected bool faceDirectionLocked;
        protected bool actionsLocked;
        protected bool movementLocked;
        private Coroutine movementLockCoroutine;

        protected virtual void Awake()
        {
            ttransform = transform;
            rb = GetComponent<Rigidbody2D>();
        }

        protected virtual void Update() => UpdateParameters();
        protected virtual void FixedUpdate() => DoForceMovement();

        /// <summary>
        /// Enables/Disables the entities ability to collide with objects and trigger events.
        /// </summary>
        /// <param name="isEnabled">Changes the collision state.</param>
        public virtual void ChangeCollideMode(bool isEnabled)
        {
            actionsLocked = !isEnabled;
            if (collider != null) collider.enabled = isEnabled;
            if (trigger != null) trigger.enabled = isEnabled;
        }

        /// <summary>
        /// Locks the entity's movement for a certain amount of time.
        /// </summary>
        /// <param name="time">The time to lock movement for.</param>
        public void LockMovement(float time)
        {
            if (movementLockCoroutine != null) StopCoroutine(movementLockCoroutine);
            movementLockCoroutine = StartCoroutine(MovementLockCoroutine());
            IEnumerator MovementLockCoroutine()
            {
                movementLocked = true;
                yield return new WaitForSeconds(time);
                movementLocked = false;
            }
        }
        
        /// <summary>
        /// Force movement in a specific direction.
        /// </summary>
        /// <param name="direction">The direction of the movement.</param>
        /// <param name="forceInfo">The data to use for the force.</param>
        public void ForceMove(Vector2 direction, ForcedMoveInfo forceInfo)
        {
            ForceMove(direction, forceInfo.forceSpeed, forceInfo.time, forceInfo.lockFaceDirection);
        }

        /// <summary>
        /// Force movement in a specific direction.
        /// </summary>
        /// <param name="direction">The direction of the movement.</param>
        /// <param name="force">The force of the movement.</param>
        /// <param name="time">The time to take for the movement.</param>
        /// <param name="lockFaceDirection">Lock the face direction during the movement.</param>
        public void ForceMove(Vector2 direction, float force, float time, bool lockFaceDirection)
        {
            actionsLocked = true;
            faceDirectionLocked = lockFaceDirection;
            
            forceMove.moveDirection = direction.normalized;
            forceMove.force = force;
            forceMove.timer = Time.time + time;
            forceMove.activated = true;
        }

        /// <summary>
        /// Handles Forced Movement processing.
        /// </summary>
        private void DoForceMovement()
        {
            if (!forceMove.activated) return;
            if (Time.time > forceMove.timer)
            {
                faceDirectionLocked = false;
                actionsLocked = false;
                forceMove.activated = false;
                return;
            }
            rb.MovePosition(rb.position + forceMove.force * 10 * Time.fixedDeltaTime * forceMove.moveDirection);
        }

        /// <summary>
        /// Updates the readable entity parameters.
        /// </summary>
        private void UpdateParameters()
        {
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
        
        private struct ForceMoveInfo
        {
            public Vector2 moveDirection;
            public float force;
            public float timer;
            public bool activated;
        }
    }
}