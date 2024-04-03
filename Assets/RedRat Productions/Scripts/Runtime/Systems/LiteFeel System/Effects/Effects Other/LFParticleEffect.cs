using RedRats.Systems.Particles;
using Sirenix.OdinInspector;
using UnityEngine;

namespace RedRats.Systems.LiteFeel.Effects
{
    /// <summary>
    /// An effect that plays a particle system.
    /// </summary>
    public class LFParticleEffect : LFEffectBase
    {
        [SerializeField, InfoBox("Missing effect", InfoMessageType.Error, "@effectData == null")] private ParticleSystem effectData;
        [SerializeField, InfoBox("Missing target", InfoMessageType.Error, "@target == null")] private Transform target;
        [SerializeField] private Vector3 offset;
        [SerializeField] private int id;
        [SerializeField] private bool followTarget;
        
        
        private ParticlesSystem particlesSystem;
        private ParticleSystem effect;
        private float effectDuration;
        
        protected override void Initialize() => particlesSystem = ParticlesSystem.GetInstance();

        protected override void PlaySelf()
        {
            effectDuration = effectData.main.duration;
            effect = particlesSystem.Play(effectData, target, offset, followTarget, id);
        }

        protected override void StopSelf()
        {
            if (id != 0)
            {
                particlesSystem.Stop(id);
                return;
            }
            if (effect != null) particlesSystem.Stop(effect);
        }

        protected override void ResetState()
        {
            //Nothing to reset
        }
        
        protected override float TotalDuration { get => effectDuration; }
        protected override string FeedbackColor { get => "#FF8CCD"; }
    }
}