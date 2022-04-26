using System;
using Rogium.Editors.Core;
using Rogium.Editors.Enemies;

namespace Rogium.ExternalStorage.Serialization
{
    /// <summary>
    /// Serialized form of the <see cref="EnemyAsset"/>.
    /// </summary>
    [System.Serializable]
    public class SerializedEnemyAsset : SerializedEntityAssetBase<EnemyAsset>
    {
        private int maxHealth;
        private float invincibilityTime;
        private string[] weaponsIDs;

        public SerializedEnemyAsset(EnemyAsset asset) : base(asset)
        {
            maxHealth = asset.MaxHealth;
            invincibilityTime = asset.InvincibilityTime;
            weaponsIDs = asset.WeaponIDs;
        }

        public override EnemyAsset Deserialize()
        {
            return new EnemyAsset(id,
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
                                  maxHealth,
                                  invincibilityTime,
                                  weaponsIDs,
                                  DateTime.Parse(creationDate));
        }
    }
}