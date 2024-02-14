using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;

namespace RedRats.Systems.LiteFeel.Effects
{
    public class LFShaderGlowEffect : LFShaderBase
    {
        [Header("Glow Intensity")]
        [SerializeField, Range(0f, 12f)] private float beginIntensity = 0;
        [SerializeField, Range(0f, 12f)] private float targetIntensity = 1;
        [SerializeField] protected SmoothingType intensitySmoothing = SmoothingType.AnimationCurve;
        [SerializeField, HideIf("intensitySmoothing", SmoothingType.AnimationCurve)] protected Ease intensityEasing = Ease.InOutSine;
        [SerializeField, HideIf("intensitySmoothing", SmoothingType.Tween)] protected AnimationCurve intensityCurve = new(new Keyframe(0, 0), new Keyframe(1, 1));
        
        [Header("Glow Saturation")]
        [SerializeField, Range(0f, 8f)] private float beginSaturation = 1;
        [SerializeField, Range(0f, 8f)] private float targetSaturation = 2;
        [SerializeField] protected SmoothingType saturationSmoothing = SmoothingType.AnimationCurve;
        [SerializeField, HideIf("saturationSmoothing", SmoothingType.AnimationCurve)] protected Ease saturationEasing = Ease.InOutSine;
        [SerializeField, HideIf("saturationSmoothing", SmoothingType.Tween)] protected AnimationCurve saturationCurve = new(new Keyframe(0, 0), new Keyframe(1, 1));
        
        private static readonly int IntensityProperty = Shader.PropertyToID("_GlowIntensity");
        private static readonly int SaturationProperty = Shader.PropertyToID("_GlowSaturation");
        
        private float startIntensity;
        private float startSaturation;
        
        protected override void ResetTargetState()
        {
            material.SetFloat(IntensityProperty, startIntensity);
            material.SetFloat(SaturationProperty, startSaturation);
        }

        protected override void UpdateStartingValues()
        {
            startIntensity = material.GetFloat(IntensityProperty);
            startSaturation = material.GetFloat(SaturationProperty);
        }

        protected override void SetBeginState()
        {
            material.SetFloat(IntensityProperty, beginIntensity);
            material.SetFloat(SaturationProperty, beginSaturation);
        }

        protected override void SetupTweens()
        {
            AddFloatTween(IntensityProperty, targetIntensity, intensitySmoothing, intensityEasing, intensityCurve);
            AddFloatTween(SaturationProperty, targetSaturation, saturationSmoothing, saturationEasing, saturationCurve);
        }
    }
}