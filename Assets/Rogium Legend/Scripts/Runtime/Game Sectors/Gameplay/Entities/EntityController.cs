using System;
using System.Collections;
using BoubakProductions.Core;
using Rogium.Gameplay.Core;
using UnityEngine;

namespace Rogium.Gameplay.Entities
{
    /// <summary>
    /// The base for all entities.
    /// </summary>
    [RequireComponent(typeof(Rigidbody2D))]
    public abstract class EntityController : MonoBehaviour
    {
        public virtual event Action OnDeath;
        
        [SerializeField] private new Collider2D collider;
        [SerializeField] private Collider2D trigger;
        [SerializeField] private bool showGizmos;

        private ForceMoveInfo forceMove;

        private Vector3 previousPos;
        private float currentSpeed;
        
        protected Transform ttransform;
        private Rigidbody2D rb;

        protected Vector2 faceDirection;
        protected bool faceDirectionLocked;
        protected bool actionsLocked;
        protected bool movementLocked;
        private Coroutine movementLockCoroutine;
        
        protected virtual void Awake()
        {
            ttransform = transform;
            rb = GetComponent<Rigidbody2D>();
            
            ForceMove(GameplayDefaults.StartingFaceDirection, 0.1f, 0.01f, true);
        }

        protected virtual void FixedUpdate()
        {
            DoForceMovement();
            UpdateParameters();
        }

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
        /// Locks the entities movement for a certain amount of time.
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
            forceMove.moveDirection = direction.normalized;
            forceMove.force = force;
            forceMove.timer = Time.time + time;
            forceMove.activated = true;

            faceDirectionLocked = lockFaceDirection;
            actionsLocked = true;
        }

        /// <summary>
        /// Handles Forced Movement processing.
        /// </summary>
        private void DoForceMovement()
        {
            if (!forceMove.activated) return;
            
            if (Time.time > forceMove.timer)
            {
                forceMove.activated = false;
                actionsLocked = false;
                WhenForceMoveEnd();
                return;
            }
            
            rb.MovePosition(rb.position + forceMove.force * 10 * Time.fixedDeltaTime * forceMove.moveDirection);
        }

        /// <summary>
        /// Updates the readable entity parameters.
        /// </summary>
        private void UpdateParameters()
        {
            Vector3 velocityChange = (ttransform.position - previousPos);
            currentSpeed = velocityChange.magnitude * 1000f;
            UpdateFaceDirection();
            previousPos = ttransform.position;
            
            void UpdateFaceDirection()
            {
                if (faceDirectionLocked) return;
                faceDirection = (Vector3.Distance(velocityChange, Vector3.zero) > 0.01f) ? velocityChange.normalized.Round() : faceDirection;
            }
        }

        private void WhenForceMoveEnd()
        {
            faceDirectionLocked = false;
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