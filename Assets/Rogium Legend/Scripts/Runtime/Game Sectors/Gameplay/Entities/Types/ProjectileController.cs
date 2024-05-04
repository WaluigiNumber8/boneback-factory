using System;
using System.Collections;
using RedRats.Core;
using RedRats.Systems.Clocks;
using Rogium.Editors.Core;
using Rogium.Editors.Core.Defaults;
using Rogium.Editors.Projectiles;
using Rogium.Gameplay.Entities.Characteristics;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Rogium.Gameplay.Entities
{
    /// <summary>
    /// The main controller of the projectile entity.
    /// </summary>
    public class ProjectileController : EntityController
    {
        public event Action OnDie;
        public event Action OnFinishLife;
        
        [Title("Characteristics")]
        [SerializeField] private CharacteristicMove move;
        [SerializeField] private CharacteristicDamageGiver damageGiver;
        [SerializeField] private CharacteristicVisual visual;
        [Title("Settings")]
        [SerializeField] private MissingInfo missingInfo;
        [SerializeField, Range(0f, 2f)] private float deathTime = 0.04f;
        [SerializeField] private LayerMask wallMask;

        private Color color;
        private PierceType pierceType;
        private CountdownTimer lifeTimer;
        private bool isDead;

        protected override void Awake()
        {
            base.Awake();
            lifeTimer = new CountdownTimer(Kill);
        }

        protected override void Update()
        {
            lifeTimer.Tick();
            base.Update();
        }

        protected void FixedUpdate() => HandleMovement();

        private IEnumerator OnTriggerEnter2D(Collider2D col)
        {
            yield return null; //Wait 2 frames, otherwise collisions are not detected.
            yield return null;
            if (col.gameObject.CompareTag("Blocks Everything")) Kill();
            
            if (pierceType == PierceType.All) yield break;
            if (col.gameObject.layer == gameObject.layer) yield break;
            if (pierceType == PierceType.Entities && col.TryGetComponent(out EntityController _)) yield break;
            if (pierceType == PierceType.Walls && GameObjectUtils.IsInLayerMask(col.gameObject, wallMask)) yield break;
            if (col.TryGetComponent(out WeaponController _)) yield break;
            Kill();
        }

        /// <summary>
        /// Construct a new projectile and activate it.
        /// </summary>
        /// <param name="asset"></param>
        public void Construct(ProjectileAsset asset)
        {
            color = asset.Color;
            lifeTimer.Set(asset.UseDelay);
            pierceType = asset.PierceType;
            isDead = false;
            
            move.Construct(new CharMoveInfo(asset.FlightSpeed, asset.Acceleration, asset.BrakeForce));
            ForcedMoveInfo selfKnockback = new(asset.KnockbackForceSelf, (asset.KnockbackForceSelf > 0), asset.KnockbackLockDirectionSelf);
            ForcedMoveInfo otherKnockback = new(asset.KnockbackForceOther,(asset.KnockbackForceOther > 0), asset.KnockbackLockDirectionOther);
            damageGiver.Construct(new CharDamageGiverInfo(asset.BaseDamage, selfKnockback, otherKnockback));
            visual.Construct(new CharVisualInfo(asset.Icon, asset.AnimationType, asset.FrameDuration, asset.IconAlt));
            
            UpdateCollideMode(true);
        }

        /// <summary>
        /// Constructs a new projectile with missing parameters.
        /// </summary>
        public void ConstructMissing()
        {
            lifeTimer.Set(missingInfo.lifetime);
            pierceType = missingInfo.pierce;
            isDead = false;
            
            move.Construct(new CharMoveInfo(EditorConstants.ProjectileFlightSpeed, EditorConstants.ProjectileAcceleration, EditorConstants.ProjectileBrakeForce));
            ForcedMoveInfo selfKnockback = new(EditorConstants.ProjectileKnockbackForceSelf, false, EditorConstants.ProjectileKnockbackLockDirectionSelf);
            ForcedMoveInfo otherKnockback = new(EditorConstants.ProjectileKnockbackForceOther, false, EditorConstants.ProjectileKnockbackLockDirectionOther);
            damageGiver.Construct(new CharDamageGiverInfo(EditorConstants.ProjectileBaseDamage, selfKnockback, otherKnockback));
            visual.Construct(new CharVisualInfo(missingInfo.missingSprite, AnimationType.NoAnimation, 0, null));
            
            UpdateCollideMode(true);
        }

        /// <summary>
        /// Process Movement.
        /// </summary>
        private void HandleMovement()
        {
            Vector2 direction = (lifeTimer.TimeLeft > 0) ? TTransform.up : Vector2.zero;
            move.Move(direction);
        }

        /// <summary>
        /// Finish the life of the projectile.
        /// </summary>
        private void Kill()
        {
            if (isDead) return;
            isDead = true;
            OnFinishLife?.Invoke();
            UpdateCollideMode(false);
            
            StartCoroutine(DeathCoroutine());
            IEnumerator DeathCoroutine()
            {
                
                yield return new WaitForSeconds(deathTime);
                OnDie?.Invoke();
            }
        }

        public Color RepresentativeColor { get => color; }
        public CharacteristicDamageGiver DamageGiver { get => damageGiver; }
        
        [Serializable]
        public struct MissingInfo
        {
            public Sprite missingSprite;
            public float lifetime;
            public PierceType pierce;
        }
    }
}