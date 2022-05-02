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
        public SerializedProjectileAsset(ProjectileAsset asset) : base(asset)
        {
            
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
                                       DateTime.Parse(creationDate));
        }
    }
}