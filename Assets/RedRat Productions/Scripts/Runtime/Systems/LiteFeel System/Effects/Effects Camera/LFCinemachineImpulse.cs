using Cinemachine;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;

namespace RedRats.Systems.LiteFeel.Effects
{
    public class LFCinemachineImpulse : LFEffectTweenBase
    {
        [Header("Amplitude")] 
        [SerializeField] private float amplitudeGain = 1f;
        [SerializeField] protected AnimationCurve amplitudeCurve = new(new Keyframe(0, 0), new Keyframe(1, 1));
        
        [Header("Frequency")]
        [SerializeField] private float frequencyGain = 1f;
        [SerializeField] protected AnimationCurve frequencyCurve = new(new Keyframe(0, 0), new Keyframe(1, 1));
        
        private CinemachineVirtualCamera cam;
        private CinemachineBasicMultiChannelPerlin perlin;
        private float startAmplitudeGain;
        private float startFrequencyGain;

        protected override void Initialize()
        {
            cam = (CinemachineVirtualCamera) CinemachineCore.Instance.GetActiveBrain(0).ActiveVirtualCamera;
            perlin = cam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        }

        protected override void SetBeginState()
        {
            // Do nothing
        }

        protected override void SetupTweens()
        {
            Tween amplitudeTween = DOTween.To(() => perlin.m_AmplitudeGain, x => perlin.m_AmplitudeGain = x, amplitudeGain, duration);
            AddFloatTweenToSequence(amplitudeTween, amplitudeCurve);
            
            Tween frequencyTween = DOTween.To(() => perlin.m_FrequencyGain, x => perlin.m_FrequencyGain = x, frequencyGain, duration);
            AddFloatTweenToSequence(frequencyTween, frequencyCurve);
        }

        protected override void ResetTargetState()
        {
            perlin.m_AmplitudeGain = startAmplitudeGain;
            perlin.m_FrequencyGain = startFrequencyGain;
        }

        protected override void UpdateStartingValues()
        {
            startFrequencyGain = perlin.m_FrequencyGain;
            startAmplitudeGain = perlin.m_AmplitudeGain;
        }
        
        protected override string FeedbackColor { get => "#56CDFF"; }
    }
}