using System;
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
                                       baseDamage,
                                       useDelay,
                                       knockbackForceSelf,
                                       knockbackForceOther,
                                       DateTime.Parse(creationDate));
        }
    }
}