using System;
using System.Collections.Generic;
using RedRats.Core;
using Rogium.Editors.Enemies;
using Rogium.Editors.Weapons;
using Rogium.Gameplay.Core;
using Rogium.Gameplay.Entities.Characteristics;
using Sirenix.OdinInspector;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Rogium.Gameplay.Entities.Enemy
{
    /// <summary>
    /// Overseers and controls the enemy object.
    /// </summary>
    public class EnemyController : EntityController
    {
        [Title("Characteristics")]
        [SerializeField] private CharacteristicDamageGiver damageGiver;
        [SerializeField] private CharacteristicDamageReceiver damageReceiver;
        [SerializeField] private CharacteristicWeaponHold weaponHold;
        [SerializeField] private CharacteristicVisual visual;
        [SerializeField] private CharacteristicSoundEmitter soundEmitter;
        [SerializeField] private CharacteristicFloorInteractor floorInteractor;
        [Title("Parameters")]
        [SerializeField, Range(0f, 2f)] private float deathTime;
        
        private GameplayOverseerMono gameplayOverseer;
        private Transform playerTransform;
        
        private AIType ai;
        private Vector2 startingDirection;
        private float refreshFaceDirectionTime;
        private float refreshFaceDirectionTimer;
        private bool isLooking;
        private Action updateFaceDirectionMethod;
        private bool seamlessMovement;

        private float weaponUseTime;
        private float weaponUseTimer;
        private float attackProbability;
        
        protected override void Awake()
        {
            base.Awake();
            gameplayOverseer = GameplayOverseerMono.GetInstance();
            updateFaceDirectionMethod = FaceDirectionLook;
        }

        private void OnEnable()
        {
            if (damageReceiver != null) damageReceiver.OnDeath += Die;
            if (damageReceiver != null && soundEmitter != null) damageReceiver.OnDamageReceived += soundEmitter.PlayHurtSound;
            if (damageReceiver != null && soundEmitter != null) damageReceiver.OnDeath += soundEmitter.PlayDeathSound;
            if (floorInteractor != null && visual != null) visual.OnFrameChange += floorInteractor.TakeStep;
        }

        private void OnDisable()
        {
            if (damageReceiver != null) damageReceiver.OnDeath -= Die;
            if (damageReceiver != null && soundEmitter != null) damageReceiver.OnDamageReceived -= soundEmitter.PlayHurtSound;
            if (damageReceiver != null && soundEmitter != null) damageReceiver.OnDeath -= soundEmitter.PlayDeathSound;
            if (floorInteractor != null && visual != null) visual.OnFrameChange -= floorInteractor.TakeStep;
        }

        protected override void Update()
        {
            base.Update();
            UseWeapon();
        }

        /// <summary>
        /// Constructs the enemy.
        /// </summary>
        /// <param name="asset">The asset to take data from.</param>
        public void Construct(EnemyAsset asset, IList<WeaponAsset> weapons = null)
        {
            playerTransform = GameObject.FindWithTag("Player").transform;
            
            ai = asset.AI;
            startingDirection = RedRatUtils.DirectionTypeToVector(asset.StartingDirection);
            refreshFaceDirectionTime = asset.NextStepTime;
            seamlessMovement = asset.SeamlessMovement;
            updateFaceDirectionMethod = ai switch
            {
                AIType.LookInDirection => FaceDirectionLook,
                AIType.RotateTowardsPlayer => FaceDirectionRotate,
                _ => throw new ArgumentOutOfRangeException($"The AI of type {ai} is not supported.")
            };
            
            //Damage Giver
            if (damageGiver != null)
            {
                ForcedMoveInfo knockbackSelf = new(asset.KnockbackForceSelf, (asset.KnockbackForceSelf > 0), asset.KnockbackLockDirectionSelf);
                ForcedMoveInfo knockbackOther = new(asset.KnockbackForceOther, (asset.KnockbackForceOther > 0), asset.KnockbackLockDirectionOther);
                CharDamageGiverInfo damageGiver = new(asset.BaseDamage, knockbackSelf, knockbackOther);
                this.damageGiver.Construct(damageGiver);
            }

            //Damage Receiver
            if (damageReceiver != null)
            {
                CharDamageReceiverInfo damageReceiver = new(asset.MaxHealth, asset.InvincibilityTime);
                this.damageReceiver.Construct(damageReceiver);
            }

            //Visual
            if (visual != null)
            {
                CharVisualInfo visual = new(asset.Icon, asset.AnimationType, asset.FrameDuration, asset.IconAlt);
                this.visual.Construct(visual);
            }

            //Sound Emitter
            if (soundEmitter != null)
            {
                CharSoundInfo sound = new(asset.HurtSound, asset.DeathSound);
                this.soundEmitter.Construct(sound);
            }
            
            //Weapon Hold
            if (weaponHold != null)
            {
                CharWeaponHoldInfo weaponHold = new(asset.UseDelay);
                this.weaponHold.Construct(weaponHold, weapons);
                
                weaponUseTime = asset.UseDelay;
                attackProbability = asset.AttackProbability;
            }
        }

        protected override void UpdateFaceDirection()
        {
            if (actionsLocked) { base.UpdateFaceDirection(); }
            else updateFaceDirectionMethod();
        }

        /// <summary>
        /// Uses a random weapon.
        /// </summary>
        private void UseWeapon()
        {
            if (gameplayOverseer.IsInSafePeriod()) return;
            if (weaponUseTimer > Time.time) return;
            if (weaponHold == null) return;
            if (weaponHold.WeaponCount <= 0) return;

            weaponUseTimer = Time.time + weaponUseTime + Random.Range(0f, 0.05f);

            if (Random.Range(0f, 1f) > attackProbability) return;
            
            int slot = (weaponHold.WeaponCount > 1) ? Random.Range(0, weaponHold.WeaponCount) : 0;
            weaponHold.Use(slot);
        }
        
        private void Die()
        {
            ChangeCollideMode(false);
            weaponHold.WipeInventory();
            Destroy(gameObject, deathTime);
        }

        private void FaceDirectionLook() => faceDirection = startingDirection;

        private void FaceDirectionRotate()
        {
            Vector2 direction = (ttransform.position - playerTransform.position).normalized * -1;
            if (!seamlessMovement) direction = direction.Round();

            if (!isLooking && faceDirection.DistanceTo(direction) > 0.1f)
            {
                refreshFaceDirectionTimer = Time.time + refreshFaceDirectionTime;
                isLooking = true;
                return;
            }
            
            if (isLooking && refreshFaceDirectionTimer < Time.time)
            {
                faceDirection = direction;
                isLooking = false;
            }
        }
        
        public CharacteristicDamageReceiver DamageReceiver { get => damageReceiver; } 
        public CharacteristicSoundEmitter SoundEmitter { get => soundEmitter; }
    }
}