using System;
using Rogium.Editors.Core;
using Rogium.Editors.Weapons;

namespace Rogium.ExternalStorage.Serialization
{
    /// <summary>
    /// Serialized form of the <see cref="WeaponAsset"/>.
    /// </summary>
    [System.Serializable]
    public class SerializedWeaponAsset : SerializedEntityAssetBase<WeaponAsset>
    {
        public SerializedWeaponAsset(WeaponAsset asset) : base(asset)
        {
            
        }

        public override WeaponAsset Deserialize()
        {
            return new WeaponAsset(id,
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
                                   knockbackForceOther,
                                   knockbackTimeOther,
                                   DateTime.Parse(creationDate));
        }
    }
}