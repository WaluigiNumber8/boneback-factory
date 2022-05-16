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
        private const int deathTime = 6;
        
        [SerializeField] private CharacteristicMove move;
        [SerializeField] private CharacteristicDamageGiver giver;
        [SerializeField] private CharacteristicVisual visual;
        [SerializeField] private MissingInfo missingInfo;
        [SerializeField] private LayerMask wallMask;
        
        private PierceType pierceType;
        private float lifeTimer;
        private int deathTimer;

        protected override void Awake()
        {
            base.Awake();
            lifeTimer = Time.time + 100000;
        }

        private void Update()
        {
            HandleMovement();
            HandleDeath();
        }

        private void OnTriggerEnter2D(Collider2D col)
        {
            if (pierceType == PierceType.All) return;
            if (col.gameObject.layer == gameObject.layer) return;
            if (pierceType == PierceType.Entities && col.TryGetComponent(out EntityController _)) return;
            if (pierceType == PierceType.Walls && GameObjectUtils.IsInLayerMask(col.gameObject, wallMask)) return;
            if (col.TryGetComponent(out WeaponController _)) return;
            Destruct();
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
            pierceType = missingInfo.pierce;
            
            move.Construct(new CharMoveInfo(EditorDefaults.ProjectileFlightSpeed, EditorDefaults.ProjectileAcceleration, EditorDefaults.ProjectileBrakeForce));
            ForcedMoveInfo selfKnockback = new(EditorDefaults.ProjectileKnockbackForceSelf, EditorDefaults.ProjectileKnockbackTimeSelf, EditorDefaults.ProjectileKnockbackLockDirectionSelf);
            ForcedMoveInfo otherKnockback = new(EditorDefaults.ProjectileKnockbackForceOther, EditorDefaults.ProjectileKnockbackTimeOther, EditorDefaults.ProjectileKnockbackLockDirectionOther);
            giver.Construct(new CharDamageGiverInfo(EditorDefaults.ProjectileBaseDamage, selfKnockback, otherKnockback));
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
            Destruct();
        }
        
        /// <summary>
        /// Destroy the projectile.
        /// </summary>
        private void Destruct()
        {
            Destroy(gameObject, 0.01f);
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