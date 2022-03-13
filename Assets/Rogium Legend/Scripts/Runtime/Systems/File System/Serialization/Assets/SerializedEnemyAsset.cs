using System;
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
        
        public SerializedEnemyAsset(EnemyAsset asset) : base(asset)
        {
            maxHealth = asset.MaxHealth;
            invincibilityTime = asset.InvincibilityTime;
        }

        public override EnemyAsset Deserialize()
        {
            return new EnemyAsset(id,
                                  title,
                                  icon.Deserialize(),
                                  author,
                                  baseDamage,
                                  useDelay,
                                  knockbackForceSelf,
                                  knockbackTimeSelf,
                                  knockbackForceOther,
                                  knockbackTimeOther,
                                  maxHealth,
                                  invincibilityTime,
                                  DateTime.Parse(creationDate));
        }
    }
}