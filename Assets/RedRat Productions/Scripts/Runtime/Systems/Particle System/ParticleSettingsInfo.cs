using UnityEngine;

namespace RedRats.Systems.Particles
{
    /// <summary>
    /// Contains settings for a particle effect.
    /// </summary>
    public struct ParticleSettingsInfo
    {
        public readonly Transform target;
        public readonly Vector3 offsetPos;
        public readonly Vector3 offsetRot;
        public readonly bool followTarget;
        public readonly FollowRotationType followRotation;
        public readonly int id;

        public ParticleSettingsInfo(Transform target, Vector3 offset, bool followTarget = false, FollowRotationType followRotation = FollowRotationType.NoFollow,
                                    Vector3 offsetRot = new(), int id = 0)
        {
            this.target = target;
            this.offsetPos = offset;
            this.followTarget = followTarget;
            this.followRotation = followRotation;
            this.offsetRot = offsetRot;
            this.id = id;
        }
    }
}