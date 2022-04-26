using System;
using System.Collections.Generic;
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
        private float attackProbability;
        private float invincibilityTime;
        private List<string> weaponsIDs;

        public SerializedEnemyAsset(EnemyAsset asset) : base(asset)
        {
            maxHealth = asset.MaxHealth;
            attackProbability = asset.AttackProbability;
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
                                  attackProbability,
                                  invincibilityTime,
                                  weaponsIDs,
                                  DateTime.Parse(creationDate));
        }
    }
}