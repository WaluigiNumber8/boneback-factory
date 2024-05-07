using RedRats.Systems.LiteFeel.Core;
using Rogium.Gameplay.Entities;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Rogium.Systems.LiteFeel.Brains
{
    /// <summary>
    /// Allocates LF effects to a weapon.
    /// </summary>
    public class LFBrainWeapon : MonoBehaviour
    {
        [SerializeField, Required, GUIColor(0.85f, 0.8f, 0f)] private WeaponController weapon;
        [Space] 
        [SerializeField, ChildGameObjectsOnly, GUIColor(0.05f, 1f, 0.25f)] private LFEffector onUseEffector;
        [Space] 
        [SerializeField] private DamageParticleSettingsInfo useSettings;
        private void OnEnable()
        {
            if (onUseEffector != null) weapon.OnUse += WhenUse;
        }

        private void OnDisable()
        {
            if (onUseEffector != null) weapon.OnUse -= WhenUse;
        }
        
        private void WhenUse()
        {
            int damageAmount = weapon.DamageGiver.GetDamageTaken();
            int particleAmount = useSettings.GetAmountOfParticles(damageAmount);
            useSettings.particleEffect.UpdateBurstAmount(0, particleAmount);
            useSettings.particleEffect.UpdateColor(weapon.RepresentativeColor);
            onUseEffector.Play();
        }

    }
}