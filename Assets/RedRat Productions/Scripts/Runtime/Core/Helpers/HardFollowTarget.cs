using UnityEngine;

namespace RedRats.Core.Helpers
{
    /// <summary>
    /// Snaps to a specified target.
    /// </summary>
    public class HardFollowTarget : MonoBehaviour
    {
        private Transform ttransform;
        private Transform target;
        private Vector3 offset;

        private void Awake() => ttransform = transform;

        private void Update()
        {
            if (target == null) return;
            ttransform.position = target.position + offset;
        }
        
        public void SetTarget(Transform target) => SetTarget(target, Vector3.zero);
        public void SetTarget(Transform target, Vector3 offset)
        {
            this.target = target;
            this.offset = offset;
        }

        public void ClearTarget() => target = null;
    }
}