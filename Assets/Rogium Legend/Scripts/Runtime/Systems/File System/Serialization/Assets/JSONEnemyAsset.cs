using System;
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

        public AssetData hurtSound;
        public AssetData deathSound;
        public AssetData idleSound;
        
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

            hurtSound = new AssetData(asset.HurtSound);
            deathSound = new AssetData(asset.DeathSound);
            idleSound = new AssetData(asset.IdleSound);
        }

        public override EnemyAsset Decode()
        {
            return new EnemyAsset.Builder()
                .WithID(id)
                .WithTitle(title)
                .WithIcon(icon.Decode())
                .WithAuthor(author)
                .WithCreationDate(DateTime.Parse(creationDate))
                .WithColor(color.Decode())
                .WithAssociatedSpriteID(associatedSpriteID)
                .WithAnimationType((AnimationType)animationType)
                .WithFrameDuration(frameDuration)
                .WithIconAlt(iconAlt.Decode())
                .WithBaseDamage(baseDamage)
                .WithUseDelay(useDelay)
                .WithKnockbackForceSelf(knockbackForceSelf)
                .WithKnockbackLockDirectionSelf(knockbackLockDirectionSelf)
                .WithKnockbackForceOther(knockbackForceOther)
                .WithKnockbackLockDirectionOther(knockbackLockDirectionOther)
                .WithMaxHealth(maxHealth)
                .WithAttackProbability(attackProbability)
                .WithInvincibilityTime(invincibilityTime)
                .WithWeaponIDs(weaponsIDs)
                .WithAI((AIType)ai)
                .WithNextStepTime(nextStepTime)
                .WithStartingDirection((DirectionType)startingDirection)
                .WithSeamlessMovement(seamlessMovement)
                .WithHurtSound(hurtSound)
                .WithDeathSound(deathSound)
                .WithIdleSound(idleSound)
                .Build();
        }
    }
}