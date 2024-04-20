using RedRats.Systems.LiteFeel.Core;
using RedRats.Systems.LiteFeel.Effects;
using Rogium.Gameplay.Entities.Player;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Rogium.Systems.LiteFeel.Brains
{
    public class LFBrainPlayer : MonoBehaviour
    {
        [SerializeField, GUIColor(0.85f, 0.8f, 0f)] private PlayerController player;
        [Space] 
        [SerializeField, ChildGameObjectsOnly, GUIColor(0.15f, 0.7f, 1f)] private LFEffector onTurnEffector;
        [SerializeField, ChildGameObjectsOnly, GUIColor(0.15f, 0.7f, 1f)] private LFEffector onHitEffector;
        [Space]
        [SerializeField] private LFParticleEffect hitParticle;

        private void OnEnable()
        {
            if (onTurnEffector != null) player.OnTurn += onTurnEffector.Play;
            if (onHitEffector != null) player.DamageReceiver.OnHit += WhenHit;
        }

        private void OnDisable()
        {
            if (onTurnEffector != null) player.OnTurn -= onTurnEffector.Play;
            if (onHitEffector != null) player.DamageReceiver.OnHit -= WhenHit;
        }
        
        private void WhenHit(int damage, Vector3 hitDirection)
        {
            hitParticle.UpdateRotationOffset(-hitDirection);
            onHitEffector.Play();
        }

    }
}