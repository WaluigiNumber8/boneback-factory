using System;
using System.Collections;
using RedRats.Core;
using RedRats.Systems.Clocks;
using Rogium.Editors.Core;
using Rogium.Editors.Core.Defaults;
using Rogium.Editors.Projectiles;
using Rogium.Gameplay.Entities.Characteristics;
using UnityEngine;

namespace Rogium.Gameplay.Entities
{
    /// <summary>
    /// The main controller of the projectile entity.
    /// </summary>
    public class ProjectileController : EntityController
    {
        public event Action OnDie;

        private const float deathTime = 0.06f;
        
        [SerializeField] private CharacteristicMove move;
        [SerializeField] private CharacteristicDamageGiver giver;
        [SerializeField] private CharacteristicVisual visual;
        [SerializeField] private MissingInfo missingInfo;
        [SerializeField] private LayerMask wallMask;
        
        private PierceType pierceType;
        private CountdownTimer lifeTimer;
        private CountdownTimer deathTimer;
        private bool isDead;

        protected override void Awake()
        {
            base.Awake();
            lifeTimer = new CountdownTimer(() => deathTimer.Start(deathTime));
            deathTimer = new CountdownTimer(Kill);
        }

        protected override void Update()
        {
            lifeTimer.Tick();
            deathTimer.Tick();
            base.Update();
        }

        protected void FixedUpdate() => HandleMovement();

        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.gameObject.CompareTag("Blocks Everything")) Kill();
            
            if (pierceType == PierceType.All) return;
            if (col.gameObject.layer == gameObject.layer) return;
            if (col.gameObject.CompareTag("Blocks Everything")) return;
            if (pierceType == PierceType.Entities && col.TryGetComponent(out EntityController _)) return;
            if (pierceType == PierceType.Walls && GameObjectUtils.IsInLayerMask(col.gameObject, wallMask)) return;
            if (col.TryGetComponent(out WeaponController _)) return;
            Kill();
        }

        /// <summary>
        /// Construct a new projectile and activate it.
        /// </summary>
        /// <param name="asset"></param>
        public void Construct(ProjectileAsset asset)
        {
            lifeTimer.Start(asset.UseDelay);
            pierceType = asset.PierceType;
            isDead = false;
            
            move.Construct(new CharMoveInfo(asset.FlightSpeed, asset.Acceleration, asset.BrakeForce));
            ForcedMoveInfo selfKnockback = new(asset.KnockbackForceSelf, true, asset.KnockbackLockDirectionSelf);
            ForcedMoveInfo otherKnockback = new(asset.KnockbackForceOther, true, asset.KnockbackLockDirectionOther);
            giver.Construct(new CharDamageGiverInfo(asset.BaseDamage, selfKnockback, otherKnockback));
            visual.Construct(new CharVisualInfo(asset.Icon, asset.AnimationType, asset.FrameDuration, asset.IconAlt));
        }

        /// <summary>
        /// Constructs a new projectile with missing parameters.
        /// </summary>
        public void ConstructMissing()
        {
            lifeTimer.Start(missingInfo.lifetime);
            pierceType = missingInfo.pierce;
            isDead = false;
            
            move.Construct(new CharMoveInfo(EditorConstants.ProjectileFlightSpeed, EditorConstants.ProjectileAcceleration, EditorConstants.ProjectileBrakeForce));
            ForcedMoveInfo selfKnockback = new(EditorConstants.ProjectileKnockbackForceSelf, true, EditorConstants.ProjectileKnockbackLockDirectionSelf);
            ForcedMoveInfo otherKnockback = new(EditorConstants.ProjectileKnockbackForceOther, true, EditorConstants.ProjectileKnockbackLockDirectionOther);
            giver.Construct(new CharDamageGiverInfo(EditorConstants.ProjectileBaseDamage, selfKnockback, otherKnockback));
            visual.Construct(new CharVisualInfo(missingInfo.missingSprite, AnimationType.NoAnimation, 0, null));
        }

        /// <summary>
        /// Process Movement.
        /// </summary>
        private void HandleMovement()
        {
            Vector2 direction = (lifeTimer.TimeLeft > 0) ? ttransform.up : Vector2.zero;
            move.Move(direction);
        }

        /// <summary>
        /// Finish the life of the projectile.
        /// </summary>
        private void Kill()
        {
            if (isDead) return;
            isDead = true;
            StartCoroutine(DeathCoroutine());
            IEnumerator DeathCoroutine()
            {
                yield return new WaitForSeconds(0.01f);
                OnDie?.Invoke();
            }
        }

        [Serializable]
        public struct MissingInfo
        {
            public Sprite missingSprite;
            public float lifetime;
            public PierceType pierce;
        }
    }
}