using UnityEngine;

namespace RedRats.Systems.Audio
{
    /// <summary>
    /// Contains settings needed for a 3D sound.
    /// </summary>
    [System.Serializable]
    public struct AudioSpatialSettingsInfo
    {
        [Range(0f, 1f)] public float spatialBlend;
        public Transform soundTarget;
        public float minDistance;
        public float maxDistance;
        
        public AudioSpatialSettingsInfo(float spatialBlend, Transform soundTarget, float minDistance = 2.5f, float maxDistance = 4f)
        {
            this.spatialBlend = spatialBlend;
            this.soundTarget = soundTarget;
            this.minDistance = minDistance;
            this.maxDistance = maxDistance;
        }
    }
}