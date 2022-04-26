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
        private int useType;
        private float useDuration;
        private bool isEvasive;
        
        public SerializedWeaponAsset(WeaponAsset asset) : base(asset)
        {
            useType = (int)asset.UseType;
            useDuration = asset.UseDuration;
            isEvasive = asset.IsEvasive;
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
                                   (WeaponUseType)useType,
                                   useDuration,
                                   isEvasive,
                                   DateTime.Parse(creationDate));
        }
    }
}