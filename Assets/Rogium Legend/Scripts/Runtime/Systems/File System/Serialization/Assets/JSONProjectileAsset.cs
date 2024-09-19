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
            return new ProjectileAsset.Builder()
                .WithID(id)
                .WithTitle(title)
                .WithIcon(icon.Decode())
                .WithAuthor(author)
                .WithCreationDate(DateTime.Parse(creationDate))
                .WithColor(color.Decode())
                .WithAssociatedSpriteID(associatedSpriteID)
                .WithAnimationType((AnimationType)animationType)
                .WithFrameDuration(frameDuration)
                .WithIconAlt(iconAlt.Decode())
                .WithBaseDamage(baseDamage)
                .WithUseDelay(useDelay)
                .WithKnockbackForceSelf(knockbackForceSelf)
                .WithKnockbackLockDirectionSelf(knockbackLockDirectionSelf)
                .WithKnockbackForceOther(knockbackForceOther)
                .WithKnockbackLockDirectionOther(knockbackLockDirectionOther)
                .WithFlightSpeed(flightSpeed)
                .WithAcceleration(acceleration)
                .WithBrakeForce(brakeForce)
                .WithPierceType((PierceType)pierceType)
                .Build();
        }
    }
}