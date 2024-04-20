using System;
using RedRats.Core;
using RedRats.Systems.LiteFeel.Core;
using RedRats.Systems.LiteFeel.Effects;
using Rogium.Gameplay.Entities.Enemy;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Rogium.Systems.LiteFeel.Brains
{
    /// <summary>
    /// Allocates LF effects to an enemy.
    /// </summary>
    public class LFBrainEnemy : MonoBehaviour
    {
        [SerializeField, GUIColor(0.85f, 0.8f, 0f)] private EnemyController enemy;
        [Space] 
        [SerializeField, GUIColor(1f, 0.25f, 0f)] private LFEffector onHitEffector;
        [SerializeField, GUIColor(1f, 0.25f, 0f)] private LFEffector onDeathEffector;
        [Space] 
        [SerializeField] private HitEffectSettingsInfo hitSettings;
        
        private void OnEnable()
        {
            if (onHitEffector != null) enemy.DamageReceiver.OnHit += WhenHit;
            if (onDeathEffector != null) enemy.DamageReceiver.OnDeath += onDeathEffector.Play;
        }

        private void OnDisable()
        {
            if (onHitEffector != null) enemy.DamageReceiver.OnHit -= WhenHit;
            if (onDeathEffector != null) enemy.DamageReceiver.OnDeath -= onDeathEffector.Play;
        }
        
        private void WhenHit(int damage, Vector3 hitDirection)
        {
            int amount = GetAmountOfParticles(damage);
            Debug.Log(amount);
            hitSettings.particleEffect.UpdateBurstAmount(0, amount - 1, amount + 1);
            hitSettings.particleEffect.UpdateColor(enemy.RepresentativeColor);
            hitSettings.particleEffect.UpdateRotationOffset(hitDirection);
            onHitEffector.Play();
        }
        
        private int GetAmountOfParticles(int damage)
        {
            float dmg = ((float)damage).Remap(hitSettings.minDamage , hitSettings.maxDamage, 0f, 1f);
            float curveValue = hitSettings.amountCurve.Evaluate(dmg);
            int amount = (int)curveValue.Remap(0f, 1f, hitSettings.minAmount, hitSettings.maxAmount);
            return amount;
        }

        [Serializable]
        public struct HitEffectSettingsInfo
        {
            [Required] public LFParticleEffect particleEffect;
            public AnimationCurve amountCurve;
            [MinValue(0)] public int minDamage;
            [MinValue(0)] public int maxDamage;
            [MinValue(0)] public int minAmount;
            [MinValue(0)] public int maxAmount;
        }
    }
}