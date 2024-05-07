using RedRats.Systems.LiteFeel.Core;
using Rogium.Gameplay.Entities;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Rogium.Systems.LiteFeel.Brains
{
    /// <summary>
    /// Allocates LF effects to a projectile.
    /// </summary>
    public class LFBrainProjectile : MonoBehaviour
    {
        [SerializeField, Required, GUIColor(0.85f, 0.8f, 0f)] private ProjectileController projectile;
        [Space] 
        [SerializeField, ChildGameObjectsOnly, GUIColor(0.25f, 1f, 0.15f)] private LFEffector onDeathEffector;
        [Space]
        [SerializeField] private DamageParticleSettingsInfo deathSettings;
        
        private void OnEnable()
        {
            if (onDeathEffector != null) projectile.OnFinishLife += WhenDie;
        }

        private void OnDisable()
        {
            if (onDeathEffector != null) projectile.OnFinishLife -= WhenDie;
        }

        private void WhenDie()
        {
            int damageAmount = projectile.DamageGiver.GetDamageTaken();
            int particleAmount = deathSettings.GetAmountOfParticles(damageAmount);
            deathSettings.particleEffect.UpdateBurstAmount(0, particleAmount);
            deathSettings.particleEffect.UpdateColor(projectile.RepresentativeColor);
            onDeathEffector.Play();
        }
    }
}