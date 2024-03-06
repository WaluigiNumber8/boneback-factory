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
        [Title("References")]
        [SerializeField, GUIColor(0.85f, 0.8f, 0f)] private EnemyController enemy;
        [Space] 
        [SerializeField, GUIColor(1f, 0.25f, 0f)] private LFEffector onHitEffector;

        private void OnEnable()
        {
            if (onHitEffector != null) enemy.DamageReceiver.OnDamageReceived += WhenHit;
        }

        private void OnDisable()
        {
            if (onHitEffector != null) enemy.DamageReceiver.OnDamageReceived -= WhenHit;
        }
        
        private void WhenHit(int damage) => onHitEffector.Play();
    }
}