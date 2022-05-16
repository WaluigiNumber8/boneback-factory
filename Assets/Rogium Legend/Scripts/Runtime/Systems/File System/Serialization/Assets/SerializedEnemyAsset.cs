using System;
using System.Collections.Generic;
using RedRats.Core;
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
        
        private int ai;
        private float nextStepTime;
        private int startingDirection;
        private bool seamlessMovement;

        public SerializedEnemyAsset(EnemyAsset asset) : base(asset)
        {
            maxHealth = asset.MaxHealth;
            attackProbability = asset.AttackProbability;
            invincibilityTime = asset.InvincibilityTime;
            weaponsIDs = asset.WeaponIDs;
            
            ai = (int) asset.AI;
            nextStepTime = asset.NextStepTime;
            startingDirection = (int)asset.StartingDirection;
            seamlessMovement = asset.SeamlessMovement;
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
                                  knockbackLockDirectionSelf,
                                  knockbackForceOther,
                                  knockbackTimeOther,
                                  knockbackLockDirectionOther,
                                  maxHealth,
                                  attackProbability,
                                  invincibilityTime,
                                  weaponsIDs,
                                  (AIType)ai,
                                  nextStepTime,
                                  (DirectionType)startingDirection,
                                  seamlessMovement,
                                  DateTime.Parse(creationDate));
        }
    }
}