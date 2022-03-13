using Rogium.Gameplay.Core;
using UnityEngine;

namespace Rogium.Gameplay.Entities
{
    [RequireComponent(typeof(Rigidbody2D))]
    public abstract class EntityController : MonoBehaviour
    {
        [SerializeField] private bool showGizmos;
        
        protected Transform ttransform;
        protected Rigidbody2D rb;

        protected Vector2 faceDirection;
        protected bool actionsLocked;
        
        private ForceMoveInfo forceMove;
        
        protected virtual void Awake()
        {
            ttransform = transform;
            rb = GetComponent<Rigidbody2D>();
            faceDirection = GameplayDefaults.FaceDirection;
        }

        protected virtual void FixedUpdate()
        {
            DoForceMovement();
        }

        /// <summary>
        /// Force movement in a specific direction.
        /// </summary>
        /// <param name="direction">The direction of the movement.</param>
        /// <param name="forceInfo">The data to use for the force.</param>
        public void ForceMove(Vector2 direction, ForcedMoveInfo forceInfo)
        {
            ForceMove(direction, forceInfo.forceSpeed, forceInfo.time);
        }
        /// <summary>
        /// Force movement in a specific direction.
        /// </summary>
        /// <param name="direction">The direction of the movement.</param>
        /// <param name="force">The force of the movement.</param>
        /// <param name="time">The time to take for the movement.</param>
        public void ForceMove(Vector2 direction, float force, float time)
        {
            forceMove.moveDirection = direction;
            forceMove.force = force;
            forceMove.timer = Time.time + time;
            forceMove.activated = true;

            faceDirection = direction;
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
            
            rb.MovePosition(rb.position + forceMove.moveDirection * forceMove.force * 10 * Time.fixedDeltaTime);
        }

        protected virtual void WhenForceMoveEnd() { }

        protected void OnDrawGizmos()
        {
            if (!showGizmos) return;
            Gizmos.color = Color.yellow;
            Gizmos.DrawRay(transform.position, faceDirection * 1f);
        }

        public Transform Transform { get => ttransform; }
        public Rigidbody2D Rigidbody { get => rb; }
        public Vector2 FaceDirection { get => faceDirection; }

        private struct ForceMoveInfo
        {
            public Vector2 moveDirection;
            public float force;
            public float timer;
            public bool activated;
        }
    }
}