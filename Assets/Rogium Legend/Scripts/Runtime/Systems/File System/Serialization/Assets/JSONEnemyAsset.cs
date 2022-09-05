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
    public class JSONEnemyAsset : JSONEntityAssetBase<EnemyAsset>
    {
        public int maxHealth;
        public float attackProbability;
        public float invincibilityTime;
        public string[] weaponsIDs;
        
        public int ai;
        public float nextStepTime;
        public int startingDirection;
        public bool seamlessMovement;

        public JSONEnemyAsset(EnemyAsset asset) : base(asset)
        {
            maxHealth = asset.MaxHealth;
            attackProbability = asset.AttackProbability;
            invincibilityTime = asset.InvincibilityTime;
            weaponsIDs = asset.WeaponIDs.ToArray();
            
            ai = (int) asset.AI;
            nextStepTime = asset.NextStepTime;
            startingDirection = (int)asset.StartingDirection;
            seamlessMovement = asset.SeamlessMovement;
        }

        public override EnemyAsset Decode()
        {
            return new EnemyAsset(id,
                                  title,
                                  icon.Decode(),
                                  author,
                                  (AnimationType)animationType,
                                  frameDuration,
                                  iconAlt.Decode(),
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