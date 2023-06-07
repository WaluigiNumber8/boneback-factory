using System;
using System.Collections;
using RedRats.Core;
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

        private const int deathTime = 4;
        
        [SerializeField] private CharacteristicMove move;
        [SerializeField] private CharacteristicDamageGiver giver;
        [SerializeField] private CharacteristicVisual visual;
        [SerializeField] private MissingInfo missingInfo;
        [SerializeField] private LayerMask wallMask;
        
        private PierceType pierceType;
        private float lifeTimer;
        private int deathTimer;
        private bool isDead;

        protected override void Awake()
        {
            base.Awake();
            lifeTimer = Time.time + 100_000;
        }

        protected override void FixedUpdate()
        {
            HandleMovement();
            HandleDeath();
            base.FixedUpdate();
        }

        private void OnTriggerEnter2D(Collider2D col)
        {
            if (pierceType == PierceType.All) return;
            if (col.gameObject.layer == gameObject.layer) return;
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
            lifeTimer = Time.time + asset.UseDelay;
            deathTimer = -1;
            pierceType = asset.PierceType;
            isDead = false;
            
            move.Construct(new CharMoveInfo(asset.FlightSpeed, asset.Acceleration, asset.BrakeForce));
            ForcedMoveInfo selfKnockback = new(asset.KnockbackForceSelf, asset.KnockbackTimeSelf, asset.KnockbackLockDirectionSelf);
            ForcedMoveInfo otherKnockback = new(asset.KnockbackForceOther, asset.KnockbackTimeOther, asset.KnockbackLockDirectionOther);
            giver.Construct(new CharDamageGiverInfo(asset.BaseDamage, selfKnockback, otherKnockback));
            visual.Construct(new CharVisualInfo(asset.Icon, asset.AnimationType, asset.FrameDuration, asset.IconAlt));
        }

        /// <summary>
        /// Constructs a new projectile with missing parameters.
        /// </summary>
        public void ConstructMissing()
        {
            lifeTimer = Time.time + missingInfo.lifetime;
            deathTimer = -1;
            pierceType = missingInfo.pierce;
            isDead = false;
            
            move.Construct(new CharMoveInfo(EditorConstants.ProjectileFlightSpeed, EditorConstants.ProjectileAcceleration, EditorConstants.ProjectileBrakeForce));
            ForcedMoveInfo selfKnockback = new(EditorConstants.ProjectileKnockbackForceSelf, EditorConstants.ProjectileKnockbackTimeSelf, EditorConstants.ProjectileKnockbackLockDirectionSelf);
            ForcedMoveInfo otherKnockback = new(EditorConstants.ProjectileKnockbackForceOther, EditorConstants.ProjectileKnockbackTimeOther, EditorConstants.ProjectileKnockbackLockDirectionOther);
            giver.Construct(new CharDamageGiverInfo(EditorConstants.ProjectileBaseDamage, selfKnockback, otherKnockback));
            visual.Construct(new CharVisualInfo(missingInfo.missingSprite, AnimationType.NoAnimation, 0, null));
        }

        /// <summary>
        /// Process Movement.
        /// </summary>
        private void HandleMovement()
        {
            Vector2 direction = (lifeTimer > Time.time) ? ttransform.up : Vector2.zero;
            move.Move(direction);
        }

        /// <summary>
        /// Process Death.
        /// </summary>
        private void HandleDeath()
        {
            if (lifeTimer > Time.time) return;
            if (CurrentSpeed > 0.01f)
            {
                deathTimer = deathTime;
                return;
            }
            if (deathTimer > 0)
            {
                deathTimer--;
                return;
            }
            Kill();
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

        [System.Serializable]
        public struct MissingInfo
        {
            public Sprite missingSprite;
            public float lifetime;
            public PierceType pierce;
        }
    }
}