using System;
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
                                   baseDamage,
                                   useDelay,
                                   knockbackSelf,
                                   knockbackOther,
                                   DateTime.Parse(creationDate));
        }
    }
}