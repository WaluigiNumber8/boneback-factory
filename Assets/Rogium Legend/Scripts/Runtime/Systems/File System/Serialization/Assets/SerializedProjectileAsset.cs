using System;
using Rogium.Editors.Core;
using Rogium.Editors.Projectiles;

namespace Rogium.ExternalStorage.Serialization
{
    /// <summary>
    /// Serialized form of the <see cref="ProjectileAsset"/>.
    /// </summary>
    [System.Serializable]
    public class SerializedProjectileAsset : SerializedEntityAssetBase<ProjectileAsset>
    {
        private float flightSpeed;
        private float acceleration;
        private float brakeForce;
        private int pierceType;
        
        public SerializedProjectileAsset(ProjectileAsset asset) : base(asset)
        {
            flightSpeed = asset.FlightSpeed;
            acceleration = asset.Acceleration;
            brakeForce = asset.BrakeForce;
            pierceType = (int)asset.PierceType;
        }

        public override ProjectileAsset Deserialize()
        {
            return new ProjectileAsset(id,
                                       title,
                                       icon.Deserialize(),
                                       author,
                                       (AnimationType)animationType,
                                       frameDuration,
                                       iconAlt.Deserialize(),
                                       baseDamage,
                                       useDelay,
                                       knockbackForceSelf,
                                       knockbackTimeSelf,
                                       knockbackLockDirectionSelf,
                                       knockbackForceOther,
                                       knockbackTimeOther,
                                       knockbackLockDirectionOther,
                                       flightSpeed,
                                       acceleration,
                                       brakeForce,
                                       (PierceType)pierceType,
                                       DateTime.Parse(creationDate));
        }
    }
}