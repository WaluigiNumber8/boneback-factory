using Rogium.Gameplay.Core;
using UnityEngine;

namespace Rogium.Gameplay.Entities
{
    [RequireComponent(typeof(Rigidbody2D))]
    public abstract class EntityController : MonoBehaviour
    {
        [SerializeField] private new Collider2D collider;
        [SerializeField] private Collider2D trigger;
        [SerializeField] private bool showGizmos;

        private ForceMoveInfo forceMove;

        private UpdateFaceDirectionType faceDirectionType;
        private Vector3 previousPos;
        private float currentSpeed;
        
        protected Transform ttransform;
        private Rigidbody2D rb;

        protected Vector2Int faceDirection;
        protected bool actionsLocked;
        
        protected virtual void Awake()
        {
            ttransform = transform;
            rb = GetComponent<Rigidbody2D>();
            
            faceDirection = GameplayDefaults.StartingFaceDirection;
        }

        protected virtual void FixedUpdate()
        {
            DoForceMovement();
            UpdateParameters();
        }

        /// <summary>
        /// Enables/Disables the entities ability to collide with objects and trigger events.
        /// </summary>
        /// <param name="enable">When on, collision is enabled.</param>
        public void SetCollideMode(bool enable)
        {
            actionsLocked = !enable;
            collider.enabled = enable;
            trigger.enabled = enable;
        }

        /// <summary>
        /// Changes the way the face direction is updated.
        /// </summary>
        /// <param name="type"></param>
        public void SetFaceDirectionUpdateType(UpdateFaceDirectionType type) => faceDirectionType = type;
        
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
            forceMove.moveDirection = direction.normalized;
            forceMove.force = force;
            forceMove.timer = Time.time + time;
            forceMove.activated = true;

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
            Vector3 velocityChangeNormalized = (velocityChange * 0.01f).normalized;
            previousPos = ttransform.position;

            faceDirection = (velocityChange != Vector3.zero) ? new Vector2Int(Mathf.RoundToInt(velocityChangeNormalized.x), Mathf.RoundToInt(velocityChangeNormalized.y)) : faceDirection;
            currentSpeed = velocityChange.magnitude * 100f;
            
            
        }
        
        protected virtual void WhenForceMoveEnd() { }
        
        protected void OnDrawGizmos()
        {
            if (!showGizmos) return;
            Gizmos.color = Color.yellow;
        }

        public Transform Transform { get => ttransform; }
        public Rigidbody2D Rigidbody { get => rb; }
        public Vector2Int FaceDirection { get => faceDirection; }
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