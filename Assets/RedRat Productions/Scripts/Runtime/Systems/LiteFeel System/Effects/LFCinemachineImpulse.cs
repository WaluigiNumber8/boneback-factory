using Cinemachine;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;

namespace RedRats.Systems.LiteFeel.Effects
{
    public class LFCinemachineImpulse : LFEffectTweensBase
    {
        [Header("Amplitude")] 
        [SerializeField] private float amplitudeGain = 1f;
        [SerializeField] protected SmoothingType amplitudeSmoothing = SmoothingType.Tween;
        [SerializeField, HideIf("amplitudeSmoothing", SmoothingType.AnimationCurve)] protected Ease amplitudeEasing = Ease.InOutSine;
        [SerializeField, HideIf("amplitudeSmoothing", SmoothingType.Tween)] protected AnimationCurve amplitudeCurve = new(new Keyframe(0, 0), new Keyframe(1, 1));
        
        [Header("Frequency")]
        [SerializeField] private float frequencyGain = 1f;
        [SerializeField] protected SmoothingType frequencySmoothing = SmoothingType.Tween;
        [SerializeField, HideIf("frequencySmoothing", SmoothingType.AnimationCurve)] protected Ease frequencyEasing = Ease.InOutSine;
        [SerializeField, HideIf("frequencySmoothing", SmoothingType.Tween)] protected AnimationCurve frequencyCurve = new(new Keyframe(0, 0), new Keyframe(1, 1));
        
        private CinemachineVirtualCamera cam;
        private CinemachineBasicMultiChannelPerlin perlin;
        private float startAmplitudeGain;
        private float startFrequencyGain;

        protected override void Start()
        {
            base.Start();
            cam = (CinemachineVirtualCamera) CinemachineCore.Instance.GetActiveBrain(0).ActiveVirtualCamera;
            perlin = cam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        }

        protected override void SetupTweens(Sequence usedTweens, float duration)
        {
            Tween amplitudeTween = DOTween.To(() => perlin.m_AmplitudeGain, x => perlin.m_AmplitudeGain = x, amplitudeGain, duration);
            amplitudeTween = (amplitudeSmoothing == SmoothingType.Tween) ? amplitudeTween.SetEase(amplitudeEasing) : amplitudeTween.SetEase(amplitudeCurve);
            
            Tween frequencyTween = DOTween.To(() => perlin.m_FrequencyGain, x => perlin.m_FrequencyGain = x, frequencyGain, duration);
            frequencyTween = (frequencySmoothing == SmoothingType.Tween) ? frequencyTween.SetEase(frequencyEasing) : frequencyTween.SetEase(frequencyCurve);
            
            usedTweens.Append(amplitudeTween);
            usedTweens.Join(frequencyTween);
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
    }
}