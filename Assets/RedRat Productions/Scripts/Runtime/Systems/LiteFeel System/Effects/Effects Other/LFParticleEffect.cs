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
        [SerializeField] private FollowRotationType followRotation;
        [SerializeField, LabelText("@RotationOffsetName"), HideIf("@followRotation == FollowRotationType.NoFollow")] private Vector3 rotationOffset;
        
        private ParticlesSystem particlesSystem;
        private ParticleSystem effect;
        private float effectDuration;

        #region Update Values
        public void UpdateBurstAmount(int burstID, int newAmountMin, int newAmountMax)
        {
            ParticleSystem.EmissionModule emit = effectData.emission;
            ParticleSystem.Burst burst = emit.GetBurst(burstID);
            burst.count = new ParticleSystem.MinMaxCurve(newAmountMin, newAmountMax);
            emit.SetBurst(burstID, burst);
        }
        public void UpdateColor(Color newColor)
        {
            ParticleSystem.MainModule main = effectData.main;
            main.startColor = newColor;
        }

        public void UpdateRotationOffset(Vector3 newRotationOffset) => rotationOffset = newRotationOffset;
        #endregion
        
        protected override void Initialize() => particlesSystem = ParticlesSystem.GetInstance();

        protected override void PlaySelf()
        {
            effectDuration = effectData.main.duration;
            effect = particlesSystem.Play(effectData, new ParticleSettingsInfo(target, offset, followTarget, followRotation, rotationOffset, id));
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

        private string RotationOffsetName => followRotation == FollowRotationType.Custom ? "Rotation" : "Rotation Offset";
        
        protected override float TotalDuration { get => effectDuration; }
        protected override string FeedbackColor { get => "#FF8CCD"; }
    }
}