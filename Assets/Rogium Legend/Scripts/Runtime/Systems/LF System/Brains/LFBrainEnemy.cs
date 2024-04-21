using RedRats.Core;
using RedRats.Systems.LiteFeel.Core;
using Rogium.Gameplay.Entities.Enemy;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Serialization;

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
        [SerializeField, FormerlySerializedAs("damageSettings")] private DamageParticleSettingsInfo hitSettings;
        
        
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
            int amount = RedRatUtils.RemapAndEvaluate(damage, hitSettings.amountCurve, hitSettings.minDamage, hitSettings.maxDamage, hitSettings.minAmount, hitSettings.maxAmount);
            hitSettings.particleEffect.UpdateBurstAmount(0, amount);
            hitSettings.particleEffect.UpdateColor(enemy.RepresentativeColor);
            hitSettings.particleEffect.UpdateRotationOffset(hitDirection);
            onHitEffector.Play();
        }
    }
}