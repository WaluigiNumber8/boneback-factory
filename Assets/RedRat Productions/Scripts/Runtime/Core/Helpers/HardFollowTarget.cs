using UnityEngine;

namespace RedRats.Core.Helpers
{
    public class HardFollowTarget : MonoBehaviour
    {
        private Transform ttransform;
        private Transform target;

        private void Awake()
        {
            ttransform = transform;
        }

        private void Update()
        {
            if (target == null) return;
            ttransform.position = target.position;
        }
        
        public void SetTarget(Transform newTarget) => target = newTarget;
        public void ClearTarget() => target = null;
    }
}