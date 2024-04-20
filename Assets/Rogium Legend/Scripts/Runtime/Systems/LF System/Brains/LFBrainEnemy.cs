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
        [SerializeField] private LFParticleEffect hitParticle;
        
        
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
        
        private void WhenHit(Vector3 hitDirection)
        {  
            hitParticle.UpdateColor(enemy.RepresentativeColor);
            hitParticle.UpdateRotationOffset(hitDirection);
            onHitEffector.Play();
        }
    }
}