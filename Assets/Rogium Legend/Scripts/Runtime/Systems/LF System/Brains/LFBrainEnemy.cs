using RedRats.Systems.LiteFeel.Core;
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

        private void OnEnable()
        {
            if (onHitEffector != null) enemy.DamageReceiver.OnHit += onHitEffector.Play;
            if (onDeathEffector != null) enemy.DamageReceiver.OnDeath += onDeathEffector.Play;
        }

        private void OnDisable()
        {
            if (onHitEffector != null) enemy.DamageReceiver.OnHit -= onHitEffector.Play;
            if (onDeathEffector != null) enemy.DamageReceiver.OnDeath -= onDeathEffector.Play;
        }
    }
}