using System;
using Rogium.Editors.Core;
using Rogium.Editors.Projectiles;

namespace Rogium.ExternalStorage.Serialization
{
    /// <summary>
    /// Serialized form of the <see cref="ProjectileAsset"/>.
    /// </summary>
    [System.Serializable]
    public class JSONProjectileAsset : JSONEntityAssetBase<ProjectileAsset>
    {
        public float flightSpeed;
        public float acceleration;
        public float brakeForce;
        public int pierceType;
        
        public JSONProjectileAsset(ProjectileAsset asset) : base(asset)
        {
            flightSpeed = asset.FlightSpeed;
            acceleration = asset.Acceleration;
            brakeForce = asset.BrakeForce;
            pierceType = (int)asset.PierceType;
        }

        public override ProjectileAsset Decode()
        {
            return new ProjectileAsset(id,
                                       title,
                                       icon.Decode(),
                                       author,
                                       associatedSpriteID,
                                       (AnimationType)animationType,
                                       frameDuration,
                                       iconAlt.Decode(),
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