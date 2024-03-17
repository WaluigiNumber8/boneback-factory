using System;
using UnityEngine;

namespace Rogium.UserInterface.Editors.Utils
{
    /// <summary>
    /// Follows a specific position.
    /// </summary>
    public class UIPositionFollower : MonoBehaviour
    {
        public event Action OnBeginMove;
        
        [SerializeField] private float speed = 2f;
        
        private RectTransform ttransform;
        private Vector2 targetPosition;
        private bool follow;

        private void Awake() => ttransform = GetComponent<RectTransform>();

        private void Update()
        {
            if (!follow) return;
            ttransform.localPosition = Vector2.Lerp(ttransform.localPosition, targetPosition, Time.deltaTime * speed);
        }

        /// <summary>
        /// Sets the target position to follow.
        /// </summary>
        /// <param name="position">The new position to follow.</param>
        public void SetTargetAndGo(Vector2 position)
        {
            targetPosition = position;
            follow = true;
            OnBeginMove?.Invoke();
        }
        
        /// <summary>
        /// Change if the object should follow the target position.
        /// </summary>
        /// <param name="follow">TRUE if the object should follow.</param>
        public void ChangeFollowState(bool follow) => this.follow = follow;
    }
}