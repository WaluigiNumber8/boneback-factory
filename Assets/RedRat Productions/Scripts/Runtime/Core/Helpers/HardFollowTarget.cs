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
        private Vector3 offsetPos;
        
        private bool followRotation;
        private Vector3 offsetRot;

        private void Awake() => ttransform = transform;

        private void Update()
        {
            if (target == null) return;
            ttransform.position = target.position + offsetPos;
            
            if (!followRotation) return;
            ttransform.eulerAngles = target.eulerAngles + offsetRot;
        }
        
        public void SetTarget(Transform target) => SetTarget(target, Vector3.zero);
        public void SetTarget(Transform target, Vector3 offsetPos)
        {
            this.target = target;
            this.offsetPos = offsetPos;
            this.followRotation = false;
        }
        
        public void EnableRotationFollow(Vector3 offsetRot = new())
        {
            this.followRotation = true;
            this.offsetRot = offsetRot;
        }

        public void ClearTarget() => target = null;
    }
}